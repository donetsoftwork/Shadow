using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Arithmetic;

/// <summary>
/// 计算视图
/// </summary>
/// <param name="left">左</param>
/// <param name="op">操作</param>
/// <param name="right">右</param>
public class ArithmeticView(ICompareView left, ArithmeticSymbol op, ICompareView right)
    : ICompareView
{
    #region 配置
    private readonly ICompareView _left = left;
    private readonly ArithmeticSymbol _op = op;
    private readonly ICompareView _right = right;

    /// <summary>
    /// 左
    /// </summary>
    public ICompareView Left 
        => _left;
    /// <summary>
    /// 操作符
    /// </summary>
    public ArithmeticSymbol Op 
        => _op;
    /// <summary>
    /// 右
    /// </summary>
    public ICompareView Right 
        => _right;
    #endregion
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append('(');
        _left.Write(engine, sql);
        _op.WriteCore(engine, sql);
        _right.Write(engine, sql);
        sql.Append(')');
    }
}
