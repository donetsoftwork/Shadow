using ShadowSql.Engines;
using ShadowSql.Services;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ShadowSql.Logics;

/// <summary>
/// 语句否定逻辑
/// </summary>
#if NET7_0_OR_GREATER
public partial class NotStatementLogic : AtomicLogic
#else
public class NotStatementLogic : AtomicLogic
#endif
{
    private readonly string _statement;
    private NotStatementLogic(string statement)
    {
        _statement = statement;
    }
    /// <summary>
    /// 构造否定逻辑(not开头负负得正)
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    public static AtomicLogic CreateLogic(string statement)
    {
        var len = CheckNotLength(statement);
        //not开头负负得正
        if (len > 0)
            return StatementLogic.Use(statement[len..]);
        return Use(statement);
    }    

    /// <summary>
    /// 检查是否含Not(返回长度以便切除)
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    public static int CheckNotLength(string statement)
    {
        var match = NotRegex().Match(statement);
        if (match.Success)
            return match.Length;
        return -1;
    }
#if NET7_0_OR_GREATER
    /// <summary>
    /// 匹配Not
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^\W*NOT\W+", RegexOptions.IgnoreCase)]
    public static partial Regex NotRegex();
#else
    private static readonly Lazy<Regex> _notRegex = new(static () => new(@"^\W*NOT\W+", RegexOptions.IgnoreCase | RegexOptions.Compiled));
    /// <summary>
    /// 匹配Not
    /// </summary>
    /// <returns></returns>
    public static Regex NotRegex()
        => _notRegex.Value;
#endif

    /// <summary>
    /// 获取逻辑对象
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    internal static AtomicLogic Use(string statement)
        => _cacher.Get(statement);
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<AtomicLogic> _cacher = new(static statement => new NotStatementLogic(statement));

    #region AtomicLogic
    /// <inheritdoc/>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        engine.LogicNot(sql);
        sql.Append(_statement);
        return true;
    }
    /// <inheritdoc/>
    public override AtomicLogic Not()
        => StatementLogic.Use(_statement);
    #endregion
}
