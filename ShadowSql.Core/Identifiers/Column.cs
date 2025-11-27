using Hand.Cache;

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
    /// <inheritdoc/>
    IColumn IFieldView.ToColumn()
        => this;
    #region ColumnCacher
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly ColumnCacher _cacher = new(new DictionaryCacher<string, Column>());
    /// <summary>
    /// 列缓存器
    /// </summary>
    /// <param name="cacher"></param>
    internal class ColumnCacher(ICacher<string, Column> cacher)
        : CacheFactoryBase<string, Column>(cacher)
    {
        /// <inheritdoc/>
        protected override Column CreateNew(in string key)
            => new(key);
    }
    #endregion
}
