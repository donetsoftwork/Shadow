using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// 参数化执行器
/// </summary>
/// <param name="engine">数据库引擎</param>
/// <param name="connection">数据库连接</param>
/// <param name="buffered"></param>
/// <param name="capacity"></param>
public class ParametricExecutor(ISqlEngine engine, IDbConnection connection, bool buffered = true, int capacity = 128)
    : DapperExecutor(engine, connection, buffered, capacity)
{
    /// <summary>
    /// 构造参数化上下文
    /// </summary>
    /// <param name="param">参数</param>
    /// <returns></returns>
    protected ParametricContext CreateContext(object? param)
        => new(_engine, param);
    /// <inheritdoc/>
    public override int Execute(ISqlEntity fragment, object? param = null)
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return Execute(sql, context.Parameters);
    }
    /// <inheritdoc/>
    public override T? ExecuteScalar<T>(ISqlEntity fragment, object? param = null) where T : default
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return ExecuteScalar<T>(sql, context.Parameters);
    }
    /// <inheritdoc/>
    public override T? Count<T>(ITableView view, object? param = null) where T : default
    {
        var context = CreateContext(param);
        var sql = context.CountSql(view, _capacity);
        return ExecuteScalar<T>(sql, context.Parameters);
    }
    /// <inheritdoc/>
    public override Task<int> ExecuteAsync(ISqlEntity fragment, object? param = null)
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return ExecuteAsync(sql, context.Parameters);
    }
    /// <inheritdoc/>
    public override Task<T?> ExecuteScalarAsync<T>(ISqlEntity fragment, object? param = null) where T : default
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return ExecuteScalarAsync<T>(sql, context.Parameters);
    }
    /// <inheritdoc/>
    public override Task<T?> CountAsync<T>(ITableView view, object? param = null) where T : default
    {
        var context = CreateContext(param);
        var sql = context.CountSql(view, _capacity);
        return ExecuteScalarAsync<T>(sql, context.Parameters);
    }
    /// <inheritdoc/>
    public override IEnumerable<T> Query<T>(ISqlEntity fragment, object? param = null)
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return Query<T>(sql, context.Parameters);
    }
    /// <inheritdoc/>
    public override Task<IEnumerable<T>> QueryAsync<T>(ISqlEntity fragment, object? param = null)
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return QueryAsync<T>(sql, context.Parameters);
    }
    /// <inheritdoc/>
    public override T? QueryFirstOrDefault<T>(ISqlEntity fragment, object? param = null)
        where T : default
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return QueryFirstOrDefault<T>(sql, context.Parameters);
    }
    /// <inheritdoc/>
    public override Task<T?> QueryFirstOrDefaultAsync<T>(ISqlEntity fragment, object? param = null)
        where T : default
    {
        var context = CreateContext(param);
        var sql = context.Sql(fragment, _capacity);
        return QueryFirstOrDefaultAsync<T>(sql, context.Parameters);
    }
}
