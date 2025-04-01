using ShadowSql.Fragments;
using ShadowSql.Identifiers;

namespace ShadowSql.SqlVales;

/// <summary>
/// 数据库的值
/// </summary>
public interface ISqlValue : ISqlEntity;
/// <summary>
/// 参数标识
/// </summary>
public interface IParameter : IIdentifier, ISqlValue;
/// <summary>
/// 参数信息
/// </summary>
public interface IParameterInfo
{
    /// <summary>
    /// 构建参数
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    IParameter CreateParameter(IIdentifier identifier);
}

/// <summary>
/// 值信息
/// </summary>
public interface IValueInfo
{
    /// <summary>
    /// 值
    /// </summary>
    ISqlValue Value { get; }
}
