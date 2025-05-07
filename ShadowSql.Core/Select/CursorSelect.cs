using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 范围(分页)及列筛选
/// </summary>
/// <param name="cursor"></param>
public sealed class CursorSelect(ICursor cursor) : SelectFieldsBase, ISelect
{
    #region 配置
    private readonly ICursor _source = cursor;
    /// <summary>
    /// 表视图
    /// </summary>
    public ICursor Source
        => _source;
    ITableView ISelect.Source
        => _source;
    #endregion
    #region TableViewBase
    /// <summary>
    /// 获取所有字段
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IField> GetFields()
        => _source.Fields;
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
    /// 拼写分页sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
    #endregion
}
