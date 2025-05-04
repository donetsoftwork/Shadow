using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Linq;
using System.Text;

namespace ShadowSql.Delete;

/// <summary>
/// 多表(联表)数据删除
/// </summary>
/// <param name="multiTable"></param>
/// <param name="target"></param>
public class MultiTableDelete(IMultiView multiTable, IAliasTable target)
    : IDelete
{
    /// <summary>
    /// 多表(联表)数据删除
    /// </summary>
    /// <param name="multiTable"></param>
    public MultiTableDelete(IMultiView multiTable)
        : this(multiTable, multiTable.Tables.First())
    { 
    }
    #region 配置
    private readonly IMultiView _multiTable = multiTable;
    internal IAliasTable _source = target;
    /// <summary>
    /// 被删除表
    /// </summary>
    public IAliasTable Source
        => _source;
    /// <summary>
    /// 多表(联表)
    /// </summary>
    public IMultiView MultiTable
        => _multiTable;
    #endregion
    #region IDelete
    ITableView IDelete.Source
        => _source;
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.DeletePrefix(sql);
        sql.Append(_source.Alias);
        sql.Append(" FROM ");
        _multiTable.Write(engine, sql);
    }
    #endregion
}
