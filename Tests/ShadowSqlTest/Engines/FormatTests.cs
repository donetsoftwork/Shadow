using ShadowSql.Engines;
using System.Text;
using TestSupports;

namespace ShadowSqlTest.Engines;

public class FormatTests
{
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "[Id]")]
    [InlineData(SqlEngineNames.MySql, "`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "\"Id\"")]
    [InlineData(SqlEngineNames.Oracle, "\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "\"Id\"")]
    public void Identifier(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        StringBuilder builder = new();
        engine.Identifier(builder, "Id");
        Assert.Equal(expected, builder.ToString());
    }

    [Theory]
    [InlineData(SqlEngineNames.MsSql, "@Id")]
    [InlineData(SqlEngineNames.MySql, "@Id")]
    [InlineData(SqlEngineNames.Sqlite, "@Id")]
    [InlineData(SqlEngineNames.Oracle, ":Id")]
    [InlineData(SqlEngineNames.Postgres, "@Id")]
    public void Parameter(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        StringBuilder builder = new();
        engine.Parameter(builder, "Id");
        Assert.Equal(expected, builder.ToString());
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "Count(*) AS Total")]
    [InlineData(SqlEngineNames.MySql, "Count(*) AS Total")]
    [InlineData(SqlEngineNames.Sqlite, "Count(*) AS Total")]
    [InlineData(SqlEngineNames.Oracle, "Count(*) Total")]
    [InlineData(SqlEngineNames.Postgres, "Count(*) AS Total")]
    public void ColumnAs(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        StringBuilder builder = new("Count(*)");
        engine.ColumnAs(builder, "Total");
        Assert.Equal(expected, builder.ToString());
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, " AS t1")]
    [InlineData(SqlEngineNames.MySql, " AS t1")]
    [InlineData(SqlEngineNames.Sqlite, " AS t1")]
    [InlineData(SqlEngineNames.Oracle, " t1")]
    [InlineData(SqlEngineNames.Postgres, " AS t1")]
    public void TableAs(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        StringBuilder builder = new();
        engine.TableAs(builder, "t1");
        Assert.Equal(expected, builder.ToString());
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "INSERT INTO ")]
    [InlineData(SqlEngineNames.MySql, "INSERT INTO ")]
    [InlineData(SqlEngineNames.Sqlite, "INSERT INTO ")]
    [InlineData(SqlEngineNames.Oracle, "INSERT INTO ")]
    [InlineData(SqlEngineNames.Postgres, "INSERT INTO ")]
    public void InsertPrefix(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        StringBuilder builder = new();
        engine.InsertPrefix(builder);
        Assert.Equal(expected, builder.ToString());
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "INSERT INTO ")]
    [InlineData(SqlEngineNames.MySql, "INSERT INTO ")]
    [InlineData(SqlEngineNames.Sqlite, "INSERT INTO ")]
    [InlineData(SqlEngineNames.Oracle, "INSERT ALL INTO ")]
    [InlineData(SqlEngineNames.Postgres, "INSERT INTO ")]
    public void InsertMultiPrefix(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        StringBuilder builder = new();
        engine.InsertMultiPrefix(builder);
        Assert.Equal(expected, builder.ToString());
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "NOT ")]
    [InlineData(SqlEngineNames.MySql, "NOT ")]
    [InlineData(SqlEngineNames.Sqlite, "NOT ")]
    [InlineData(SqlEngineNames.Oracle, "NOT ")]
    [InlineData(SqlEngineNames.Postgres, "NOT ")]
    public void LogicNot(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        StringBuilder builder = new();
        engine.LogicNot(builder);
        Assert.Equal(expected, builder.ToString());
    }
}
