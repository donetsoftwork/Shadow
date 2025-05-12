using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Tables;
using TestSupports;

namespace ShadowSqlCoreTest.GroupBy;

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
        var groupBy = GroupByQuery.Create(table, "City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] GROUP BY [City]", sql);
    }

    [Fact]
    public void TableQuery()
    {
        var table = _db.From("Users");
        var query = new TableQuery(table)
            .And(Age.EqualValue(20));
        var groupBy = GroupByQuery.Create(query, "City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [City]", sql);
    }
    [Fact]
    public void HavingQuery()
    {
        var table = _db.From("Users");
        var query = new TableQuery(table)
            .And(Age.EqualValue(20));
        var groupBy = GroupByQuery.Create(query, "CityId")
            .And(CityId.BetweenValue(1, 11));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING [CityId] BETWEEN 1 AND 11", sql);
    }
    [Fact]
    public void SourceField()
    {
        var table = _db.From("Users");
        var query = new TableQuery(table)
            .And(Age.EqualValue(20));
        var groupBy = GroupByQuery.Create(query, "CityId")
            .And(CityId.BetweenValue(1, 11));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING [CityId] BETWEEN 1 AND 11", sql);
    }
    [Fact]
    public void SourceAggregate()
    {
        var table = _db.From("Users");
        var query = new TableQuery(table)
            .And(Age.EqualValue(20));
        var groupBy = GroupByQuery.Create(query, "CityId")
            .And(Level.Max().GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void Func()
    {
        var table = _db.From("Users");
        var query = new TableQuery(table)
            .And(Age.EqualValue(20));
        var groupBy = GroupByQuery.Create(query, "CityId")
            .And(Level.Max().GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void DefineTable()
    {
        CommentTable table = new();
        var query = new TableQuery(table)
            .And(table.Pick.EqualValue(true));
        var groupBy = GroupByQuery.Create(query, table.PostId)
            .And(g => g.Count().GreaterValue(10));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] WHERE [Pick]=1 GROUP BY [PostId] HAVING COUNT(*)>10", sql);
    }
    [Fact]
    public void TableName()
    {
        var groupBy = GroupByQuery.Create("Users", "City")
            .And(g => g.Count().GreaterValue(10));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] GROUP BY [City] HAVING COUNT(*)>10", sql);
    }
    [Fact]
    public void JoinTable()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnQuery.Create(c, p)
            .And(c.PostId.Equal(p.Id));
        JoinTableQuery query = joinOn.Root
            .And(c.Pick.EqualValue(true))
            .And(p.Author.EqualValue("张三"));
        var groupBy = GroupByQuery.Create(query, c.PostId)
            .And(g => g.Count().GreaterValue(10));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三' GROUP BY c.[PostId] HAVING COUNT(*)>10", sql);
    }
    [Fact]
    public void GroupApply()
    {
        var groupBy = GroupByQuery.Create("Users", "CityId")
            .Apply(static (q, g) => q
                .And(g.Count().GreaterValue(100))
                .And(g.Max("Level").GreaterValue(9))
            );
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] GROUP BY [CityId] HAVING COUNT(*)>100 AND MAX([Level])>9", sql);
    }
    [Fact]
    public void Or()
    {
        var groupBy = GroupByQuery.Create("Comments", "PostId")
            .Or(g => g.Sum("Hits").GreaterValue(1000))
            .Or(g => g.Max("Recommend").GreaterValue(10));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] GROUP BY [PostId] HAVING SUM([Hits])>1000 OR MAX([Recommend])>10", sql);
    }
    [Fact]
    public void AndOr()
    {
        var groupBy = GroupByQuery.Create("Comments", "PostId")
            .And(g => g.Count().GreaterValue(10))
            .And(g => (g.Sum("Hits").GreaterValue(1000) | g.Max("Recommend").GreaterValue(10)));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] GROUP BY [PostId] HAVING COUNT(*)>10 AND (SUM([Hits])>1000 OR MAX([Recommend])>10)", sql);
    }
    [Fact]
    public void OrAnd()
    {
        var groupBy = GroupByQuery.Create("Comments", "PostId")
            .Or(g => g.Count().GreaterValue(10))
            .Or(g => (g.Sum("Hits").GreaterValue(1000) & g.Max("Recommend").GreaterValue(10)));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] GROUP BY [PostId] HAVING COUNT(*)>10 OR (SUM([Hits])>1000 AND MAX([Recommend])>10)", sql);
    }
}
