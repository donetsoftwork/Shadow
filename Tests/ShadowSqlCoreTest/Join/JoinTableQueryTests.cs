using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using TestSupports;

namespace ShadowSqlCoreTest.Join;

public class JoinTableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void SelfJoin()
    {
        var departments = _db.From("Departments");
        var departmentJoinOn = JoinOnQuery.Create(departments, departments)
            .AsLeftJoin();
        var t1 = departmentJoinOn.Left;
        var t2 = departmentJoinOn.Source;
        departmentJoinOn.And(t1.Field("ParentId").Equal(t2.Field("Id")));
        var sql = _engine.Sql(departmentJoinOn.Root);
        Assert.Equal("[Departments] AS t1 LEFT JOIN [Departments] AS t2 ON t1.[ParentId]=t2.[Id]", sql);
    }    
    [Fact]
    public void Query()
    {
        var joinOn = JoinOnQuery.Create("Employees", "Departments");
        var (t1, t2) = (joinOn.Left, joinOn.Source);
        joinOn.And(t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var joinTable = joinOn.Root
            .And(t1.Field("Age").GreaterValue(30))
            .And(t2.Field("Manager").EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t1.[Age]>30 AND t2.[Manager]='CEO'", sql);
    }
    [Fact]
    public void Query2()
    {
        var e = new Table("Employees").As("e");
        var d = new Table("Departments").As("d");
        var joinOn = JoinOnQuery.Create(e, d)
            .And(e.Field("DepartmentId").Equal(d.Field("Id")));
        var joinTable = joinOn.Root
            .And(e.Field("Age").GreaterValue(30))
            .And(d.Field("Manager").EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS e INNER JOIN [Departments] AS d ON e.[DepartmentId]=d.[Id] WHERE e.[Age]>30 AND d.[Manager]='CEO'", sql);
    }

    [Fact]
    public void DefineAliasTable()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnQuery.Create(c, p)
            .And(c.PostId.Equal(p.Id));
        JoinTableQuery query = joinOn.Root
            .And(c.Pick.EqualValue(true))
            .And(p.Author.EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'", sql);
    }
    [Fact]
    public void AliasTable2()
    {
        var c = new Table("Comments")
            .As("c");
        var p = new Table("Posts")
            .As("p");
        var joinOn = JoinOnQuery.Create(c, p)
            .And(c.Field("PostId").Equal(p.Field("Id")));
        JoinTableQuery query = joinOn.Root
            .And(c.Field("Pick").EqualValue(true))
            .And(p.Field("Author").EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'", sql);
    }
}
