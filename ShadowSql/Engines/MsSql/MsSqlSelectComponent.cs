using ShadowSql.Components;
using ShadowSql.Fetches;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Engines.MsSql;

/// <summary>
/// 数据获取组件基类
/// </summary>
public class MsSqlSelectComponent : SelectComponentBase
{
    /// <summary>
    /// 数据分页获取
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="select"></param>
    /// <param name="cursor"></param>
    public override void SelectCursor(ISqlEngine engine, StringBuilder sql, ISelect select, ICursor cursor)
    {
        int offset = cursor.Offset;
        int limit = cursor.Limit;
        //优先Top查询
        if (offset < 1)
        {
            SelectTop(engine, sql, select, limit);
            return;
        }

        sql.Append("SELECT ");
        WriteView(engine, sql, select.Source, select);
        if (limit > 0)
        {
            if (offset > 0)
            {
                sql.Append(" OFFSET ");
                WriteOffset(engine, sql, offset);
                sql.Append(" ROWS");
            }
            sql.Append(" FETCH NEXT ");
            WriteLimit(engine, sql, limit);
            sql.Append(" ROWS ONLY");
        }
    }
    /// <summary>
    /// SELECT TOP
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="select"></param>
    /// <param name="top"></param>
    public void SelectTop(ISqlEngine engine, StringBuilder sql, ISelect select, int top)
    {
        sql.Append("SELECT ");
        if (top > 0)
            sql.Append("TOP ").Append(top).Append(' ');
        WriteView(engine, sql, select.Source, select);
    }
}
