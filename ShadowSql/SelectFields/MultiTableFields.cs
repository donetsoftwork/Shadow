using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SelectFields;

/// <summary>
/// 多(联)表字段筛选
/// </summary>
/// <param name="source"></param>
public class MultiTableFields(IMultiView source) :
    SelectFieldsBase<IMultiView>(source), ISelectFields
{
    #region 配置
    private readonly List<IAliasTable> _selectTables = [];
    /// <summary>
    /// 选择表
    /// </summary>
    public IEnumerable<IAliasTable> SelectTables 
        => _selectTables;
    #endregion
    #region Select
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select"></param>
    public MultiTableFields Select(Func<IMultiView, IFieldView> select)
    {
        SelectCore(select(_source));
        return this;
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFields Select(string tableName, Func<IAliasTable, IFieldView> select)
    {
        var member = _source.From(tableName);
        SelectCore(select(member));
        return this;
    }
    /// <summary>
    /// 筛选多列
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFields Select(string tableName, Func<IAliasTable, IEnumerable<IFieldView>> select)
    {
        var member = _source.From(tableName);
        SelectCore(select(member));
        return this;
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFields Select<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable
    {
        var member = _source.Table<TTable>(tableName);
        //增加前缀
        if (member.GetPrefixColumn(select(member.Target)) is IPrefixColumn prefixColumn)
            SelectCore(prefixColumn);
        return this;
    }
    /// <summary>
    /// 筛选多列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableFields Select<TTable>(string tableName, Func<TTable, IEnumerable<IColumn>> select)
        where TTable : ITable
    {
        var member = _source.Table<TTable>(tableName);
        var columns = select(member.Target);
        foreach (var column in columns)
        {
            //增加前缀
            if (member.GetPrefixColumn(column) is IPrefixColumn prefixColumn)
                SelectCore(prefixColumn);
        }
        return this;
    }
    #endregion
    #region SelectTable
    /// <summary>
    /// 添加表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public MultiTableFields SelectTable(string tableName)
    {
            _selectTables.Add(_source.From(tableName));
        return this;
    }
    /// <summary>
    /// 添加表
    /// </summary>
    /// <param name="aliasTable"></param>
    /// <returns></returns>
    public MultiTableFields SelectTable(IAliasTable aliasTable)
    {
        _selectTables.Add(aliasTable);
        return this;
    }
    #endregion
    #region ISelectFields
    /// <summary>
    /// 写入筛选字段
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="appended"></param>
    /// <returns></returns>
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
    /// <summary>
    /// 获取列
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IColumn> ToColumnsCore()
    {
        foreach (var item in base.ToColumnsCore())
            yield return item;
        foreach (var table in _selectTables)
        {
            foreach (var column in table.PrefixColumns)
                yield return column;
        }
    }
    #endregion
}
