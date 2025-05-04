using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Simples;
using TestSupports;

namespace ShadowSqlTest.Join;

public class JoinTableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void WhereLeft()
    {
        var joinOn = new CommentTable()
            .SqlJoin(new PostTable())
            .On(c => c.PostId, p => p.Id)
            .WhereLeft(c => c.Pick, Pick => Pick.EqualValue(true));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1", sql);
    }
    [Fact]
    public void WhereLeft2()
    {
        var joinOn = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .WhereLeft(c => c.Pick.EqualValue(true));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1", sql);
    }
    [Fact]
    public void WhereLeft3()
    {
        var joinOn = SimpleDB.From("Comments")
            .SqlJoin(SimpleDB.From("Posts"))
            .OnColumn("PostId", "Id")
            .WhereLeft("Pick", Pick => Pick.EqualValue(true));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1", sql);
    }
    [Fact]
    public void WhereRight()
    {
        var joinOn = new CommentTable()
            .SqlJoin(new PostTable())
            .On(c => c.PostId, p => p.Id)
            .WhereRight(p => p.Author, Author => Author.NotEqualValue("张三"));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t2.[Author]<>'张三'", sql);
    }
    [Fact]
    public void WhereRight2()
    {
        var joinOn = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .WhereRight(p => p.Author.NotEqualValue("张三"));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]<>'张三'", sql);
    }
    [Fact]
    public void WhereRight3()
    {
        var joinOn = SimpleDB.From("Comments")
            .SqlJoin(SimpleDB.From("Posts"))
            .OnColumn("PostId", "Id")
            .WhereLeft("Pick", Pick => Pick.EqualValue(true))
            .WhereRight("Author", Author => Author.NotEqualValue("张三"));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1 AND t2.[Author]<>'张三'", sql);
    }
    [Fact]
    public void Normal()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var query = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .Where(c.Pick.EqualValue(true))
            .Where(p.Author.EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'", sql);
    }

    [Fact]
    public void TableColumn()
    {
        var joinOn = JoinOnSqlQuery.Create("Employees", "Departments")
            .OnColumn("DepartmentId", "Id");
        var joinTable = joinOn.Root
            .TableFieldEqual("t1", "Age")
            .TableFieldEqualValue("t2", "Manager", "CEO");
        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t1.[Age]=@Age AND t2.[Manager]='CEO'", sql);
    }
    [Fact]
    public void Table()
    {
        var departmentJoinOn = _db.From("Employees").SqlJoin(_db.From("Departments"))
            .OnColumn("DepartmentId", "Id");
        var joinTable = departmentJoinOn.Root
            .Apply("t1", static (query, employee) => query.And(employee.Field("Age").GreaterEqualValue(40)));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t1.[Age]>=40", sql);
    }
    [Fact]
    public void Apply()
    {
        var query = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .Root
            .Apply<CommentAliasTable>("c", (q, c) => q.And(c.Pick.EqualValue(true)))
            .Apply<PostAliasTable>("p", (q, p) => q.And(p.Author.EqualValue("张三")));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'", sql);
    }
}
