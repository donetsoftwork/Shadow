using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.GroupBy;

public class GroupBySqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void Table()
    {
        var table = _db.From("Users");
        var groupBy = table.SqlGroupBy("City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] GROUP BY [City]", sql);
    }

    [Fact]
    public void TableQuery()
    {
        var table = _db.From("Users");
        var query = table.ToSqlQuery()
            .ColumnEqualValue("Age", 20);
        var groupBy = query.GroupBy("City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [City]", sql);
    }

    [Fact]
    public void HavingFunc()
    {
        var table = _db.From("Users");
        var query = table.ToSqlQuery()
            .ColumnEqualValue("Age", 20);
        var groupBy = query.GroupBy("City");
        groupBy.Having(q => q
                .And("Count(City)>100")
            );


        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [City] HAVING Count(City)>100", sql);
    }
    [Fact]
    public void HavingQuery()
    {
        var table = _db.From("Users");
        var query = table.ToSqlQuery()
            .ColumnEqualValue("Age", 20);
        var groupBy = query.GroupBy("CityId")
            .ColumnInValue("City", "北京", "上海")
            .ColumnBetweenValue("CityId", 1, 11);
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING [City] IN ('北京','上海') AND [CityId] BETWEEN 1 AND 11", sql);
    }
    [Fact]
    public void Having()
    {
        var view = _db.From("Users")
            .SqlGroupBy("Grade")
            .Having("Grade IN @Grades");
        var sql = _engine.Sql(view);
        Assert.Equal("[Users] GROUP BY [Grade] HAVING Grade IN @Grades", sql);
    }
    [Fact]
    public void Source()
    {
        var view = _db.From("Users")
            .SqlGroupBy("Grade")
            .Having(source => source.Field("Grade").In("Grades"));
        var sql = _engine.Sql(view);
        Assert.Equal("[Users] GROUP BY [Grade] HAVING [Grade] IN @Grades", sql);
    }
    [Fact]
    public void Count()
    {
        var view = _db.From("Users")
            .SqlGroupBy("Grade")
            .Having(FieldInfo.Use("Score").Max().GreaterValue(90));
        var sql = _engine.Sql(view);
        Assert.Equal("[Users] GROUP BY [Grade] HAVING MAX([Score])>90", sql);
    }
    [Fact]
    public void SourceField()
    {
        var table = _db.From("Users");
        var query = table.ToSqlQuery()
            .ColumnEqualValue("Age", 20);
        var groupBy = query.GroupBy("CityId")
            .Having(source => source.Field("City").InValue("北京", "上海"));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING [City] IN ('北京','上海')", sql);
    }
    [Fact]
    public void SourceAggregate()
    {
        var table = _db.From("Users");
        var query = table.ToSqlQuery()
            .ColumnEqualValue("Age", 20);
        var groupBy = query.GroupBy("CityId")
            .Having(q => q.Aggregate("MAX", "Level").GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void SourceCount()
    {
        var table = _db.From("Users");
        var query = table.ToSqlQuery()
            .ColumnEqualValue("Age", 20);
        var groupBy = query.GroupBy("CityId")
            .Having(source => source.Max("Level").GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
}
