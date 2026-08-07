using Shadow.DDL.Components;
using Shadow.DDL.SqlTypes;
using Shadow.DDL.TypeParse;
using System;
using System.Runtime.CompilerServices;

namespace ShadowSql.MySql;

/// <summary>
/// MsSql类型
/// </summary>
public class MySqlType(int defaultLength = 50)
    : TypeManager(defaultLength)
{
    #region 配置
    /// <summary>
    /// bool
    /// </summary>
    public const string BoolTypeName = "BOOL";
    /// <summary>
    /// int
    /// </summary>
    public const string IntTypeName = "INT";
    /// <summary>
    /// bigint
    /// </summary>
    public const string BigIntTypeName = "BIGINT";
    /// <summary>
    /// tinyint
    /// </summary>
    public const string TinyIntTypeName = "TINYINT";
    /// <summary>
    /// smallint
    /// </summary>
    public const string SmallIntTypeName = "SMALLINT";
    /// <summary>
    /// mediumint
    /// </summary>
    public const string MediumIntTypeName = "MediumInt";
    /// <summary>
    /// float
    /// </summary>
    public const string FloatTypeName = "FLOAT";
    /// <summary>
    /// double
    /// </summary>
    public const string DoubleTypeName‌ = "DOUBLE‌";
    /// <summary>
    /// decimal
    /// </summary>
    public const string DecimalTypeName = "DECIMAL";
    /// <summary>
    /// year
    /// </summary>
    public const string YearTypeName = "YEAR";
    /// <summary>
    /// date
    /// </summary>
    public const string DateTypeName = "DATE";
    /// <summary>
    /// time
    /// </summary>
    public const string TimeTypeName = "TIME";
    /// <summary>
    /// datetime
    /// </summary>
    public const string DatetimeTypeName = "DATETIME";
    /// <summary>
    /// timestamp
    /// </summary>
    public const string TimestampTypeName = "TIMESTAMP";
    /// <summary>
    /// char
    /// </summary>
    public const string CharTypeName = "CHAR";
    /// <summary>
    /// varchar
    /// </summary>
    public const string VarCharTypeName = "VARCHAR";
    /// <summary>
    /// nchar
    /// </summary>
    public const string NCharTypeName = "NCHAR";
    /// <summary>
    /// nvarchar
    /// </summary>
    public const string NVarCharTypeName = "NVARCHAR";
    /// <summary>
    /// bit
    /// </summary>
    public const string BitTypeName = "BIT";
    /// <summary>
    /// binary
    /// </summary>
    public const string BinaryTypeName = "BINARY";
    /// <summary>
    /// varbinary
    /// </summary>
    public const string VarBinaryTypeName = "VARBINARY";
    /// <summary>
    /// 无符号后缀
    /// </summary>
    public const string UnsignedSuffix = " UNSIGNED";

    private static readonly SqlRawType _bool = new(BoolTypeName);
    private static readonly SqlRawType _int = new(IntTypeName);
    private static readonly SqlRawType _bigInt = new(BigIntTypeName);
    private static readonly SqlRawType _tinyInt = new(TinyIntTypeName);
    private static readonly SqlRawType _smallInt = new(SmallIntTypeName);
    private static readonly SqlRawType _mediumInt = new(MediumIntTypeName);
    private static readonly SqlRawType _float = new(FloatTypeName);
    private static readonly SqlRawType _double = new(DoubleTypeName);
    private static readonly SqlRawType _decimal = new(DecimalTypeName);
    private static readonly SqlRawType _year = new(YearTypeName);
    private static readonly SqlRawType _date = new(DateTypeName);
    private static readonly SqlRawType _time = new(TimeTypeName);
    private static readonly SqlRawType _datetime = new(DatetimeTypeName);
    private static readonly SqlRawType _timestamp = new(TimestampTypeName);
    private static readonly SqlRawType _bit = new(BitTypeName);

    /// <summary>
    /// bool
    /// </summary>
    public static SqlRawType Bool
        => _bool;
    /// <summary>
    /// bool
    /// </summary>
    public static SqlRawType Boolean
        => _bool;
    /// <summary>
    /// date
    /// </summary>
    public static SqlRawType Date
        => _date;
    #endregion
    /// <summary>
    /// int
    /// </summary>
    public static SqlRawType Int(int max)
        => Create(IntTypeName, max);
    /// <summary>
    /// int
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Int()
        => _int;
    /// <summary>
    /// int
    /// </summary>
    public static SqlRawType Integer(int max)
        => Int(max);
    /// <summary>
    /// int
    /// </summary>
    public static SqlRawType Integer()
        => _int;
    /// <summary>
    /// bigint
    /// </summary>
    public static SqlRawType BigInt(int max)
        => Create(BigIntTypeName, max);
    /// <summary>
    /// bigint
    /// </summary>
    /// <returns></returns>
    public static SqlRawType BigInt()
        => _bigInt;
    /// <summary>
    /// tinyint
    /// </summary>
    public static SqlRawType TinyInt(int max)
        => Create(TinyIntTypeName, max);
    /// <summary>
    /// tinyint
    /// </summary>
    public static SqlRawType TinyInt()
        => _tinyInt;
    /// <summary>
    /// smallint
    /// </summary>
    public static SqlRawType SmallInt(int max)
        => Create(SmallIntTypeName, max);
    /// <summary>
    /// smallint
    /// </summary>
    public static SqlRawType SmallInt()
        => _smallInt;
    /// <summary>
    /// mediumint
    /// </summary>
    public static SqlRawType MediumInt(int max)
        => Create(MediumIntTypeName, max);
    /// <summary>
    /// mediumint
    /// </summary>
    public static SqlRawType MediumInt()
        => _mediumInt;
    #region Float
    /// <summary>
    /// float
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public static SqlRawType Float(int  max)
        => Create(FloatTypeName, max);
    /// <summary>
    /// float
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Float()
        => _float;
    #endregion
    #region Double
    /// <summary>
    /// double
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public static SqlRawType Double(int max)
        => Create(DoubleTypeName, max);
    /// <summary>
    /// double
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Double()
        => _double;
    #endregion
    #region Decimal
    /// <summary>
    /// Decimal
    /// </summary>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static SqlRawType Decimal(int precision, int scale)
        => Create(DecimalTypeName, precision, scale);
    /// <summary>
    /// Decimal
    /// </summary>
    /// <param name="precision"></param>
    /// <returns></returns>
    public static SqlRawType Decimal(int precision)
        => Create(DecimalTypeName, precision);
    /// <summary>
    /// Decimal
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Decimal()
        => _decimal;
    #endregion
    #region Dec
    /// <summary>
    /// Dec
    /// </summary>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static SqlRawType Dec(int precision, int scale)
        => Decimal(precision, scale);
    /// <summary>
    /// Dec
    /// </summary>
    /// <param name="precision"></param>
    /// <returns></returns>
    public static SqlRawType Dec(int precision)
        => Decimal(precision);
    /// <summary>
    /// Dec
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Dec()
        => _decimal;
    /// <summary>
    /// bit
    /// </summary>
    public static SqlRawType Bit(int max)
        => Create(BitTypeName, max);
    /// <summary>
    /// bit
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Bit()
        => _bit;
    #endregion
    #region Numeric
    /// <summary>
    /// Numeric
    /// </summary>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static SqlRawType Numeric(int precision, int scale)
        => Decimal(precision, scale);
    /// <summary>
    /// Numeric
    /// </summary>
    /// <param name="precision"></param>
    /// <returns></returns>
    public static SqlRawType Numeric(int precision)
        => Decimal(precision);
    /// <summary>
    /// Numeric
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Numeric()
        => _decimal;
    #endregion
    #region Fixed
    /// <summary>
    /// Fixed
    /// </summary>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static SqlRawType Fixed(int precision, int scale)
        => Decimal(precision, scale);
    /// <summary>
    /// Fixed
    /// </summary>
    /// <param name="precision"></param>
    /// <returns></returns>
    public static SqlRawType Fixed(int precision)
        => Decimal(precision);
    /// <summary>
    /// Fixed
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Fixed()
        => _decimal;
    #endregion
    #region Year
    /// <summary>
    /// year
    /// </summary>
    public static SqlRawType Year(int precision)
        => Create(YearTypeName, precision);
    /// <summary>
    /// year
    /// </summary>
    public static SqlRawType Year()
        => _year;
    #endregion
    #region Time
    /// <summary>
    /// time
    /// </summary>
    public static SqlRawType Time(int precision)
        => Create(TimeTypeName, precision);
    /// <summary>
    /// time
    /// </summary>
    public static SqlRawType Time()
        => _time;
    #endregion
    #region Datetime
    /// <summary>
    /// datetime
    /// </summary>
    public static SqlRawType Datetime(int precision)
        => Create(DatetimeTypeName, precision);
    /// <summary>
    /// datetime
    /// </summary>
    public static SqlRawType Datetime()
        => _datetime;
    #endregion
    #region Timestamp
    /// <summary>
    /// timestamp
    /// </summary>
    public static SqlRawType Timestamp(int precision)
        => Create(TimestampTypeName, precision);
    /// <summary>
    /// timestamp
    /// </summary>
    public static SqlRawType Timestamp()
        => _timestamp;
    #endregion
    /// <summary>
    /// char
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType Char(int length)
        => Create(CharTypeName, length);
    /// <summary>
    /// varchar
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType VarChar(int length)
        => Create(VarCharTypeName, length);
    /// <summary>
    /// nchar
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType NChar(int length)
        => Create(NCharTypeName, length);
    /// <summary>
    /// nvarchar
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType NVarChar(int length)
        => Create(NVarCharTypeName, length);
    /// <summary>
    /// binary
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType Binary(int length)
        => Create(BinaryTypeName, length);
    /// <summary>
    /// varbinary
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType VarBinary(int length)
        => Create(VarBinaryTypeName, length);
    #region Use
    /// <summary>
    /// bool
    /// </summary>
    protected override void UseBool()
        => Use<bool>(BitTypeName);
    /// <summary>
    /// int
    /// </summary>
    protected override void UseInt()
        => UsePrecision<int>(IntTypeName);
    /// <summary>
    /// long
    /// </summary>
    protected override void UseLong()
        => UsePrecision<long>(BigIntTypeName);
    /// <summary>
    /// short
    /// </summary>
    protected override void UseShort()
        => UsePrecision<short>(SmallIntTypeName);
    /// <summary>
    /// byte
    /// </summary>
    protected override void UseByte()
        => UsePrecision<byte>(TinyIntTypeName);
    /// <summary>
    /// int
    /// </summary>
    protected override void UseUInt()
        => UseUnsigned<uint>(IntTypeName);
    /// <summary>
    /// long
    /// </summary>
    protected override void UseULong()
        => UseUnsigned<ulong>(BigIntTypeName);
    /// <summary>
    /// short
    /// </summary>
    protected override void UseUShort()
        => UseUnsigned<ushort>(SmallIntTypeName);
    /// <summary>
    /// sbyte
    /// </summary>
    protected override void UseSByte()
        => UsePrecision<sbyte>(TinyIntTypeName);
    /// <summary>
    /// float
    /// </summary>
    protected override void UseFloat()
        => UsePrecision<float>(FloatTypeName);
    /// <summary>
    /// double 
    /// </summary>
    protected override void UseDouble()
        => UsePrecision<double>(DoubleTypeName);
#if NET7_0_OR_GREATER
    /// <summary>
    /// DateOnly
    /// </summary>
    protected override void UseDateOnly()
        => Use<DateOnly>(DateTypeName);
    /// <summary>
    /// DateOnly
    /// </summary>
    protected override void UseTimeOnly()
        => Use<TimeOnly>(TimeTypeName);
#endif
    /// <summary>
    /// DateTime
    /// </summary>
    protected override void UseDateTime()
        => Use<DateTime>(DatetimeTypeName);
    /// <summary>
    /// TimeSpan 
    /// </summary>
    protected override void UseTimeSpan()
        => Use<TimeSpan>(TimeTypeName);
    /// <summary>
    /// DateTimeOffset
    /// </summary>
    protected override void UseDateTimeOffset()
        => Use<DateTimeOffset>(DatetimeTypeName);
    /// <summary>
    /// Guid
    /// </summary>
    protected override void UseGuid()
        => Use<Guid>("CHAR(36)");
    /// <summary>
    /// string
    /// </summary>
    protected override void UseString()
        => UseLength<string>(VarCharTypeName);
    /// <summary>
    /// char
    /// </summary>
    protected override void UseChar()
        => Use<char>(CharTypeName);
    /// <summary>
    /// char[]
    /// </summary>
    protected override void UseChars()
        => UseLength<char[]>(VarCharTypeName);
    /// <summary>
    /// byte[]
    /// </summary>
    protected override void UseBytes()
        => UseLength<byte[]>(VarBinaryTypeName);
    /// <summary>
    /// decimal
    /// </summary>
    protected override void UseDecimal()
        => UsePrecision<decimal>(DecimalTypeName);
    #endregion
    /// <summary>
    /// 配置无符号解析器(只有mysql支持无符号类型)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dbType"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UseUnsigned<T>(string dbType)
        => Use<T>(new SuffixTypeParser(new PrecisionTypeParser(dbType), UnsignedSuffix));
}
