using ShadowSql.Identifiers;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 别名表字段信息
/// </summary>
/// <param name="aliasTable"></param>
/// <param name="name"></param>
public class AliasTableFieldInfo(IAliasTable aliasTable, string name)
    : TableFieldInfo(aliasTable.Alias, name), IFieldView
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
    /// 获取前缀列
    /// </summary>
    /// <returns></returns>
    public IPrefixColumn? GetPrefixColumn()
        => _aliasTable.GetPrefixColumn(_name);
    IColumn IFieldView.ToColumn()
        => GetPrefixColumn()
        ?? Column.Use(_name).GetPrefixColumn([_aliasTable.Alias, "."]);
}
