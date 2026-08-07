using Shadow.DDL.SqlTypes;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Linq;

namespace Shadow.DDL.Schemas;

/// <summary>
/// 列定义
/// </summary>
/// <param name="columnName">列</param>
/// <param name="memberName">成员名</param>
/// <param name="sqlType">数据库字段类型</param>
/// <param name="columnType"></param>
public class ColumnSchema(string columnName, string memberName, ISqlType sqlType, ColumnType columnType = ColumnType.Empty)
    : ColumnBase(columnName), IColumn
{
    /// <summary>
    /// 列定义
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="sqlType"></param>
    /// <param name="columnType"></param>
    public ColumnSchema(string columnName, ISqlType sqlType, ColumnType columnType = ColumnType.Empty)
        : this(columnName, columnName, sqlType, columnType)
    {
    }
    /// <summary>
    /// 列定义
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="memberName">成员名</param>
    /// <param name="sqlType"></param>
    /// <param name="columnType"></param>
    public ColumnSchema(string columnName, string memberName, string sqlType, ColumnType columnType = ColumnType.Empty)
        : this(columnName, memberName, new SqlRawType(sqlType), columnType)
    {
    }
    /// <summary>
    /// 列定义
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="sqlType"></param>
    /// <param name="columnType"></param>
    public ColumnSchema(string columnName, string sqlType, ColumnType columnType = ColumnType.Empty)
        : this(columnName, columnName, new SqlRawType(sqlType), columnType)
    {
    }
    #region 配置

    private readonly string _memberName = memberName;
    /// <summary>
    /// 成员名
    /// </summary>
    public string MemberName
        => _memberName;
    /// <inheritdoc/>
    string IMember.MemberName
        => _memberName;
    /// <summary>
    /// 数据库类型
    /// </summary>
    public ISqlType SqlType { get; } = sqlType;

    private readonly ColumnType _columnType = columnType;
    /// <summary>
    /// 字段类型
    /// </summary>
    public ColumnType ColumnType
        => _columnType;
    private string _default = string.Empty;
    /// <summary>
    /// 数据库默认值
    /// </summary>
    public string Default
    {
        get { return _default; }
        set { _default = value; }
    }
    #endregion
    /// <summary>
    /// 转化为列
    /// </summary>
    /// <returns></returns>
    public IColumn ToColumn()
        => this;
    /// <summary>
    /// 插入忽略字段类型
    /// </summary>
    public static readonly ColumnType InsertIgnoreType = ColumnType.Identity | ColumnType.Computed;
    /// <summary>
    /// 更新忽略字段类型
    /// </summary>
    public static readonly ColumnType UpdateIgnoreType = ColumnType.Identity | ColumnType.Key | ColumnType.Computed;
    /// <summary>
    /// 获取主键
    /// </summary>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    internal static IEnumerable<ColumnSchema> GetKeys(IEnumerable<ColumnSchema> fields)
        => fields.Where(o => (o._columnType & ColumnType.Key) == ColumnType.Key);
    /// <summary>
    /// 获取插入列
    /// </summary>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    internal static IEnumerable<ColumnSchema> GetInsertColumns(IEnumerable<ColumnSchema> fields)
        => fields.Where(o => (o._columnType & InsertIgnoreType) == ColumnType.Empty);
    /// <summary>
    /// 获取修改列
    /// </summary>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    internal static IEnumerable<ColumnSchema> GetUpdateColumns(IEnumerable<ColumnSchema> fields)
        => fields.Where(o => (o._columnType & UpdateIgnoreType) == ColumnType.Empty);
}
