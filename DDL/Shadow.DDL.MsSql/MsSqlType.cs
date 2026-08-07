using Shadow.DDL.Components;
using Shadow.DDL.SqlTypes;
using ShadowSql.Components;
using ShadowSql.Engines.MsSql;
using System;
using System.Collections;

namespace ShadowSql.MsSql;

/// <summary>
/// MsSql类型
/// </summary>
public class MsSqlType(int defaultLength = 50)
    : TypeManager(defaultLength)
{
    #region 配置
    /// <summary>
    /// bit
    /// </summary>
    public const string BitTypeName = "BIT";
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
    /// float
    /// </summary>
    public const string FloatTypeName = "FLOAT";
    /// <summary>
    /// real
    /// </summary>
    public const string RealTypeName = "REAL";
    /// <summary>
    /// decimal
    /// </summary>
    public const string DecimalTypeName = "DECIMAL";
    /// <summary>
    /// money
    /// </summary>
    public const string MoneyTypeName = "MONEY";
    /// <summary>
    /// smallmoney
    /// </summary>
    public const string SmallMoneyTypeName = "SMALLMONEY";
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
    /// datetime2
    /// </summary>
    public const string Datetime2TypeName = "DATETIME2";
    /// <summary>
    /// datetimeoffset
    /// </summary>
    public const string DatetimeOffsetTypeName = "DATETIMEOFFSET";
    /// <summary>
    /// smalldatetime
    /// </summary>
    public const string SmallDatetimeTypeName = "SMALLDATETIME";
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
    /// binary
    /// </summary>
    public const string BinaryTypeName = "BINARY";
    /// <summary>
    /// varbinary
    /// </summary>
    public const string VarBinaryTypeName = "VARBINARY";
    /// <summary>
    /// uniqueidentifier
    /// </summary>
    public const string UniqueidentifierTypeName = "UNIQUEIDENTIFIER";

    private static readonly SqlRawType _bit = new(BitTypeName);
    private static readonly SqlRawType _int = new(IntTypeName);
    private static readonly SqlRawType _bigInt = new(BigIntTypeName);
    private static readonly SqlRawType _tinyInt = new(TinyIntTypeName);
    private static readonly SqlRawType _smallInt = new(SmallIntTypeName);
    private static readonly SqlRawType _float = new(FloatTypeName);
    private static readonly SqlRawType _real = new(RealTypeName);
    private static readonly SqlRawType _money = new(MoneyTypeName);
    private static readonly SqlRawType _smallMoney = new(SmallMoneyTypeName);
    private static readonly SqlRawType _date = new(DateTypeName);
    private static readonly SqlRawType _time = new(TimeTypeName);
    private static readonly SqlRawType _datetime = new(DatetimeTypeName);
    private static readonly SqlRawType _datetime2 = new(Datetime2TypeName);
    private static readonly SqlRawType _datetimeOffset = new(DatetimeOffsetTypeName);
    private static readonly SqlRawType _smallDatetime = new(SmallDatetimeTypeName);
    private static readonly SqlRawType _decimal = new(DecimalTypeName);
    private static readonly SqlRawType _uniqueidentifier = new(UniqueidentifierTypeName);

    /// <summary>
    /// bit
    /// </summary>
    public static SqlRawType Bit
        => _bit;
    /// <summary>
    /// int
    /// </summary>
    public static SqlRawType Int
        => _int;
    /// <summary>
    /// bigint
    /// </summary>
    public static SqlRawType BigInt
        => _bigInt;
    /// <summary>
    /// tinyint
    /// </summary>
    public static SqlRawType TinyInt
        => _tinyInt;
    /// <summary>
    /// smallint
    /// </summary>
    public static SqlRawType SmallInt
        => _smallInt;
    /// <summary>
    /// real
    /// </summary>
    public static SqlRawType Real
        => _real;
    /// <summary>
    /// money
    /// </summary>
    public static SqlRawType Money
        => _money;
    /// <summary>
    /// smallmoney
    /// </summary>
    public static SqlRawType SmallMoney
        => _smallMoney;
    /// <summary>
    /// date
    /// </summary>
    public static SqlRawType Date
        => _date;
    /// <summary>
    /// time
    /// </summary>
    public static SqlRawType Time
        => _time;
    /// <summary>
    /// datetime
    /// </summary>
    public static SqlRawType Datetime
        => _datetime;
    /// <summary>
    /// datetime2
    /// </summary>
    public static SqlRawType Datetime2
        => _datetime2;
    /// <summary>
    /// datetimeoffset
    /// </summary>
    public static SqlRawType DatetimeOffset
        => _datetimeOffset;
    /// <summary>
    /// smalldatetime
    /// </summary>
    public static SqlRawType SmallDatetime
        => _smallDatetime;
    /// <summary>
    /// uniqueidentifier
    /// </summary>
    public static SqlRawType Uniqueidentifier
        => _uniqueidentifier;
    #endregion
    #region Float
    /// <summary>
    /// float
    /// </summary>
    /// <param name="precision"></param>
    /// <returns></returns>
    public static SqlRawType Float(int precision)
        => Create(FloatTypeName, precision);
    /// <summary>
    /// float
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Float()
        => _float;
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
        => Use<int>(IntTypeName);
    /// <summary>
    /// long
    /// </summary>
    protected override void UseLong()
        => Use<long>(BigIntTypeName);
    /// <summary>
    /// short
    /// </summary>
    protected override void UseShort()
        => Use<short>(SmallIntTypeName);
    /// <summary>
    /// byte
    /// </summary>
    protected override void UseByte()
        => Use<byte>(TinyIntTypeName);
    /// <summary>
    /// int
    /// </summary>
    protected override void UseUInt()
        => Use<uint>(IntTypeName);
    /// <summary>
    /// long
    /// </summary>
    protected override void UseULong()
        => Use<ulong>(BigIntTypeName);
    /// <summary>
    /// short
    /// </summary>
    protected override void UseUShort()
        => Use<ushort>(SmallIntTypeName);
    /// <summary>
    /// sbyte
    /// </summary>
    protected override void UseSByte()
        => Use<sbyte>(TinyIntTypeName);
    /// <summary>
    /// float
    /// </summary>
    protected override void UseFloat()
        => Use<float>(RealTypeName);
    /// <summary>
    /// double 
    /// </summary>
    protected override void UseDouble()
        => UsePrecision<double>(FloatTypeName);
    /// <summary>
    /// decimal
    /// </summary>
    protected override void UseDecimal()
        => UsePrecision<decimal>(DecimalTypeName);
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
        => Use<DateTime>(Datetime2TypeName);
    /// <summary>
    /// TimeSpan 
    /// </summary>
    protected override void UseTimeSpan()
        => Use<TimeSpan>(TimeTypeName);
    /// <summary>
    /// DateTimeOffset
    /// </summary>
    protected override void UseDateTimeOffset()
        => Use<DateTimeOffset>(DatetimeOffsetTypeName);
    /// <summary>
    /// Guid
    /// </summary>
    protected override void UseGuid()
        => Use<Guid>(UniqueidentifierTypeName);
    /// <summary>
    /// string
    /// </summary>
    protected override void UseString()
        => UseLength<string>(VarCharTypeName);
    /// <summary>
    /// char
    /// </summary>
    protected override void UseChar()
        => UseLength<char>(VarCharTypeName);
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
    #endregion

    /// <summary>
    /// MsSql引擎
    /// </summary>
    public static MsSqlEngine Engine
        => Inner.Engine;

    class Inner
    {
        private static readonly DictionaryProvider _provider = new(new Hashtable() {
            { typeof(IDefineColumComponent), new DefineMsSqlColumComponent() }
        });
        /// <summary>
        /// 单例
        /// </summary>
        public static readonly MsSqlEngine Engine = new(new MsSqlSelectComponent(), new SqlValueComponent("1", "0", "NULL"), _provider);
    }
}