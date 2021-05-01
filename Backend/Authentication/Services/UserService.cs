using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Reservations.Models.Data;
using Models.Reservations.Models.Dto;
using Models.Users.Models.Dao;
using Models.Users.Models.Data;
using Models.Users.Models.Dto;
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

        public async Task Register(RegisterRequest registerRequest)
        {
            if (await _usersRepository.Exists(u => u.Email == registerRequest.Email))
            {
                throw new InvalidOperationException("Email already in use");
            }

            var user = _mapper.Map<UserDao>(registerRequest);
            (user.Password, user.Salt) = HashPassword(registerRequest.Password);

            await _usersRepository.Create(user);
        }

        public async Task<PagedResponse<UserDataRow>> GetPagedAndFiltered(PagedFilteredParams<UserFilters> parameters)
        {
            var filters = new List<Expression<Func<UserDao, bool>>>
            {
                u => u.Role != Role.Admin
            };
            if (parameters.Filters is not null)
            {
                if (!string.IsNullOrEmpty(parameters.Filters.FirstName))
                {
                    filters.Add(u => u.FirstName.ToLower().Contains(parameters.Filters.FirstName.ToLower()));
                }
                
                if (!string.IsNullOrEmpty(parameters.Filters.LastName))
                {
                    filters.Add(u => u.LastName.ToLower().Contains(parameters.Filters.LastName.ToLower()));
                }
                
                if (!string.IsNullOrEmpty(parameters.Filters.Email))
                {
                    filters.Add(u => u.Email.ToLower().Contains(parameters.Filters.Email.ToLower()));
                }
                
                if (!string.IsNullOrEmpty(parameters.Filters.PhoneNumber))
                {
                    filters.Add(u => u.PhoneNumber.ToLower().Contains(parameters.Filters.PhoneNumber.ToLower()));
                }

                if (parameters.Filters.Role is not null)
                {
                    filters.Add(u => u.Role == parameters.Filters.Role);
                }
            }

            Expression<Func<UserDao, object>> orderBy = null;
            if (parameters.Paginator.SortOrder != 0 && !string.IsNullOrEmpty(parameters.Paginator.SortBy))
            {
                orderBy = parameters.Paginator.SortBy switch
                {
                    nameof(UserDataRow.FirstName) => u => u.FirstName,
                    nameof(UserDataRow.LastName) => u => u.LastName,
                    nameof(UserDataRow.Email) => u => u.Email,
                    nameof(UserDataRow.Role) => u => u.Role,
                    nameof(UserDataRow.PhoneNumber) => u => u.PhoneNumber,
                    _ => throw new ArgumentOutOfRangeException(nameof(parameters.Paginator.SortBy), parameters.Paginator.SortBy)
                };
            }

            var pagedResults = await _usersRepository.GetPaged<UserDataRow>(parameters.Paginator, filters, orderBy);

            return pagedResults;
        }

        public async Task Delete(int userId)
        {
            await _usersRepository.Delete(u => u.Id == userId);
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            var userDao = await _usersRepository.Get(u => u.Email.Equals(model.Email));

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