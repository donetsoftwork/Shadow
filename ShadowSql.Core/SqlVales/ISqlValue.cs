using ShadowSql.Identifiers;

namespace ShadowSql.SqlVales;

/// <summary>
/// 数据库的值
/// </summary>
public interface ISqlValue : ICompareView;
/// <summary>
/// 参数标识
/// </summary>
public interface IParameter : IIdentifier, ISqlValue;
