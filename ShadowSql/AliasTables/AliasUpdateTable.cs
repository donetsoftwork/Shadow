using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.AliasTables;

/// <summary>
/// 别名修改表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="aliasTable">别名表</param>
public class AliasUpdateTable<TTable>(IAliasTable<TTable> aliasTable)
    : IUpdateTable
    where TTable : ITable
{
    #region 配置
    private readonly IAliasTable<TTable> _source = aliasTable;
    /// <summary>
    /// 原别名表
    /// </summary>
    public IAliasTable<TTable> Source
        => _source;
    private readonly TTable _table = aliasTable.Target;
    /// <summary>
    /// 
    /// </summary>
    public TTable Table
        => _table;

    private readonly List<IPrefixField> _assignFields = [.. aliasTable.GetAssignFields()];
    /// <summary>
    /// 所有更新字段
    /// </summary>
    public IEnumerable<IAssignView> AssignFields
        => _assignFields;
    private readonly string _alias = aliasTable.Alias;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _alias;
    #endregion
    /// <summary>
    /// 获取更新字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    public IAssignView? GetAssignField(string fieldName)
    {
        foreach (var assignField in _assignFields)
        {
            if (assignField.IsMatch(fieldName))
                return assignField;
        }
        if (_table.GetAssignField(fieldName) is IColumn column)
            return _source.NewPrefixField(column);
        return null;
    }
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
}
