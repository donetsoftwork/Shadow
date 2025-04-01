using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Services;
using System.Text;

namespace ShadowSql.Orders;

/// <summary>
/// 降序字段
/// </summary>
internal class OrderByDescField : Identifier, IOrderField
{
    /// <summary>
    /// 降序
    /// </summary>
    public const string Desc = " DESC";
    private OrderByDescField(string name)
        : base(name)
    {
    }
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        base.Write(engine, sql);
        sql.Append(Desc);
    }
    /// <summary>
    /// 获取降序字段信息(已缓存,避免重复构造)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static OrderByDescField Use(string name)
        => _cacher.Get(name);

    private static readonly CacheService<OrderByDescField> _cacher = new(name => new OrderByDescField(name));

    string IView.ViewName
        => _name;
}
