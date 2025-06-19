using ShadowSql.Engines;
using ShadowSql.Fragments;
using System;
using System.Text;

namespace ShadowSql.Assigns;

/// <summary>
/// 赋值运算符
/// </summary>
public class AssignSymbol : ISqlEntity
{
    /// <summary>
    /// 赋值运算符
    /// </summary>
    protected AssignSymbol()
    {
    }
    /// <inheritdoc/>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
        => sql.Append('=');
    #region Manager
    /// <summary>
    /// 符号
    /// </summary>
    public virtual string Operation
        => "=";

    private static readonly AssignSymbol _assign = new();
    /// <summary>
    /// 等于
    /// </summary>
    public static AssignSymbol Assign => _assign;
    /// <summary>
    /// 加上并赋值
    /// </summary>
    public static AssignSymbol AddAssign => _manager.Value.Add;
    /// <summary>
    /// 减去并赋值
    /// </summary>
    public static AssignSymbol SubAssign => _manager.Value.Sub;
    /// <summary>
    /// 乘上并赋值
    /// </summary>
    public static AssignSymbol MulAssign => _manager.Value.Mul;
    /// <summary>
    /// 除去并赋值
    /// </summary>
    public static AssignSymbol DivAssign => _manager.Value.Div;
    /// <summary>
    /// 取模并赋值
    /// </summary>
    public static AssignSymbol ModAssign => _manager.Value.Mod;
    /// <summary>
    /// “位与”并赋值
    /// </summary>
    public static AssignSymbol AndAssign => _manager.Value.And;
    /// <summary>
    /// “位或”并赋值
    /// </summary>
    public static AssignSymbol OrAssign => _manager.Value.Or;
    /// <summary>
    /// “异或”并赋值
    /// </summary>
    public static AssignSymbol XorAssign => _manager.Value.Xor;
    /// <summary>
    /// 操作符管理
    /// </summary>
    public static AssignManager Manager
        => _manager.Value;

    /// <summary>
    /// 操作符管理
    /// </summary>
    private readonly static Lazy<AssignManager> _manager = new(CreateManager);
    private static AssignManager CreateManager()
    {
        //  += | -= | *= | /= | %= | &= | |= | ^=
        return new AssignManager(_assign
            , new ComplexSymbol("+=")
            , new ComplexSymbol("-=")
            , new ComplexSymbol("*=")
            , new ComplexSymbol("/=")
            , new ComplexSymbol("%=")
            , new ComplexSymbol("&=")
            , new ComplexSymbol("|=")
            , new ComplexSymbol("^=")
            );
    }
    /// <summary>
    /// 获取操作符
    /// </summary>
    /// <param name="operation">操作</param>
    /// <returns></returns>
    public static AssignSymbol Get(string operation)
        => _manager.Value.Get(operation);
    #endregion

    /// <inheritdoc />
    class ComplexSymbol(string operation) : AssignSymbol
    {
        private readonly string _operation = operation;
        /// <inheritdoc/>
        public override string Operation
            => _operation;
        /// <inheritdoc/>
        public override void Write(ISqlEngine engine, StringBuilder sql)
        {
            sql.Append(_operation);
        }
    }
}
