using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Queries;

namespace ShadowSqlTest.Queries;

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
}