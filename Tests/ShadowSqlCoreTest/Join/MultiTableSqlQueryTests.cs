using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using TestSupports;

namespace ShadowSqlCoreTest.Join;

public class MultiTableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void AddMembers()
    {
        var e = _db.From("Employees")
            .As("e");
        var d = _db.From("Departments")
            .As("d");
        var multiTable = new MultiTableSqlQuery()
            .AddMembers(e, d)
            .Where(e.Field("DepartmentId").Equal(d.Field("Id")));
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Employees] AS e,[Departments] AS d WHERE e.[DepartmentId]=d.[Id]", sql);
    }
    [Fact]
    public void AddMembers2()
    {
        var multiTable = new MultiTableSqlQuery()
            .AddMembers(_db.From("Employees"), _db.From("Departments"))
            .Where("t1.DepartmentId=t2.Id");
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Employees] AS t1,[Departments] AS t2 WHERE t1.DepartmentId=t2.Id", sql);
    }
    [Fact]
    public void AddMembers3()
    {
        var multiTable = new MultiTableSqlQuery()
            .AddMembers("Employees", "Departments")
            .Where("t1.DepartmentId=t2.Id");
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Employees] AS t1,[Departments] AS t2 WHERE t1.DepartmentId=t2.Id", sql);
    }
    [Fact]
    public void CreateMember()
    {
        var multiTable = new MultiTableSqlQuery();
        var e = multiTable.CreateMember(_db.From("Employees"));
        var d = multiTable.CreateMember(_db.From("Departments"));
        multiTable.Where(e.Field("DepartmentId").Equal(d.Field("Id")));
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Employees] AS t1,[Departments] AS t2 WHERE t1.[DepartmentId]=t2.[Id]", sql);
    }
    [Fact]
    public void DefineAliasTable()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var multiTable = new MultiTableSqlQuery()
            .AddMembers(c, p)
            .Where(c.PostId.Equal(p.Id))
            .Where(c.Pick.EqualValue(true))
            .Where(p.Author.EqualValue("张三"));
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id] AND c.[Pick]=1 AND p.[Author]='张三'", sql);
    }

    [Fact]
    public void AliasTable2()
    {
        var multiTable = new MultiTableSqlQuery()
            .AddMembers("Comments", "Posts");
        IAliasTable t1 = multiTable.From("Comments");
        IAliasTable t2 = multiTable.From("Posts");
        var query = multiTable
            .Where(t1.Field("PostId").Equal(t2.Field("Id")))
            .Where(t1.Field("Pick").EqualValue(true))
            .Where(t2.Field("Author").EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1,[Posts] AS t2 WHERE t1.[PostId]=t2.[Id] AND t1.[Pick]=1 AND t2.[Author]='张三'", sql);
    }
    [Fact]
    public void AliasTable3()
    {
        var c = new Table("Comments")
            .As("c");
        var p = new Table("Posts")
            .As("p");
        var multiTable = new MultiTableSqlQuery()
            .AddMembers(c, p)
            .Where(c.Field("PostId").Equal(p.Field("Id")))
            .Where(c.Field("Pick").EqualValue(true))
            .Where(p.Field("Author").EqualValue("张三"));
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id] AND c.[Pick]=1 AND p.[Author]='张三'", sql);
    }
    [Fact]
    public void SelfJoin()
    {
        var multiTable = new MultiTableSqlQuery()
             .AddMembers("Departments", "Departments");

        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Departments] AS t1,[Departments] AS t2", sql);
    }
}
