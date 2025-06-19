using ShadowSql.Assigns;
using ShadowSql.Identifiers;
using ShadowSql.SqlVales;

namespace ShadowSql;

/// <summary>
/// 赋值扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region 参数
    /// <summary>
    /// 等于
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static AssignOperation Assign(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.Assign, Parameter.Use(parameter, field));
    /// <summary>
    /// 加上
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static AssignOperation AddAssign(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.AddAssign, Parameter.Use(parameter, field));
    /// <summary>
    /// 减去
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static AssignOperation SubAssign(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.SubAssign, Parameter.Use(parameter, field));

    /// <summary>
    /// 乘上
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static AssignOperation MulAssign(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.MulAssign, Parameter.Use(parameter, field));
    /// <summary>
    /// 除去
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static AssignOperation DivAssign(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.DivAssign, Parameter.Use(parameter, field));
    /// <summary>
    /// 取模
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static AssignOperation ModAssign(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.ModAssign, Parameter.Use(parameter, field));
    /// <summary>
    /// 位与
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static AssignOperation AndAssign(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.AndAssign, Parameter.Use(parameter, field));
    /// <summary>
    /// 位或
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static AssignOperation OrAssign(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.OrAssign, Parameter.Use(parameter, field));
    /// <summary>
    /// 位异或
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static AssignOperation XorAssign(this IAssignView field, string parameter = "")
        => new(field, AssignSymbol.XorAssign, Parameter.Use(parameter, field));
    #endregion
    #region 数据库值
    /// <summary>
    /// 等于
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AssignOperation Assign(this IAssignView field, ICompareView right)
        => new(field, AssignSymbol.Assign, right);
    /// <summary>
    /// 加上
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AssignOperation AddAssign(this IAssignView field, ICompareView right)
        => new(field, AssignSymbol.AddAssign, right);
    /// <summary>
    /// 减去
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AssignOperation SubAssign(this IAssignView field, ICompareView right)
        => new(field, AssignSymbol.SubAssign, right);
    /// <summary>
    /// 乘上
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AssignOperation MulAssign(this IAssignView field, ICompareView right)
        => new(field, AssignSymbol.MulAssign, right);
    /// <summary>
    /// 除去
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AssignOperation DivAssign(this IAssignView field, ICompareView right)
        => new(field, AssignSymbol.DivAssign, right);
    /// <summary>
    /// 取模
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AssignOperation ModAssign(this IAssignView field, ICompareView right)
        => new(field, AssignSymbol.ModAssign, right);
    /// <summary>
    /// 位与
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AssignOperation AndAssign(this IAssignView field, ICompareView right)
        => new(field, AssignSymbol.AndAssign, right);
    /// <summary>
    /// 位或
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AssignOperation OrAssign(this IAssignView field, ICompareView right)
        => new(field, AssignSymbol.OrAssign, right);
    /// <summary>
    /// 位异或
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AssignOperation XorAssign(this IAssignView field, ICompareView right)
        => new(field, AssignSymbol.XorAssign, right);
    #endregion
    #region 值
    /// <summary>
    /// 等于
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static AssignOperation AssignValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.Assign, SqlValue.From(value));
    /// <summary>
    /// 加上
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static AssignOperation AddValue<TValue>(this IAssignView field, TValue value)
    => new(field, AssignSymbol.AddAssign, SqlValue.From(value));
    /// <summary>
    /// 减去
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static AssignOperation SubValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.SubAssign, SqlValue.From(value));
    /// <summary>
    /// 乘上
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static AssignOperation MulValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.MulAssign, SqlValue.From(value));
    /// <summary>
    /// 除去
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static AssignOperation DivValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.DivAssign, SqlValue.From(value));
    /// <summary>
    /// 取模
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static AssignOperation ModValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.ModAssign, SqlValue.From(value));
    /// <summary>
    /// 位与
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static AssignOperation AndValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.AndAssign, SqlValue.From(value));
    /// <summary>
    /// 位或
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static AssignOperation OrValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.OrAssign, SqlValue.From(value));
    /// <summary>
    /// 位异或
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static AssignOperation XorValue<TValue>(this IAssignView field, TValue value)
        => new(field, AssignSymbol.XorAssign, SqlValue.From(value));
    #endregion
}
