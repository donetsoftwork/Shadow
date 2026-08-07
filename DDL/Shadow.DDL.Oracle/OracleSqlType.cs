using Shadow.DDL.Components;
using Shadow.DDL.SqlTypes;
using System;

namespace ShadowSql.Oracle;

/// <summary>
/// MsSql类型
/// </summary>
public class OracleSqlType(int defaultLength = 50)
    : TypeManager(defaultLength)
{
    #region 配置
    /// <summary>
    /// bool
    /// </summary>
    public const string BooleanTypeName = "BOOLEAN";
    /// <summary>
    /// number
    /// </summary>
    public const string NumberTypeName = "NUMBER";
    /// <summary>
    /// float
    /// </summary>
    public const string FloatTypeName = "FLOAT";
    /// <summary>
    /// BINARY_FLOAT
    /// </summary>
    public const string BinaryFloatTypeName = "BINARY_FLOAT";
    /// <summary>
    /// BINARY_DOUBLE
    /// </summary>
    public const string BinaryDoubleTypeName‌ = "BINARY_DOUBLE‌";
    /// <summary>
    /// date
    /// </summary>
    public const string DateTypeName = "DATE";
    /// <summary>
    /// datetimeoffset
    /// </summary>
    public const string TimestampTypeName = "TIMESTAMP";
    /// <summary>
    /// char
    /// </summary>
    public const string CharTypeName = "CHAR";
    /// <summary>
    /// varchar
    /// </summary>
    public const string VarChar2TypeName = "VARCHAR2";
    /// <summary>
    /// nchar
    /// </summary>
    public const string NCharTypeName = "NCHAR";
    /// <summary>
    /// nvarchar
    /// </summary>
    public const string NVarChar2TypeName = "NVARCHAR2";
    /// <summary>
    /// binary
    /// </summary>
    public const string RawTypeName = "RAW";
    /// <summary>
    /// varbinary
    /// </summary>
    public const string BlobTypeName = "BLOB";

    private static readonly SqlRawType _boolean = new(BooleanTypeName);
    private static readonly SqlRawType _number = new(NumberTypeName);
    private static readonly SqlRawType _float = new(FloatTypeName);
    private static readonly SqlRawType _binaryFloat = new(BinaryFloatTypeName);
    private static readonly SqlRawType _binaryDouble = new(BinaryDoubleTypeName);
    private static readonly SqlRawType _date = new(DateTypeName);
    private static readonly SqlRawType _timestamp = new(TimestampTypeName);
    private static readonly SqlRawType _blob = new(BlobTypeName);

    /// <summary>
    /// bool
    /// </summary>
    public static SqlRawType Boolean
        => _boolean;
    /// <summary>
    /// binaryFloat
    /// </summary>
    /// <returns></returns>
    public static SqlRawType BinaryFloat
        => _binaryFloat;
    /// <summary>
    /// binaryDouble
    /// </summary>
    /// <returns></returns>
    public static SqlRawType BinaryDouble
        => _binaryDouble;
    /// <summary>
    /// date
    /// </summary>
    public static SqlRawType Date
        => _date;
    /// <summary>
    /// blob
    /// </summary>
    public static SqlRawType Blob
        => _blob;
    #endregion
    #region Number
    /// <summary>
    /// Number
    /// </summary>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static SqlRawType Number(int precision, int scale)
        => Create(NumberTypeName, precision, scale);
    /// <summary>
    /// Number
    /// </summary>
    /// <param name="precision"></param>
    /// <returns></returns>
    public static SqlRawType Number(int precision)
        => Create(NumberTypeName, precision);
    /// <summary>
    /// Number
    /// </summary>
    /// <returns></returns>
    public static SqlRawType Number()
        => _number;
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
    public static SqlRawType VarChar2(int length)
        => Create(VarChar2TypeName, length);
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
    public static SqlRawType NVarChar2(int length)
        => Create(NVarChar2TypeName, length);
    /// <summary>
    /// binary
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType Raw(int length)
        => Create(RawTypeName, length);
    /// <summary>
    /// varbinary
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType VarBinary(int length)
        => Create(BlobTypeName, length);
    #region Use
    /// <summary>
    /// bool
    /// </summary>
    protected override void UseBool()
        => Use<bool>("NUMBER(1)");
    /// <summary>
    /// int
    /// </summary>
    protected override void UseInt()
        => Use<int>("NUMBER(6)");
    /// <summary>
    /// long
    /// </summary>
    protected override void UseLong()
        => Use<long>("NUMBER(11)");
    /// <summary>
    /// short
    /// </summary>
    protected override void UseShort()
        => Use<short>("NUMBER(5)");
    /// <summary>
    /// byte
    /// </summary>
    protected override void UseByte()
        => Use<byte>("NUMBER(2)");
    /// <summary>
    /// int
    /// </summary>
    protected override void UseUInt()
        => Use<uint>("NUMBER(7)");
    /// <summary>
    /// long
    /// </summary>
    protected override void UseULong()
        => Use<ulong>("NUMBER(12)");
    /// <summary>
    /// short
    /// </summary>
    protected override void UseUShort()
        => Use<ushort>("NUMBER(6)");
    /// <summary>
    /// sbyte
    /// </summary>
    protected override void UseSByte()
        => Use<sbyte>("NUMBER(2)");
    /// <summary>
    /// float
    /// </summary>
    protected override void UseFloat()
        => Use<float>(BinaryFloatTypeName);
    /// <summary>
    /// double 
    /// </summary>
    protected override void UseDouble()
        => Use<double>(BinaryDoubleTypeName);
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
        => Use<TimeOnly>(TimestampTypeName);
#endif
    /// <summary>
    /// DateTime
    /// </summary>
    protected override void UseDateTime()
        => UsePrecision<DateTime>(TimestampTypeName);
    /// <summary>
    /// TimeSpan 
    /// </summary>
    protected override void UseTimeSpan()
        => Use<TimeSpan>("INTERVAL DAY TO SECOND");
    /// <summary>
    /// DateTimeOffset
    /// </summary>
    protected override void UseDateTimeOffset()
        => Use<DateTimeOffset>(TimestampTypeName);
    /// <summary>
    /// Guid
    /// </summary>
    protected override void UseGuid()
        => Use<Guid>("VARCHAR2(64)");
    /// <summary>
    /// string
    /// </summary>
    protected override void UseString()
        => UseLength<string>(VarChar2TypeName);
    /// <summary>
    /// char
    /// </summary>
    protected override void UseChar()
        => Use<char>(CharTypeName);
    /// <summary>
    /// char[]
    /// </summary>
    protected override void UseChars()
        => UseLength<char[]>(VarChar2TypeName);
    /// <summary>
    /// byte[]
    /// </summary>
    protected override void UseBytes()
        => UseLength<byte[]>(RawTypeName);
    /// <summary>
    /// decimal
    /// </summary>
    protected override void UseDecimal()
        => UsePrecision<decimal>(NumberTypeName);
    #endregion
}
