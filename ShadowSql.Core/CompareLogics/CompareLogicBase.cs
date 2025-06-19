using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.CompareLogics;

/// <summary>
/// 字段操作基类
/// </summary>
/// <param name="field">字段</param>
/// <param name="op">操作</param>
public abstract class CompareLogicBase(ICompareView field, CompareSymbol op)
    : AtomicLogic/*, ICompareLogic*/
{
    #region 配置
    /// <summary>
    /// 字段
    /// </summary>
    protected readonly ICompareView _field = field;
    /// <summary>
    /// 运算符
    /// </summary>
    protected readonly CompareSymbol _operation = op;

    /// <summary>
    /// 左边字段
    /// </summary>
    public ICompareView Field
        => _field;
    /// <summary>
    /// 运算符
    /// </summary>
    public CompareSymbol Operation
        => _operation;
    #endregion
}
