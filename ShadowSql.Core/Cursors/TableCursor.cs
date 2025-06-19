using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Cursors;

/// <summary>
/// 表视图范围筛选游标
/// </summary>
public class TableCursor : CursorBase, ICursor
{
    /// <summary>
    /// 表视图范围筛选游标
    /// </summary>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <param name="view"></param>
    internal TableCursor(int limit, int offset, ITableView view)
        : base(limit, offset)
    {
        _source = view;
    }
    /// <summary>
    /// 表范围筛选游标
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public TableCursor(ITable table, int limit = 0, int offset = 0)
        : this(limit, offset, table)
    {
    }
    /// <summary>
    /// 别名表范围筛选游标
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public TableCursor(IAliasTable aliasTable, int limit = 0, int offset = 0)
        : this(limit, offset, aliasTable)
    {
    }
    /// <summary>
    /// 查询再范围筛选游标
    /// </summary>
    /// <param name="view"></param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public TableCursor(IDataFilter view, int limit = 0, int offset = 0)
        : this(limit, offset, view)
    {
    }
    #region 配置
    /// <summary>
    /// 数据源
    /// </summary>
    private readonly ITableView _source;
    /// <summary>
    /// 数据源
    /// </summary>
    public ITableView Source
        => _source;
    #endregion
    #region ICursor
    /// <inheritdoc/>
    ICursor ICursor.Take(int limit)
    {
        TakeCore(limit);
        return this;
    }
    /// <inheritdoc/>
    ICursor ICursor.Skip(int offset)
    {
        SkipCore(offset);
        return this;
    }
    #endregion
    #region TableViewBase
    /// <inheritdoc/>
    protected override IEnumerable<IField> GetFields()
        => _source.Fields;
    /// <inheritdoc/>
    protected override IField? GetField(string fieldName)
        => _source.GetField(fieldName);
    /// <inheritdoc/>
    protected override ICompareField GetCompareField(string fieldName)
        => _source.GetCompareField(fieldName);
    /// <inheritdoc/>
    protected override IField NewField(string fieldName)
         => _source.NewField(fieldName);
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        _source.Write(engine, sql);
        WriteOrderBy(engine, sql);
    }
    #endregion
}
