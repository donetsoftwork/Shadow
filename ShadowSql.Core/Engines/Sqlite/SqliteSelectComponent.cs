using ShadowSql.Components;
using ShadowSql.Cursors;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Engines.Sqlite;

/// <summary>
/// Sqlite数据获取组件
/// </summary>
public class SqliteSelectComponent : SelectComponentBase
{
    /// <inheritdoc/>
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
