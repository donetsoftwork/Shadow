using ShadowSql.Engines;
using System;
using System.Text;

namespace Shadow.DDL.SqlTypes;

/// <summary>
/// 长度类型
/// </summary>
/// <param name="originalType"></param>
/// <param name="length"></param>
public class LengthSqlType(Type originalType, int length)
    : SqlType(originalType)
{
    #region 配置
    private readonly int _length = length;
    /// <summary>
    /// 长度
    /// </summary>
    public int Length
        => _length;
    #endregion
    /// <inheritdoc/>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.GetTypeParser(_originalType)
            .Write(sql, _length);
    }
}
