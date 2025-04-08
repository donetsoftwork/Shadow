using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Linq;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 多表(联表)修改
/// </summary>
/// <param name="multiTable"></param>
/// <param name="target"></param>
public class MultiTableUpdate(IMultiView multiTable, IAliasTable target)
    : UpdateBase<IAliasTable>(target)
{
    /// <summary>
    /// 多表(联表)修改
    /// </summary>
    /// <param name="multiTable"></param>
    public MultiTableUpdate(IMultiView multiTable)
        : this(multiTable, multiTable.Tables.First())
    {
    }
    #region 配置
    private readonly IMultiView _multiTable = multiTable;
    /// <summary>
    /// 多表(联表)视图
    /// </summary>
    public IMultiView MultiTable 
        => _multiTable;
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写Update子句
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteUpdate(ISqlEngine engine, StringBuilder sql)
    {
        base.WriteUpdate(engine, sql);
        sql.Append(_source.Alias);
    }
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(" FROM ");
        base.WriteSource(engine, sql);
    }
    #endregion
}
