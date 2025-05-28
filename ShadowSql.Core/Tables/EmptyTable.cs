using ShadowSql.Identifiers;
using ShadowSql.Services;
using System.Collections.Generic;

namespace ShadowSql.Tables;

/// <summary>
/// 简单表名对象
/// </summary>
public class EmptyTable : Identifier, ITable, IInsertTable, IUpdateTable
{
    private EmptyTable(string name)
        : base(name)
    {
    }
    private static readonly CacheService<EmptyTable> _cacher = new(static name => new EmptyTable(name));
    /// <summary>
    /// 获取表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static EmptyTable Use(string tableName)
        => _cacher.Get(tableName);
    #region ITable
    IEnumerable<IColumn> ITable.Columns => [];
    IColumn? ITable.GetColumn(string columName) => null;
    IEnumerable<IField> ITableView.Fields => [];
    IEnumerable<IColumn> IInsertTable.InsertColumns => [];
    IEnumerable<IAssignView> IUpdateTable.AssignFields => [];

    IField? ITableView.GetField(string fieldName) => null;
    ICompareField ITableView.GetCompareField(string fieldName)
        => Column.Use(fieldName);
    IField ITableView.NewField(string fieldName)
        => Column.Use(fieldName);
    IColumn? IInsertTable.GetInsertColumn(string columnName)
        => Column.Use(columnName);
    IAssignView? IUpdateTable.GetAssignField(string fieldName)
        => Column.Use(fieldName);
    #endregion
}
