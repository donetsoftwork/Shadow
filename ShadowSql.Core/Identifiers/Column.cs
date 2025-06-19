using ShadowSql.Services;

namespace ShadowSql.Identifiers;

/// <summary>
/// 简单列对象
/// </summary>
public class Column : ColumnBase, IColumn
{
    /// <summary>
    /// 简单列对象
    /// </summary>
    /// <param name="columnName">列名</param>
    protected Column(string columnName)
        : base(columnName)
    {
    }
    /// <summary>
    /// 获取列(已缓存,避免重复构造)
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    public static Column Use(string columnName)
        => _cacher.Get(columnName); 
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<Column> _cacher = new(static columnName => new Column(columnName));
    IColumn IFieldView.ToColumn()
        => this;
}
