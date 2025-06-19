using ShadowSql.Services;

namespace ShadowSql.Identifiers;

/// <summary>
/// 库名对象
/// </summary>
/// <param name="dbName">数据库名</param>
public sealed class DB(string dbName)
    : Identifier(dbName), IDB
{
    /// <summary>
    /// 获取数据库
    /// 已缓存,避免重复构造
    /// </summary>
    /// <param name="dbName">数据库名</param>
    /// <returns></returns>
    public static DB Use(string dbName)
        => _cacher.Get(dbName);
    /// <summary>
    /// 获取表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public Table From(string tableName)
        => _tables.Get(tableName);

    ITable IDB.From(string tableName)
        => From(tableName);
    /// <summary>
    /// 数据表
    /// </summary>
    private readonly CacheService<Table> _tables = new(static tableName => new Table(tableName));
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<DB> _cacher = new(static dbName => new DB(dbName));
}
