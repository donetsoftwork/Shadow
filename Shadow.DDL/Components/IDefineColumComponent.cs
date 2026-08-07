using Shadow.DDL.Schemas;
using Shadow.DDL.TypeParse;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
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
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="column">列</param>
    bool WriteColumnSchema(ISqlEngine engine, StringBuilder sql, ColumnSchema column);
    /// <summary>
    /// 获取类型解析器
    /// </summary>
    /// <param name="originalType"></param>
    /// <returns></returns>
    ITypeParser? GetTypeParser(Type originalType);
    /// <summary>
    /// 写入检查约束
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="name"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    bool WriteCheckConstraint(ISqlEngine engine, StringBuilder sql, string name, string expression);
    /// <summary>
    /// 写入唯一约束
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="name"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    bool WriteUniqueConstraint(ISqlEngine engine, StringBuilder sql, string name, IColumn[] columns);
}
