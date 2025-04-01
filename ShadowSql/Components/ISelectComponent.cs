using ShadowSql.Engines;
using ShadowSql.Fetches;
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
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="select"></param>
    void Select(ISqlEngine engine, StringBuilder sql, ISelect select);
    /// <summary>
    /// 数据分页获取
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="select"></param>
    /// <param name="cursor"></param>
    void SelectCursor(ISqlEngine engine, StringBuilder sql, ISelect select, ICursor cursor);
    /// <summary>
    /// 输出跳过数量
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="offset"></param>
    void WriteOffset(ISqlEngine engine, StringBuilder sql, int offset);
    /// <summary>
    /// 输出获取数量
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="limit"></param>
    void WriteLimit(ISqlEngine engine, StringBuilder sql, int limit);
}
