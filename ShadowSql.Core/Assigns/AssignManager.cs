using System.Collections.Generic;

namespace ShadowSql.Assigns;

/// <summary>
/// 赋值运算符管理
/// </summary>
public class AssignManager
{
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
    internal AssignManager(
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
        _equal = TryAdd(equal);
        _add = TryAdd(add);
        _sub = TryAdd(sub);
        _mul = TryAdd(mul);
        _div = TryAdd(div);
        _mod = TryAdd(mod);
        _and = TryAdd(and);
        _or = TryAdd(or);
        _xor = TryAdd(xor);

    }
    private readonly AssignSymbol _equal;
    private readonly AssignSymbol _add;
    private readonly AssignSymbol _sub;
    private readonly AssignSymbol _mul;
    private readonly AssignSymbol _div;
    private readonly AssignSymbol _mod;
    private readonly AssignSymbol _and;
    private readonly AssignSymbol _or;
    private readonly AssignSymbol _xor;
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

    private readonly Dictionary<string, AssignSymbol> _operations = [];
    private AssignSymbol TryAdd(AssignSymbol compare)
    {
        TryAdd(compare.Operation, compare);
        return compare;
    }
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="compare"></param>
    private void TryAdd(string operation, AssignSymbol compare)
    {
        if (_operations.ContainsKey(operation))
            return;
        _operations.Add(operation, compare);
    }
    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public AssignSymbol Get(string operation)
    {
        return _operations[operation];
    }
}
