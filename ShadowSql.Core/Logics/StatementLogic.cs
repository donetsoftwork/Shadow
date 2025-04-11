using ShadowSql.Engines;
using ShadowSql.Services;
using System.Text;

namespace ShadowSql.Logics;

/// <summary>
/// 语句逻辑
/// </summary>
public sealed class StatementLogic : AtomicLogic
{
    private readonly string _statement;
    /// <summary>
    /// 逻辑语句
    /// </summary>
    public string Statement
        => _statement;

    private StatementLogic(string statement)
    {
        _statement = statement;
    }
    /// <summary>
    /// 构造逻辑(not开头转化为否定逻辑)
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    public static AtomicLogic CreateLogic(string statement)
    {
        var len = NotStatementLogic.CheckNotLength(statement);
        if (len > 0)
            return NotStatementLogic.Use(statement.Substring(len));
        return Use(statement);
    }

    /// <summary>
    /// 获取逻辑对象
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    internal static StatementLogic Use(string statement)
        => _cacher.Get(statement);
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<StatementLogic> _cacher = new(statement => new StatementLogic(statement));

    #region AtomicLogic
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_statement);
        return true;
    }
    /// <summary>
    /// 否定逻辑
    /// </summary>
    /// <returns></returns>
    public override AtomicLogic Not()
        => NotStatementLogic.Use(_statement);
    #endregion
}
