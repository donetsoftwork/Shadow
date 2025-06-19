using ShadowSql.Aggregates;
using ShadowSql.FieldInfos;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace ShadowSql.Variants;

/// <summary>
/// 前缀字段
/// </summary>
/// <param name="column">列</param>
/// <param name="prefix">前缀</param>
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
    /// <inheritdoc/>
    public IAggregateFieldAlias AggregateAs(string aggregate, string alias)
    {
        if (AggregateConstants.MatchCount(aggregate))
            return new DistinctCountAliasFieldInfo(this, aggregate);
        return new AggregateAliasFieldInfo(this, aggregate,  alias);
    }
    /// <inheritdoc/>
    public IAggregateField AggregateTo(string aggregate)
    {
        if (AggregateConstants.MatchCount(aggregate))
            return new DistinctCountFieldInfo(this);
        return new AggregateFieldInfo(this, aggregate);
    }
    /// <inheritdoc/>
    public IFieldAlias As(string aliasName)
        => new AliasFieldInfo(this, aliasName);
    /// <inheritdoc/>
    public bool IsMatch(IField field)
        => field == this || field == _target;
    /// <summary>
    /// 是否匹配
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    public bool IsMatch(string columnName)
        => _target.IsMatch(columnName)
        || MatchPrefixColumn(_tablePrefix, _target.ViewName, columnName);
    /// <summary>
    /// 判断是否为前缀字段名
    /// </summary>
    /// <param name="prefix">前缀</param>
    /// <param name="column">列</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    internal static bool MatchPrefixColumn(string prefix, string column, string columnName)
        => CheckTablePrefix(prefix, columnName)
        && Identifier.Match(columnName[prefix.Length..], column);

    /// <summary>
    /// 判断是否含表名前缀
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="column">列</param>
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
    /// <param name="columns">列</param>
    /// <returns></returns>
    internal static IEnumerable<IPrefixField> GetFields(string[] tablePrefix, IEnumerable<IColumn> columns)
    {
        foreach (var column in columns)
            yield return new PrefixField(column, tablePrefix);
    }
    /// <inheritdoc/>
    string IView.ViewName
        => _target.ViewName;
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => _target;
}
