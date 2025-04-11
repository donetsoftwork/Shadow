using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 插入Select子查询
/// </summary>
/// <param name="columns"></param>
/// <param name="select"></param>
public abstract class SelectInsertBase(List<IColumn> columns, ISelect select)
{
    #region 配置
    private readonly List<IColumn> _columns = columns;
    private readonly ISelect _select = select;
    /// <summary>
    /// 插入的列
    /// </summary>
    public IEnumerable<IColumn> Columns
        => CheckColumns();
    /// <summary>
    /// Select子查询
    /// </summary>
    public ISelect Select
        => _select;
    #endregion
    /// <summary>
    /// 检查设置列和Select列
    /// </summary>
    /// <returns></returns>
    private IEnumerable<IColumn> CheckColumns()
    {
        var count = _columns.Count;
        var selectColumns = _select.ToColumns().ToList();
        var fieldCount = selectColumns.Count;
        if (count == fieldCount)
            return _columns;
        else if (count > fieldCount)
            return _columns.Take(fieldCount);
        if (count == 0)
            return selectColumns;
        else
            return _columns.Concat(selectColumns.Skip(fieldCount).Take(fieldCount - count));
    }
    /// <summary>
    /// 增加插入列
    /// </summary>
    /// <param name="column"></param>
    internal void Add(IColumn column)
        => _columns.Add(column);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="table"></param>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <exception cref="InvalidOperationException"></exception>
    protected void WriteInsert(IInsertTable table, ISqlEngine engine, StringBuilder sql)
    {
        engine.InsertPrefix(sql);
        table.Write(engine, sql);
        var appended = false;
        sql.Append('(');
        var columns = Columns;
        foreach (var column in columns)
        {
            if (appended)
                sql.Append(',');
            engine.WriteInsertColumnName(sql, column);
            appended = true;
        }
        sql.Append(')');
        if (!appended)
            throw new InvalidOperationException("Insert columns is empty.");
        _select.Write(engine, sql);
    }
}
