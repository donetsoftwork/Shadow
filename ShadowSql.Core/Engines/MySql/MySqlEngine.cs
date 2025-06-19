using ShadowSql.Components;
using System.Text;

namespace ShadowSql.Engines.MySql;

/// <summary>
/// MySql
/// </summary>
/// <param name="select">筛选</param>
/// <param name="sqlValues"></param>
/// <param name="components"></param>
/// <param name="parameterPrefix"></param>
public class MySqlEngine(ISelectComponent select, ISqlValueComponent sqlValues, IPluginProvider? components, char parameterPrefix = '@')
    : EngineBase(select, sqlValues, components), ISqlEngine
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
    /// <inheritdoc/>
    public override void Identifier(StringBuilder sql, string name)
    {
        sql.Append('`').Append(name).Append('`');
    }
    /// <inheritdoc/>
    public override void Parameter(StringBuilder sql, string name)
    {
        sql.Append(_parameterPrefix).Append(name);
    }
    /// <inheritdoc/>
    public override bool InsertedIdentity(StringBuilder sql)
    {
        sql.Append(";SELECT last_insert_id()");
        return true;
    }
}
