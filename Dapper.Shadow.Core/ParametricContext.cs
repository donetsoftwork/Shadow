using ShadowSql.Identifiers;
using ShadowSql.Generators;
using ShadowSql.SqlVales;
using ShadowSql.Engines;
using System.Text;
using ShadowSql.Select;
using ShadowSql.Components;
using ShadowSql;
using ShadowSql.Fragments;
using ShadowSql.Cursors;

namespace Dapper.Shadow;

/// <summary>
/// 参数化上线文
/// </summary>
/// <param name="engine">数据库引擎</param>
/// <param name="parameterGenerator"></param>
/// <param name="param">参数</param>
public class ParametricContext(ISqlEngine engine, IIdentifierGenerator parameterGenerator, object? param = null)
    : ISqlValueComponent, ISqlEngine
{
    /// <summary>
    /// 参数化上线文
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="param">参数</param>
    public ParametricContext(ISqlEngine engine, object? param = null)
        : this(engine, new IdIncrementGenerator("p"), param)
    {
    }
    #region 配置
    private readonly ISqlEngine _engine = engine;
    private readonly IIdentifierGenerator _parameterGenerator = parameterGenerator;
    private readonly DynamicParameters _parameters = new(param);
    /// <summary>
    /// 参数名生成器
    /// </summary>
    public IIdentifierGenerator ParameterGenerator
        => _parameterGenerator;
    /// <summary>
    /// 参数
    /// </summary>
    public DynamicParameters Parameters
        => _parameters;
    ISelectComponent ISqlEngine.SelectComponent
        => _engine.SelectComponent;
    ISqlValueComponent ISqlEngine.SqlValueComponent
        => this;
    IPluginProvider? ISqlEngine.PluginProvider
        => _engine.PluginProvider;
    #endregion

    /// <summary>
    /// 构造sql
    /// </summary>
    /// <param name="fragment"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public string BuildSql(ISqlFragment fragment, int capacity = 128)
        => this.Sql(fragment, capacity);

    ISqlValue ISqlValueComponent.SqlValue<T>(T value)
    {
        var name = _parameterGenerator.NewName();
        _parameters.Add(name, value);
        return Parameter.Use(name);
    }
    #region ISqlEngine
    void ISqlEngine.Identifier(StringBuilder sql, string name)
        => _engine.Identifier(sql, name);
    void ISqlEngine.Parameter(StringBuilder sql, string name)
        => _engine.Parameter(sql, name);
    void ISqlEngine.ColumnAs(StringBuilder sql, string alias)
        => _engine.ColumnAs(sql, alias);
    void ISqlEngine.TableAs(StringBuilder sql, string alias)
        => _engine.TableAs(sql, alias);
    void ISqlEngine.InsertPrefix(StringBuilder sql)
        => _engine.InsertPrefix(sql);
    void ISqlEngine.InsertMultiPrefix(StringBuilder sql)
        => _engine.InsertMultiPrefix(sql);
    bool ISqlEngine.InsertedIdentity(StringBuilder sql)
        => _engine.InsertedIdentity(sql);
    void ISqlEngine.Count(StringBuilder sql)
        => _engine.Count(sql);
    void ISqlEngine.LogicNot(StringBuilder sql)
        => _engine.LogicNot(sql);
    string ISqlEngine.Escape(string sqlValue)
        => _engine.Escape(sqlValue);
    void ISqlEngine.Select(StringBuilder sql, ISelect select)
        => _engine.SelectComponent.Select(this, sql, select);
    void ISqlEngine.SelectCursor(StringBuilder sql, ISelect select, ICursor cursor)
        => _engine.SelectComponent.SelectCursor(this, sql, select, cursor);
    void ISqlEngine.DeletePrefix(StringBuilder sql)
        => _engine.DeletePrefix(sql);
    void ISqlEngine.TruncatePrefix(StringBuilder sql)
        => _engine.TruncatePrefix(sql);
    void ISqlEngine.UpdatePrefix(StringBuilder sql)
        => _engine.UpdatePrefix(sql);
    void ISqlEngine.SelectPrefix(StringBuilder sql)
        => _engine.SelectPrefix(sql);
    #endregion
}
