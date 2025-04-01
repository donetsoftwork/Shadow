using ShadowSql.Fragments;

namespace ShadowSql.Identifiers;

/// <summary>
/// 赋值字段(可insert和update)
/// </summary>
public interface IAssignField : IIdentifier, IAssignView;
/// <summary>
/// 赋值表达式(可update)
/// </summary>
public interface IAssignView : ISqlEntity;

