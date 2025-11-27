using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.SubQueries;

/// <summary>
/// 子查询逻辑(EXISTS/NOT EXISTS)
/// </summary>
/// <param name="op">操作</param>
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
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
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
    /// <inheritdoc/>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        Write(engine, sql);
        return true;
    }
    #endregion
}
