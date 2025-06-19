using ShadowSql.Components;
using ShadowSql.Cursors;
using ShadowSql.Select;
using ShadowSql.SqlVales;
using System.Text;
namespace ShadowSql.Engines;

/// <summary>
/// 数据库引擎基类
/// </summary>
/// <param name="select">筛选</param>
/// <param name="sqlValue"></param>
/// <param name="plugin"></param>
public abstract class EngineBase(ISelectComponent select, ISqlValueComponent sqlValue, IPluginProvider? plugin)
    : ISqlEngine
{
    #region 配置
    /// <summary>
    /// 数据获取
    /// </summary>
    private readonly ISelectComponent _select = select;
    /// <summary>
    /// SqlVale处理程序
    /// </summary>
    private readonly ISqlValueComponent _sqlValue = sqlValue;
    /// <summary>
    /// 组件
    /// </summary>
    private readonly IPluginProvider? _plugin = plugin;
    /// <summary>
    /// 数据获取组件
    /// </summary>
    public ISelectComponent SelectComponent 
        => _select;
    /// <summary>
    /// 数据库值处理组件
    /// </summary>
    public ISqlValueComponent SqlValueComponent
        => _sqlValue;
    /// <summary>
    /// 插件
    /// </summary>
    public IPluginProvider? PluginProvider 
        => _plugin;
    #endregion

    #region 格式化
    /// <inheritdoc/>
    public abstract void Identifier(StringBuilder sql, string name);
    /// <inheritdoc/>
    public virtual void Parameter(StringBuilder sql, string name)
    {
        sql.Append('@').Append(name);
    }
    /// <inheritdoc/>
    public virtual void ColumnAs(StringBuilder sql, string aliasName)
    {
        sql.Append(" AS ").Append(aliasName);
    }
    /// <inheritdoc/>
    public virtual void TableAs(StringBuilder sql, string aliasName)
    {
        sql.Append(" AS ").Append(aliasName);
    }
    /// <summary>
    /// 转义(防sql注入)
    /// 依赖转义的功能慎用
    /// 简单转义无法杜绝sql注入
    /// </summary>
    /// <param name="sqlValue">数据库值</param>
    /// <returns></returns>
    public virtual string Escape(string sqlValue)
    {
        if (string.IsNullOrEmpty(sqlValue))
            return sqlValue;
        return sqlValue.Replace("\\", "\\\\")
            .Replace("\'", "\\\'")
            .Replace("\"", "\\\"");
    }
    #endregion
    /// <inheritdoc/>
    public virtual void LogicNot(StringBuilder sql)
    {
        sql.Append("NOT ");
    }
    /// <summary>
    /// 处理SqlVale
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">值</param>
    /// <returns></returns>
    public virtual ISqlValue SqlValue<T>(T value)
        => SqlValueComponent.SqlValue(value);
    /// <summary>
    /// 获取组件(插件)
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    public virtual TComponent? GetPlugin<TComponent>()
        where TComponent : class
        => PluginProvider?.GetPlugin<TComponent>();
    #region Fragments
    /// <inheritdoc/>
    public virtual void InsertPrefix(StringBuilder sql)
    {
        sql.Append("INSERT INTO ");
    }
    /// <inheritdoc/>
    public virtual void InsertMultiPrefix(StringBuilder sql)
    {
        sql.Append("INSERT INTO ");
    }
    /// <inheritdoc/>
    public abstract bool InsertedIdentity(StringBuilder sql);
    /// <inheritdoc/>
    public virtual void Count(StringBuilder sql)
        => sql.Append("COUNT(*)");
    /// <inheritdoc/>
    public virtual void DeletePrefix(StringBuilder sql)
        => sql.Append("DELETE ");
    /// <inheritdoc/>
    public virtual void TruncatePrefix(StringBuilder sql)
        => sql.Append("TRUNCATE TABLE ");
    /// <inheritdoc/>
    public virtual void UpdatePrefix(StringBuilder sql)
        => sql.Append("UPDATE ");
    #region SELECT
    /// <inheritdoc/>
    public virtual void SelectPrefix(StringBuilder sql)
        => sql.Append("SELECT ");

    #endregion
    /// <inheritdoc/>
    void ISqlEngine.Select(StringBuilder sql, ISelect select)
        => SelectComponent.Select(this, sql, select);
    /// <inheritdoc/>
    void ISqlEngine.SelectCursor(StringBuilder sql, ISelect select, ICursor cursor)
         => SelectComponent.SelectCursor(this, sql, select, cursor);
    #endregion
}
