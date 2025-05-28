using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 多表筛选基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
/// <param name="target"></param>
public abstract class MultiSelectBase<TSource>(TSource source, IMultiView target)
    : SelectBase<TSource, IMultiView>(source, target), IMultiSelect, ISelect
    where TSource : ITableView
{
    #region 配置
    private readonly List<IAliasTable> _selectTables = [];
    /// <summary>
    /// 选择表
    /// </summary>
    public ICollection<IAliasTable> SelectTables
        => _selectTables;
    #endregion
    #region IColumn
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    internal void SelectCore<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable
    {
        var member = _target.Alias<TTable>(tableName);
        //增加前缀
        if (member.GetPrefixField(select(member.Target)) is IPrefixField prefixField)
            SelectCore(prefixField);
    }
    /// <summary>
    /// 筛选多列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    internal void SelectCore<TTable>(string tableName, Func<TTable, IEnumerable<IColumn>> select)
        where TTable : ITable
    {
        var member = _target.Alias<TTable>(tableName);
        var columns = select(member.Target);
        foreach (var column in columns)
        {
            //增加前缀
            if (member.GetPrefixField(column) is IPrefixField prefixField)
                SelectCore(prefixField);
        }
    }
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 写入筛选字段
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="appended"></param>
    /// <returns></returns>
    protected override bool WriteSelectedCore(ISqlEngine engine, StringBuilder sql, bool appended)
    {
        foreach (var table in _selectTables)
        {
            if (appended)
                sql.Append(',');
            sql.Append(table.Alias)
                .Append('.')
                .Append('*');
            appended = true;
        }
        return base.WriteSelectedCore(engine, sql, appended);
    }
    #endregion
}
