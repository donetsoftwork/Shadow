using ShadowSql;
using ShadowSql.ColumnQueries;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;


namespace ShadowSqlTest.Queries;

public class ColumnQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ColumnParameter()
    {
        var userTable = new Table("Users")
            .DefineColums("Id", "Status");
        var query = userTable.ToSqlQuery()
            .ColumnParameter("Id", "<", "LastId")
            .ColumnParameter("Status", "=", "state");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId AND [Status]=@state", sql);
    }
    [Fact]
    public void ColumnIn()
    {
        var userTable = new Table("Users")
            .DefineColums("Id");
        var query = userTable.ToSqlQuery()
            .ColumnIn("Id", "Ids");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id] IN @Ids", sql);
    }
    [Fact]
    public void ColumnValue()
    {
        var userTable = new Table("Users")
            .DefineColums("Id", "Status");
        var query = userTable.ToSqlOrQuery()
            .ColumnValue("Id", 100, "<")
            .ColumnValue("Status", true);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100 OR [Status]=1", sql);
    }
    [Fact]
    public void Column()
    {
        var u = new Table("Users")
            .DefineColums("Id", "Status");
        var query = u.ToSqlQuery()
            .Where(u.Column("Id").GreaterEqual("LastId"))
            .Where(u => u.Column("Status").EqualValue(true));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]>=@LastId AND [Status]=1", sql);
    }
    [Fact]
    public void TableColumnParameter()
    {
        var commentTable = new Table("Comments")
            .DefineColums("PostId", "Pick");
        var postTable = new Table("Posts")
            .DefineColums("Id", "Author");
        var query = commentTable.SqlJoin(postTable)
            .OnColumn("PostId", "Id")
            .Root            
            .TableColumnParameter("Comments", "Pick", "=", "PickState")
            .TableColumnParameter("Posts", "Author");
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=@PickState AND t2.[Author]=@Author", sql);
    }
    [Fact]
    public void TableColumn()
    {
        var commentTable = new Table("Comments")
            .DefineColums("PostId", "Pick");
        var postTable = new Table("Posts")
            .DefineColums("Id", "Author");
        var query = commentTable.SqlJoin(postTable)
            .OnColumn("PostId", "Id")
            .Root
            .Where(join => join.From("Comments").Column("Pick").Equal())
            .Where("t2", p => p.Column("Author").EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=@Pick AND t2.[Author]='张三'", sql);
    }
    [Fact]
    public void TableColumnValue()
    {
        var commentTable = new Table("Comments")
            .DefineColums("PostId", "Pick");
        var postTable = new Table("Posts")
            .DefineColums("Id", "Author");

        var query = commentTable.SqlMulti(postTable)
            .Where("t1.PostId=t2.Id")
            .TableColumnValue("Comments", "Pick", false)
            .TableColumnValue("Posts", "Author", "张三");
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1,[Posts] AS t2 WHERE t1.[Pick]=0 AND t2.[Author]='张三' AND t1.PostId=t2.Id", sql);
    }
}
