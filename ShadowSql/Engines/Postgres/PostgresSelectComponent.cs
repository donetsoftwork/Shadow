using ShadowSql.Components;
using ShadowSql.Cursors;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Engines.Postgres;

/// <summary>
/// 数据获取组件基类
/// </summary>
public class PostgresSelectComponent : SelectComponentBase
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
        int limit = cursor.Limit;
        sql.Append("SELECT ");
        WriteView(engine, sql, select.Source, select);
        if (limit > 0)
        {
            int offset = cursor.Offset;
            sql.Append(" LIMIT ");
            WriteLimit(engine, sql, limit);
            if (offset > 0)
            {
                sql.Append(" OFFSET ");
                WriteOffset(engine, sql, offset);
            }
            else
            {
                sql.Append(" OFFSET 0");
            }
        }
    }
}
