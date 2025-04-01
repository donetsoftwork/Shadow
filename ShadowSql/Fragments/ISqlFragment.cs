using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.Fragments;

/// <summary>
/// sql片段
/// </summary>
public interface ISqlFragment
{
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <returns></returns>
    bool TryWrite(ISqlEngine engine, StringBuilder sql);
}
/// <summary>
/// 可执行的sql
/// </summary>
public interface IExecuteSql : ISqlEntity;
///// <summary>
///// 查询数据的sql
///// </summary>
//public interface IQuerySql : IExecuteSql;
/// <summary>
/// sql实体
/// </summary>
public interface ISqlEntity
{
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    void Write(ISqlEngine engine, StringBuilder sql);
}
