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
/// <param name="view"></param>
/// <param name="multiView">多(联)表</param>
public abstract class MultiSelectBase<TSource>(TSource view, IMultiView multiView)
    : SelectBase<TSource, IMultiView>(view, multiView), IMultiSelect, ISelect
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
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
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
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
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
    /// <inheritdoc/>
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
