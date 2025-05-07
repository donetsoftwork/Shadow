using ShadowSql.Identifiers;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 别名表字段信息
/// </summary>
/// <param name="aliasTable"></param>
/// <param name="name"></param>
public class AliasTableFieldInfo(IAliasTable aliasTable, string name)
    : TableFieldInfo(aliasTable.Alias, name), IFieldView, IPrefixField
{
    #region 配置
    private readonly IAliasTable _aliasTable = aliasTable;
    /// <summary>
    /// 别名表
    /// </summary>
    public IAliasTable AliasTable
        => _aliasTable;
    #endregion
    /// <summary>
    /// 获取前缀字段
    /// </summary>
    /// <returns></returns>
    public IPrefixField? GetPrefixField()
        => _aliasTable.GetPrefixField(_name);
    IColumn IFieldView.ToColumn()
        => Column.Use(_name);
}
