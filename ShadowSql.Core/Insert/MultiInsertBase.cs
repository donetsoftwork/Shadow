using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 插入多条基类
/// </summary>
/// <param name="items"></param>
public abstract class MultiInsertBase(List<IInsertValues> items)
{
    #region 配置
    private readonly List<IInsertValues> _items = items;

    /// <summary>
    /// 插入单值列表
    /// </summary>
    public IEnumerable<IInsertValues> Items
        => _items;
    #endregion
    /// <summary>
    /// 增加插入值
    /// </summary>
    /// <param name="value"></param>
    internal void Add(IInsertValues value)
        => _items.Add(value);
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
        foreach (var item in _items)
        {
            if (appended)
                sql.Append(',');
            engine.WriteInsertColumnName(sql, item.Column);
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
}
