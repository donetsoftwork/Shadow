using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 别名字段信息
/// </summary>
public class AliasFieldInfo(ICompareView field, string alias)
     : VariantFieldInfoBase<ICompareView>(field), IFieldAlias
{
    #region 配置
    private readonly string _alias = alias;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _alias;
    #endregion
    string IView.ViewName
        => _alias;
    IColumn IFieldView.ToColumn()
        => Column.Use(_alias);
    IFieldAlias IFieldView.As(string alias)
        => new AliasFieldInfo(_target, alias);
    bool IMatch.IsMatch(string name)
         => Identifier.Match(name, _alias);
    #region ISqlEntity
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        _target.Write(engine, sql);
        engine.ColumnAs(sql, _alias);
    }
    #endregion
}
