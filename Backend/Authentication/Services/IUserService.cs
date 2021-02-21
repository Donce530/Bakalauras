using System.Threading.Tasks;
using Users.Models.Authentication;
using Users.Models.Data;
using Users.Models.Dto;

namespace Users.Api.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
        User GetById(int id);
        public User User { get; }
    }
}