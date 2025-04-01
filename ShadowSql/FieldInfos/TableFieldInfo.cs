using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 带表名的字段信息
/// </summary>
/// <param name="tableName"></param>
/// <param name="name"></param>
public class TableFieldInfo(string tableName, string name)
    : FieldInfoBase(name), IField, ICompareField
{
    #region 配置
    private readonly string _tableName = tableName;
    /// <summary>
    /// 表名
    /// </summary>
    public string TableName
        => _tableName;
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_tableName).Append('.');
        engine.Identifier(sql, _name);
    }

    string IView.ViewName
        => _name;
    IColumn IFieldView.ToColumn()
        => Column.Use(_name).GetPrefixColumn([_tableName, "."]);
}
