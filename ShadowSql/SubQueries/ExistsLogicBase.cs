using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SubQueries;

/// <summary>
/// 子查询逻辑(EXISTS/NOT EXISTS)基类
/// </summary>
/// <param name="source"></param>
/// <param name="op"></param>
public abstract class ExistsLogicBase(ITableView source, CompareSymbol op)
    : SubLogicBase(op), ISelect
{
    #region 配置
    /// <summary>
    /// 数据源表(视图)
    /// </summary>
    protected readonly ITableView _source = source;
    /// <summary>
    /// 数据源表(视图)
    /// </summary>
    public ITableView Source
        => _source;
    ///// <summary>
    ///// 右侧子查询字段按*自行拼写
    ///// </summary>
    //public override ISelect Select
    //    => this;
    #endregion
    /// <summary>
    /// 拼写右侧子查询
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSub(ISqlEngine engine, StringBuilder sql)
        => engine.Select(sql, this);
    #region ISelect
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append('*');
        return true;
    }
    IEnumerable<IFieldView> ISelectFields.Selected
        => [];
    #endregion
}
