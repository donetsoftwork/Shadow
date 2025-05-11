using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;

namespace Dapper.ShadowTests;

public class ParametricContextTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

    [Fact]
    public void ColumnValue()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .FieldValue("Id", 100, "<")
            .FieldValue("Status", true);
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        
        Assert.Equal("[Users] WHERE [Id]<@p1 AND [Status]=@p2", sql);
        Assert.NotNull(context.Parameters);
    }

    [Fact]
    public void ColumnInValue()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .FieldInValue("Id", 1, 2, 3)
            .FieldValue("Status", true);
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);

        Assert.Equal("[Users] WHERE [Id] IN (@p1,@p2,@p3) AND [Status]=@p4", sql);
        Assert.NotNull(context.Parameters);
    }
    [Fact]
    public void Select()
    {
        var query = new Table("Posts")
            .ToSqlQuery()
            .FieldEqualValue("Id", 10)
            .ToSelect();
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);

        Assert.Equal("SELECT * FROM [Posts] WHERE [Id]=@p1", sql);
        Assert.NotNull(context.Parameters);
    }
}