using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Expressions.Select;

/// <summary>
/// 多表筛选基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="view"></param>
/// <param name="multiView">多(联)表</param>
public abstract class MultiSelectBase<TSource>(TSource view, IMultiView multiView)
    : SelectBase<TSource, IMultiView>(view, multiView), IMultiSelect, ISelect
    where TSource : ITableView
{
    #region 配置
    private readonly List<IAliasTable> _selectTables = [];
    /// <summary>
    /// 选择表
    /// </summary>
    public ICollection<IAliasTable> SelectTables
        => _selectTables;
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override bool WriteSelectedCore(ISqlEngine engine, StringBuilder sql, bool appended)
    {
        foreach (var table in _selectTables)
        {
            if (appended)
                sql.Append(',');
            sql.Append(table.Alias)
                .Append('.')
                .Append('*');
            appended = true;
        }
        return base.WriteSelectedCore(engine, sql, appended);
    }
    #endregion
}
