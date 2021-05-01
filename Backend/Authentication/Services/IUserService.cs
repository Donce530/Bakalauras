using System.Threading.Tasks;
using Models.Reservations.Models.Dto;
using Models.Users.Models.Data;
using Models.Users.Models.Dto;

namespace Users.Api.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
        User GetById(int id);
        public User User { get; }
        Task Register(RegisterRequest registerRequest);
        Task<PagedResponse<UserDataRow>> GetPagedAndFiltered(PagedFilteredParams<UserFilters> parameters);
        Task Delete(int userId);
    }
}