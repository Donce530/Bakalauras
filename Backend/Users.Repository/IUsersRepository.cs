using Repository;
using Users.Models.Dao;

namespace Users.Repository
{
    public interface IUsersRepository : IRepositoryBase<UserDao>
    {

    }
}