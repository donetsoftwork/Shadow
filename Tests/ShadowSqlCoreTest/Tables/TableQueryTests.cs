using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Simples;
using ShadowSql.Tables;

namespace ShadowSqlCoreTest.Tables;

public class TableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");
    static readonly IColumn _id = Column.Use("Id");
    static readonly IColumn _status = Column.Use("Status");

    [Fact]
    public void AndQuery()
    {
        var query = new TableQuery("Users")
            .And(_id.Equal())
            .And(_status.Equal("Status"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }
    [Fact]
    public void OrQuery()
    {
        var users = new UserTable();
        var query = new TableQuery("Users", new OrLogic())
             .Or(users.Id.Equal())
             .Or(users.Status.Equal("Status"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
    [Fact]
    public void Logic()
    {
        var users = new UserTable();
        var query = new TableQuery(users)
             .And(users.Id.LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
    [Fact]
    public void LogicAnd()
    {
        var users = new UserTable();
        var query = new TableQuery(users)
            .And(users.Id.Less("LastId") & users.Status.EqualValue(true));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId AND [Status]=1", sql);
    }
    [Fact]
    public void LogicOr()
    {
        var users = new UserTable();
        var query = new TableQuery(users, new OrLogic())
            .Or(users.Id.LessValue(100) | users.Status.EqualValue(true));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100 OR [Status]=1", sql);
    }
    [Fact]
    public void AndAtomicLogic()
    {
        var query = new TableQuery("Users")
            .And(_id.Equal())
            .And(_status.Equal("Status"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }
    [Fact]
    public void AndAndLogic()
    {
        var users = new UserTable();
        AndLogic andLogic = users.Id.Equal() & users.Status.Equal("Status");
        var query = new TableQuery(users)
            .And(andLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }

    [Fact]
    public void AndComplexAndLogic()
    {
        ComplexAndLogic complexAndLogic = new();
        complexAndLogic.AddLogic(_id.Equal());
        complexAndLogic.AddLogic(_status.Equal("Status"));
        var query = new TableQuery("Users")
            .And(complexAndLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }
    [Fact]
    public void AndOrLogic()
    {
        OrLogic orLogic = _id.Equal() | _status.Equal("Status");
        var query = new TableQuery("Users")
            .And(orLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
    [Fact]
    public void AndComplexOrLogic()
    {
        ComplexOrLogic complexOrLogic = new();
        complexOrLogic.AddLogic(_id.Equal());
        complexOrLogic.AddLogic(_status.Equal("Status"));
        var query = new TableQuery("Users")
            .And(complexOrLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
    [Fact]
    public void OrAtomicLogic()
    {
        var query = new TableQuery("Users")
            .Or(_id.Equal())
            .Or(_status.Equal("Status"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
    [Fact]
    public void OrAndLogic()
    {
        AndLogic andLogic = _id.Equal() & _status.Equal("Status");
        var query = new TableQuery("Users")
            .Or(andLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }

    [Fact]
    public void OrComplexAndLogic()
    {
        ComplexAndLogic complexAndLogic = new();
        complexAndLogic.AddLogic(_id.Equal());
        complexAndLogic.AddLogic(_status.Equal("Status"));
        var query = new TableQuery("Users")
            .Or(complexAndLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }
    [Fact]
    public void OrOrLogic()
    {
        OrLogic orLogic = _id.Equal() | _status.Equal("Status");
        var query = new TableQuery("Users")
            .Or(orLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
    [Fact]
    public void OrComplexOrLogic()
    {
        ComplexOrLogic complexOrLogic = new();
        complexOrLogic.AddLogic(_id.Equal());
        complexOrLogic.AddLogic(_status.Equal("Status"));
        var query = new TableQuery("Users")
            .Or(complexOrLogic);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id OR [Status]=@Status", sql);
    }
    [Fact]
    public void Apply()
    {
        var query = new TableQuery("Users")
            .Apply(static (q, u) => q
                .And(u.Field("Id").Equal())
                .And(u.Field("Status").EqualValue(true))
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=1", sql);
    }
    [Fact]
    public void Apply2()
    {
        var query = new TableQuery("Users")
            .Apply(static q => q
                .And(_id.Equal())
                .And(_status.Equal("Status"))
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=@Id AND [Status]=@Status", sql);
    }
    class UserTable : Table
    {
        public UserTable()
            : base("Users")
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
