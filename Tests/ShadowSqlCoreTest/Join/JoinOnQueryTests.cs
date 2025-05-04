using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Simples;
using TestSupports;

namespace ShadowSqlCoreTest.Join;

public class JoinOnQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void Create()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnQuery.Create(c, p)
            .And(c.PostId.Equal(p.Id));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]", sql);
    }
    [Fact]
    public void Create2()
    {
        var joinOn = JoinOnQuery.Create(_db.From("Comments"), _db.From("Posts"))
            .Apply("PostId", "Id", (logic, PostId, Id) => logic.And(PostId.Equal(Id)));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]", sql);
    }
    [Fact]
    public void Create3()
    {
        var joinOn = JoinOnQuery.Create("Comments", "Posts")
            .Apply("PostId", "Id", (logic, PostId, Id) => logic.And(PostId.Equal(Id)));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinPosts = JoinOnQuery.Create(c, p)
            .And(c.PostId.Equal(p.Id));
        UserAliasTable u = new("u");
        var joinUsers = joinPosts.LeftTableJoin(u)
            .And(c.UserId.Equal(u.Id));
        var sql = _engine.Sql(joinUsers.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin2()
    {
        var joinPosts = JoinOnQuery.Create(_db.From("Comments"), _db.From("Posts"))
            .Apply("PostId", "Id", (logic, PostId, Id) => logic.And(PostId.Equal(Id)));
        var joinUsers = joinPosts.LeftTableJoin(_db.From("Users"))
            .Apply("UserId", "Id", (logic, UserId, Id) => logic.And(UserId.Equal(Id)));
        var sql = _engine.Sql(joinUsers.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin3()
    {
        var joinPosts = JoinOnQuery.Create("Comments", "Posts")
            .Apply("PostId", "Id", (logic, PostId, Id) => logic.And(PostId.Equal(Id)));
        var joinUsers = joinPosts.LeftTableJoin("Users")
            .Apply("UserId", "Id", (logic, UserId, Id) => logic.And(UserId.Equal(Id)));
        var sql = _engine.Sql(joinUsers.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin()
    {
        PostAliasTable p = new("p");
        CommentAliasTable c = new("c");        
        var joinComments = JoinOnQuery.Create(p, c)
            .And(p.Id.Equal(c.PostId));
        UserAliasTable u = new("u");
        var joinUsers = joinComments.RightTableJoin(u)
            .And(c.UserId.Equal(u.Id));
        var sql = _engine.Sql(joinUsers.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    } 

    [Fact]
    public void Source()
    {
        var joinOn = CreateJoin()
            .AsLeftJoin()
            .Apply("DepartmentId", "Id", (logic, DepartmentId, Id) => logic.And(DepartmentId.Equal(Id)));
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
        joinOn.And(departmentId.Equal(id));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id]", sql);
    }
    [Fact]
    public void Apply()
    {
        var joinOn = JoinOnQuery.Create("Comments", "Posts")
            .Apply("PostId", "Id", (q, PostId, Id) => q.And(PostId.Equal(Id)));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]", sql);
    }


    private static JoinOnQuery CreateJoin()
    {
        var employees = _db.From("Employees")
            .DefineColums("Id", "Name", "DepartmentId");
        var departments = _db.From("Departments")
            .DefineColums("Id", "Name", "Manager", "ParentId", "RootId");
        return JoinOnQuery.Create(employees, departments);
    }
}
