using ShadowSql.Engines;
using ShadowSql.Queries;
using System.Text;

namespace ShadowSql.Logics;

/// <summary>
/// 空逻辑
/// </summary>
public sealed class EmptyLogic : ISqlLogic
{
    private EmptyLogic()
    {
    }
    /// <summary>
    /// 单例
    /// </summary>
    public readonly static EmptyLogic Instance = new();

    #region ISqlLogic
    /// <inheritdoc/>
    public bool TryWrite(ISqlEngine engine, StringBuilder sql)
        => false;
    /// <inheritdoc/>
    ISqlLogic ISqlLogic.Not()
        => this;
    /// <summary>
    /// not Empty还为Empty
    /// </summary>
    /// <returns></returns>
    public EmptyLogic Not()
        => this;
    #endregion
    #region 运算符重载
    #region And
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static EmptyLogic operator &(EmptyLogic logic, EmptyLogic other)
        => logic;
    #region AtomicLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AtomicLogic operator &(EmptyLogic logic, AtomicLogic other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static AtomicLogic operator &(AtomicLogic other, EmptyLogic logic)
        => other;
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(EmptyLogic logic, IAndLogic other)
    //    => other;
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IAndLogic other, EmptyLogic logic)
    //    => other;
    //#endregion
    #region AndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AndLogic operator &(EmptyLogic logic, AndLogic other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static AndLogic operator &(AndLogic other, EmptyLogic logic)
        => other;
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(EmptyLogic logic, ComplexAndLogic other)
        => other;
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static ComplexAndLogic operator &(ComplexAndLogic other, EmptyLogic logic)
        => other;
    #endregion
    //#region SqlAndQuery
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static SqlAndQuery operator &(EmptyLogic logic, SqlAndQuery other)
    //    => other;
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static SqlAndQuery operator &(SqlAndQuery other, EmptyLogic logic)
    //    => other;
    //#endregion
    //#region IOrLogic
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IAndLogic operator &(EmptyLogic logic, IOrLogic other)
    //    => other.ToAnd();
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static IAndLogic operator &(IOrLogic other, EmptyLogic logic)
    //  => other.ToAnd();
    //#endregion
    #region OrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(EmptyLogic logic, OrLogic other)
        => other.ToAndCore();
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static Logic operator &(OrLogic other, EmptyLogic logic)
      => other.ToAndCore();
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator &(EmptyLogic logic, ComplexOrLogic other)
        => other.ToAndCore();
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static Logic operator &(ComplexOrLogic other, EmptyLogic logic)
      => other.ToAndCore();
    #endregion
    //#region SqlOrQuery
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static SqlAndQuery operator &(EmptyLogic logic, SqlOrQuery other)
    //    => other.ToAnd();
    ///// <summary>
    ///// 与逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static SqlAndQuery operator &(SqlOrQuery other, EmptyLogic logic)
    //  => other.ToAnd();
    //#endregion
    #endregion
    #region Or
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static EmptyLogic operator |(EmptyLogic logic, EmptyLogic other)
        => logic;
    #region AtomicLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static AtomicLogic operator |(EmptyLogic logic, AtomicLogic other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static AtomicLogic operator |(AtomicLogic other, EmptyLogic logic)
        => other;
    #endregion
    //#region IAndLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(EmptyLogic logic, IAndLogic other)
    //    => other.ToOr();
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static IOrLogic operator |(IAndLogic other, EmptyLogic logic)
    //    => other.ToOr();
    //#endregion
    #region AndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(EmptyLogic logic, AndLogic other)
        => other.ToOrCore();
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static Logic operator |(AndLogic other, EmptyLogic logic)
        => other.ToOrCore();
    #endregion
    #region ComplexAndLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Logic operator |(EmptyLogic logic, ComplexAndLogic other)
        => other.ToOrCore();
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static Logic operator |(ComplexAndLogic other, EmptyLogic logic)
        => other.ToOrCore();
    #endregion
    //#region SqlAndQuery
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static SqlOrQuery operator |(EmptyLogic logic, SqlAndQuery other)
    //    => other.ToOr();
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static SqlOrQuery operator |(SqlAndQuery other, EmptyLogic logic)
    //    => other.ToOr();
    //#endregion
    //#region IOrLogic
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static IOrLogic operator |(EmptyLogic logic, IOrLogic other)
    //    => other;
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static IOrLogic operator |(IOrLogic other, EmptyLogic logic)
    //    => other;
    //#endregion
    #region OrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static OrLogic operator |(EmptyLogic logic, OrLogic other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static OrLogic operator |(OrLogic other, EmptyLogic logic)
        => other;
    #endregion
    #region ComplexOrLogic
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(EmptyLogic logic, ComplexOrLogic other)
        => other;
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="other"></param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static ComplexOrLogic operator |(ComplexOrLogic other, EmptyLogic logic)
        => other;
    #endregion
    //#region SqlOrQuery
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="logic">查询逻辑</param>
    ///// <param name="other"></param>
    ///// <returns></returns>
    //public static SqlOrQuery operator |(EmptyLogic logic, SqlOrQuery other)
    //    => other;
    ///// <summary>
    ///// 或逻辑
    ///// </summary>
    ///// <param name="other"></param>
    ///// <param name="logic">查询逻辑</param>
    ///// <returns></returns>
    //public static SqlOrQuery operator |(SqlOrQuery other, EmptyLogic logic)
    //    => other;
    //#endregion
    #endregion
    #region Not
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public static EmptyLogic operator !(EmptyLogic logic)
        => logic;
    #endregion
    #endregion
}
