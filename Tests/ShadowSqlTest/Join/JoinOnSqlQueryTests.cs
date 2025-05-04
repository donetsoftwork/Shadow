using ShadowSql;
using ShadowSql.ColumnQueries;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using TestSupports;

namespace ShadowSqlTest.Join;

public class JoinOnSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void On()
    {
        var join = CreateJoin()
            .On("t1.DepartmentId=t2.Id");
        var sql = _engine.Sql(join.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.DepartmentId=t2.Id", sql);
    }

    [Fact]
    public void Source()
    {
        var join = CreateJoin()
            .AsLeftJoin()
            .On((t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")))
            //多表同名字段,优先本次被联接的表
            .On(view => view.Column("Id").IsNull());
        var sql = _engine.Sql(join.Root);
        Assert.Equal("[Employees] AS t1 LEFT JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] AND t2.[Id] IS NULL", sql);
    }

    [Fact]
    public void TableLogicInfo()
    {
        var join = CreateJoin();
        var left = join.Left;
        var right = join.Source;

        var departmentId = left.Field("DepartmentId");
        var id = right.Field("Id");
        join.On(departmentId.Equal(id));
        var sql = _engine.Sql(join.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id]", sql);
    }



    private static JoinOnSqlQuery<Table, Table> CreateJoin()
    {
        var employees = _db.From("Employees")
            .DefineColums("Id", "Name", "DepartmentId");
        var departments = _db.From("Departments")
            .DefineColums("Id", "Name", "Manager", "ParentId", "RootId");

        return employees.SqlJoin(departments);
    }

    [Fact]
    public void SqlJoin()
    {
        var joinOn = _db.From("Employees").SqlJoin(_db.From("Departments"))
            .OnColumn("DepartmentId", "Id");
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id]", sql);
    }


    [Fact]
    public void On2()
    {
        var joinOn = new CommentTable()
            .SqlJoin(new PostTable())
            .On(c => c.PostId, p => p.Id);
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]", sql);
    }
    [Fact]
    public void On3()
    {
        var joinOn = new CommentTable()
            .SqlJoin(new PostTable())
            .On(
                left => left.PostId,
                right => right.Id,
                (PostId, Id) => PostId.Equal(Id)
            );
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]", sql);
    }
    [Fact]
    public void OnLeft()
    {
        var joinOn = new CommentTable()
            .SqlJoin(new PostTable())
            .AsRightJoin()
            .On(c => c.PostId, p => p.Id)
            .OnLeft(c => c.Pick, Pick => Pick.EqualValue(true));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 RIGHT JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] AND t1.[Pick]=1", sql);
    }
    [Fact]
    public void OnRight()
    {
        var joinOn = new CommentTable()
            .SqlJoin(new PostTable())
            .AsLeftJoin()
            .On(c => c.PostId, p => p.Id)
            .OnRight(p => p.Author, Author => Author.NotEqualValue("张三"));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Comments] AS t1 LEFT JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] AND t2.[Author]<>'张三'", sql);
    }

    [Fact]
    public void LeftTableJoin()
    {
        var query = new CommentTable()
            .SqlJoin(new PostTable())
            .On(c => c.PostId, p => p.Id)
            .LeftTableJoin(new UserTable())
            .On(c => c.UserId, u => u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]", sql);
    }

    [Fact]
    public void RightTableJoin()
    {
        var query = new PostTable()
            .SqlJoin(new CommentTable())
            .On(p => p.Id, c => c.PostId)
            .RightTableJoin(new UserTable())
            .On(c => c.UserId, u => u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void TableName()
    {
        var joinOn = JoinOnSqlQuery.Create("Comments", "Posts")
            .OnColumn("PostId", "Id");
        JoinTableSqlQuery query = joinOn.Root
            .Where("t1", static comments => comments.Field("Pick").EqualValue(true))
            .Where("t2", static posts => posts.Field("Author").EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1 AND t2.[Author]='张三'", sql);
    }
}
