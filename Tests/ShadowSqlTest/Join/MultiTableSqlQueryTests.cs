using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using TestSupports;

namespace ShadowSqlTest.Join;

public class MultiTableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void SqlMulti()
    {
        var multiTable = _db.From("Employees")
            .SqlMulti(_db.From("Departments"));
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Employees] AS t1,[Departments] AS t2", sql);
    }
    [Fact]
    public void SqlMulti2()
    {
        var multiTable = _db.From("Employees")
            .As("e")
            .SqlMulti(_db.From("Departments").As("d"))
            .Where("e.DepartmentId=d.Id");
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Employees] AS e,[Departments] AS d WHERE e.DepartmentId=d.Id", sql);
    }

    [Fact]
    public void TableColumnParameter()
    {
        var query = new MultiTableSqlQuery()
            .AddMembers("Comments", "Posts")
            .Where("t1.PostId=t2.Id")
            .TableFieldParameter("Posts", "Author")
            .TableFieldParameter("Comments", "Pick", "=", "PickState");
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1,[Posts] AS t2 WHERE t2.[Author]=@Author AND t1.[Pick]=@PickState AND t1.PostId=t2.Id", sql);
    }
    [Fact]
    public void TableColumnValue()
    {
        var query = new MultiTableSqlQuery()
            .AddMembers("Comments", "Posts")
            .Where("t1.PostId=t2.Id")
            .TableFieldValue("Posts", "Author", "张三")
            .TableFieldValue("Comments", "Pick", false);
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1,[Posts] AS t2 WHERE t2.[Author]='张三' AND t1.[Pick]=0 AND t1.PostId=t2.Id", sql);
    }
    [Fact]
    public void DefineTable()
    {
        CommentTable c = new();
        PostTable p = new();
        var query = new MultiTableSqlQuery()
            .AddMembers(c.As("c"), p.As("p"))
            .Where("c.PostId=p.Id")
            .Where<CommentTable>("c", c => c.Pick, Pick => Pick.EqualValue(true))
            .Where<PostTable>("p", p => p.Author, Author => Author.EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c,[Posts] AS p WHERE c.[Pick]=1 AND p.[Author]='张三' AND c.PostId=p.Id", sql);
    }
    [Fact]
    public void TableName()
    {
        var query = new MultiTableSqlQuery()
            .AddMembers("Comments", "Posts")
            .Where("t1.PostId=t2.Id")
            .Where("t1", c => c.Field("Pick").EqualValue(true))
            .Where("t2", p => p.Field("Author").EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1,[Posts] AS t2 WHERE t1.[Pick]=1 AND t2.[Author]='张三' AND t1.PostId=t2.Id", sql);
    }
    [Fact]
    public void Apply()
    {
        var query = new CommentAliasTable("c")
            .SqlMulti(new PostAliasTable("p"))
            .Where("c.PostId=p.Id")
            .Apply<CommentAliasTable>("c", (q, c) => q.And(c.Pick.EqualValue(true)))
            .Apply<PostAliasTable>("p", (q, p) => q.And(p.Author.EqualValue("张三")));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c,[Posts] AS p WHERE c.[Pick]=1 AND p.[Author]='张三' AND c.PostId=p.Id", sql);
    }
}
