using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.Queries;

public class HavingQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void Having()
    {
        var view = _db.From("Users")
            .GroupBy("Grade")
            .Having("Grade IN @Grades");
        var sql = _engine.Sql(view);
        Assert.Equal("[Users] GROUP BY [Grade] HAVING Grade IN @Grades", sql);
    }
    [Fact]
    public void Source()
    {
        var view = _db.From("Users")
            .GroupBy("Grade")
            .Having(source => source.Field("Grade").In("Grades"));
        var sql = _engine.Sql(view);
        Assert.Equal("[Users] GROUP BY [Grade] HAVING [Grade] IN @Grades", sql);
    }
    [Fact]
    public void Count()
    {
        var view = _db.From("Users")
            .GroupBy("Grade")
            .Having(FieldInfo.Use("Score").Max().GreaterValue(90));
        var sql = _engine.Sql(view);
        Assert.Equal("[Users] GROUP BY [Grade] HAVING MAX([Score])>90", sql);
    }
}
