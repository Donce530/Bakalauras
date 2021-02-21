using AutoMapper;
using Repository;
using Users.Models.Dao;

namespace Users.Repository
{
    public class UsersRepository : RepositoryBase<UserDao>, IUsersRepository
    {
        public UsersRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
