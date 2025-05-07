using ShadowSql.Aggregates;
using ShadowSql.Fragments;

namespace ShadowSql.Identifiers;

/// <summary>
/// 字段
/// </summary>
public interface IField : IFieldView, ICompareField, IOrderField, IAssignView;
/// <summary>
/// 前缀字段(前缀限定符)
/// </summary>
public interface IPrefixField : IField
{
    /// <summary>
    /// 匹配字段
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    bool IsMatch(IField field);
}
///// <summary>
///// 原始字段
///// </summary>
//public interface IIdentifierField : IIdentifier, IField;
/// <summary>
/// 字段或表达式(select使用)
/// </summary>
public interface IFieldView : IView, IMatch, ISqlEntity
{
    /// <summary>
    /// 转化为列(用于新表/视图(insert、groupby等))
    /// </summary>
    /// <returns></returns>
    IColumn ToColumn();
    /// <summary>
    /// 别名
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    IFieldAlias As(string alias);
}

/// <summary>
/// 去重字段统计
/// </summary>
public interface IDistinctCountField : IAggregateField;
/// <summary>
/// 去重字段统计别名
/// </summary>
public interface IDistinctCountFieldAlias : IAggregateFieldAlias;
/// <summary>
/// 字段别名
/// </summary>
public interface IFieldAlias : IFieldView
{
    /// <summary>
    /// 别名
    /// </summary>
    string Alias { get; }
}

