using System;

namespace Shadow.DDL.Schemas;

/// <summary>
/// 字段类型
/// </summary>
[Flags]
public enum ColumnType
{
    /// <summary>
    /// 空
    /// </summary>
    Empty = 0,
    /// <summary>
    /// 自增列
    /// </summary>
    Identity = 1,
    /// <summary>
    /// 主键之一
    /// </summary>
    Key = 2,
    /// <summary>
    /// 唯一列
    /// </summary>
    Unique = 4,
    /// <summary>
    /// 非空
    /// </summary>
    NotNull = 8,
    /// <summary>
    /// 计算列
    /// </summary>
    Computed = 16
}
