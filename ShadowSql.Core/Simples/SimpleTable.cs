using ShadowSql.Identifiers;
using ShadowSql.Services;
using System;
using System.Collections.Generic;

namespace ShadowSql.Simples;

/// <summary>
/// 简单表名对象
/// </summary>
public class SimpleTable : TableBase
{
    private SimpleTable(string name)
        : base(name)
    {
    }
    private static readonly CacheService<SimpleTable> _cacher = new(name => new SimpleTable(name));
    /// <summary>
    /// 默认表对象
    /// </summary>
    public static readonly Lazy<SimpleTable> Empty = new(() => Use("Table"));
    /// <summary>
    /// 获取表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static SimpleTable Use(string tableName)
        => _cacher.Get(tableName);

    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public override IColumn? GetColumn(string columName)
        => null;
    #region ITable
    /// <summary>
    /// 列为空
    /// </summary>
    public override IEnumerable<IColumn> Columns
        => [];
    /// <summary>
    /// 插入列为空
    /// </summary>
    public override IEnumerable<IColumn> InsertColumns
         => [];
    /// <summary>
    /// 修改列为空
    /// </summary>
    public override IEnumerable<IColumn> UpdateColumns
        => [];
    #endregion
}
