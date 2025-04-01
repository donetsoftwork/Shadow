using ShadowSql.Aggregates;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Variants;

/// <summary>
/// 列别名
/// </summary>
/// <typeparam name="TColumn"></typeparam>
/// <param name="target"></param>
/// <param name="alias"></param>
public class AliasColumn<TColumn>(TColumn target, string alias)
    : SqlAlias<TColumn>(target, alias), IFieldAlias
    where TColumn : IColumn
{
    string IView.ViewName
        => _name;

    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public IAggregateFieldAlias AggregateAs(string aggregate, string alias)
    {
        return Column.Use(_name)
            .AggregateAs(aggregate, alias);
    }
    /// <summary>
    /// 生成别名
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public IFieldAlias As(string alias)
    {
        return Column.Use(_name)
            .As(alias);
    }

    IColumn IFieldView.ToColumn()
        => Column.Use(_name);

    #region ISqlEntity
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="builder"></param>
    /// <returns></returns>
    internal override void Write(ISqlEngine engine, StringBuilder builder)
    {
        _target.Write(engine, builder);
        engine.ColumnAs(builder, _name);
    }
    #endregion
}
