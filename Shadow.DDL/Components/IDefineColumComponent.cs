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
    /// <param name="column"></param>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    void WriteColumnSchema(ColumnSchema column, ISqlEngine engine, StringBuilder sql);
}
