using ShadowSql.Components;
using ShadowSql.Cursors;
using ShadowSql.Select;
using System.Text;

namespace ShadowSql.Engines;

/// <summary>
/// 数据库引擎(方言处理)
/// </summary>
public interface ISqlEngine : ISqlValueComponent
{
    /// <summary>
    /// 数据获取组件
    /// </summary>
    public ISelectComponent SelectComponent { get; }
    /// <summary>
    /// 数据库值处理组件
    /// </summary>
    public ISqlValueComponent SqlValeComponent { get; }
    /// <summary>
    /// 插件
    /// </summary>
    public IPluginProvider? PluginProvider { get; }
    /// <summary>
    /// 标识符格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="name"></param>
    void Identifier(StringBuilder sql, string name);
    /// <summary>
    /// 参数格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    void Parameter(StringBuilder sql, string name);
    /// <summary>
    /// 字段别名格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="alias"></param>
    void ColumnAs(StringBuilder sql, string alias);
    /// <summary>
    /// 表别名格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="alias"></param>
    void TableAs(StringBuilder sql, string alias);
    #region Prefix
    /// <summary>
    /// 插入单条前缀
    /// </summary>
    /// <param name="sql"></param>
    void InsertPrefix(StringBuilder sql);
    /// <summary>
    /// 插入多条前缀
    /// </summary>
    /// <param name="sql"></param>
    void InsertMultiPrefix(StringBuilder sql);
    /// <summary>
    /// 删除数据前缀
    /// </summary>
    /// <param name="sql"></param>
    void DeletePrefix(StringBuilder sql);
    /// <summary>
    /// 清空表前缀
    /// </summary>
    /// <param name="sql"></param>
    void TruncatePrefix(StringBuilder sql);
    /// <summary>
    /// 更新数据前缀
    /// </summary>
    /// <param name="sql"></param>
    void UpdatePrefix(StringBuilder sql);
    /// <summary>
    /// 筛选字段前缀
    /// </summary>
    /// <param name="sql"></param>
    void SelectPrefix(StringBuilder sql);
    /// <summary>
    /// SELECT
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    void Select(StringBuilder sql, ISelect select);
    /// <summary>
    /// 分页处理
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="select"></param>
    /// <param name="cursor"></param>
    /// <returns></returns>
    void SelectCursor(StringBuilder sql, ISelect select, ICursor cursor);
    #endregion
    /// <summary>
    /// 插入自增列sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns>是否支持</returns>
    bool InsertedIdentity(StringBuilder sql);
    /// <summary>
    /// 否定sql条件
    /// </summary>
    /// <param name="sql"></param>
    void LogicNot(StringBuilder sql);
    /// <summary>
    /// 转义
    /// </summary>
    /// <param name="sqlValue"></param>
    /// <returns></returns>
    string Escape(string sqlValue);
}
