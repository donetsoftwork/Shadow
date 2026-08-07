using Shadow.DDL.SqlTypes;

namespace Shadow.DDL.Schemas;

/// <summary>
/// 列定义
/// </summary>
/// <typeparam name="T"></typeparam>
public class ColumnSchema<T>
    : ColumnSchema
{
    private ColumnSchema(string columnName, string memberName, ISqlType sqlType, ColumnType columnType)
        : base(columnName, memberName, sqlType, columnType)
    {
    }
    /// <summary>
    /// 列定义
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <param name="memberName">成员名</param>
    /// <param name="columnType"></param>
    public ColumnSchema(string columnName, string memberName, ColumnType columnType = ColumnType.Empty)
        : this(columnName, memberName, new SqlType(typeof(T)), columnType)
    {
    }
    /// <summary>
    /// 列定义
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <param name="columnType"></param>
    public ColumnSchema(string columnName, ColumnType columnType = ColumnType.Empty)
        : this(columnName, columnName, new SqlType(typeof(T)), columnType)
    {
    }
    /// <summary>
    /// 列定义
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <param name="memberName">成员名</param>
    /// <param name="length"></param>
    /// <param name="columnType"></param>
    public ColumnSchema(string columnName, string memberName, int length, ColumnType columnType = ColumnType.Empty)
        : this(columnName, memberName, new LengthSqlType(typeof(T), length), columnType)
    {
    }
    /// <summary>
    /// 列定义
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <param name="length"></param>
    /// <param name="columnType"></param>
    public ColumnSchema(string columnName, int length, ColumnType columnType = ColumnType.Empty)
        : this(columnName, columnName, new LengthSqlType(typeof(T), length), columnType)
    {
    }
    /// <summary>
    /// 列定义
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <param name="memberName">成员名</param>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <param name="columnType"></param>
    public ColumnSchema(string columnName, string memberName, int precision, int scale, ColumnType columnType = ColumnType.Empty)
        : this(columnName, memberName, scale > 0 ? new ScaleSqlType(typeof(T), precision, scale) : new PrecisionSqlType(typeof(T), precision), columnType)
    {
    }
    /// <summary>
    /// 列定义
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    /// <param name="columnType"></param>
    public ColumnSchema(string columnName, int precision, int scale, ColumnType columnType = ColumnType.Empty)
        : this(columnName, columnName, scale > 0 ? new ScaleSqlType(typeof(T), precision, scale) : new PrecisionSqlType(typeof(T), precision), columnType)
    {
    }
}
