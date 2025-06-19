using Shadow.DDL.Schemas;
using ShadowSql.Engines;
using System.Text;

namespace Shadow.DDL.Components;

/// <summary>
/// 定义列组件
/// </summary>
public interface IDefineColumComponent
{
    /// <summary>
    /// 写入的列定义
    /// </summary>
    /// <param name="column">列</param>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    void WriteColumnSchema(ColumnSchema column, ISqlEngine engine, StringBuilder sql);
}
