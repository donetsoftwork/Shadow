using ShadowSql.Aggregates;
using ShadowSql.FieldInfos;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace ShadowSql.Variants;

/// <summary>
/// 前缀字段
/// </summary>
/// <param name="column"></param>
/// <param name="prefix"></param>
public class PrefixField(IColumn column, params string[] prefix)
    : PrefixFragment<IColumn>(column, prefix), IPrefixField
{
    //内联的展开运算符“..”
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
        return new AggregateAliasFieldInfo(this, aggregate,  alias);
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
        return new AggregateFieldInfo(this, aggregate);
    }
    /// <summary>
    /// 生成别名
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public IFieldAlias As(string alias)
        => new AliasFieldInfo(this, alias);
    /// <summary>
    /// 匹配字段
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    public bool IsMatch(IField field)
        => field == this || field == _target;
    /// <summary>
    /// 是否匹配
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsMatch(string name)
        => _target.IsMatch(name)
        && MatchPrefixColumn(_tablePrefix, _target.ViewName, name);
    /// <summary>
    /// 判断是否为前缀字段名
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="column"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    internal static bool MatchPrefixColumn(string prefix, string column, string name)
        => CheckTablePrefix(prefix, name)
        && Identifier.Match(name[prefix.Length..], column);

    /// <summary>
    /// 判断是否含表名前缀
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    internal static bool CheckTablePrefix(string table,  string column)
    {
        var prefixIndex = table.Length;
        return column.Length > prefixIndex
            && column.StartsWith(table)
            && column[prefixIndex] == '.';
    }
    /// <summary>
    /// 转化联表字段
    /// </summary>
    /// <param name="tablePrefix"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    internal static IEnumerable<IPrefixField> GetFields(string[] tablePrefix, IEnumerable<IColumn> columns)
    {
        foreach (var column in columns)
            yield return new PrefixField(column, tablePrefix);
    }
    string IView.ViewName
        => _target.ViewName;
    IColumn IFieldView.ToColumn()
        => _target;
}
