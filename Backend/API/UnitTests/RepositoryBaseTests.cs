using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Configuration;
using Microsoft.Extensions.Options;
using Models.Reservations.Models.Dto;
using Moq;
using Repository;
using Xunit;

namespace UnitTests
{
    // using TEntity = String;
    // using TEntity = String;
    // using TResult = String;
    //
    // public class RepositoryBase_1Tests
    // {
    //     private readonly AppDbContext _dbContext;
    //     private readonly IMapper _mapper;
    //
    //     private readonly TestRepositoryBase _testClass;
    //
    //     public RepositoryBase_1Tests()
    //     {
    //         _dbContext = new AppDbContext(new Mock<IOptions<AppSettings>>().Object);
    //         _mapper = new Mock<IMapper>().Object;
    //         _testClass = new TestRepositoryBase(_dbContext, _mapper);
    //     }
    //
    //     [Fact]
    //     public void CannotConstructWithNullDbContext()
    //     {
    //         Assert.Throws<ArgumentNullException>(() => new TestRepositoryBase(default, new Mock<IMapper>().Object));
    //     }
    //
    //     [Fact]
    //     public void CannotConstructWithNullMapper()
    //     {
    //         Assert.Throws<ArgumentNullException>(() =>
    //             new TestRepositoryBase(new AppDbContext(new Mock<IOptions<AppSettings>>().Object), default));
    //     }
    //
    //     [Fact]
    //     public async Task CanCallGet()
    //     {
    //         var filter = new Expression<Func<TEntity, bool>>();
    //         var tracking = true;
    //         var result = await _testClass.Get(filter, tracking);
    //         Assert.True(false, "Create or modify test");
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallGetWithNullFilter()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.Get(default, true));
    //     }
    //
    //     [Fact]
    //     public async Task CanCallGetAllWithSelectAndFilterAndDistinct()
    //     {
    //         var select = new Expression<Func<TEntity, string>>();
    //         var filter = new Expression<Func<TEntity, bool>>();
    //         var distinct = false;
    //         var result = await _testClass.GetAll(select, filter, distinct);
    //         Assert.True(false, "Create or modify test");
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallGetAllWithSelectAndFilterAndDistinctWithNullSelect()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() =>
    //             _testClass.GetAll(default(Expression<Func<TEntity, string>>), new Expression<Func<TEntity, bool>>()));
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallGetAllWithSelectAndFilterAndDistinctWithNullFilter()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() =>
    //             _testClass.GetAll(new Expression<Func<TEntity, string>>(), default));
    //     }
    //
    //     [Fact]
    //     public async Task CanCallGetAllWithTResultAndFilterAndDistinct()
    //     {
    //         var filter = new Expression<Func<TEntity, bool>>();
    //         var distinct = false;
    //         var result = await _testClass.GetAll<string>(filter, distinct);
    //         Assert.True(false, "Create or modify test");
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallGetAllWithTResultAndFilterAndDistinctWithNullFilter()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.GetAll<string>(default));
    //     }
    //
    //     [Fact]
    //     public async Task CanCallGetPaged()
    //     {
    //         var paginator = new Paginator
    //         {
    //             Rows = 602039455, Offset = 1452997117, SortBy = "TestValue1869873901", SortOrder = 1043048483,
    //             TotalRows = 477665033
    //         };
    //         var filters = new[]
    //         {
    //             new Expression<Func<TEntity, bool>>(), new Expression<Func<TEntity, bool>>(),
    //             new Expression<Func<TEntity, bool>>()
    //         };
    //         var orderBy = new Expression<Func<TEntity, object>>();
    //         var result = await _testClass.GetPaged<string>(paginator, filters, orderBy);
    //         Assert.True(false, "Create or modify test");
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallGetPagedWithNullPaginator()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.GetPaged<string>(default(Paginator),
    //             new[]
    //             {
    //                 new Expression<Func<TEntity, bool>>(), new Expression<Func<TEntity, bool>>(),
    //                 new Expression<Func<TEntity, bool>>()
    //             }, new Expression<Func<TEntity, object>>()));
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallGetPagedWithNullFilters()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() =>
    //             _testClass.GetPaged<string>(
    //                 new Paginator
    //                 {
    //                     Rows = 1742614014, Offset = 1781303335, SortBy = "TestValue1531448582", SortOrder = 123176217,
    //                     TotalRows = 739465362
    //                 }, default, new Expression<Func<TEntity, object>>()));
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallGetPagedWithNullOrderBy()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.GetPaged<string>(
    //             new Paginator
    //             {
    //                 Rows = 910351378, Offset = 1590088428, SortBy = "TestValue1050780174", SortOrder = 2142420000,
    //                 TotalRows = 1531643614
    //             },
    //             new[]
    //             {
    //                 new Expression<Func<TEntity, bool>>(), new Expression<Func<TEntity, bool>>(),
    //                 new Expression<Func<TEntity, bool>>()
    //             }, default(Expression<Func<TEntity, object>>)));
    //     }
    //
    //     [Fact]
    //     public async Task GetPagedPerformsMapping()
    //     {
    //         var paginator = new Paginator
    //         {
    //             Rows = 1681862247, Offset = 393804085, SortBy = "TestValue1490050233", SortOrder = 1389279363,
    //             TotalRows = 1242735654
    //         };
    //         var filters = new[]
    //         {
    //             new Expression<Func<TEntity, bool>>(), new Expression<Func<TEntity, bool>>(),
    //             new Expression<Func<TEntity, bool>>()
    //         };
    //         var orderBy = new Expression<Func<TEntity, object>>();
    //         var result = await _testClass.GetPaged<string>(paginator, filters, orderBy);
    //         Assert.Equal(paginator, result.Paginator);
    //     }
    //
    //     [Fact]
    //     public async Task CanCallGetMappedWithTResultAndExpressionOfFuncOfTEntityAndBool()
    //     {
    //         var filter = new Expression<Func<TEntity, bool>>();
    //         var result = await _testClass.GetMapped<string>(filter);
    //         Assert.True(false, "Create or modify test");
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallGetMappedWithTResultAndExpressionOfFuncOfTEntityAndBoolWithNullFilter()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.GetMapped<string>(default));
    //     }
    //
    //     [Fact]
    //     public async Task CanCallGetMappedWithFilterAndSelect()
    //     {
    //         var filter = new Expression<Func<TEntity, bool>>();
    //         var select = new Expression<Func<TEntity, string>>();
    //         var result = await _testClass.GetMapped(filter, select);
    //         Assert.True(false, "Create or modify test");
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallGetMappedWithFilterAndSelectWithNullFilter()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() =>
    //             _testClass.GetMapped(default, new Expression<Func<TEntity, string>>()));
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallGetMappedWithFilterAndSelectWithNullSelect()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() =>
    //             _testClass.GetMapped(new Expression<Func<TEntity, bool>>(),
    //                 default(Expression<Func<TEntity, string>>)));
    //     }
    //
    //     [Fact]
    //     public async Task CanCallExists()
    //     {
    //         var filter = new Expression<Func<TEntity, bool>>();
    //         var result = await _testClass.Exists(filter);
    //         Assert.True(false, "Create or modify test");
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallExistsWithNullFilter()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.Exists(default));
    //     }
    //
    //     [Fact]
    //     public async Task CanCallCreate()
    //     {
    //         var entity = "TestValue1801109836";
    //         await _testClass.Create(entity);
    //         Assert.True(false, "Create or modify test");
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallCreateWithNullEntity()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.Create(default(TEntity)));
    //     }
    //
    //     [Fact]
    //     public async Task CanCallDelete()
    //     {
    //         var filter = new Expression<Func<TEntity, bool>>();
    //         var result = await _testClass.Delete(filter);
    //         Assert.True(false, "Create or modify test");
    //     }
    //
    //     [Fact]
    //     public async Task CannotCallDeleteWithNullFilter()
    //     {
    //         await Assert.ThrowsAsync<ArgumentNullException>(() => _testClass.Delete(default));
    //     }
    //
    //     private class TestRepositoryBase : RepositoryBase<TEntity>
    //     {
    //         public TestRepositoryBase(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    //         {
    //         }
    //
    //         public override Task Update(TEntity entity, Expression<Func<TEntity, bool>> filter)
    //         {
    //             return default;
    //         }
    //     }
    // }
}