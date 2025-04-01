using ShadowSql.Aggregates;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;

namespace ShadowSql.Variants;

/// <summary>
/// 前缀列
/// </summary>
/// <param name="column"></param>
/// <param name="prefix"></param>
public class PrefixColumn(IColumn column, params string[] prefix)
    : PrefixFragment<IColumn>(column, prefix), IPrefixColumn
{
    //////内联的展开运算符“..”
    //private readonly string _fullName = string.Concat([.. tablePrefix, column.Name]);
    private readonly string _tablePrefix = prefix[^2];
    //private readonly string _tablePrefix = prefix[prefix.Length - 2];
    /// <summary>
    /// 前缀
    /// </summary>
    public string TablePrefix 
        => _tablePrefix;
    /// <summary>
    /// 聚合别名
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public IAggregateFieldAlias AggregateAs(string aggregate, string alias)
    {
        if (AggregateConstants.MatchCount(aggregate))
            return new DistinctCountAliasFieldInfo(this, aggregate);
        return new AggregateColumnAlias<IPrefixColumn>(aggregate, this, alias);
    }
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public IAggregateField AggregateTo(string aggregate)
    {
        if (AggregateConstants.MatchCount(aggregate))
            return new DistinctCountFieldInfo(this);
        return new AggregateColumn<IPrefixColumn>(aggregate, this);
    }
    /// <summary>
    /// 生成别名
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public IFieldAlias As(string alias)
    {
        return new AliasColumn<IPrefixColumn>(this, alias);
    }
    /// <summary>
    /// 匹配列
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    public bool IsMatch(IColumn column)
        => column == this || column == _target;
    /// <summary>
    /// 是否匹配
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsMatch(string name)
        => _target.IsMatch(name)
        && MatchPrefixColumn(_tablePrefix, _target.ViewName, name);
    /// <summary>
    /// 判断是否为前缀列名
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="column"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool MatchPrefixColumn(string prefix, string column, string name)
        => CheckTablePrefix(prefix, name)
        && Identifier.Match(name[prefix.Length..], column);

    /// <summary>
    /// 判断是否含表名前缀
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public static bool CheckTablePrefix(string table,  string column)
    {
        var prefixIndex = table.Length;
        return column.Length > prefixIndex
            && column.StartsWith(table)
            && column[prefixIndex] == '.';
    }

    string IView.ViewName
        => _target.ViewName;

    IColumn IFieldView.ToColumn()
        => this;
    IPrefixColumn IColumn.GetPrefixColumn(params string[] prefix)
        => _target.GetPrefixColumn(prefix);
}
