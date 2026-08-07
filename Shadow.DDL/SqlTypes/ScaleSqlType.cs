using ShadowSql.Engines;
using System;
using System.Text;

namespace Shadow.DDL.SqlTypes;

/// <summary>
/// 刻度
/// </summary>
/// <param name="originalType"></param>
/// <param name="precision"></param>
/// <param name="scale"></param>
public class ScaleSqlType(Type originalType, int precision, int scale)
    : PrecisionSqlType(originalType, precision)
{
    #region 配置
    private readonly int _scale = scale;
    /// <summary>
    /// 刻度
    /// </summary>
    public int Scale
        => _scale;
    #endregion
    /// <inheritdoc/>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.GetTypeParser(_originalType)
            .Write(sql, _precision, _scale);
    }
}
