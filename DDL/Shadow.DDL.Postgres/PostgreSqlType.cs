using Shadow.DDL.Components;
using Shadow.DDL.SqlTypes;
using System;

namespace ShadowSql.Postgres;

/// <summary>
/// MsSql类型
/// </summary>
public class PostgreSqlType(int defaultLength = 50)
    : TypeManager(defaultLength)
{
    #region 配置
    /// <summary>
    /// bool
    /// </summary>
    public const string BooleanTypeName = "BOOLEAN";
    /// <summary>
    /// integer
    /// </summary>
    public const string IntegerTypeName = "INTEGER";
    /// <summary>
    /// bigint
    /// </summary>
    public const string BigIntTypeName = "BIGINT";
    /// <summary>
    /// smallint
    /// </summary>
    public const string SmallIntTypeName = "SMALLINT";
    /// <summary>
    /// serial
    /// </summary>
    public const string SerialTypeName = "SERIAL";
    /// <summary>
    /// smallserial
    /// </summary>
    public const string SmallSerialTypeName = "SMALLSERIAL";
    /// <summary>
    /// bigserial
    /// </summary>
    public const string BigSerialTypeName = "BIGSERIAL";
    /// <summary>
    /// double precision
    /// </summary>
    public const string DoubleTypeName‌ = "DOUBLE PRECISION‌";
    /// <summary>
    /// float
    /// </summary>
    public const string FloatTypeName = "FLOAT";
    /// <summary>
    /// real
    /// </summary>
    public const string RealTypeName = "REAL";
    /// <summary>
    /// numeric
    /// </summary>
    public const string NumericTypeName = "NUMERIC";
    /// <summary>
    /// money
    /// </summary>
    public const string MoneyTypeName = "MONEY";
    /// <summary>
    /// date
    /// </summary>
    public const string DateTypeName = "DATE";
    /// <summary>
    /// time
    /// </summary>
    public const string TimeTypeName = "TIME";
    /// <summary>
    /// time
    /// </summary>
    public const string TimezTypeName = "TIMEZ";
    /// <summary>
    /// timestamp
    /// </summary>
    public const string TimestampTypeName = "TIMESTAMP";
    /// <summary>
    /// timestampz
    /// </summary>
    public const string TimestampzTypeName = "TIMESTAMPZ";
    /// <summary>
    /// text
    /// </summary>
    public const string TextTypeName = "text";
    /// <summary>
    /// character
    /// </summary>
    public const string CharacterTypeName = "CHARACTER";
    /// <summary>
    /// varchar
    /// </summary>
    public const string VarCharTypeName = "VARCHAR";
    /// <summary>
    /// bit
    /// </summary>
    public const string BitTypeName = "BIT";
    /// <summary>
    /// varbit
    /// </summary>
    public const string VarBitTypeName = "VARBIT";
    /// <summary>
    /// bytea
    /// </summary>
    public const string ByteaTypeName = "BYTEA";
    /// <summary>
    /// uuid
    /// </summary>
    public const string UuidTypeName = "UUID";

    private static readonly SqlRawType _boolean = new(BooleanTypeName);
    private static readonly SqlRawType _integer = new(IntegerTypeName);
    private static readonly SqlRawType _bigInt = new(BigIntTypeName);
    private static readonly SqlRawType _smallInt = new(SmallIntTypeName);
    private static readonly SqlRawType _serial = new(SerialTypeName);
    private static readonly SqlRawType _smallSerial = new(SmallSerialTypeName);
    private static readonly SqlRawType _bigSerial = new(BigSerialTypeName);
    private static readonly SqlRawType _float = new(FloatTypeName);
    private static readonly SqlRawType _double = new(DoubleTypeName);
    private static readonly SqlRawType _real = new(RealTypeName);
    private static readonly SqlRawType _numeric = new(NumericTypeName);
    private static readonly SqlRawType _money = new(MoneyTypeName);
    private static readonly SqlRawType _date = new(DateTypeName);
    private static readonly SqlRawType _time = new(TimeTypeName);
    private static readonly SqlRawType _timez = new(TimezTypeName);
    private static readonly SqlRawType _timestamp = new(TimestampTypeName);
    private static readonly SqlRawType _timestampz = new(TimestampzTypeName);
    private static readonly SqlRawType _text = new(TextTypeName);
    private static readonly SqlRawType _bit = new(BitTypeName);
    private static readonly SqlRawType _bytea = new(ByteaTypeName);
    private static readonly SqlRawType _uuid = new(UuidTypeName);    

    #region Boolean
    /// <summary>
    /// boolean
    /// </summary>
    public static SqlRawType Boolean
        => _boolean;
    /// <summary>
    /// bool
    /// </summary>
    public static SqlRawType Bool
        => _boolean;
    #endregion
    #region Integer
    /// <summary>
    /// int
    /// </summary>
    public static SqlRawType Integer
        => _integer;
    /// <summary>
    /// int
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Int
        => _integer;
    /// <summary>
    /// int4
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Int4
        => _integer;
    #endregion
    #region BigInt
    /// <summary>
    /// bigint
    /// </summary>
    /// <returns></returns>
    public static SqlRawType BigInt
        => _bigInt;
    /// <summary>
    /// int8
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Int8
        => _bigInt;
    #endregion
    #region SmallInt
    /// <summary>
    /// smallint
    /// </summary>
    public static SqlRawType SmallInt
        => _smallInt;
    /// <summary>
    /// smallint
    /// </summary>
    public static SqlRawType Int2
        => _smallInt;
    /// <summary>
    /// serial
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Serial
        => _serial;
    /// <summary>
    /// serial
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Serial4
        => _serial;
    /// <summary>
    /// serial
    /// </summary>
    /// <returns></returns>
    public static SqlRawType SmallSerial
        => _smallSerial;
    /// <summary>
    /// serial
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Serial2
        => _smallSerial;
    /// <summary>
    /// serial
    /// </summary>
    /// <returns></returns>
    public static SqlRawType BigSerial
        => _bigSerial;
    /// <summary>
    /// serial
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Serial8
        => _bigSerial;
    #endregion
    #region Double
    /// <summary>
    /// double
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Double
        => _double;
    /// <summary>
    /// float
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Float
        => _float;
    /// <summary>
    /// float
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Float8
        => _float;
    #endregion
    #region Real
    /// <summary>
    /// real
    /// </summary>
    public static SqlRawType Real
        => _real;
    /// <summary>
    /// real
    /// </summary>
    public static SqlRawType Float4
        => _real;
    #endregion
    /// <summary>
    /// money
    /// </summary>
    public static SqlRawType Money
        => _money;
    /// <summary>
    /// date
    /// </summary>
    public static SqlRawType Date
        => _date;
    /// <summary>
    /// text
    /// </summary>
    public static SqlRawType Text
        => _text;
    /// <summary>
    /// bytea
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Bytea
        => _bytea;
    /// <summary>
    /// uuid
    /// </summary>
    public static SqlRawType Uuid
        => _uuid;
    #endregion
    #region Numeric
    /// <summary>
    /// Numeric
    /// </summary>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static SqlRawType Numeric(int precision, int scale)
        => Create(NumericTypeName, precision, scale);
    /// <summary>
    /// Numeric
    /// </summary>
    /// <param name="precision"></param>
    /// <returns></returns>
    public static SqlRawType Numeric(int precision)
        => Create(NumericTypeName, precision);
    /// <summary>
    /// Numeric
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Numeric()
        => _numeric;
    #endregion
    #region Decimal
    /// <summary>
    /// Decimal
    /// </summary>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static SqlRawType Decimal(int precision, int scale)
        => Numeric(precision, scale);
    /// <summary>
    /// Decimal
    /// </summary>
    /// <param name="precision"></param>
    /// <returns></returns>
    public static SqlRawType Decimal(int precision)
        => Numeric(precision);
    /// <summary>
    /// Decimal
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Decimal()
        => _numeric;
    #endregion
    #region Bit
    /// <summary>
    /// bit
    /// </summary>
    public static SqlRawType Bit(int length)
        => Create(BitTypeName, length);
    /// <summary>
    /// bit
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Bit()
        => _bit;
    #endregion
    /// <summary>
    /// varbit
    /// </summary>
    public static SqlRawType VarBit(int length)
        => Create(VarBitTypeName, length);
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
    #region Time
    /// <summary>
    /// time
    /// </summary>
    public static SqlRawType Timez(int precision)
        => Create(TimezTypeName, precision);
    /// <summary>
    /// time
    /// </summary>
    public static SqlRawType Timez()
        => _timez;
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
    #region Timestampz
    /// <summary>
    /// timestamp
    /// </summary>
    public static SqlRawType Timestampz(int precision)
        => Create(TimestampzTypeName, precision);
    /// <summary>
    /// timestamp
    /// </summary>
    public static SqlRawType Timestampz()
        => _timestampz;
    #endregion
    /// <summary>
    /// character
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType Character(int length)
        => Create(CharacterTypeName, length);
    /// <summary>
    /// char
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType Char(int length)
        => Character(length);
    /// <summary>
    /// varchar
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType VarChar(int length)
        => Create(VarCharTypeName, length);
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
        => Use<int>(IntegerTypeName);
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
        => Use<byte>(SmallIntTypeName);
    /// <summary>
    /// int
    /// </summary>
    protected override void UseUInt()
        => Use<uint>(IntegerTypeName);
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
        => Use<sbyte>(SmallIntTypeName);
    /// <summary>
    /// float
    /// </summary>
    protected override void UseFloat()
        => Use<float>(FloatTypeName);
    /// <summary>
    /// double 
    /// </summary>
    protected override void UseDouble()
        => Use<double>(DoubleTypeName);
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
        => Use<DateTime>(TimestampTypeName);
    /// <summary>
    /// TimeSpan 
    /// </summary>
    protected override void UseTimeSpan()
        => Use<TimeSpan>(TimeTypeName);
    /// <summary>
    /// DateTimeOffset
    /// </summary>
    protected override void UseDateTimeOffset()
        => Use<DateTimeOffset>(TimestampTypeName);
    /// <summary>
    /// Guid
    /// </summary>
    protected override void UseGuid()
        => Use<Guid>(UuidTypeName);
    /// <summary>
    /// string
    /// </summary>
    protected override void UseString()
        => UseLength<string>(VarCharTypeName);
    /// <summary>
    /// char
    /// </summary>
    protected override void UseChar()
        => Use<char>(CharacterTypeName);
    /// <summary>
    /// char[]
    /// </summary>
    protected override void UseChars()
        => UseLength<char[]>(VarCharTypeName);
    /// <summary>
    /// byte[]
    /// </summary>
    protected override void UseBytes()
        => Use<byte[]>(ByteaTypeName);
    /// <summary>
    /// decimal
    /// </summary>
    protected override void UseDecimal()
        => UsePrecision<decimal>(NumericTypeName);
    #endregion
}
