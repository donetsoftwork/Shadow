using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SingleSelect;
using System.Text;

namespace ShadowSql.SubQueries;

/// <summary>
/// 字段子查询逻辑(IN/NOT IN)基类
/// </summary>
/// <param name="field"></param>
/// <param name="op"></param>
/// <param name="select"></param>
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
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSub(ISqlEngine engine, StringBuilder sql)
        => _singleSelect.Write(engine, sql);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        _field.Write(engine, sql);
        base.Write(engine, sql);
    }
    #endregion
}
