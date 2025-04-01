using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.Identifiers;

namespace ShadowSqlTest.Select;

public class JoinTableSelectTests
{
    private static ISqlEngine _engine = new MySqlEngine();
    private static CommentAliasTable c = new("c");
    private static PostAliasTable p = new("p");

    [Fact]
    public void ByFieldName()
    {
        var joinOn = new Table("Comments").As("c")
            .Join(new Table("Posts").As("p"))
            .OnColumn("PostId", "Id")
            .WhereLeft(left => left.Field("Pick").EqualValue(true))
            .WhereRight(right => right.Field("Author").EqualValue("jxj"));

        var query = joinOn.Root.ToFetch()
             .Desc("c", c => c.Field("Id"))
             .ToSelect();
        query.Fields.Select("c", c => [c.Field("Id"), c.Field("Content")]);
        var sql = _engine.Sql(query);
        Assert.Equal("SELECT c.`Id`,c.`Content` FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id` WHERE c.`Pick`=1 AND p.`Author`='jxj' ORDER BY c.`Id` DESC", sql);
    }

    [Fact]
    public void ByLogic()
    {
        var joinOn = c.Join(p)
            .On(c.PostId.Equal(p.Id));
        var query = joinOn.Root
            .Where(c.Pick.Equal())
            .Where(p.Author.Equal())
            .ToFetch()
            .Desc(c.Id)
            .ToSelect();
        query.Fields.Select(c.Id, c.Content);
        var sql = _engine.Sql(query);
        //Console.WriteLine(sql);
        Assert.Equal("SELECT c.`Id`,c.`Content` FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id` WHERE c.`Pick`=@Pick AND p.`Author`=@Author ORDER BY c.`Id` DESC", sql);
    }
}
