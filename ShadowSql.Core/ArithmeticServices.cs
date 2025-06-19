using ShadowSql.Arithmetic;
using ShadowSql.Identifiers;

namespace ShadowSql;

/// <summary>
/// 算术运算扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    /// <summary>
    /// 加
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static ArithmeticView Add(this ICompareView left, ICompareView right)
        => new(left, ArithmeticSymbol.Add, right);
    /// <summary>
    /// 减
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static ArithmeticView Sub(this ICompareView left, ICompareView right)
        => new(left, ArithmeticSymbol.Sub, right);
    /// <summary>
    /// 乘
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static ArithmeticView Mul(this ICompareView left, ICompareView right)
        => new(left, ArithmeticSymbol.Mul, right);
    /// <summary>
    /// 除
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static ArithmeticView Div(this ICompareView left, ICompareView right)
        => new(left, ArithmeticSymbol.Div, right);
    /// <summary>
    /// 取模
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static ArithmeticView Mod(this ICompareView left, ICompareView right)
        => new(left, ArithmeticSymbol.Mod, right);
    /// <summary>
    /// 位与
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static ArithmeticView And(this ICompareView left, ICompareView right)
        => new(left, ArithmeticSymbol.And, right);
    /// <summary>
    /// 位或
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static ArithmeticView Or(this ICompareView left, ICompareView right)
        => new(left, ArithmeticSymbol.Or, right);
    /// <summary>
    /// 异或
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static ArithmeticView Xor(this ICompareView left, ICompareView right)
        => new(left, ArithmeticSymbol.Xor, right);
}
