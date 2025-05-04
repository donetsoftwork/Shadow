using ShadowSql.Services;

namespace ShadowSql.Identifiers;

/// <summary>
/// 库名对象
/// </summary>
public sealed class DB(string name)
    : Identifier(name), IDB
{
    /// <summary>
    /// 获取数据库
    /// 已缓存,避免重复构造
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static DB Use(string name)
        => _cacher.Get(name);
    /// <summary>
    /// 获取表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public Table From(string tableName)
        => _tables.Get(tableName);

    ITable IDB.From(string tableName)
        => From(tableName);
    /// <summary>
    /// 数据表
    /// </summary>
    private readonly CacheService<Table> _tables = new(static name => new Table(name));
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<DB> _cacher = new(static name => new DB(name));
}
