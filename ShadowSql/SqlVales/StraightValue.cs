using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.SqlVales;

/// <summary>
/// 直接拼写
/// </summary>
/// <param name="val"></param>
public class StraightValue(string val) : StringValue(val)
{
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_val);
    }
}
