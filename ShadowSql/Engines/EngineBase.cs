using ShadowSql.Components;
using ShadowSql.Fetches;
using ShadowSql.Select;
using ShadowSql.SqlVales;
using System.Text;
namespace ShadowSql.Engines;

/// <summary>
/// 数据库引擎基类
/// </summary>
/// <param name="select"></param>
/// <param name="sqlVale"></param>
/// <param name="plugin"></param>
public abstract class EngineBase(ISelectComponent select, ISqlValueComponent sqlVale, IPluginProvider? plugin)
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
    private readonly ISqlValueComponent _sqlVale = sqlVale;
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
    public ISqlValueComponent SqlValeComponent 
        => _sqlVale;
    /// <summary>
    /// 插件
    /// </summary>
    public IPluginProvider? PluginProvider 
        => _plugin;
    #endregion

    #region 格式化
    /// <summary>
    /// 标识符格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="name"></param>
    public abstract void Identifier(StringBuilder sql, string name);
    /// <summary>
    /// 参数格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="name"></param>
    public virtual void Parameter(StringBuilder sql, string name)
    {
        sql.Append('@').Append(name);
    }
    /// <summary>
    /// 字段别名格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="alias"></param>
    public virtual void ColumnAs(StringBuilder sql, string alias)
    {
        sql.Append(" AS ").Append(alias);
    }
    /// <summary>
    /// 表别名格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="alias"></param>
    public virtual void TableAs(StringBuilder sql, string alias)
    {
        sql.Append(" AS ").Append(alias);
    }
    /// <summary>
    /// 转义(防sql注入)
    /// 依赖转义的功能慎用
    /// 简单转义无法杜绝sql注入
    /// </summary>
    /// <param name="sqlValue"></param>
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
    /// <summary>
    /// 否定sql条件
    /// </summary>
    /// <param name="sql"></param>
    public virtual void LogicNot(StringBuilder sql)
    {
        sql.Append("NOT ");
    }
    /// <summary>
    /// 处理SqlVale
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual ISqlValue SqlValue<T>(T value)
        => SqlValeComponent.SqlValue(value);
    /// <summary>
    /// 获取组件(插件)
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    public virtual TComponent? GetPlugin<TComponent>()
        where TComponent : class
        => PluginProvider?.GetPlugin<TComponent>();
    #region Fragments
    /// <summary>
    /// 插入单条前缀
    /// </summary>
    /// <param name="sql"></param>
    public virtual void InsertPrefix(StringBuilder sql)
    {
        sql.Append("INSERT INTO ");
    }
    /// <summary>
    /// 插入多条前缀
    /// </summary>
    /// <param name="sql"></param>
    public virtual void InsertMultiPrefix(StringBuilder sql)
    {
        sql.Append("INSERT INTO ");
    }
    /// <summary>
    /// 插入自增列sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public abstract bool InsertedIdentity(StringBuilder sql);
    /// <summary>
    /// 删除前缀
    /// </summary>
    /// <param name="sql"></param>
    public virtual void DeletePrefix(StringBuilder sql)
        => sql.Append("DELETE ");
    /// <summary>
    /// 清空表前缀
    /// </summary>
    /// <param name="sql"></param>
    public virtual void TruncatePrefix(StringBuilder sql)
        => sql.Append("TRUNCATE TABLE ");
    /// <summary>
    /// 修改前缀
    /// </summary>
    /// <param name="sql"></param>
    public virtual void UpdatePrefix(StringBuilder sql)
        => sql.Append("UPDATE ");
    #region SELECT
    /// <summary>
    /// SELECT前缀
    /// </summary>
    /// <param name="sql"></param>
    public virtual void SelectPrefix(StringBuilder sql)
        => sql.Append("SELECT ");

    #endregion
    void ISqlEngine.Select(StringBuilder sql, ISelect select)
        => SelectComponent.Select(this, sql, select);
    void ISqlEngine.SelectCursor(StringBuilder sql, ISelect select, ICursor cursor)
         => SelectComponent.SelectCursor(this, sql, select, cursor);
    #endregion
}
