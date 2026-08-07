using Shadow.DDL.SqlTypes;
using ShadowSql;
using ShadowSql.Engines.MsSql;
using ShadowSql.MsSql;

namespace Shadow.DDLTests;

public class MsSqlTypeTests
{
    private readonly MsSqlEngine _engine = MsSqlType.Engine;

    [Fact]
    public void Bool()
    {
        var sqlType = SqlType.Create<bool>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("BIT", sql);
    }
    [Fact]
    public void Int()
    {
        var sqlType = SqlType.Create<int>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("INT", sql);
    }
    [Fact]
    public void Long()
    {
        var sqlType = SqlType.Create<long>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("BIGINT", sql);
    }
    [Fact]
    public void Short()
    {
        var sqlType = SqlType.Create<short>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("SMALLINT", sql);
    }
    [Fact]
    public void Byte()
    {
        var sqlType = SqlType.Create<byte>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("TINYINT", sql);
    }
    [Fact]
    public void UInt()
    {
        var sqlType = SqlType.Create<uint>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("INT", sql);
    }
    [Fact]
    public void ULong()
    {
        var sqlType = SqlType.Create<ulong>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("BIGINT", sql);
    }
    [Fact]
    public void UShort()
    {
        var sqlType = SqlType.Create<ushort>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("SMALLINT", sql);
    }
    [Fact]
    public void SByte()
    {
        var sqlType = SqlType.Create<sbyte>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("TINYINT", sql);
    }
    [Fact]
    public void Float()
    {
        var sqlType = SqlType.Create<float>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("REAL", sql);
    }
    [Fact]
    public void Double()
    {
        var sqlType = SqlType.Create<double>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("FLOAT", sql);
    }
    [Fact]
    public void Decimal()
    {
        var sqlType = SqlType.Create<Decimal>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("DECIMAL", sql);
    }
#if NET7_0_OR_GREATER
    [Fact]
    public void DateOnly()
    {
        var sqlType = SqlType.Create<DateOnly>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("DATE", sql);
    }
    [Fact]
    public void TimeOnly()
    {
        var sqlType = SqlType.Create<TimeOnly>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("TIME", sql);
    }
#endif
    [Fact]
    public void DateTime()
    {
        var sqlType = SqlType.Create<DateTime>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("DATETIME2", sql);
    }
    [Fact]
    public void TimeSpan()
    {
        var sqlType = SqlType.Create<TimeSpan>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("TIME", sql);
    }
    [Fact]
    public void DateTimeOffset()
    {
        var sqlType = SqlType.Create<DateTimeOffset>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("DATETIMEOFFSET", sql);
    }
    [Fact]
    public void Guid()
    {
        var sqlType = SqlType.Create<Guid>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("UNIQUEIDENTIFIER", sql);
    }
    [Fact]
    public void String()
    {
        var sqlType = SqlType.Create<string>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("VARCHAR(50)", sql);
    }
    [Fact]
    public void Char()
    {
        var sqlType = SqlType.Create<char>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("VARCHAR(50)", sql);
    }
    [Fact]
    public void Chars()
    {
        var sqlType = SqlType.Create<char[]>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("VARCHAR(50)", sql);
    }
    [Fact]
    public void Bytes()
    {
        var sqlType = SqlType.Create<byte[]>();
        var sql = _engine.Sql(sqlType);
        Assert.Equal("VARBINARY(50)", sql);
    }
}
