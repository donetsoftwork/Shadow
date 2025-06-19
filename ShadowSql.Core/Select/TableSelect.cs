using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using ShadowSql.Tables;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 获取表视图数据
/// </summary>
/// <param name="table">表</param>
public class TableSelect(ITableView table) : SelectFieldsBase, ISelect
{
    /// <summary>
    /// 获取表视图数据
    /// </summary>
    /// <param name="tableName">表名</param>
    public TableSelect(string tableName)
        : this(EmptyTable.Use(tableName))
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
    /// <inheritdoc/>
    protected override IEnumerable<IField> GetFields()
        => _source.Fields;
    /// <inheritdoc/>
    protected override IField? GetField(string fieldName)
        => _source.GetField(fieldName);
    /// <inheritdoc/>
    protected override IField NewField(string fieldName)
        => _source.NewField(fieldName);
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
         => engine.Select(sql, this);
    #endregion
}
