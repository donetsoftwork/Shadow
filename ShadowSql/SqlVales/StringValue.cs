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

    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append('\'').Append(_val).Append('\'');
    }
}
