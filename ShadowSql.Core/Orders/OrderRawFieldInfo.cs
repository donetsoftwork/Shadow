using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Services;
using System.Text;

namespace ShadowSql.Orders;

/// <summary>
/// 比较原始sql语句
/// </summary>
public sealed class RawOrderByInfo : Identifier, IOrderView
{
    private RawOrderByInfo(string statement)
        : base(statement)
    {
    }
    /// <summary>
    /// 获取列字段信息(已缓存,避免重复构造)
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    public static RawOrderByInfo Use(string statement)
        => _cacher.Get(statement);
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<RawOrderByInfo> _cacher = new(static statement => new RawOrderByInfo(statement));

    /// <inheritdoc/>
    internal override void Write(ISqlEngine engine, StringBuilder sql)
        => sql.Append(_name);
}
