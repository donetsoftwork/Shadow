using ShadowSql.Components;
using ShadowSql.Cursors;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Engines.Postgres;

/// <summary>
/// Postgres数据获取组件
/// </summary>
public class PostgresSelectComponent : SelectComponentBase
{
    /// <inheritdoc/>
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
