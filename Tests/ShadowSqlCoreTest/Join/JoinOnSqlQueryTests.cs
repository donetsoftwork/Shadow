using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using TestSupports;

namespace ShadowSqlCoreTest.Join;

public class JoinOnSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void Create()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnSqlQuery.Create(c, p)
            .On(c.PostId, p.Id);
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]", sql);
    }
    [Fact]
    public void Create2()
    {
        var joinOn = JoinOnSqlQuery.Create(_db.From("Comments"), _db.From("Posts"))
            .OnColumn("PostId", "Id");
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]", sql);
    }
    [Fact]
    public void Create3()
    {
        var joinOn = JoinOnSqlQuery.Create("Comments", "Posts")
            .OnColumn("PostId", "Id");
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinPosts = JoinOnSqlQuery.Create(c, p)
            .On(c.PostId, p.Id);
        UserAliasTable u = new("u");
        var joinUsers = joinPosts.LeftTableJoin(u)
            .On(c.UserId, u.Id);
        var sql = _engine.Sql(joinUsers.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin2()
    {
        var joinPosts = JoinOnSqlQuery.Create(_db.From("Comments"), _db.From("Posts"))
            .OnColumn("PostId", "Id");
        var joinUsers = joinPosts.LeftTableJoin(_db.From("Users"))
            .OnColumn("UserId", "Id");
        var sql = _engine.Sql(joinUsers.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin3()
    {
        var joinPosts = JoinOnSqlQuery.Create("Comments", "Posts")
            .OnColumn("PostId", "Id");
        var joinUsers = joinPosts.LeftTableJoin("Users")
            .OnColumn("UserId", "Id");
        var sql = _engine.Sql(joinUsers.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin()
    {
        PostAliasTable p = new("p");
        CommentAliasTable c = new("c");
        var joinComments = JoinOnSqlQuery.Create(p, c)
            .On(p.Id, c.PostId);
        UserAliasTable u = new("u");
        var joinUsers = joinComments.RightTableJoin(u)
            .On(c.UserId, u.Id);
        var sql = _engine.Sql(joinUsers.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin2()
    {
        var joinComments = JoinOnSqlQuery.Create(_db.From("Posts"), _db.From("Comments"))
            .OnColumn("Id", "PostId");
        var joinUsers = joinComments.RightTableJoin(_db.From("Users"))
            .OnColumn("UserId", "Id");
        var sql = _engine.Sql(joinUsers.Root);
        Assert.Equal("[Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin3()
    {
        var joinComments = JoinOnSqlQuery.Create("Posts", "Comments")
            .OnColumn("Id", "PostId");
        var joinUsers = joinComments.RightTableJoin("Users")
            .OnColumn("UserId", "Id");
        var sql = _engine.Sql(joinUsers.Root);
        Assert.Equal("[Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]", sql);
    }

    [Fact]
    public void On()
    {
        var joinOn = CreateJoin()
            .On("t1.DepartmentId=t2.Id");
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.DepartmentId=t2.Id", sql);
    }

    [Fact]
    public void Source()
    {
        var joinOn = CreateJoin()
            .AsLeftJoin()
            .On((t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Employees] AS t1 LEFT JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id]", sql);
    }

    [Fact]
    public void TableLogicInfo()
    {
        var joinOn = CreateJoin();
        var left = joinOn.Left;
        var right = joinOn.Source;

        var departmentId = left.Field("DepartmentId");
        var id = right.Field("Id");
        joinOn.On(departmentId.Equal(id));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id]", sql);
    }

    [Fact]
    public void Apply()
    {
        var joinOn = JoinOnSqlQuery.Create("Comments", "Posts")
            .Apply((on, t1, t2) => on.And(t1.Field("PostId").Equal(t2.Field("Id"))));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]", sql);
    }

    private static JoinOnSqlQuery CreateJoin()
    {
        var employees = _db.From("Employees")
            .DefineColums("Id", "Name", "DepartmentId");
        var departments = _db.From("Departments")
            .DefineColums("Id", "Name", "Manager", "ParentId", "RootId");
        return JoinOnSqlQuery.Create(employees, departments);
    }
}
