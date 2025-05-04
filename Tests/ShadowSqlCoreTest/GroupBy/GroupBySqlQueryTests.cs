using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldInfos;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Simples;
using ShadowSql.Tables;
using TestSupports;

namespace ShadowSqlCoreTest.GroupBy;

public class GroupBySqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void Table()
    {
        var table = _db.From("Users");
        var groupBy = GroupBySqlQuery.Create(table, "City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] GROUP BY [City]", sql);
    }

    [Fact]
    public void TableQuery()
    {
        var query = new TableSqlQuery("Users")
             .Where("Age=20");
        var groupBy = GroupBySqlQuery.Create(query, "City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE Age=20 GROUP BY [City]", sql);
    }

    [Fact]
    public void Apply()
    {
        var table = _db.From("Users");
        var query = new TableSqlQuery(table)
             .Where("Age=20");
        var groupBy = GroupBySqlQuery.Create(query, "City");
        groupBy.Apply(q => q
                .And("Count(City)>100")
            );

        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE Age=20 GROUP BY [City] HAVING Count(City)>100", sql);
    }
    [Fact]
    public void HavingQuery()
    {
        var table = _db.From("Users");
        var query = new TableSqlQuery(table)
             .Where("Age=20");
        var groupBy = GroupBySqlQuery.Create(query, "CityId")
            .Having("CityId BETWEEN 1 AND 11");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE Age=20 GROUP BY [CityId] HAVING CityId BETWEEN 1 AND 11", sql);
    }
    [Fact]
    public void Having()
    {
        var view = GroupBySqlQuery.Create(_db.From("Users"), "Grade")
            .Having("Grade IN @Grades");
        var sql = _engine.Sql(view);
        Assert.Equal("[Users] GROUP BY [Grade] HAVING Grade IN @Grades", sql);
    }
    [Fact]
    public void Source()
    {
        var view = GroupBySqlQuery.Create(_db.From("Users"), "Grade")
            .Having(source => source.Field("Grade").In("Grades"));
        var sql = _engine.Sql(view);
        Assert.Equal("[Users] GROUP BY [Grade] HAVING [Grade] IN @Grades", sql);
    }
    [Fact]
    public void Count()
    {
        var view = GroupBySqlQuery.Create(_db.From("Users"), "Grade")
            .Having(FieldInfo.Use("Score").Max().GreaterValue(90));
        var sql = _engine.Sql(view);
        Assert.Equal("[Users] GROUP BY [Grade] HAVING MAX([Score])>90", sql);
    }
    [Fact]
    public void SourceField()
    {
        var table = _db.From("Users");
        var query = new TableSqlQuery(table)
            .Where("Age=20");
        var groupBy = GroupBySqlQuery.Create(query, "CityId")
            .Having(source => source.Field("City").InValue("北京", "上海"));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE Age=20 GROUP BY [CityId] HAVING [City] IN ('北京','上海')", sql);
    }
    [Fact]
    public void Field()
    {
        var level = Column.Use("Level");
        var groupBy = GroupBySqlQuery.Create("Users", "CityId")
            .Having(level.Max().GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void SourceAggregate()
    {
        var table = _db.From("Users");
        var query = new TableSqlQuery(table)
            .Where("Age=20");
        var groupBy = GroupBySqlQuery.Create(query, "CityId")
            .Having(g => g.Aggregate("MAX", "Level").GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE Age=20 GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void Func()
    {
        var query = new TableSqlQuery("Users")
             .Where("Age=20");
        var groupBy = GroupBySqlQuery.Create(query, "CityId")
            .Having(g => g.Max("Level").GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE Age=20 GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void DefineTable()
    {
        CommentTable table = new();
        var query = new TableSqlQuery(table)
            .Where(table.Pick.EqualValue(true));
        var groupBy = GroupBySqlQuery.Create(query, table.PostId)
            .Having(g => g.Count().GreaterValue(10));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] WHERE [Pick]=1 GROUP BY [PostId] HAVING COUNT(*)>10", sql);
    }
    [Fact]
    public void TableName()
    {
        var groupBy = GroupBySqlQuery.Create("Users", "City")
            .Having("COUNT(*)>10");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] GROUP BY [City] HAVING COUNT(*)>10", sql);
    }
    [Fact]
    public void JoinTable()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnSqlQuery.Create(c, p)
            .On(c.PostId, p.Id);
        JoinTableSqlQuery query = joinOn.Root
            .Where(c.Pick.EqualValue(true))
            .Where(p.Author.EqualValue("张三"));
        var groupBy = GroupBySqlQuery.Create(query, c.PostId)
            .Having(g => g.Count().GreaterValue(10));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三' GROUP BY c.[PostId] HAVING COUNT(*)>10", sql);
    }
    [Fact]
    public void HavingAggregate()
    {
        var groupBy = GroupBySqlQuery.Create("Users", "CityId")
            .HavingAggregate("MAX", "Level", static level => level.GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void GroupApply()
    {
        var groupBy = GroupBySqlQuery.Create("Users", "CityId")
            .Apply(static (q, g) => q
                .And(g.Count().GreaterValue(100))
                .And(g.Max("Level").GreaterValue(9))
            );
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] GROUP BY [CityId] HAVING COUNT(*)>100 AND MAX([Level])>9", sql);
    }
}
