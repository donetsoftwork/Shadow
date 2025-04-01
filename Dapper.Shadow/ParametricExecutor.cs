using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// 参数化执行器
/// </summary>
/// <param name="engine"></param>
/// <param name="connection"></param>
/// <param name="buffered"></param>
/// <param name="capacity"></param>
public class ParametricExecutor(ISqlEngine engine, IDbConnection connection, bool buffered = true, int capacity = 32)
    : DapperExecutor(engine, connection, buffered, capacity)
{
    /// <summary>
    /// 构造参数化上下文
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    protected ParametricContext CreateContext(object? param)
        => new(_engine, param);
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="fragment"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public override int Execute(ISqlEntity fragment, object? param = null)
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return Execute(sql, context.Parameters);
    }
    /// <summary>
    /// 执行(返回一行一列)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fragment"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public override T? ExecuteScalar<T>(ISqlEntity fragment, object? param = null) where T : default
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return ExecuteScalar<T>(sql, context.Parameters);
    }
    /// <summary>
    /// 异步执行
    /// </summary>
    /// <param name="fragment"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public override Task<int> ExecuteAsync(ISqlEntity fragment, object? param = null)
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return ExecuteAsync(sql, context.Parameters);
    }
    /// <summary>
    /// 异步执行(返回一行一列)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fragment"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public override Task<T?> ExecuteScalarAsync<T>(ISqlEntity fragment, object? param = null) where T : default
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return ExecuteScalarAsync<T>(sql, context.Parameters);
    }
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fragment"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public override IEnumerable<T> Query<T>(ISqlEntity fragment, object? param = null)
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return Query<T>(sql, context.Parameters);
    }
    /// <summary>
    /// 异步获取列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fragment"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public override Task<IEnumerable<T>> QueryAsync<T>(ISqlEntity fragment, object? param = null)
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return QueryAsync<T>(sql, context.Parameters);
    }
    /// <summary>
    /// 获取单条
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fragment"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public override T? QueryFirstOrDefault<T>(ISqlEntity fragment, object? param = null)
        where T : default
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return QueryFirstOrDefault<T>(sql, context.Parameters);
    }
    /// <summary>
    /// 异步获取单条
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fragment"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public override Task<T?> QueryFirstOrDefaultAsync<T>(ISqlEntity fragment, object? param = null)
        where T : default
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return QueryFirstOrDefaultAsync<T>(sql, context.Parameters);
    }
}
