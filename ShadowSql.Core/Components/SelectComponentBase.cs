using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using System.Text;

namespace ShadowSql.Components;

/// <summary>
/// 数据获取组件基类
/// </summary>
public abstract class SelectComponentBase : ISelectComponent
{
    /// <summary>
    /// 数据获取
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="select">筛选</param>
    public virtual void Select(ISqlEngine engine, StringBuilder sql, ISelect select)
    {
        engine.SelectPrefix(sql);
        WriteView(engine, sql, select.Source, select);
    }
    /// <summary>
    /// 数据分页获取
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="select">筛选</param>
    /// <param name="cursor">游标</param>
    public abstract void SelectCursor(ISqlEngine engine, StringBuilder sql, ISelect select, ICursor cursor);
    /// <summary>
    /// Select查询主体
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="view"></param>
    /// <param name="fields">字段</param>
    protected virtual void WriteView(ISqlEngine engine, StringBuilder sql, ITableView view, ISelectFields fields)
    {
        WriteFields(engine, sql, fields);
        sql.Append(" FROM ");
        view.Write(engine, sql);
    }
    /// <summary>
    /// 输出字段
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="fields">字段</param>
    protected virtual void WriteFields(ISqlEngine engine, StringBuilder sql, ISelectFields fields)
    {
        if (!fields.WriteSelected(engine, sql))
            sql.Append('*');
    }
    /// <summary>
    /// 输出跳过数量
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="offset">跳过数量</param>
    public virtual void WriteOffset(ISqlEngine engine, StringBuilder sql, int offset)
        => sql.Append(offset);
    /// <summary>
    /// 输出获取数量
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="limit">筛选数量</param>
    public virtual void WriteLimit(ISqlEngine engine, StringBuilder sql, int limit)
        => sql.Append(limit);

}
