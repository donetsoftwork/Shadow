using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Linq;
using System.Text;

namespace ShadowSql.Aggregates;

/// <summary>
/// 去重字段统计别名
/// </summary>
public class DistinctCountAliasFieldInfo
    : DistinctCountFieldInfoBase, IDistinctCountFieldAlias
{
    /// <summary>
    /// 按字段去重统计字段信息
    /// </summary>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    public DistinctCountAliasFieldInfo(IField field, string alias/* = "Count"*/)
        : this(alias, field)
    {
    }
    /// <summary>
    /// 按列去重统计字段信息
    /// </summary>
    /// <param name="column"></param>
    /// <param name="alias"></param>
    public DistinctCountAliasFieldInfo(IColumn column, string alias/* = "Count"*/)
        : this(alias, column)
    {
    }
    private DistinctCountAliasFieldInfo(string alias, IFieldView target)
        : base(target)
    {
        _alias = alias;
    }
    #region 配置
    private readonly string _alias;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _alias;
    #endregion

    #region IFieldView
    string IView.ViewName
        => _alias;

    IColumn IFieldView.ToColumn()
        => Column.Use(_alias);
    IFieldAlias IFieldView.As(string alias)
        => new DistinctCountAliasFieldInfo(alias, _target);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        base.Write(engine, sql);
        engine.ColumnAs(sql, _alias);
    }
    /// <summary>
    /// 匹配
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsMatch(string name)
        => Identifier.Match(name, _alias);
    #endregion
}
