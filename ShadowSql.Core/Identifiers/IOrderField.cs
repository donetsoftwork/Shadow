using ShadowSql.Fragments;

namespace ShadowSql.Identifiers;

/// <summary>
/// 排序字段
/// </summary>
public interface IOrderField : IView, IOrderAsc;
/// <summary>
/// 正序
/// </summary>
public interface IOrderAsc : IOrderView;
/// <summary>
/// 倒序
/// </summary>
public interface IOrderDesc : IOrderView;
/// <summary>
/// 排序字段或表达式
/// </summary>
public interface IOrderView : ISqlEntity;
