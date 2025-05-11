using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 获取表视图数据
/// </summary>
/// <param name="table"></param>
public class TableSelect(ITableView table) : SelectFieldsBase, ISelect
{
    /// <summary>
    /// 获取表视图数据
    /// </summary>
    /// <param name="tableName"></param>
    public TableSelect(string tableName)
        : this(new Table(tableName))
    {
    }
    #region 配置
    private readonly ITableView _source = table;
    /// <summary>
    /// 表视图
    /// </summary>
    public ITableView Source
        => _source;
    #endregion
    #region TableViewBase
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField? GetField(string fieldName)
        => _source.GetField(fieldName);
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField NewField(string fieldName)
        => _source.NewField(fieldName);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
         => engine.Select(sql, this);
    #endregion
}
