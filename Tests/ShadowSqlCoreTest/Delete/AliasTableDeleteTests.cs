using ShadowSql;
using ShadowSql.Delete;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using TestSupports;

namespace ShadowSqlCoreTest.Delete;

public class AliasTableDeleteTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Filter()
    {
        PostAliasTable table = new("p");
        var delete = new AliasTableDelete(table, table.Id.EqualValue(1));
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE p FROM [Posts] AS p WHERE p.[Id]=1", sql);
    }    
    [Fact]
    public void ToDelete()
    {
        var table = new PostAliasTable("p");
        var delete = table.ToDelete(table.Id.EqualValue(1));
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE p FROM [Posts] AS p WHERE p.[Id]=1", sql);
    }
}
