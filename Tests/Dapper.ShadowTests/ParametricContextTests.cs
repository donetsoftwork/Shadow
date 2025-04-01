using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace Dapper.ShadowTests;

public class ParametricContextTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void ColumnValue()
    {
        var query = _db.From("Users")
            .ToQuery()
            .ColumnValue("Id", 100, "<")
            .ColumnValue("Status", true);
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        
        Assert.Equal("[Users] WHERE [Id]<@p1 AND [Status]=@p2", sql);
        Assert.NotNull(context.Parameters);
    }

    [Fact]
    public void ColumnInValue()
    {
        var query = _db.From("Users")
            .ToQuery()
            .ColumnInValue("Id", 1, 2, 3)
            .ColumnValue("Status", true);
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);

        Assert.Equal("[Users] WHERE [Id] IN (@p1,@p2,@p3) AND [Status]=@p4", sql);
        Assert.NotNull(context.Parameters);
    }
    [Fact]
    public void Select()
    {
        var query = new Table("Posts")
            .ToQuery()
            .ColumnEqualValue("Id", 10)
            .ToSelect();
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);

        Assert.Equal("SELECT * FROM [Posts] WHERE [Id]=@p1", sql);
        Assert.NotNull(context.Parameters);
    }
}