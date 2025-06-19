using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Components;

/// <summary>
/// 数据获取组件
/// </summary>
public interface ISelectComponent
{
    /// <summary>
    /// 数据获取
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="select">筛选</param>
    void Select(ISqlEngine engine, StringBuilder sql, ISelect select);
    /// <summary>
    /// 数据分页获取
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="select">筛选</param>
    /// <param name="cursor">游标</param>
    void SelectCursor(ISqlEngine engine, StringBuilder sql, ISelect select, ICursor cursor);
    /// <summary>
    /// 输出跳过数量
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="offset">跳过数量</param>
    void WriteOffset(ISqlEngine engine, StringBuilder sql, int offset);
    /// <summary>
    /// 输出获取数量
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="limit">筛选数量</param>
    void WriteLimit(ISqlEngine engine, StringBuilder sql, int limit);
}
