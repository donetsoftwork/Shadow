using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Services;
using ShadowSql.SqlVales;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 比较原始sql语句
/// </summary>
public sealed class CompareRawFieldInfo : Identifier, ISqlValue
{
    private CompareRawFieldInfo(string statement)
        : base(statement)
    {
    }
    /// <summary>
    /// 获取列字段信息(已缓存,避免重复构造)
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    public static CompareRawFieldInfo Use(string statement)
        => _cacher.Get(statement);
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<CompareRawFieldInfo> _cacher = new(statement => new CompareRawFieldInfo(statement));

    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_name);
    }
}
