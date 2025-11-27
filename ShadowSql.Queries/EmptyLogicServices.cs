using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// 空逻辑扩展方法
/// (与EmptyLogic重载运算符一一对应)
/// </summary>
public static partial class ShadowSqlQueryServices
{
    #region And
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic And(this EmptyLogic _, ComplexAndLogic other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static ComplexAndLogic And(this ComplexAndLogic other, EmptyLogic _)
        => other;
    #endregion
    #region SqlAndQuery
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static SqlAndQuery And(this EmptyLogic _, SqlAndQuery other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static SqlAndQuery And(this SqlAndQuery other, EmptyLogic _)
        => other;
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic And(this EmptyLogic _, ComplexOrLogic other)
        => other.ToAndCore();
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static Logic And(this ComplexOrLogic other, EmptyLogic _)
      => other.ToAndCore();
    #endregion
    #region SqlOrQuery
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static SqlAndQuery And(this EmptyLogic _, SqlOrQuery other)
        => other.ToAnd();
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static SqlAndQuery And(this SqlOrQuery other, EmptyLogic _)
      => other.ToAnd();
    #endregion
    #endregion
    #region Or
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic Or(this EmptyLogic _, ComplexAndLogic other)
        => other.ToOrCore();
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static Logic Or(this ComplexAndLogic other, EmptyLogic _)
        => other.ToOrCore();
    #endregion
    #region SqlAndQuery
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static SqlOrQuery Or(this EmptyLogic _, SqlAndQuery other)
        => other.ToOr();
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static SqlOrQuery Or(this SqlAndQuery other, EmptyLogic _)
        => other.ToOr();
    #endregion
    #region IOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static IOrLogic Or(this EmptyLogic _, IOrLogic other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static IOrLogic Or(this IOrLogic other, EmptyLogic _)
        => other;
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic Or(this EmptyLogic _, ComplexOrLogic other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static ComplexOrLogic Or(this ComplexOrLogic other, EmptyLogic _)
        => other;
    #endregion
    #region SqlOrQuery
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="_">空逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static SqlOrQuery Or(this EmptyLogic _, SqlOrQuery other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="_">空逻辑</param>
    /// <returns></returns>
    public static SqlOrQuery Or(this SqlOrQuery other, EmptyLogic _)
        => other;
    #endregion
    #endregion
}
