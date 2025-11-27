using Dapper.Shadow;
using Shadow.DDL;
using Shadow.DDL.Schemas;

namespace Dapper.ShadowCoreTests;

/// <summary>
/// 数据表资源
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="table"></param>
internal class TableResource<TTable>(DapperExecutor executor, TTable table)
    : IAsyncDisposable
    where TTable : TableSchema
{
    protected readonly DapperExecutor _executor = executor;
    protected readonly TTable _table = table;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    public virtual async ValueTask InitAsync()
    {
        try
        {
            await _table.ToCreate()
                .ExecuteAsync(_executor);
        }
        catch { }
    }
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            await _table.ToDrop()
                .ExecuteAsync(_executor);
        }
        catch { }
    }
}
