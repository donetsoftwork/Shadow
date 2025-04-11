using ShadowSql.Identifiers;
using ShadowSql.Services;
using System;

namespace ShadowSql.Simples;

/// <summary>
/// 简单库名对象
/// </summary>
public sealed class SimpleDB : Identifier, IDB
{
    private SimpleDB(string name)
        : base(name)
    {
    }
    private static readonly CacheService<SimpleDB> _cacher = new(name => new SimpleDB(name));
    /// <summary>
    /// 默认表对象
    /// </summary>
    public static readonly Lazy<SimpleDB> Empty = new(() => Use("DB"));
    /// <summary>
    /// 获取库
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static SimpleDB Use(string name = "")
        => _cacher.Get(name);
    /// <summary>
    /// 获取表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static SimpleTable From(string tableName)
        => SimpleTable.Use(tableName);
    ITable IDB.From(string tableName)
        => SimpleTable.Use(tableName);
}
