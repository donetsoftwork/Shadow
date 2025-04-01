using ShadowSql.Components;
using ShadowSql.Fetches;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Engines.Sqlite;

/// <summary>
/// 数据获取组件基类
/// </summary>
public class SqliteSelectComponent : SelectComponentBase
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
        sql.Append("SELECT ");
        WriteView(engine, sql, select.Source, select);
        if (limit > 0)
        {
            sql.Append(" LIMIT ");
            WriteLimit(engine, sql, limit);
            if (offset > 0)
            {
                sql.Append(" OFFSET ");
                WriteOffset(engine, sql, offset);
            }
        }
        else if (offset > 0)
        {
            sql.Append("LIMIT -1 OFFSET ");
            WriteOffset(engine, sql, offset);
        }
    }
}
