using ShadowSql.Components;
using System.Text;

namespace ShadowSql.Engines.MySql;

/// <summary>
/// MySql
/// </summary>
/// <param name="select"></param>
/// <param name="sqlVales"></param>
/// <param name="components"></param>
/// <param name="parameterPrefix"></param>
public class MySqlEngine(ISelectComponent select, ISqlValueComponent sqlVales, IPluginProvider? components, char parameterPrefix = '@')
    : EngineBase(select, sqlVales, components), ISqlEngine
{
    #region 配置
    private readonly char _parameterPrefix = parameterPrefix;
    /// <summary>
    /// 参数前缀(@或?)
    /// </summary>
    public char ParameterPrefix 
        => _parameterPrefix;
    #endregion

    /// <summary>
    /// MySql
    /// </summary>
    public MySqlEngine()
        : this(new MySqlSelectComponent(), new SqlValueComponent("1", "0", "NULL"), null)
    {
    }
    /// <summary>
    /// 标识符格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="name"></param>
    public override void Identifier(StringBuilder sql, string name)
    {
        sql.Append('`').Append(name).Append('`');
    }
    /// <summary>
    /// 参数格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="name"></param>
    public override void Parameter(StringBuilder sql, string name)
    {
        sql.Append(_parameterPrefix).Append(name);
    }
    /// <summary>
    /// 插入自增列sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override bool InsertedIdentity(StringBuilder sql)
    {
        sql.Append(";SELECT last_insert_id()");
        return true;
    }
}
