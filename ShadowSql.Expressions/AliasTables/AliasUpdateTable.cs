using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Expressions.AliasTables;

/// <summary>
/// 别名修改表
/// </summary>
/// <param name="source"></param>
public class AliasUpdateTable(IAliasTable<ITable> source)
    : IUpdateTable
{
    #region 配置
    internal readonly IAliasTable<ITable> _source = source;
    /// <summary>
    /// 原别名表
    /// </summary>
    public IAliasTable<ITable> Source
        => _source;
    private readonly ITable _table = source.Target;
    /// <summary>
    /// 源表
    /// </summary>
    public ITable Table
        => _table;

    private readonly List<IPrefixField> _assignFields = [.. source.GetAssignFields()];
    /// <summary>
    /// 所有更新字段
    /// </summary>
    public IEnumerable<IAssignView> AssignFields
        => _assignFields;
    private readonly string _alias = source.Alias;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _alias;
    #endregion
    /// <summary>
    /// 获取更新字段
    /// </summary>
    /// <param name="fieldName"></param>
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

    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
}
