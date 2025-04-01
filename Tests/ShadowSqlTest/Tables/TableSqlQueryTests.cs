using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.Tables;

public class TableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");
    static readonly IColumn _id = ShadowSql.Identifiers.Column.Use("Id");

    [Fact]
    public void AndQuery()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void OrQuery()
    {
        var query = _db.From("Users")
            .ToSqlOrQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void Where()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void WhereAnd()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .Where(q => q
                .And("Id=@Id")
                .And("Status=@Status")
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void WhereOr()
    {
        var query = _db.From("Users")
            .ToSqlOrQuery()
            .Where(q => q
                .Or("Id=@Id")
                .Or("Status=@Status")
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void Column()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .ColumnParameter("Id", "<", "LastId")
            .ColumnParameter("Status", "=", "state");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId AND [Status]=@state", sql);
    }

    [Fact]
    public void ColumnValue()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .ToOr()
            .ColumnValue("Id", 100, "<")
            .ColumnValue("Status", true);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100 OR [Status]=1", sql);
    }

    [Fact]
    public void FieldCompare()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .Where(q => q.Field("Id").Less("LastId"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId", sql);
    }
    [Fact]
    public void Logic()
    {
        var query = new Users()
            .ToSqlQuery()
            .Where(_id.LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
    [Fact]
    public void TableLogic()
    {
        var query = new Users()
            .ToSqlQuery()
            .Where(user => user.Id.Less("LastId"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId", sql);
    }

    class Users : Table
    {
        public Users()
            : base(nameof(Users))
        {
            Id = DefineColumn(nameof(Id));
            Status = DefineColumn(nameof(Status));
        }
        #region Columns
        public IColumn Id { get; private set; }
        public IColumn Status { get; private set; }
        #endregion
    }
}
