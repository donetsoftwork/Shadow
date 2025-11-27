using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Variants;

/// <summary>
/// 别名字段信息
/// </summary>
/// <param name="original">原始字段</param>
/// <param name="aliasName">别名</param>
public class AliasFieldInfo(ICompareView original, string aliasName)
     : VariantFieldInfoBase<ICompareView>(original), IFieldAlias
{
    #region 配置
    private readonly string _alias = aliasName;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _alias;
    #endregion
    /// <inheritdoc/>
    string IView.ViewName
        => _alias;
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => Column.Use(_alias);
    ///// <inheritdoc/>
    //IFieldAlias IFieldView.As(string aliasName)
    //    => new AliasFieldInfo(_original, aliasName);
    /// <inheritdoc/>
    bool IMatch.IsMatch(string name)
         => Identifier.Match(name, _alias);
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        _original.Write(engine, sql);
        engine.ColumnAs(sql, _alias);
    }
    #endregion
}
