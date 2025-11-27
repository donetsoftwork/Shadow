using Hand.Structural;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Orders;

/// <summary>
/// 倒序字段
/// </summary>
/// <param name="original">原始正序字段</param>
public class DescView(IOrderAsc original)
    : IOrderDesc
    , IWrapper<IOrderAsc>
{
    private readonly IOrderAsc _original = original;
    /// <summary>
    /// 原始正序字段
    /// </summary>
    public IOrderAsc Original
        => _original;

    /// <inheritdoc/>
    public void Write(ISqlEngine engine, StringBuilder sql)
    {
        _original.Write(engine, sql);
        sql.Append(" DESC");
    }
}
