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
    /// <param name="engine">数据库</param>
    /// <param name="sql">sql</param>
    /// <returns></returns>
    bool TryWrite(ISqlEngine engine, StringBuilder sql);
}
