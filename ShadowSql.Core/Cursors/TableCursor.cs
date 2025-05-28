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
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="source"></param>
    internal TableCursor(int limit, int offset, ITableView source)
        : base(limit, offset)
    {
        _source = source;
    }
    /// <summary>
    /// 表范围筛选游标
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public TableCursor(ITable source, int limit = 0, int offset = 0)
        : this(limit, offset, source)
    {
    }
    /// <summary>
    /// 别名表范围筛选游标
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public TableCursor(IAliasTable source, int limit = 0, int offset = 0)
        : this(limit, offset, source)
    {
    }
    /// <summary>
    /// 查询再范围筛选游标
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public TableCursor(IDataFilter source, int limit = 0, int offset = 0)
        : this(limit, offset, source)
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
    ICursor ICursor.Take(int limit)
    {
        TakeCore(limit);
        return this;
    }
    ICursor ICursor.Skip(int offset)
    {
        SkipCore(offset);
        return this;
    }
    #endregion
    #region TableViewBase
    /// <summary>
    /// 所有字段
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
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override ICompareField GetCompareField(string fieldName)
        => _source.GetCompareField(fieldName);
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
    /// <exception cref="System.NotImplementedException"></exception>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
    {
        _source.Write(engine, sql);
        WriteOrderBy(engine, sql);
    }
    #endregion
}
