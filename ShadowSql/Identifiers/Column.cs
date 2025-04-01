using ShadowSql.Aggregates;
using ShadowSql.Services;
using ShadowSql.Variants;

namespace ShadowSql.Identifiers;

/// <summary>
/// 简单列对象
/// </summary>
public class Column : ColumnBase, IColumn
{
    /// <summary>
    /// 简单列对象
    /// </summary>
    /// <param name="name"></param>
    protected Column(string name)
        : base(name)
    {
    }
    /// <summary>
    /// 获取列(已缓存,避免重复构造)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Column Use(string name)
        => _cacher.Get(name);
    /// <summary>
    /// 生成别名
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public override IFieldAlias As(string alias)
    {
        return new AliasColumn<Column>(this, alias);
    }
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public IPrefixColumn GetPrefixColumn(params string[] prefix)
    {
        return new PrefixColumn(this, prefix);
    }
    /// <summary>
    /// 聚合别名
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public override IAggregateFieldAlias AggregateAs(string aggregate, string alias)
    {
        if (AggregateConstants.MatchCount(aggregate))
            return new DistinctCountAliasFieldInfo(this, aggregate);
        return new AggregateColumnAlias<Column>(aggregate, this, alias);
    }
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public override IAggregateField AggregateTo(string aggregate)
    {
        if (AggregateConstants.MatchCount(aggregate))
            return new DistinctCountFieldInfo(this);
        return new AggregateColumn<Column>(aggregate, this);
    }



    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<Column> _cacher = new(name => new Column(name));

    string IView.ViewName
        => _name;
    IColumn IFieldView.ToColumn()
        => this;
}
