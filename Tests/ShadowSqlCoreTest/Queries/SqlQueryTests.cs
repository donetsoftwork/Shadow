using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Queries;

namespace ShadowSqlCoreTest.Queries;

public class SqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Create()
    {
        var query = SqlQuery.CreateAndQuery()
            .And("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void CreateOr()
    {
        var query = SqlQuery.CreateOrQuery()
            .Or("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void And()
    {
        var query = SqlQuery.CreateAndQuery()
            .And("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void Or()
    {
        var query = SqlQuery.CreateOrQuery()
            .Or("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void Not()
    {
        var query = SqlQuery.CreateAndQuery()
            .And("Id=@Id").Not();
        var sql = _engine.Sql(query);
        Assert.Equal("NOT Id=@Id", sql);
    }
    [Fact]
    public void NotMulti()
    {
        var query = SqlQuery.CreateAndQuery()
            .And("Id=@Id", "Status=@Status").Not();
        var sql = _engine.Sql(query);
        Assert.Equal("NOT (Id=@Id AND Status=@Status)", sql);
    }
    [Fact]
    public void NotOr()
    {
        var query = SqlQuery.CreateOrQuery()
            .Or("Id=@Id", "Status=@Status").Not();
        var sql = _engine.Sql(query);
        Assert.Equal("NOT (Id=@Id OR Status=@Status)", sql);
    }
    [Fact]
    public void AndOr()
    {
        var score = Column.Use("Score");
        var age = Column.Use("Age");
        var query = SqlQuery.CreateAndQuery()
            .And(score.LessValue(60))
            .And(age.LessValue(10) | age.GreaterValue(15));
        var sql = _engine.Sql(query);
        Assert.Equal("[Score]<60 AND ([Age]<10 OR [Age]>15)", sql);
    }
    [Fact]
    public void OrAnd()
    {
        var score = Column.Use("Score");
        var age = Column.Use("Age");
        var query = SqlQuery.CreateOrQuery()
            .Or(score.GreaterValue(90))
            .Or(age.GreaterValue(10) & age.LessValue(13));
        var sql = _engine.Sql(query);
        Assert.Equal("[Score]>90 OR ([Age]>10 AND [Age]<13)", sql);
    }
}