using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.AliasTables;

public class AliasTableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");
    static readonly IColumn _id = ShadowSql.Identifiers.Column.Use("Id");

    [Fact]
    public void AndQuery()
    {
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void OrQuery()
    {
        var query = _db.From("Users")
            .As("u")
            .ToSqlOrQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void Where()
    {
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void WhereAnd()
    {
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Where(q => q
                .And("Id=@Id")
                .And("Status=@Status")
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void WhereOr()
    {
        var query = _db.From("Users")
            .As("u")
            .ToSqlOrQuery()
            .Where(q => q
                .Or("Id=@Id")
                .Or("Status=@Status")
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void Column()
    {
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .ColumnParameter("Id", "<", "LastId")
            .ColumnParameter("Status", "=", "state");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE u.[Id]<@LastId AND u.[Status]=@state", sql);
    }

    [Fact]
    public void ColumnValue()
    {
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .ToOr()
            .ColumnValue("Id", 100, "<")
            .ColumnValue("Status", true);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE u.[Id]<100 OR u.[Status]=1", sql);
    }
    [Fact]
    public void FieldCompare()
    {
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Where(q => q.Field("Id").Less("LastId"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE u.[Id]<@LastId", sql);
    }
    [Fact]
    public void Logic()
    {
        var query = new Users()
            .As("u")
            .ToSqlQuery()
            //_id是游离状态,不属于别名表,拼接sql时没有表前缀
            .Where(_id.LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE [Id]<100", sql);
    }
    [Fact]
    public void Table()
    {
        var query = new Users()
            .As("u")
            .ToSqlQuery()
            .Where(user => user.Id, Id => Id.Greater("LastId"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE u.[Id]>@LastId", sql);
    }
    [Fact]
    public void TableLogic()
    {
        var query = new Users()
            .As("u")
            .ToSqlQuery()
            .Where(user => user.Column("Id").Less("LastId"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE u.[Id]<@LastId", sql);
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
