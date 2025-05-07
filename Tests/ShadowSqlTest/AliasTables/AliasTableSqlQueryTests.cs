using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using ShadowSql.Simples;
using TestSupports;

namespace ShadowSqlTest.AliasTables;

public class AliasTableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");
    static readonly IColumn _id = ShadowSql.Identifiers.Column.Use("Id");

    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE Id=@Id AND Status=@Status")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE Id=@Id AND Status=@Status")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE Id=@Id AND Status=@Status")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE Id=@Id AND Status=@Status")]
    public void AndQuery(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData("\"Users\" u WHERE Id=:Id AND Status=:Status")]
    public void AndQueryOracle(string expected)
    {
        ISqlEngine engine = SqlEngines.Oracle;
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Where("Id=:Id", "Status=:Status");
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }

    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE Id=@Id OR Status=@Status")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE Id=@Id OR Status=@Status")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE Id=@Id OR Status=@Status")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE Id=@Id OR Status=@Status")]
    public void OrQuery(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = _db.From("Users")
            .As("u")
            .ToSqlOrQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData("\"Users\" u WHERE Id=:Id OR Status=:Status")]
    public void OrQueryOracle(string expected)
    {
        ISqlEngine engine = SqlEngines.Oracle;
        var query = _db.From("Users")
            .As("u")
            .ToSqlOrQuery()
            .Where("Id=:Id", "Status=:Status");
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }

    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE Id=@Id AND Status=@Status")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE Id=@Id AND Status=@Status")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE Id=@Id AND Status=@Status")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE Id=@Id AND Status=@Status")]
    public void Where(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData("\"Users\" u WHERE Id=:Id AND Status=:Status")]
    public void WhereOracle(string expected)
    {
        ISqlEngine engine = SqlEngines.Oracle;
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Where("Id=:Id", "Status=:Status");
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE Id=@Id AND Status=@Status")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE Id=@Id AND Status=@Status")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE Id=@Id AND Status=@Status")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE Id=@Id AND Status=@Status")]
    public void WhereAnd(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Apply(q => q
                .And("Id=@Id")
                .And("Status=@Status")
            );
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData("\"Users\" u WHERE Id=:Id AND Status=:Status")]
    public void WhereAndOracle(string expected)
    {
        ISqlEngine engine = SqlEngines.Oracle;
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Apply(q => q
                .And("Id=:Id")
                .And("Status=:Status")
            );
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE Id=@Id OR Status=@Status")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE Id=@Id OR Status=@Status")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE Id=@Id OR Status=@Status")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE Id=@Id OR Status=@Status")]
    public void WhereOr(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = _db.From("Users")
            .As("u")
            .ToSqlOrQuery()
            .Apply(q => q
                .Or("Id=@Id")
                .Or("Status=@Status")
            );
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData("\"Users\" u WHERE Id=:Id OR Status=:Status")]
    public void WhereOrOracle(string expected)
    {
        ISqlEngine engine = SqlEngines.Oracle;
        var query = _db.From("Users")
            .As("u")
            .ToSqlOrQuery()
            .Apply(q => q
                .Or("Id=:Id")
                .Or("Status=:Status")
            );
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE u.[Id]<@LastId AND u.[Status]=@state")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE u.`Id`<@LastId AND u.`Status`=@state")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE u.\"Id\"<@LastId AND u.\"Status\"=@state")]
    [InlineData(SqlEngineNames.Oracle, "\"Users\" u WHERE u.\"Id\"<:LastId AND u.\"Status\"=:state")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE u.\"Id\"<@LastId AND u.\"Status\"=@state")]
    public void Column(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .FieldParameter("Id", "<", "LastId")
            .FieldParameter("Status", "=", "state");
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }

    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE u.[Id]<100 OR u.[Status]=1")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE u.`Id`<100 OR u.`Status`=1")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE u.\"Id\"<100 OR u.\"Status\"=1")]
    [InlineData(SqlEngineNames.Oracle, "\"Users\" u WHERE u.\"Id\"<100 OR u.\"Status\"=1")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE u.\"Id\"<100 OR u.\"Status\"=1")]
    public void ColumnValue(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .ToOr()
            .FieldValue("Id", 100, "<")
            .FieldValue("Status", true);
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE u.[Id]<@LastId")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE u.`Id`<@LastId")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE u.\"Id\"<@LastId")]
    [InlineData(SqlEngineNames.Oracle, "\"Users\" u WHERE u.\"Id\"<:LastId")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE u.\"Id\"<@LastId")]
    public void FieldCompare(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = _db.From("Users")
            .As("u")
            .ToSqlQuery()
            .Where(q => q.Field("Id").Less("LastId"));
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE [Id]<100")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE `Id`<100")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE \"Id\"<100")]
    [InlineData(SqlEngineNames.Oracle, "\"Users\" u WHERE \"Id\"<100")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE \"Id\"<100")]
    public void Logic(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = new UserTable()
            .As("u")
            .ToSqlQuery()
            //_id是游离状态,不属于别名表,拼接sql时没有表前缀
            .Where(_id.LessValue(100));
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE u.[Id]>@LastId")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE u.`Id`>@LastId")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE u.\"Id\">@LastId")]
    [InlineData(SqlEngineNames.Oracle, "\"Users\" u WHERE u.\"Id\">:LastId")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE u.\"Id\">@LastId")]
    public void Table(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = new UserTable()
            .As("u")
            .ToSqlQuery()
            .Where(user => user.Id, Id => Id.Greater("LastId"));
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Users] AS u WHERE u.[Id]<@LastId")]
    [InlineData(SqlEngineNames.MySql, "`Users` AS u WHERE u.`Id`<@LastId")]
    [InlineData(SqlEngineNames.Sqlite, "\"Users\" AS u WHERE u.\"Id\"<@LastId")]
    [InlineData(SqlEngineNames.Oracle, "\"Users\" u WHERE u.\"Id\"<:LastId")]
    [InlineData(SqlEngineNames.Postgres, "\"Users\" AS u WHERE u.\"Id\"<@LastId")]
    public void TableLogic(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var query = new UserTable()
            .As("u")
            .ToSqlQuery()
            .Where(user => user.Strict("Id").Less("LastId"));
        var sql = engine.Sql(query);
        Assert.Equal(expected, sql);
    }
}
