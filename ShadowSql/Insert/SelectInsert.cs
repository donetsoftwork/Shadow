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
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
/// <param name="select"></param>
public class SelectInsert<TTable>(TTable table, ISelect select)
    : InsertBase<TTable>(table), ISelectInsert
    where TTable : ITable
{
    #region 配置
    private readonly ISelect _select = select;
    private readonly List<IColumn> _columns = [];
    /// <summary>
    /// Select子查询
    /// </summary>
    public ISelect Select
        => _select;
    /// <summary>
    /// 插入的列
    /// </summary>
    public IColumn[] Columns
        => [.. CheckColumns()];
    #endregion
    private IEnumerable<IColumn> CheckColumns()
    {
        var count = _columns.Count;
        var fields = _select.Selected.ToArray();
        var fieldCount = fields.Length;
        if (count == fieldCount)
            return _columns;
        else if (count > fieldCount)
            return _columns.Take(fieldCount);
        if (count == 0) 
            return fields.Select(f => f.ToColumn());
        else
            return _columns.Concat(fields.Select(f => f.ToColumn()).Skip(fieldCount).Take(fieldCount - count));
    }
    #region Insert
    /// <summary>
    /// 设置需要插入的列
    /// </summary>
    /// <returns></returns>
    public SelectInsert<TTable> Insert(IColumn column)
    {
        _columns.Add(column);
        return this;
    }
    /// <summary>
    /// 设置需要插入的列
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public SelectInsert<TTable> Insert(string columnName)
    {
        var column = _insertColumns.FirstOrDefault(c=> c.IsMatch(columnName));
        if (column != null)
            _columns.Add(column);
        return this;
    }
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {        
        engine.InsertPrefix(sql);
        _table.Write(engine, sql);
        var appended = false;
        sql.Append('(');
        var columns = Columns;
        foreach (var column in columns)
        {
            if (appended)
                sql.Append(',');
            engine.Identifier(sql, column.ViewName);
            // 避免出现列名前缀可能导致错误
            //if (column.Write(engine, sql))
            appended = true;
        }
        sql.Append(')');
        if (!appended)
            throw new InvalidOperationException("Insert columns is empty.");
        _select.Write(engine, sql);
        //if (_table.Write(engine, sql))
        //{
        //    var appended = false;
        //    sql.Append('(');
        //    var columns = Columns;
        //    foreach (var column in columns)
        //    {
        //        if (appended)
        //            sql.Append(',');
        //        engine.Identifier(sql, column.ViewName);
        //        // 避免出现列名前缀可能导致错误
        //        //if (column.Write(engine, sql))
        //        appended = true;
        //    }
        //    sql.Append(')');
        //    if (!appended)
        //        return false;
        //    return _select.Write(engine, sql);
        //}
        //return false;
    }
}
