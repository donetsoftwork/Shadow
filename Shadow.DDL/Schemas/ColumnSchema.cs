using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Linq;

namespace Shadow.DDL.Schemas;

/// <summary>
/// 列定义
/// </summary>
/// <param name="columnName">列</param>
/// <param name="sqlType">数据库字段类型</param>
public class ColumnSchema(string columnName, string sqlType = "INT")
    : ColumnBase(columnName), IColumn
{
    private readonly string _sqlType = sqlType.ToUpperInvariant();
    /// <summary>
    /// 数据库类型
    /// </summary>
    public string SqlType
        => _sqlType;
    private string _default = string.Empty;
    /// <summary>
    /// 数据库默认值
    /// </summary>
    public string Default
    {
        get { return _default; }
        set { _default = value; }
    }
    private bool _notNull = false;
    /// <summary>
    /// 是否NotNull
    /// </summary>
    public bool NotNull
    {
        get { return _notNull; }
        set { _notNull = value; }
    }
    private ColumnType _columnType = ColumnType.Normal;
    /// <summary>
    /// 字段类型
    /// </summary>
    public ColumnType ColumnType
    {
        get { return _columnType; }
        set { _columnType = value; }
    }
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
        => fields.Where(o => (o.ColumnType & ColumnType.Key) == ColumnType.Key);
    /// <summary>
    /// 获取插入列
    /// </summary>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    internal static IEnumerable<ColumnSchema> GetInsertColumns(IEnumerable<ColumnSchema> fields)
        => fields.Where(o => (o.ColumnType & InsertIgnoreType) == ColumnType.Empty);
    /// <summary>
    /// 获取修改列
    /// </summary>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    internal static IEnumerable<ColumnSchema> GetUpdateColumns(IEnumerable<ColumnSchema> fields)
        => fields.Where(o => (o.ColumnType & UpdateIgnoreType) == ColumnType.Empty);
}
