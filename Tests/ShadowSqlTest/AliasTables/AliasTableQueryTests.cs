using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;
using TestSupports;

namespace ShadowSqlTest.AliasTables;

public class AliasTableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");
    static readonly IColumn _id = ShadowSql.Identifiers.Column.Use("Id");


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
            .ToQuery()
            .And(q => q.Field("Id").Less("LastId"));
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
