using ShadowSql.Identifiers;
using ShadowSql.Services;
using System;
using System.Collections.Generic;

namespace ShadowSql.Tables;

/// <summary>
/// 简单表名对象
/// </summary>
public class SimpleTable : Identifier, ITable
{
    private SimpleTable(string name)
        : base(name)
    {
    }
    private static readonly CacheService<SimpleTable> _cacher = new(static name => new SimpleTable(name));
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
    #region ITable
    IEnumerable<IColumn> ITable.Columns => [];
    IColumn? ITable.GetColumn(string columName) => null;
    IEnumerable<IField> ITableView.Fields => [];
    IField? ITableView.GetField(string fieldName) => null;
    ICompareField ITableView.GetCompareField(string fieldName)
        => Column.Use(fieldName);
    IField ITableView.NewField(string fieldName)
        => Column.Use(fieldName);
    #endregion
}
