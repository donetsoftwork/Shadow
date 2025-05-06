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
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
        => sql.Append('=');
    #region Manager
    /// <summary>
    /// 
    /// </summary>
    public virtual string Operation
        => "=";

    private static readonly AssignSymbol _equalTo = new();
    /// <summary>
    /// 等于
    /// </summary>
    public static AssignSymbol EqualTo => _equalTo;
    /// <summary>
    /// 加上并赋值
    /// </summary>
    public static AssignSymbol Add => _manager.Value.Add;
    /// <summary>
    /// 减去并赋值
    /// </summary>
    public static AssignSymbol Sub => _manager.Value.Sub;
    /// <summary>
    /// 乘上并赋值
    /// </summary>
    public static AssignSymbol Mul => _manager.Value.Mul;
    /// <summary>
    /// 除去并赋值
    /// </summary>
    public static AssignSymbol Div => _manager.Value.Div;
    /// <summary>
    /// 取模并赋值
    /// </summary>
    public static AssignSymbol Mod => _manager.Value.Mod;
    /// <summary>
    /// “位与”并赋值
    /// </summary>
    public static AssignSymbol And => _manager.Value.And;
    /// <summary>
    /// “位或”并赋值
    /// </summary>
    public static AssignSymbol Or => _manager.Value.Or;
    /// <summary>
    /// “异或”并赋值
    /// </summary>
    public static AssignSymbol Xor => _manager.Value.Xor;
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
        return new AssignManager(_equalTo
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
    /// <param name="operation"></param>
    /// <returns></returns>
    public static AssignSymbol Get(string operation)
        => _manager.Value.Get(operation);
    #endregion

    class ComplexSymbol(string operation) : AssignSymbol
    {
        private readonly string _operation = operation;
        /// <summary>
        /// 符号
        /// </summary>
        public override string Operation
            => _operation;
        /// <summary>
        /// 拼写sql
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="sql"></param>
        public override void Write(ISqlEngine engine, StringBuilder sql)
        {
            sql.Append(_operation);
        }
    }
}
