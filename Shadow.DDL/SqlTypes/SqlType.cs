using Shadow.DDL.Components;
using Shadow.DDL.TypeParse;
using ShadowSql.Engines;
using System;
using System.Text;

namespace Shadow.DDL.SqlTypes;

/// <summary>
/// 数据库类型
/// </summary>
/// <param name="originalType"></param>
public class SqlType(Type originalType)
    : ISqlType
{
    #region 配置
    /// <summary>
    /// 原始类型
    /// </summary>
    protected readonly Type _originalType = originalType;
    /// <summary>
    /// 原始类型
    /// </summary>
    public Type OriginalType
        => _originalType;
    #endregion
    /// <inheritdoc/>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.GetTypeParser(_originalType)
            .Write(sql);
    }
    /// <summary>
    /// 构造数据库类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ISqlType Create<T>()
        => new SqlType(typeof(T));
    /// <summary>
    /// 构造长度类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="length"></param>
    /// <returns></returns>
    public static ISqlType Create<T>(int length)
        => new LengthSqlType(typeof(T), length);
    /// <summary>
    /// 构造精度类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="precision"></param>
    /// <returns></returns>
    public static ISqlType CreatePrecision<T>(int precision)
        => new PrecisionSqlType(typeof(T), precision);
    /// <summary>
    /// 构造精度类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static ISqlType CreatePrecision<T>(int precision, int scale)
        => new ScaleSqlType(typeof(T), precision, scale);
    /// <summary>
    /// 构造原始类型
    /// </summary>
    /// <param name="rawType"></param>
    /// <returns></returns>
    public static ISqlType Create(string rawType)
        => new SqlRawType(rawType);
}
