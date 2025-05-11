using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.GroupBy;

public class GroupBySqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

    [Fact]
    public void SqlGroupBy()
    {
        var groupBy = _db.From("Comments")
            .SqlGroupBy("PostId");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] GROUP BY [PostId]", sql);
    }
    [Fact]
    public void SqlGroupBy2()
    {
        var table = new CommentTable();
        var groupBy = table.SqlGroupBy(table.PostId);
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] GROUP BY [PostId]", sql);
    }
    [Fact]
    public void HavingSum()
    {
        var groupBy = _db.From("Comments")
            .SqlGroupBy("PostId")
            .Having(g => g.Sum("Pick").GreaterValue(100));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] GROUP BY [PostId] HAVING SUM([Pick])>100", sql);
    }
    [Fact]
    public void HavingSum2()
    {
        var table = new CommentTable();
        var groupBy = table.SqlGroupBy(table.PostId)
            .Having(table.Pick.Sum().GreaterValue(100));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] GROUP BY [PostId] HAVING SUM([Pick])>100", sql);
    }
    [Fact]
    public void HavingSum3()
    {
        var groupBy = _db.From("Comments")
            .ToSqlQuery()
            .FieldGreaterEqualValue("Pick", 10)
            .SqlGroupBy("PostId")
            .Having(g => g.Sum("Pick").GreaterValue(100));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] WHERE [Pick]>=10 GROUP BY [PostId] HAVING SUM([Pick])>100", sql);
    }
    [Fact]
    public void HavingSum4()
    {
        var table = new CommentTable();
        var groupBy = table.ToSqlQuery()
            .Where(table.Pick.GreaterEqualValue(10))
            .SqlGroupBy(table.PostId)
            .Having(table.Pick.Sum().GreaterValue(100));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] WHERE [Pick]>=10 GROUP BY [PostId] HAVING SUM([Pick])>100", sql);
    }
    [Fact]
    public void SqlGroupBy3()
    {
        var groupBy = new CommentTable()
            .SqlGroupBy(table => [table.PostId]);
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] GROUP BY [PostId]", sql);
    }
    [Fact]
    public void SqlGroupByWhere()
    {
        IColumn age = Column.Use("Age");
        var groupBy = _db.From("Users")
            .SqlGroupBy(age.GreaterValue(30), "City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]>30 GROUP BY [City]", sql);
    }
    [Fact]
    public void SqlGroupByWhere2()
    {
        var table = new CommentTable();
        var groupBy = table.SqlGroupBy(table.UserId.InValue(1, 2, 3), table.PostId);
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] WHERE [UserId] IN (1,2,3) GROUP BY [PostId]", sql);
    }
    [Fact]
    public void SqlGroupByWhere3()
    {
        var groupBy = new CommentTable()
            .SqlGroupBy(table => table.UserId.LessValue(100), table => [table.PostId]);
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] WHERE [UserId]<100 GROUP BY [PostId]", sql);
    }
    [Fact]
    public void SqlGroupByQuery()
    {
        var groupBy = _db.From("Users")
            .ToSqlQuery()
            .Where("Age>30")
            .SqlGroupBy("City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE Age>30 GROUP BY [City]", sql);
    }
    [Fact]
    public void SqlGroupByQuery2()
    {
        var table = new CommentTable();
        var groupBy = table.ToSqlQuery()
            .Where(table.UserId.InValue(1, 2, 3))
            .SqlGroupBy(table.PostId);
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] WHERE [UserId] IN (1,2,3) GROUP BY [PostId]", sql);
    }
    [Fact]
    public void SqlGroupByQuery3()
    {
        var groupBy = new CommentTable()
            .ToSqlQuery()
            .Where(table => table.UserId.LessValue(100))
            .SqlGroupBy(table => [table.PostId]);
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] WHERE [UserId]<100 GROUP BY [PostId]", sql);
    }

    [Fact]
    public void TableQuery()
    {
        var table = _db.From("Users");
        var query = table.ToSqlQuery()
            .FieldEqualValue("Age", 20);
        var groupBy = query.SqlGroupBy("City");
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [City]", sql);
    }

    [Fact]
    public void Apply()
    {
        var table = _db.From("Users");
        var query = table.ToSqlQuery()
            .FieldEqualValue("Age", 20);
        var groupBy = query.SqlGroupBy("City");
        groupBy.Apply(q => q
                .And("Count(City)>100")
            );

        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [City] HAVING Count(City)>100", sql);
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
            .Having(Column.Use("Score").Max().GreaterValue(90));
        var sql = _engine.Sql(view);
        Assert.Equal("[Users] GROUP BY [Grade] HAVING MAX([Score])>90", sql);
    }
    [Fact]
    public void SourceField()
    {
        var table = _db.From("Users");
        var groupBy = table.ToSqlQuery()
            .SqlGroupBy("CityId");
        // GroupBy不能对分组之外的字段直接查询(可以聚合)
        Assert.ThrowsAny<ArgumentException>(() => groupBy.Field("City"));
    }
    [Fact]
    public void SourceAggregate()
    {
        var table = _db.From("Users");
        var query = table.ToSqlQuery()
            .FieldEqualValue("Age", 20);
        var groupBy = query.SqlGroupBy("CityId")
            .Having(g => g.Aggregate("MAX", "Level").GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void Max()
    {
        var table = _db.From("Users");
        var query = table.ToSqlQuery()
            .FieldEqualValue("Age", 20);
        var groupBy = query.SqlGroupBy("CityId")
            .Having(g => g.Max("Level").GreaterValue(9));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Users] WHERE [Age]=20 GROUP BY [CityId] HAVING MAX([Level])>9", sql);
    }
    [Fact]
    public void HavingAggregate()
    {
        var groupBy = new CommentTable()
            .SqlGroupBy(table => [table.PostId])
            .HavingAggregate(table => table.Pick.Sum(), Pick => Pick.GreaterValue(100));
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[Comments] GROUP BY [PostId] HAVING SUM([Pick])>100", sql);
    }
}
