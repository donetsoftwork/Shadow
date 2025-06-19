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
    /// <param name="aliasName">别名</param>
    private CountAliasFieldInfo(string aliasName)
        : base(aliasName)
    {
    }
    /// <summary>
    /// 获取计数字段信息(已缓存,避免重复构造)
    /// </summary>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static CountAliasFieldInfo Use(string aliasName = "Count")
        => _cacher.Get(aliasName);
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<CountAliasFieldInfo> _cacher = new(static aliasName => new CountAliasFieldInfo(aliasName));
    #region IFieldAlias
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => Column.Use(_name);
    /// <inheritdoc/>
    IFieldAlias IFieldView.As(string aliasName)
        => Use(aliasName);
    /// <inheritdoc/>
    string IFieldAlias.Alias
        => _name;
    /// <inheritdoc/>
    string IView.ViewName 
        => _name;
    /// <inheritdoc/>
    string IAggregate.Aggregate
        => AggregateConstants.Count;
    #endregion
    /// <inheritdoc/>
    IAggregateField IAggregateFieldAlias.ToAggregate()
        => CountFieldInfo.Instance;
    #region ISqlEntity
    /// <inheritdoc/>
    internal override void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.Count(sql);
        engine.ColumnAs(sql, _name);
    }
    #endregion
}
