using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Simples;
using TestSupports;

namespace ShadowSqlCoreTest.Join;

public class JoinTableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    
    [Fact]
    public void Where()
    {
        var employees = _db.From("Employees");
        var departments = _db.From("Departments");

        var departmentJoinOn = JoinOnSqlQuery.Create(employees, departments)
            .On(static (t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var joinTable = departmentJoinOn.Root
            .Where("Manager='CEO'");

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE Manager='CEO'", sql);
    }
    [Fact]
    public void Logic()
    {
        var employees = _db.From("Employees");
        var departments = _db.From("Departments");

        var departmentJoinOn = JoinOnSqlQuery.Create(employees, departments)
            .On(static (t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var manager = Column.Use("Manager");
        var joinTable = departmentJoinOn.Root
            .Where(manager.EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE [Manager]='CEO'", sql);
    }
    [Fact]
    public void SqlQuery2()
    {
        var joinOn = JoinOnSqlQuery.Create(SimpleDB.From("Employees"), SimpleDB.From("Departments"));
        var (t1, t2) = (joinOn.Left, joinOn.Source);
        joinOn.On(t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var joinTable = joinOn.Root
            .Where(t1.Field("Age").GreaterValue(30))
            .Where(t2.Field("Manager").EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t1.[Age]>30 AND t2.[Manager]='CEO'", sql);
    }
    [Fact]
    public void SqlQuery3()
    {
        var e = SimpleDB.From("Employees").As("e");
        var d = SimpleDB.From("Departments").As("d");
        var joinOn = JoinOnSqlQuery.Create(e, d)
            .On(e.Field("DepartmentId").Equal(d.Field("Id")));
        var joinTable = joinOn.Root
            .Where(e.Field("Age").GreaterValue(30))
            .Where(d.Field("Manager").EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS e INNER JOIN [Departments] AS d ON e.[DepartmentId]=d.[Id] WHERE e.[Age]>30 AND d.[Manager]='CEO'", sql);
    }

    [Fact]
    public void Atomic()
    {
        var employees = _db.From("Employees");
        var departments = _db.From("Departments");

        var departmentJoinOn = JoinOnSqlQuery.Create(employees, departments)
            .On(static (t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")));

        var sql = _engine.Sql(departmentJoinOn.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id]", sql);
    }
    [Fact]
    public void DefineAliasTable()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnSqlQuery.Create(c, p)
            .On(c.PostId, p.Id);
        JoinTableSqlQuery query = joinOn.Root
            .Where(c.Pick.EqualValue(true))
            .Where(p.Author.EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'", sql);
    }
    [Fact]
    public void AliasTable2()
    {
        var joinOn = JoinOnSqlQuery.Create("Comments", "Posts")
            .OnColumn("PostId", "Id");
        IAliasTable t1 = joinOn.Left;
        IAliasTable t2 = joinOn.Source;
        JoinTableSqlQuery query = joinOn.Root
            .Where(t1.Field("Pick").EqualValue(true))
            .Where(t2.Field("Author").EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1 AND t2.[Author]='张三'", sql);
    }
    [Fact]
    public void AliasTable3()
    {
        var c = SimpleTable.Use("Comments")
            .As("c");
        var p = SimpleTable.Use("Posts")
            .As("p");
        var joinOn = JoinOnSqlQuery.Create(c, p)
            .OnColumn("PostId", "Id");
        JoinTableSqlQuery query = joinOn.Root
            .Where(c.Field("Pick").EqualValue(true))
            .Where(p.Field("Author").EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'", sql);
    }
}
