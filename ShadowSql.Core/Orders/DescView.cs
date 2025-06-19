using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Orders;

/// <summary>
/// 倒序
/// </summary>
/// <param name="asc"></param>
public class DescView(IOrderAsc asc)
    : IOrderDesc
{
    private readonly IOrderAsc _asc = asc;
    /// <summary>
    /// 字段
    /// </summary>
    public IOrderAsc Asc
        => _asc;

    /// <inheritdoc/>
    public void Write(ISqlEngine engine, StringBuilder sql)
    {
        _asc.Write(engine, sql);
        sql.Append(" DESC");
    }
}
