using ShadowSql.Fragments;

namespace ShadowSql.Logics;

/// <summary>
/// 逻辑条件
/// </summary>
public interface ISqlLogic : ISqlFragment
{
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <returns></returns>
    ISqlLogic Not();
}
/// <summary>
/// 与逻辑标记
/// </summary>
public interface IAndLogic { }
/// <summary>
/// 或逻辑标记
/// </summary>
public interface IOrLogic { }
