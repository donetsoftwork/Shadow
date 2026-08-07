using Shadow.DDL;
using Shadow.DDL.Schemas;
using ShadowSql;
using ShadowSql.Engines.MsSql;
using ShadowSql.MsSql;

namespace Shadow.DDLTests;

public class ColumnSchemaTests
{
    private readonly MsSqlEngine _engine = MsSqlType.Engine;

    [Fact]
    public void SqlType()
    {
        var sqlType = "VARCHAR(50)";
        var column = new ColumnSchema("Name", sqlType);
        var table = new TableSchema("User", [column]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains(sqlType, sql);
    }
    [Fact]
    public void Length()
    {
        var length = 50;
        var column = new ColumnSchema<string>("Name", length);
        var table = new TableSchema("User", [column]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains(length.ToString(), sql);
    }
    [Fact]
    public void Precision()
    {
        var precision = 24;
        var column = new ColumnSchema<decimal>("Score", precision, 0);
        var table = new TableSchema("User", [column]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains(precision.ToString(), sql);
    }
    [Fact]
    public void Scale()
    {
        var precision = 24;
        var scale = 2;
        var column = new ColumnSchema<decimal>("Height", precision, scale);
        var table = new TableSchema("User", [column]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains(precision.ToString(), sql);
        Assert.Contains(scale.ToString(), sql);
    }
    [Fact]
    public void Default()
    {
        var column = new ColumnSchema("CreateTime", "DATETIME2") { Default = "GETDATE()" };
        var table = new TableSchema("User", [column]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains("DEFAULT", sql);
    }
    [Fact]
    public void Identity()
    {
        var id = new ColumnSchema<int>("Id", ColumnType.Identity);
        var table = new TableSchema("User", [id]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains("IDENTITY(1,1)", sql);
    }
    [Fact]
    public void Key()
    {
        var id = new ColumnSchema<int>("Id", ColumnType.Key);
        var table = new TableSchema("User", [id]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains("PRIMARY KEY", sql);
    }
    [Fact]
    public void Unique()
    {
        var column = new ColumnSchema<string>("Name", 100, ColumnType.Unique);
        var table = new TableSchema("User", [column]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains("UNIQUE", sql);
    }
    [Fact]
    public void NotNull()
    {
        var name = new ColumnSchema("Name", "VARCHAR(50)", ColumnType.NotNull);
        var sex = new ColumnSchema<int>("Sex");
        var table = new TableSchema("User", [name, sex]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains("NOT NULL", sql);
    }
}
