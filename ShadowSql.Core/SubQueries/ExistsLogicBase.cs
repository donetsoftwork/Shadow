using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.SubQueries;

/// <summary>
/// 子查询逻辑(EXISTS/NOT EXISTS)基类
/// </summary>
/// <param name="source"></param>
/// <param name="op">操作</param>
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
    /// <inheritdoc/>
    protected override void WriteSub(ISqlEngine engine, StringBuilder sql)
        => engine.Select(sql, this);
    #region ISelect
    /// <inheritdoc/>
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append('*');
        return true;
    }
    /// <inheritdoc/>
    IEnumerable<IColumn> ISelectFields.ToColumns()
        => _source.Fields.Select(f => f.ToColumn());
    /// <inheritdoc/>
    IEnumerable<IFieldView> ISelectFields.Selected
        => _source.Fields;
    #endregion
}
