using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public override Task Update(UserDao entity, Expression<Func<UserDao, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
