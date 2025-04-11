using ShadowSql.Identifiers;
using ShadowSql.Services;

namespace ShadowSql.Orders;

/// <summary>
/// 排序字段
/// </summary>
internal class OrderByField : Identifier, IOrderField
{
    private OrderByField(string name)
        : base(name)
    {
    }
    /// <summary>
    /// 获取排序字段信息(已缓存,避免重复构造)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static OrderByField Use(string name)
        => _cacher.Get(name);

    private static readonly CacheService<OrderByField> _cacher = new(name => new OrderByField(name));

    string IView.ViewName
        => _name;
}
