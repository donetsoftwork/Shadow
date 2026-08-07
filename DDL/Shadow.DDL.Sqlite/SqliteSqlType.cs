using Shadow.DDL.Components;
using Shadow.DDL.SqlTypes;
using Shadow.DDL.TypeParse;
using ShadowSql.Components;
using ShadowSql.Engines;
using ShadowSql.Engines.Sqlite;
using System;
using System.Collections;

namespace ShadowSql.Sqlite;

/// <summary>
/// SQLite类型
/// </summary>
public class SqliteSqlType : TypeManager
{
    #region 配置
    /// <summary>
    /// Integer
    /// </summary>
    public const string IntegerTypeName = "INTEGER";
    /// <summary>
    /// Real
    /// </summary>
    public const string RealTypeName = "REAL";
    /// <summary>
    /// Blob
    /// </summary>
    public const string BlobTypeName = "BLOB";
    /// <summary>
    /// Text
    /// </summary>
    public const string TextTypeName = "TEXT";

    private static readonly SqlRawType _integer = new(IntegerTypeName);
    private static readonly SqlRawType _real = new(RealTypeName);
    private static readonly SqlRawType _blob = new(BlobTypeName);
    private static readonly SqlRawType _text = new(TextTypeName);

    private static readonly TypeParser _integerParser = new(IntegerTypeName);
    private static readonly TypeParser _realParser = new(RealTypeName);
    private static readonly TypeParser _blobParser = new(BlobTypeName);
    private static readonly TypeParser _textParser = new(TextTypeName);
    /// <summary>
    /// Integer
    /// </summary>
    public static SqlRawType Integer 
        => _integer;
    /// <summary>
    /// Real
    /// </summary>
    public static SqlRawType Real 
        => _real;
    /// <summary>
    /// Blob
    /// </summary>
    public static SqlRawType Blob 
        => _blob;
    /// <summary>
    /// Text
    /// </summary>
    public static SqlRawType Text 
        => _text;
    #endregion
    #region Use
    /// <summary>
    /// bool
    /// </summary>
    protected override void UseBool()
        => Use<bool>(_integerParser);
    /// <summary>
    /// int
    /// </summary>
    protected override void UseInt()
        => Use<int>(_integerParser);
    /// <summary>
    /// long
    /// </summary>
    protected override void UseLong()
        => Use<long>(_integerParser);
    /// <summary>
    /// short
    /// </summary>
    protected override void UseShort()
        => Use<short>(_integerParser);
    /// <summary>
    /// byte
    /// </summary>
    protected override void UseByte()
        => Use<byte>(_integerParser);
    /// <summary>
    /// int
    /// </summary>
    protected override void UseUInt()
        => Use<uint>(_integerParser);
    /// <summary>
    /// long
    /// </summary>
    protected override void UseULong()
        => Use<ulong>(_integerParser);
    /// <summary>
    /// short
    /// </summary>
    protected override void UseUShort()
        => Use<ushort>(_integerParser);
    /// <summary>
    /// sbyte
    /// </summary>
    protected override void UseSByte()
        => Use<sbyte>(_integerParser);
    /// <summary>
    /// float
    /// </summary>
    protected override void UseFloat()
        => Use<float>(_realParser);
#if NET7_0_OR_GREATER
    /// <summary>
    /// DateOnly
    /// </summary>
    protected override void UseDateOnly()
        => Use<DateOnly>(_textParser);
    /// <summary>
    /// DateOnly
    /// </summary>
    protected override void UseTimeOnly()
        => Use<TimeOnly>(_textParser);
#endif
    /// <summary>
    /// DateTime
    /// </summary>
    protected override void UseDateTime()
        => Use<DateTime>(_textParser);
    /// <summary>
    /// TimeSpan 
    /// </summary>
    protected override void UseTimeSpan()
        => Use<TimeSpan>(_textParser);
    /// <summary>
    /// DateTimeOffset
    /// </summary>
    protected override void UseDateTimeOffset()
        => Use<DateTimeOffset>(_textParser);
    /// <summary>
    /// Guid
    /// </summary>
    protected override void UseGuid()
        => Use<Guid>(_textParser);    
    /// <summary>
    /// double 
    /// </summary>
    protected override void UseDouble()
        => Use<double>(_realParser);
    /// <summary>
    /// string
    /// </summary>
    protected override void UseString()
        => Use<string>(_textParser);
    /// <summary>
    /// char
    /// </summary>
    protected override void UseChar()
        => Use<char>(_textParser);
    /// <summary>
    /// char[]
    /// </summary>
    protected override void UseChars()
        => Use<char[]>(_textParser);
    /// <summary>
    /// byte[]
    /// </summary>
    protected override void UseBytes()
        => Use<byte[]>(_blobParser);
    /// <summary>
    /// decimal
    /// </summary>
    protected override void UseDecimal()
        => Use<decimal>(_realParser);
    #endregion

    /// <summary>
    /// MsSql引擎
    /// </summary>
    public static SqliteEngine Engine
        => Inner.Engine;

    class Inner
    {
        private static readonly DictionaryProvider _provider = new(new Hashtable() {
            { typeof(IDefineColumComponent), new DefineSqliteColumComponent() }
        });
        /// <summary>
        /// 单例
        /// </summary>
        public static readonly SqliteEngine Engine = new(new SqliteSelectComponent(), new SqlValueComponent("1", "0", "NULL"), _provider);
    }
}
