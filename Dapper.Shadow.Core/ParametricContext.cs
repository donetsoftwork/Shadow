using Hand.Creational;
using ShadowSql;
using ShadowSql.Components;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Generators;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.Values;
using System.Text;

namespace Dapper.Shadow;

/// <summary>
/// 参数化上线文
/// </summary>
/// <param name="engine">数据库引擎</param>
/// <param name="parameterGenerator"></param>
/// <param name="param">参数</param>
public class ParametricContext(ISqlEngine engine, ICreator<string> parameterGenerator, object? param)
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
    private readonly ICreator<string> _parameterGenerator = parameterGenerator;
    private readonly DynamicParameters _parameters = new(param);
    /// <summary>
    /// 参数名生成器
    /// </summary>
    public ICreator<string> ParameterGenerator
        => _parameterGenerator;
    /// <summary>
    /// 参数
    /// </summary>
    public DynamicParameters Parameters
        => _parameters;
    /// <inheritdoc/>
    ISelectComponent ISqlEngine.SelectComponent
        => _engine.SelectComponent;
    /// <inheritdoc/>
    ISqlValueComponent ISqlEngine.SqlValueComponent
        => this;
    /// <inheritdoc/>
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
    /// <inheritdoc/>
    ISqlValue ISqlValueComponent.SqlValue<T>(T value)
    {
        var name = _parameterGenerator.Create();
        _parameters.Add(name, value);
        return Parameter.Use(name);
    }
    #region ISqlEngine
    /// <inheritdoc/>
    void ISqlEngine.Identifier(StringBuilder sql, string name)
        => _engine.Identifier(sql, name);
    /// <inheritdoc/>
    void ISqlEngine.Parameter(StringBuilder sql, string name)
        => _engine.Parameter(sql, name);
    /// <inheritdoc/>
    void ISqlEngine.CollectParameter(StringBuilder sql, string name)
        => _engine.CollectParameter(sql, name);
    /// <inheritdoc/>
    void ISqlEngine.ColumnAs(StringBuilder sql, string alias)
        => _engine.ColumnAs(sql, alias);
    /// <inheritdoc/>
    void ISqlEngine.TableAs(StringBuilder sql, string alias)
        => _engine.TableAs(sql, alias);
    /// <inheritdoc/>
    void ISqlEngine.InsertPrefix(StringBuilder sql)
        => _engine.InsertPrefix(sql);
    /// <inheritdoc/>
    void ISqlEngine.InsertMultiPrefix(StringBuilder sql)
        => _engine.InsertMultiPrefix(sql);
    /// <inheritdoc/>
    bool ISqlEngine.InsertedIdentity(StringBuilder sql)
        => _engine.InsertedIdentity(sql);
    /// <inheritdoc/>
    void ISqlEngine.Count(StringBuilder sql)
        => _engine.Count(sql);
    /// <inheritdoc/>
    void ISqlEngine.LogicNot(StringBuilder sql)
        => _engine.LogicNot(sql);
    /// <inheritdoc/>
    string ISqlEngine.Escape(string sqlValue)
        => _engine.Escape(sqlValue);
    /// <inheritdoc/>
    void ISqlEngine.Select(StringBuilder sql, ISelect select)
        => _engine.SelectComponent.Select(this, sql, select);
    /// <inheritdoc/>
    void ISqlEngine.SelectCursor(StringBuilder sql, ISelect select, ICursor cursor)
        => _engine.SelectComponent.SelectCursor(this, sql, select, cursor);
    /// <inheritdoc/>
    void ISqlEngine.DeletePrefix(StringBuilder sql)
        => _engine.DeletePrefix(sql);
    /// <inheritdoc/>
    void ISqlEngine.TruncatePrefix(StringBuilder sql)
        => _engine.TruncatePrefix(sql);
    /// <inheritdoc/>
    void ISqlEngine.UpdatePrefix(StringBuilder sql)
        => _engine.UpdatePrefix(sql);
    /// <inheritdoc/>
    void ISqlEngine.SelectPrefix(StringBuilder sql)
        => _engine.SelectPrefix(sql);
    #endregion
}
