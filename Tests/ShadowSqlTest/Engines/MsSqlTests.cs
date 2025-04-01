using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using System.Text;

namespace ShadowSqlTest.Engines;

public class MsSqlTests
{
    ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Identifier()
    {
        StringBuilder builder = new();
        _engine.Identifier(builder, "Id");
        Assert.Equal("[Id]", builder.ToString());
    }
    [Fact]
    public void Parameter()
    {
        StringBuilder builder = new();
        _engine.Parameter(builder, "Id");
        Assert.Equal("@Id", builder.ToString());
    }

    [Fact]
    public void ColumnAs()
    {
        StringBuilder builder = new("Count(*)");
        _engine.ColumnAs(builder, "Total");
        Assert.Equal("Count(*) AS Total", builder.ToString());
    }
    [Fact]
    public void TableAs()
    {
        StringBuilder builder = new("[Customer_Orders]");
        _engine.ColumnAs(builder, "Order");
        Assert.Equal("[Customer_Orders] AS Order", builder.ToString());
    }

    [Fact]
    public void InsertPrefix()
    {
        StringBuilder builder = new();
        _engine.InsertPrefix(builder);
        Assert.Equal("INSERT INTO ", builder.ToString());
    }
    [Fact]
    public void InsertMultiPrefix()
    {
        StringBuilder builder = new();
        _engine.InsertMultiPrefix(builder);
        Assert.Equal("INSERT INTO ", builder.ToString());
    }
    [Fact]
    public void LogicNot()
    {
        StringBuilder builder = new();
        _engine.LogicNot(builder);
        Assert.Equal("NOT ", builder.ToString());
    }
}
