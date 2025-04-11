using ShadowSql.Fragments;

namespace ShadowSql.Identifiers;

/// <summary>
/// 原始列标识(作为原始表成员)
/// </summary>
public interface IColumn : IFieldView, IAssignView, ICompareField, IOrderField, ISqlEntity
{
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    IPrefixColumn GetPrefixColumn(params string[] prefix);
}

/// <summary>
/// 含前缀的列(作为别名表成员)
/// </summary>
public interface IPrefixColumn : IColumn
{
    /// <summary>
    /// 前缀
    /// </summary>
    string TablePrefix { get; }
    /// <summary>
    /// 匹配列
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    bool IsMatch(IColumn column);
    /// <summary>
    /// 原始列
    /// </summary>
    public IColumn Target { get; }
}
