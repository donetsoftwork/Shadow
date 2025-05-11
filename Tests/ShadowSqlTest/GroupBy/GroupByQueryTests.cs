using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.GroupBy;

public class GroupByQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");
    static readonly IColumn Age = Column.Use("Age");
    static readonly IColumn City = Column.Use("City");
    static readonly IColumn CityId = Column.Use("CityId");
    static readonly IColumn Level = Column.Use("Level");

    [Fact]
    public void Table()
    {
        var table = _db.From("Users");
        var groupBy = table.GroupBy("City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] GROUP BY [City]", sql);
    }

    [Fact]
    public void TableQuery()
    {
        var table = _db.From("Users");
        var query = table.ToQuery()
            .And(Age.EqualValue(20));
        var groupBy = query.GroupBy("City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [City]", sql);
    }
    [Fact]
    public void HavingQuery()
    {
        var table = _db.From("Users");
        var query = table.ToQuery()
            .And(Age.EqualValue(20));
        var groupBy = query.GroupBy("CityId")
            .And(City.InValue("北京", "上海"))
            .And(CityId.BetweenValue(1, 11));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING [City] IN ('北京','上海') AND [CityId] BETWEEN 1 AND 11", sql);
    }
    [Fact]
    public void SourceField()
    {
        var table = _db.From("Users");
        var query = table.ToQuery()
            .And(Age.EqualValue(20));
        var groupBy = query.GroupBy("CityId")
            .And(CityId.BetweenValue(1, 11));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING [CityId] BETWEEN 1 AND 11", sql);
    }
    [Fact]
    public void SourceAggregate()
    {
        var table = _db.From("Users");
        var query = table.ToQuery()
            .And(Age.EqualValue(20));
        var groupBy = query.GroupBy("CityId")
            .And(Level.Max().GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void SourceCount()
    {
        var table = _db.From("Users");
        var query = table.ToQuery()
            .And(Age.EqualValue(20));
        var groupBy = query.GroupBy("CityId")
            .And(Level.Max().GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void Apply()
    {
        var groupBy = new CommentTable()
            .GroupBy(static table => [table.PostId])
            .Apply(static table => table.Pick.Sum(), static (q, Pick) => q.And(Pick.GreaterValue(100)));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] GROUP BY [PostId] HAVING SUM([Pick])>100", sql);
    }
}
