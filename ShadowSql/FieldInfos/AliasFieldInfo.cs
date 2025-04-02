using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 别名字段信息
/// </summary>
public class AliasFieldInfo
     : VariantFieldInfoBase, IFieldAlias
{
    #region 配置
    private readonly string _alias;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _alias;
    #endregion
    internal AliasFieldInfo(string alias, IFieldView target)
        : base(target)
    {
        _alias = alias;
    }
    /// <summary>
    /// 别名字段信息
    /// </summary>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    public AliasFieldInfo(IField field, string alias)
        : this(alias, field)
    {
    }
    /// <summary>
    /// 别名字段信息
    /// </summary>
    /// <param name="column"></param>
    /// <param name="alias"></param>
    public AliasFieldInfo(IColumn column, string alias)
        : this(alias, column)
    {
    }

    string IView.ViewName
        => _alias;
    IColumn IFieldView.ToColumn()
        => Column.Use(_alias);
    IFieldAlias IFieldView.As(string alias)
        => new AliasFieldInfo(alias, _target);
    bool IMatch.IsMatch(string name)
         => Identifier.Match(name, _alias);
    #region ISqlEntity
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void Write(ISqlEngine engine, StringBuilder sql)
    {
        _target.Write(engine, sql);
        engine.ColumnAs(sql, _alias);
    }
    #endregion
}
