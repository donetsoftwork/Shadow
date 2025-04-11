using ShadowSql.Components;
using System.Text;

namespace ShadowSql.Engines.Oracle;

/// <summary>
/// Oracle
/// </summary>
/// <param name="select"></param>
/// <param name="sqlVales"></param>
/// <param name="components"></param>
public class OracleEngine(ISelectComponent select, ISqlValueComponent sqlVales, IPluginProvider? components)
    : EngineBase(select, sqlVales, components), ISqlEngine
{
    /// <summary>
    /// Oracle
    /// </summary>
    public OracleEngine()
        : this(new OracleSelectComponent(), new SqlValueComponent("1", "0", "NULL"), null)
    {
    }
    /// <summary>
    /// 标识符格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="name"></param>
    public override void Identifier(StringBuilder sql, string name)
    {
        sql.Append('\"').Append(name).Append('\"');
    }
    /// <summary>
    /// 参数格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="name"></param>
    public override void Parameter(StringBuilder sql, string name)
    {
        sql.Append(':').Append(name);
    }
    /// <summary>
    /// 字段别名格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="alias"></param>
    public override void ColumnAs(StringBuilder sql, string alias)
    {
        sql.Append(' ').Append(alias);
    }
    /// <summary>
    /// 表别名格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="alias"></param>
    public override void TableAs(StringBuilder sql, string alias)
    {
        sql.Append(' ').Append(alias);
    }
    /// <summary>
    /// 插入多条前缀
    /// </summary>
    /// <param name="sql"></param>
    public override void InsertMultiPrefix(StringBuilder sql)
    {
        sql.Append("INSERT ALL INTO ");
    }
    /// <summary>
    /// 每个表定义不同SEQUENCE来自增,不好通用
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override bool InsertedIdentity(StringBuilder sql)
        => false;
}
