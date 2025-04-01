using ShadowSql.Aggregates;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Variants;

/// <summary>
/// 聚合(计算)列
/// </summary>
/// <typeparam name="TColumn"></typeparam>
/// <param name="aggregate"></param>
/// <param name="target"></param>
/// <param name="alias"></param>
public class AggregateColumnAlias<TColumn>(string aggregate, TColumn target, string alias = "")
    : AggregateColumnBase<TColumn>(aggregate, target), IAggregateFieldAlias
    where TColumn : IColumn
{
    private readonly string _alias = alias;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => CheckAlias(_aggregate, _target, _alias);

    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        base.Write(engine, sql);
        engine.ColumnAs(sql, Alias);
        //if (base.Write(engine, sql))
        //{
        //    engine.ColumnAs(sql, Alias);
        //    return true;
        //}
        //return false;
    }
    /// <summary>
    /// 生成别名
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public IFieldAlias As(string alias)
    {
        return Column.Use(Alias)
            .As(alias);
    }

    /// <summary>
    /// 检查别名
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="column"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static string CheckAlias(string aggregate, IColumn column, string alias)
        => string.IsNullOrWhiteSpace(alias) ? column.ViewName + aggregate : alias;

    string IView.ViewName
        => Alias;
    IColumn IFieldView.ToColumn()
        => Column.Use(Alias);
    bool IMatch.IsMatch(string name)
        => Identifier.Match(Alias, name);
}