using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Orders;

/// <summary>
/// 倒序
/// </summary>
/// <param name="asc"></param>
internal class DescView(IOrderAsc asc)
    : IOrderDesc
{
    private readonly IOrderAsc _asc = asc;
    /// <summary>
    /// 字段
    /// </summary>
    public IOrderAsc Asc
        => _asc;

    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public void Write(ISqlEngine engine, StringBuilder sql)
    {
        _asc.Write(engine, sql);
        sql.Append(" DESC");
        //if (_asc.Write(engine, sql))
        //{
        //    sql.Append(" DESC");
        //    return true;
        //}
        //return false;
    }
}
