using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Insert;

/// <summary>
/// 插入单条基类
/// </summary>
/// <param name="items"></param>
public abstract class SingleInsertBase(List<IInsertValue> items)
{
    #region 配置
    internal readonly List<IInsertValue> _items = items;
    /// <summary>
    /// 插入单值列表
    /// </summary>
    public IEnumerable<IInsertValue> Items
        => _items;
    #endregion
    /// <summary>
    /// 增加插入值
    /// </summary>
    /// <param name="value">值</param>
    internal void Add(IInsertValue value)
        => _items.Add(value);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
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
    }
}
