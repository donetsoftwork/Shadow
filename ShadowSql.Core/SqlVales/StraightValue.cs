using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.SqlVales;

/// <summary>
/// 直接拼写
/// </summary>
/// <param name="value"></param>
public class StraightValue(string value) : StringValue(value)
{
    /// <inheritdoc/>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_val);
    }
}
