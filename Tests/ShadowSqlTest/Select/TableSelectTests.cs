using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using ShadowSql.Simples;
using TestSupports;

namespace ShadowSqlTest.Select;

public class TableSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void Select()
    {
        var select = _db.From("Users")
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users]", sql);
    }
    [Fact]
    public void Filter()
    {
        UserTable table = new();
        var select = table.ToSelect(table.Status.EqualValue(true))
            .Select(table.Id, table.Name);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void Filter2()
    {
        var select = new UserTable()
            .ToSelect(table => table.Status.EqualValue(true))
            .Select(table => [table.Id, table.Name]);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void SqlCondition()
    {
        var select = _db.From("Users")
            .ToSqlQuery()
            .Where("Status=1")
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE Status=1", sql);
    }
    [Fact]
    public void SqlTable()
    {
        var select = new UserTable()
            .ToSqlQuery()
            .Where(table => table.Status.EqualValue(true))
            .ToSelect()
            .Select(table => [table.Id, table.Name]);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void SqlTable2()
    {
        var table = new UserTable();
        var select = table.ToSqlQuery()
            .Where(table.Status.EqualValue(true))
            .ToSelect()
            .Select(table.Id, table.Name);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void SqlField()
    {
        var select = _db.From("Users")
            .ToSqlQuery()
            .Where(table => table.Field("Status").EqualValue(true))
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void SqlField2()
    {
        var table = _db.From("Users");
        var select = table.ToSqlQuery()
            .Where(table.Field("Status").EqualValue(true))
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void SqlColumnCompare()
    {
        var select = _db.From("Users")
            .ToSqlQuery()
            .FieldEqualValue("Status", true)
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void SqlColumnCompare2()
    {
        var select = _db.From("Users")
            .ToSqlQuery()
            .FieldEqual("Status", "SuccessStatus")
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=@SuccessStatus", sql);
    }
    [Fact]
    public void SqlColumnCompare3()
    {
        var select = _db.From("Users")
            .ToSqlQuery()
            .FieldValue("Id", 10, ">")
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Id]>10", sql);
    }
    [Fact]
    public void SqlColumn()
    {
        var select = new Table("Users")
            .DefineColums("Id", "Name", "Status")
            .ToSqlQuery()
            .Where(table => table.Strict("Status").EqualValue(true))
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void SqlColumn2()
    {
        var u = new Table("Users")
            .DefineColums("Id", "Name", "Status");
        var select = u.ToSqlQuery()
            .Where(u.Strict("Status").EqualValue(true))
            .ToSelect()
            .Select(u.Strict("Id"), u.Strict("Name"));
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void Query()
    {
        var select = new UserTable()
            .ToQuery()
            .And(table => table.Status.EqualValue(true))
            .ToSelect()
            .Select(table => [table.Id, table.Name]);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }

    [Fact]
    public void Cursor()
    {
        int limit = 10;
        int offset = 10;
        var select = _db.From("Users")
            .ToCursor()
            .Skip(offset)
            .Take(limit)
            .ToSelect();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [Users] OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Desc()
    {
        var select = _db.From("Users")
            .ToCursor()
            .Desc(u => u.Field("Age"))
            .Asc(u => u.Field("Id"))
            .ToSelect();
        select.Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] ORDER BY [Age] DESC,[Id]", sql);
    }
}
