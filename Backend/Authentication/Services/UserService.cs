using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Users.Models.Authentication;
using Users.Models.Data;
using Users.Models.Dto;
using Users.Repository;

namespace Users.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public User User { get; private set; }

        public UserService(IOptions<AppSettings> appSettings, IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            var userDao = await _usersRepository.Get(u => u.Username.Equals(model.Username));

            if (userDao is null)
            {
                return null;
            }

            var (hashedPassword, _) = HashPassword(model.Password, userDao.Salt);
            if (!hashedPassword.Equals(userDao.Password))
            {
                return null;
            }

            var user = _mapper.Map<User>(userDao);

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public User GetById(int id)
        {
            var userDao = _usersRepository.Get(u => u.Id == id).GetAwaiter().GetResult();
            var user = _mapper.Map<User>(userDao);

            User = user;

            return user;
        }

        private static Tuple<string, string> HashPassword(string password, string salt = null)
        {
            byte[] saltBytes;
            if (string.IsNullOrWhiteSpace(salt))
            {
                saltBytes = new byte[16];
                new RNGCryptoServiceProvider().GetBytes(saltBytes);
            }
            else
            {
                saltBytes = Convert.FromBase64String(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100000);
            var hash = pbkdf2.GetBytes(20);

            var hashBytes = new byte[36];
            Array.Copy(saltBytes, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return new Tuple<string, string>(Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
        }

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.ApiSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}