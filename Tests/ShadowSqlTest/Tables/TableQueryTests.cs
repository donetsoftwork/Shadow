using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Simples;

namespace ShadowSqlTest.Tables;

public class TableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");
    static readonly IColumn _id = ShadowSql.Identifiers.Column.Use("Id");
    static readonly IColumn _status = ShadowSql.Identifiers.Column.Use("Status");

    [Fact]
    public void AndQuery()
    {
        var query = _db.From("Users")
            .ToQuery()
            .And(_id.Equal())
            .And(_status.Equal("Status"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }
    [Fact]
    public void OrQuery()
    {
        var query = _db.From("Users")
            .ToOrQuery()
            .Or(_id.Equal())
            .Or(_status.Equal("Status"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }    
    [Fact]
    public void AndAtomicLogic()
    {
        var query = _db.From("Users")
            .ToQuery()
            .And(_id.Equal())
            .And(_status.Equal("Status"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }
    [Fact]
    public void AndAndLogic()
    {
        AndLogic andLogic = _id.Equal() & _status.Equal("Status");
        var query = _db.From("Users")
            .ToQuery()
            .And(andLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }

    [Fact]
    public void AndComplexAndLogic()
    {
        ComplexAndLogic complexAndLogic = new();
        complexAndLogic.AddLogic(_id.Equal());
        complexAndLogic.AddLogic(_status.Equal("Status"));
        var query = _db.From("Users")
            .ToQuery()
            .And(complexAndLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }
    [Fact]
    public void AndOrLogic()
    {
        OrLogic orLogic = _id.Equal() | _status.Equal("Status");
        var query = _db.From("Users")
            .ToQuery()
            .And(orLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
    [Fact]
    public void AndComplexOrLogic()
    {
        ComplexOrLogic complexOrLogic = new();
        complexOrLogic.AddLogic(_id.Equal());
        complexOrLogic.AddLogic(_status.Equal("Status"));
        var query = _db.From("Users")
            .ToQuery()
            .And(complexOrLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
    [Fact]
    public void OrAtomicLogic()
    {
        var query = _db.From("Users")
            .ToQuery()
            .Or(_id.Equal())
            .Or(_status.Equal("Status"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
    [Fact]
    public void OrAndLogic()
    {
        AndLogic andLogic = _id.Equal() & _status.Equal("Status");
        var query = _db.From("Users")
            .ToQuery()
            .Or(andLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }

    [Fact]
    public void OrComplexAndLogic()
    {
        ComplexAndLogic complexAndLogic = new();
        complexAndLogic.AddLogic(_id.Equal());
        complexAndLogic.AddLogic(_status.Equal("Status"));
        var query = _db.From("Users")
            .ToQuery()
            .Or(complexAndLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }
    [Fact]
    public void OrOrLogic()
    {
        OrLogic orLogic = _id.Equal() | _status.Equal("Status");
        var query = _db.From("Users")
            .ToQuery()
            .Or(orLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
    [Fact]
    public void OrComplexOrLogic()
    {
        ComplexOrLogic complexOrLogic = new();
        complexOrLogic.AddLogic(_id.Equal());
        complexOrLogic.AddLogic(_status.Equal("Status"));
        var query = _db.From("Users")
            .ToQuery()
            .Or(complexOrLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
}
