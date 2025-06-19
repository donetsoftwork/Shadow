using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.SingleSelect;
using System.Text;

namespace ShadowSql.SubQueries;

/// <summary>
/// 字段子查询逻辑(IN/NOT IN)基类
/// </summary>
/// <param name="field">字段</param>
/// <param name="op">操作</param>
/// <param name="select">筛选</param>
public abstract class FieldSubLogicBase(ICompareView field, CompareSymbol op, ISingleSelect select)
    : SubLogicBase(op)
{
    #region 配置
    /// <summary>
    /// 左边字段
    /// </summary>
    protected readonly ICompareView _field = field;
    /// <summary>
    /// 筛选单列
    /// </summary>
    protected readonly ISingleSelect _singleSelect = select;

    /// <summary>
    /// 左边字段
    /// </summary>
    public ICompareView Field
        => _field;
    #endregion
    #region IAtomicLogic
    /// <summary>
    /// 拼写右侧子查询
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    protected override void WriteSub(ISqlEngine engine, StringBuilder sql)
        => _singleSelect.Write(engine, sql);
    /// <inheritdoc/>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        _field.Write(engine, sql);
        base.Write(engine, sql);
    }
    #endregion
}
