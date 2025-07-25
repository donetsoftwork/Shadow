using ShadowSql.Assigns;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Linq;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 多表(联表)修改
/// </summary>
/// <param name="multiTable">多表(联表)</param>
public class MultiTableUpdate(IMultiView multiTable)
    : UpdateBase, IUpdate
{
    #region 配置
    /// <summary>
    /// 被删除的表
    /// </summary>
    internal IAliasTable<ITable>? _table;
    /// <summary>
    /// 被删除的表
    /// </summary>
    public IAliasTable<ITable> Table
        => CheckTable();
    private readonly IMultiView _multiTable = multiTable;
    /// <summary>
    /// 多表(联表)视图
    /// </summary>
    public IMultiView MultiTable 
        => _multiTable;
    IUpdateTable IUpdate.Table
        => Table.Target;
    #endregion
    internal IAliasTable<ITable> CheckTable()
    {
        if (_table != null)
            return _table;
        if (_multiTable.Tables.First() is IAliasTable<ITable> first)
            return _table = first;
        throw new ArgumentException("被修改的表不存在", nameof(Table));
    }
    #region UpdateBase
    /// <inheritdoc/>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(" FROM ");
        _multiTable.Write(engine, sql);
    }
    /// <inheritdoc/>
    /// <exception cref="ArgumentException"></exception>
    internal override IAssignView GetAssignField(string fieldName)
        => CheckTable()
            .GetAssignField(fieldName)
        ?? throw new ArgumentException(fieldName + "字段不存在", nameof(fieldName));
    #endregion
    /// <summary>
    /// 修改
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="operation">操作</param>
    /// <returns></returns>
    public MultiTableUpdate Set<TAliasTable>(Func<TAliasTable, IAssignInfo> operation)
        where TAliasTable : IAliasTable
    {
        SetCore(operation((TAliasTable)CheckTable()));        
        return this;
    }
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteUpdate(ISqlEngine engine, StringBuilder sql)
    {
        base.WriteUpdate(engine, sql);
        sql.Append(CheckTable().Alias);
    }
    #endregion
}
