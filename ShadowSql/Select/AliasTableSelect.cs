using ShadowSql.AliasTables;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SelectFields;
using ShadowSql.Tables;
using ShadowSql.Variants;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 别名表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
/// <param name="fields"></param>
public sealed class AliasTableSelect<TTable>(ITableView source, AliasTableFields<TTable> fields)
    : SelectBase<ITableView, AliasTableFields<TTable>>(source, fields)
    where TTable : ITable
{
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="source"></param>
    public AliasTableSelect(TableAlias<TTable> source)
        : this(source, new AliasTableFields<TTable>(source))
    {
    }
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="source"></param>
    /// <param name="where"></param>
    public AliasTableSelect(TableAlias<TTable> source, ISqlLogic where)
        : this(new TableFilter(source, where), new AliasTableFields<TTable>(source))
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="query"></param>
    public AliasTableSelect(AliasTableSqlQuery<TTable> query)
        : this(query, new AliasTableFields<TTable>(query.Source))
    {
    }
}
/// <summary>
/// 别名表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public sealed class AliasTableCursorSelect<TTable>(ICursor cursor, AliasTableFields<TTable> fields)
    : SelectBase<ICursor, AliasTableFields<TTable>>(cursor, fields)
    where TTable : ITable
{
    /// <summary>
    /// 别名表范围(分页)及列筛选
    /// </summary>
    /// <param name="cursor"></param>
    public AliasTableCursorSelect(AliasTableCursor<TTable> cursor)
        : this(cursor, new AliasTableFields<TTable>(cursor.Source))
    {
    }

    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
}
