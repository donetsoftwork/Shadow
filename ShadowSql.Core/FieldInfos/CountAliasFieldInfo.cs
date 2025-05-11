using ShadowSql.Aggregates;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Services;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 计数字段别名信息
/// </summary>
public sealed class CountAliasFieldInfo : IdentifierBase, IAggregateFieldAlias
{
    /// <summary>
    /// 计数字段别名信息
    /// </summary>
    /// <param name="alias"></param>
    private CountAliasFieldInfo(string alias)
        : base(alias)
    {
    }
    /// <summary>
    /// 获取计数字段信息(已缓存,避免重复构造)
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static CountAliasFieldInfo Use(string alias = "Count")
        => _cacher.Get(alias);
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<CountAliasFieldInfo> _cacher = new(static alias => new CountAliasFieldInfo(alias));
    #region IFieldAlias
    IColumn IFieldView.ToColumn()
        => Column.Use(_name);
    IFieldAlias IFieldView.As(string alias)
        => Use(alias);
    string IFieldAlias.Alias
        => _name;
    string IView.ViewName 
        => _name;
    string IAggregate.Aggregate
        => AggregateConstants.Count;
    #endregion
    #region ISqlEntity
    internal override void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.Count(sql);
        engine.ColumnAs(sql, _name);
    }
    #endregion
}
