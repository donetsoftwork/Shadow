using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.SqlVales;

/// <summary>
/// 非安全字符串
/// </summary>
/// <param name="val"></param>
public class UnSafeValue(string val) : StringValue(val)
{
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        //需要转义(防sql注入)
        sql.Append('\'').Append(engine.Escape(_val)).Append('\'');
    }
}
