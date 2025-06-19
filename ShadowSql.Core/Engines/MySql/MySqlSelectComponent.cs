using ShadowSql.Components;
using ShadowSql.Cursors;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Engines.MySql;

/// <summary>
/// MySql数据获取组件
/// </summary>
public class MySqlSelectComponent : SelectComponentBase
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
            if (offset > 0)
            {
                WriteOffset(engine, sql, offset);
                sql.Append(',');
            }
            WriteLimit(engine, sql, limit);
        }
    }
}
