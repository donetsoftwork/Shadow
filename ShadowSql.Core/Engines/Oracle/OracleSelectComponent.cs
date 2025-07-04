using ShadowSql.Components;
using ShadowSql.Cursors;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Engines.Oracle;

/// <summary>
/// Oracle数据获取组件
/// </summary>
public class OracleSelectComponent : SelectComponentBase
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
            //FETCH方式只支持oracle12c以后版本，旧版本不支持使用该方法
            if (offset > 0)
            {
                sql.Append(" OFFSET ");
                WriteOffset(engine, sql, offset);
                sql.Append(" ROWS FETCH FIRST ");
            }
            else
            {
                sql.Append("OFFSET 0 ROWS FETCH FIRST ");
            }
            WriteLimit(engine, sql, limit);
            sql.Append(" ROWS ONLY");
        }
    }
}
