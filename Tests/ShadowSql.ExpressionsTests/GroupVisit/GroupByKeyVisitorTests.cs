using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Identifiers;
using ShadowSql.Tables;
using System.Linq.Expressions;

namespace ShadowSql.ExpressionsTests.GroupVisit;

public class GroupByKeyVisitorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    //[Fact]
    //public void Member()
    //{
    //    var fileds = new List<IFieldView>();
    //    var groupBy = EmptyTable.Use("UserRoles")
    //        .GroupBy<UserScore, UserRole>(u => new UserScore { UserId = u.UserId, Score = u.Score });
    //    Expression<Func<UserScore, int>> expression = key => key.UserId;
    //    var field = GroupByKeyVisitor.GetKey(groupBy, expression);
    //    var sql = _engine.Sql(groupBy);
    //    Assert.Equal("SELECT [Id],[Name] FROM [UserRoles]", sql);
    //}
    //[Fact]
    //public void Member2()
    //{
    //    var fileds = new List<IFieldView>();
    //    var groupBy = EmptyTable.Use("UserRoles")
    //        .GroupBy<UserScore, UserRole>(u => new UserScore { UserId = u.UserId, Score = u.Score });
    //    Expression<Func<UserScore, UserScore>> expression = key => key;
    //    var field = GroupByKeyVisitor.GetKeys(groupBy, expression);
    //    var sql = _engine.Sql(groupBy);
    //    Assert.Equal("SELECT [Id],[Name] FROM [UserRoles]", sql);
    //}
}
