using Models.Users.Models.Dao;
using Repository;

namespace Users.Repository
{
    public interface IUsersRepository : IRepositoryBase<UserDao>
    {

    }
}