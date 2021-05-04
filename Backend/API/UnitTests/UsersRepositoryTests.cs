using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Configuration;
using Microsoft.Extensions.Options;
using Models.Users.Models.Dao;
using Models.Users.Models.Data;
using Moq;
using Repository;
using Users.Repository;
using Xunit;

namespace UnitTests
{
    public class UsersRepositoryTests
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UsersRepositoryTests()
        {
            _dbContext = new AppDbContext(new Mock<IOptions<AppSettings>>().Object);
            _mapper = new Mock<IMapper>().Object;
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new UsersRepository(_dbContext, _mapper);
            Assert.NotNull(instance);
        }
    }
}