using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using TestSupports;

namespace ShadowSqlTest.Cursors;

public class MultiTableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void ToCursor()
    {
        int limit = 10;
        int offset = 10;
        var cursor = new MultiTableSqlQuery()
            .AddMembers(_db.From("Employees"), _db.From("Departments"))
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void ToCursor2()
    {
        int limit = 10;
        int offset = 10;
        var cursor = new MultiTableQuery()
            .AddMembers("Employees", "Departments")
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void ToCursor3()
    {
        int limit = 10;
        int offset = 10;
        var cursor = _db.From("Employees")
            .SqlJoin(_db.From("Departments"))
            .OnColumn("DepartmentId", "Id")
            .Root
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void ToCursor4()
    {
        int limit = 10;
        int offset = 10;
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var cursor = c.Join(p)
            .And(c.PostId.Equal(p.Id))
            .Root
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void ToCursor5()
    {
        int limit = 10;
        int offset = 10;
        var cursor = new CommentTable()
            .Join(new PostTable())
            .Apply(c => c.PostId, p => p.Id, (q, PostId, Id) => q.And(PostId.Equal(Id)))
            .Root
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void SqlMulti()
    {
        int limit = 10;
        int offset = 10;
        var cursor = _db.From("Employees")
            .SqlMulti(_db.From("Departments"))
            .Where("t1.DepartmentId=t2.Id")
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void Multi()
    {
        int limit = 10;
        int offset = 10;
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var cursor = c.Multi(p)
            .And(c.PostId.Equal(p.Id))
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void Asc()
    {
        CommentTable c = new();
        PostTable p = new();
        var cursor = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .ToCursor()
            .Asc<PostTable>("t2", t2 => t2.Id);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t2.[Id]", sql);
    }
    [Fact]
    public void Desc()
    {
        CommentTable c = new();
        PostTable p = new();
        var cursor = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .ToCursor()
            .Desc<PostTable>("t2", t2 => t2.Id);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t2.[Id] DESC", sql);
    }

    [Fact]
    public void AscAliasTable()
    {
        var cursor = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .Root
            .ToCursor()
            .Asc<CommentAliasTable>("c", c => c.Id);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Id]", sql);
    }

    [Fact]
    public void DescAliasTable()
    {
        var cursor = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .Root
            .ToCursor()
            .Desc<CommentAliasTable>("c", c => c.Pick);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Pick] DESC", sql);
    }
    [Fact]
    public void AscAliasTable2()
    {
        var cursor = new CommentAliasTable("c")
            .SqlMulti(new PostAliasTable("p"))
            .ToCursor()
            .Asc<CommentAliasTable>("c", c => c.Id);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS c,[Posts] AS p ORDER BY c.[Id]", sql);
    }

    [Fact]
    public void DescAliasTable2()
    {
        var cursor = new CommentAliasTable("c")
            .SqlMulti(new PostAliasTable("p"))
            .ToCursor()
            .Desc<CommentAliasTable>("c", c => c.Pick);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS c,[Posts] AS p ORDER BY c.[Pick] DESC", sql);
    }
}
