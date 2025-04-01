using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 多条插入
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
public class MultiInsert<TTable>(TTable table)
    : InsertBase<TTable>(table), IMultiInsert
    where TTable : ITable
{
    #region 配置
    private readonly List<IInsertValues> _items = [];

    /// <summary>
    /// 插入单值列表
    /// </summary>
    public IEnumerable<IInsertValues> Items
        => _items;
    #endregion
    ///// <summary>
    ///// 增加插入值
    ///// </summary>
    ///// <param name="value"></param>
    ///// <returns></returns>
    //public MultiInsert<TTable> Insert(IInsertValues value)
    //{
    //    _items.Add(value);
    //    return this;
    //}
    /// <summary>
    /// 增加插入值
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiInsert<TTable> Insert(Func<TTable, InsertValues> select)
    {
        _items.Add(select(_table));
        return this;
    }
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    internal override void Write(ISqlEngine engine, StringBuilder sql)
    {
        if(_items.Count == 0)
            throw new InvalidOperationException("未设置插入值");
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
        sql.Append(")VALUES");
        var count = _items[0].Values.Length;
        appended = false;
        for (int i = 0; i < count; i++)
        {
            if (appended)
                sql.Append(',');
            sql.Append('(');
            var appended2 = false;
            foreach (var item in _items)
            {
                if (appended2)
                    sql.Append(',');
                item.Values[i].Write(engine, sql);
                    appended2 = true;
            }
            sql.Append(')');
            appended = true;
        }
    }

    #region IMultiInsert
    /// <summary>
    /// 插入多值
    /// </summary>
    /// <param name="value"></param>
    void IMultiInsert.InsertCore(IInsertValues value)
    {
        _items.Add(value);
    }
    ///// <summary>
    ///// 插入多值
    ///// </summary>
    ///// <param name="select"></param>
    //void IMultiInsert.InsertCore(Func<ITable, IInsertValues> select)
    //{
    //    _items.Add(select(_table));
    //}
    #endregion
}
