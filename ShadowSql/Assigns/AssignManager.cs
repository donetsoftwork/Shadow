namespace ShadowSql.Assigns;

/// <summary>
/// 赋值运算符管理
/// </summary>
/// <param name="equal"></param>
/// <param name="add"></param>
/// <param name="sub"></param>
/// <param name="mul"></param>
/// <param name="div"></param>
/// <param name="mod"></param>
/// <param name="and"></param>
/// <param name="or"></param>
/// <param name="xor"></param>
public class AssignManager(
    AssignSymbol equal
    , AssignSymbol add
    , AssignSymbol sub
    , AssignSymbol mul
    , AssignSymbol div
    , AssignSymbol mod
    , AssignSymbol and
    , AssignSymbol or
    , AssignSymbol xor
    )
{
    private readonly AssignSymbol _equal = equal;
    private readonly AssignSymbol _add = add;
    private readonly AssignSymbol _sub = sub;
    private readonly AssignSymbol _mul = mul;
    private readonly AssignSymbol _div = div;
    private readonly AssignSymbol _mod = mod;
    private readonly AssignSymbol _and = and;
    private readonly AssignSymbol _or = or;
    private readonly AssignSymbol _xor = xor;
    /// <summary>
    /// 赋值
    /// </summary>
    public AssignSymbol Equal => _equal;
    /// <summary>
    /// 相加并赋值
    /// </summary>
    public AssignSymbol Add => _add;
    /// <summary>
    /// 相减并赋值
    /// </summary>
    public AssignSymbol Sub => _sub;
    /// <summary>
    /// 相乘并赋值
    /// </summary>
    public AssignSymbol Mul => _mul;
    /// <summary>
    /// 相除并赋值
    /// </summary>
    public AssignSymbol Div => _div;
    /// <summary>
    /// 取模并赋值
    /// </summary>
    public AssignSymbol Mod => _mod;
    /// <summary>
    /// “位与”并赋值
    /// </summary>
    public AssignSymbol And => _and;
    /// <summary>
    /// “位或”并赋值
    /// </summary>
    public AssignSymbol Or => _or;
    /// <summary>
    /// “位异或”并赋值
    /// </summary>
    public AssignSymbol Xor => _xor;
}
