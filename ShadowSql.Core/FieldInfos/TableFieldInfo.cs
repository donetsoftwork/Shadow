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
    : ColumnBase(name), IField, ICompareField, IPrefixField
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
    internal override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_tableName).Append('.');
        engine.Identifier(sql, _name);
    }
    #region IPrefixField
    bool IPrefixField.IsMatch(IField field)
        => IsMatch(field.ViewName);
    #endregion
    IColumn IFieldView.ToColumn()
        => Column.Use(_name);
}
