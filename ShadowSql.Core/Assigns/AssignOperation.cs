using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Assigns;

/// <summary>
/// 赋值操作
/// </summary>
public class AssignOperation(IAssignView column, AssignSymbol assign, ICompareView value)
    : IAssignOperation
{
    #region 配置
    private readonly IAssignView _column = column;    
    private readonly AssignSymbol _assign = assign;
    private readonly ICompareView _value = value;

    /// <summary>
    /// 左边列
    /// </summary>
    public IAssignView Column
        => _column;
    /// <summary>
    /// 右边值(也可以是列)
    /// </summary>
    public ICompareView Value
        => _value;
    /// <summary>
    /// 赋值操作符(默认Equal)
    /// </summary>
    public AssignSymbol Assign 
        => _assign;
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    public void Write(ISqlEngine engine, StringBuilder sql)
    {
        _column.Write(engine, sql);
        // { += | -= | *= | /= | %= | &= } | ^= | |=
        // 复合赋值运算符：
        // += 相加并赋值
        // -= 相减并赋值
        // *= 相乘并赋值
        // /= 相除并赋值
        // %= 取模并赋值
        _assign.Write(engine, sql);
        _value.Write(engine, sql);        
    }
    #endregion
}
