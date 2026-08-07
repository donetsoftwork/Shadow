using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Shadow.DDL.SqlTypes;
using Shadow.DDL.TypeParse;

namespace Shadow.DDL.Components;

/// <summary>
/// 类型管理
/// </summary>
public abstract class TypeManager
{
    #region 配置
    private readonly int _defaultLength;
    private readonly Dictionary<Type, ITypeParser> _dic = [];
    /// <summary>
    /// 默认字段长度
    /// </summary>
    public int DefaultLength 
        => _defaultLength;
    #endregion
    /// <summary>
    /// 获取类型解析器
    /// </summary>
    /// <param name="originalType"></param>
    /// <returns></returns>
    public ITypeParser? GetTypeParser(Type originalType)
    {
        _dic.TryGetValue(originalType, out ITypeParser? typeParser);
        return typeParser;
    }
    
    /// <summary>
    /// 类型管理
    /// </summary>
    public TypeManager(int defaultLength = 50)
    {
        _defaultLength = defaultLength;
        UseBool();
        UseInt();
        UseLong();
        UseShort();
        UseUInt();
        UseULong();
        UseUShort();
        UseByte();
        UseSByte();
        UseDouble();
        UseFloat();
        UseDecimal();
#if NET7_0_OR_GREATER
        UseDateOnly();
        UseTimeOnly();
#endif
        UseDateTime();
        UseDateTimeOffset();
        UseTimeSpan();
        UseChar();
        UseString();
        UseChars();
        UseGuid();
        UseBytes();
    }
    #region Use
    /// <summary>
    /// bool
    /// </summary>
    protected abstract void UseBool();
    /// <summary>
    /// int
    /// </summary>
    protected abstract void UseInt();
    /// <summary>
    /// long
    /// </summary>
    protected abstract void UseLong();
    /// <summary>
    /// short
    /// </summary>
    protected abstract void UseShort();
    /// <summary>
    /// byte
    /// </summary>
    protected abstract void UseByte();
    /// <summary>
    /// int
    /// </summary>
    protected abstract void UseUInt();
    /// <summary>
    /// long
    /// </summary>
    protected abstract void UseULong();
    /// <summary>
    /// short
    /// </summary>
    protected abstract void UseUShort();
    /// <summary>
    /// sbyte
    /// </summary>
    protected abstract void UseSByte();
    /// <summary>
    /// float
    /// </summary>
    protected abstract void UseFloat();
    /// <summary>
    /// double 
    /// </summary>
    protected abstract void UseDouble();
#if NET7_0_OR_GREATER
    /// <summary>
    /// DateOnly
    /// </summary>
    protected abstract void UseDateOnly();
    /// <summary>
    /// DateOnly
    /// </summary>
    protected abstract void UseTimeOnly();
#endif
    /// <summary>
    /// DateTime
    /// </summary>
    protected abstract void UseDateTime();
    /// <summary>
    /// TimeSpan 
    /// </summary>
    protected abstract void UseTimeSpan();
    /// <summary>
    /// DateTimeOffset
    /// </summary>
    protected abstract void UseDateTimeOffset();
    /// <summary>
    /// Guid
    /// </summary>
    protected abstract void UseGuid();
    #endregion
    /// <summary>
    /// string
    /// </summary>
    protected abstract void UseString();
    /// <summary>
    /// char
    /// </summary>
    protected abstract void UseChar();
    /// <summary>
    /// char[]
    /// </summary>
    protected abstract void UseChars();
    /// <summary>
    /// byte[]
    /// </summary>
    protected abstract void UseBytes();
    /// <summary>
    /// decimal
    /// </summary>
    protected abstract void UseDecimal();
    /// <summary>
    /// 配置解析器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parser"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Use<T>(ITypeParser parser)
        => _dic[typeof(T)] = parser;
    /// <summary>
    /// 配置解析器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dbType"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Use<T>(string dbType)
        => Use<T>(new TypeParser(dbType));
    /// <summary>
    /// 配置解析器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dbType"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UseLength<T>(string dbType)
        => Use<T>(new LengthTypeParser(dbType, _defaultLength));
    /// <summary>
    /// 配置解析器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dbType"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UsePrecision<T>(string dbType)
        => Use<T>(new PrecisionTypeParser(dbType));
    /// <summary>
    /// 构造长度类型
    /// </summary>
    /// <param name="typeName"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static SqlRawType Create(string typeName, int length)
        => new($"{typeName}({length})");
    /// <summary>
    /// 构造精度类型
    /// </summary>
    /// <param name="typeName"></param>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static SqlRawType Create(string typeName, int precision, int scale)
        => new($"{typeName}({precision},{scale})");
}
