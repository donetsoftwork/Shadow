using ShadowSql.Engines;
using System;
using System.Text;

namespace Shadow.DDL.SqlTypes;

/// <summary>
/// 精度
/// </summary>
/// <param name="originalType"></param>
/// <param name="precision"></param>
public class PrecisionSqlType(Type originalType, int precision)
    : SqlType(originalType)
{
    #region 配置
    /// <summary>
    /// 精度
    /// </summary>
    protected readonly int _precision = precision;
    /// <summary>
    /// 精度
    /// </summary>
    public int Precision
        => _precision;
    #endregion
    /// <inheritdoc/>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.GetTypeParser(_originalType)
            .Write(sql, _precision);
    }
}
