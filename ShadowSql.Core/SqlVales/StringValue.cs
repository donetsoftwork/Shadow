using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.SqlVales;

/// <summary>
/// 字符串
/// </summary>
/// <param name="val"></param>
public class StringValue(string val) : ISqlValue
{
    /// <summary>
    /// 值
    /// </summary>
    protected readonly string _val = val;

    /// <inheritdoc/>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append('\'').Append(_val).Append('\'');
    }
}
