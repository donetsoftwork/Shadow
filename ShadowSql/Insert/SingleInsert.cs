using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 插入单条
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
public class SingleInsert<TTable>(TTable table)
    : InsertBase<TTable>(table), ISingleInsert
    where TTable : ITable
{
    #region 配置
    private readonly List<IInsertValue> _items = [];

    /// <summary>
    /// 插入单值列表
    /// </summary>
    public IEnumerable<IInsertValue> Items
        => _items;
    #endregion
    #region Insert
    void ISingleInsert.InsertCore(IInsertValue value)
    {
        _items.Add(value);
    }
    //void ISingleInsert.InsertCore(Func<ITable, IInsertValue> select)
    //{
    //    _items.Add(select(_table));
    //}
    /// <summary>
    /// 增加插入值
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public SingleInsert<TTable> Insert(Func<TTable, IInsertValue> select)
    {
        _items.Add(select(_table));
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
        foreach (var item in _items)
        {
            if (appended)
                sql.Append(',');
            engine.Identifier(sql, item.Column.ViewName);
            // 避免出现列名前缀可能导致错误
            //if (item.Column.Write(engine, sql))
            appended = true;
        }
        if (!appended)
            throw new InvalidOperationException("未找到插入列");
        sql.Append(")VALUES(");

        appended = false;
        foreach (var item in _items)
        {
            if (appended)
                sql.Append(',');
            item.Value.Write(engine, sql);
            appended = true;
        }
        sql.Append(')');

        //if (_table.Write(engine, sql))
        //{
        //    var appended = false;
        //    sql.Append('(');
        //    foreach (var item in _items)
        //    {
        //        if (appended)
        //            sql.Append(',');
        //        engine.Identifier(sql, item.Column.ViewName);
        //        // 避免出现列名前缀可能导致错误
        //        //if (item.Column.Write(engine, sql))
        //        appended = true;
        //    }
        //    if (!appended)
        //        return false;
        //    sql.Append(")VALUES(");
        //    appended = false;
        //    foreach (var item in _items)
        //    {
        //        if (appended)
        //            sql.Append(',');
        //        if (item.Value.Write(engine, sql))
        //            appended = true;
        //    }
        //    sql.Append(')');

        //    return true;
        //}
        //return false;
    }    
}
