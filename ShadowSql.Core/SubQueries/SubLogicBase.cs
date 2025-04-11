using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.SubQueries;

/// <summary>
/// 子查询逻辑(EXISTS/NOT EXISTS)
/// </summary>
/// <param name="op"></param>
public abstract class SubLogicBase(CompareSymbol op)
    : AtomicLogic
{
    #region 配置
    private readonly CompareSymbol _operation = op;

    /// <summary>
    /// 运算符
    /// </summary>
    public CompareSymbol Operation
        => _operation;
    #endregion
    #region Write
    /// <summary>
    /// 拼写右侧子查询
    /// </summary>
    protected abstract void WriteSub(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        _operation.Write(engine, sql);
        sql.Append('(');
        WriteSub(engine, sql);
        sql.Append(')');
    }
    #endregion
    #region AtomicLogic
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        Write(engine, sql);
        return true;
    }
    #endregion
}
