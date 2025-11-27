using Hand.Cache;
using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace ShadowSql.Tables;

/// <summary>
/// 简单表名对象
/// </summary>
public class EmptyTable : Identifier, ITable, IInsertTable, IUpdateTable
{
    private EmptyTable(string tableName)
        : base(tableName)
    {
    }
    /// <summary>
    /// 获取表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public static EmptyTable Use(string tableName)
        => _cacher.Get(tableName);
    #region ITable
    /// <inheritdoc/>
    IEnumerable<IColumn> ITable.Columns => [];
    /// <inheritdoc/>
    IColumn? ITable.GetColumn(string columName) => null;
    /// <inheritdoc/>
    IEnumerable<IField> ITableView.Fields => [];
    /// <inheritdoc/>
    IEnumerable<IColumn> IInsertTable.InsertColumns => [];
    /// <inheritdoc/>
    IEnumerable<IAssignView> IUpdateTable.AssignFields => [];
    /// <inheritdoc/>
    IField? ITableView.GetField(string fieldName) => null;
    /// <inheritdoc/>
    ICompareField ITableView.GetCompareField(string fieldName)
        => Column.Use(fieldName);
    /// <inheritdoc/>
    IField ITableView.NewField(string fieldName)
        => Column.Use(fieldName);
    /// <inheritdoc/>
    IColumn? IInsertTable.GetInsertColumn(string columnName)
        => Column.Use(columnName);
    /// <inheritdoc/>
    IAssignView? IUpdateTable.GetAssignField(string fieldName)
        => Column.Use(fieldName);
    #endregion
    #region TableCacher
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly TableCacher _cacher = new(new DictionaryCacher<string, EmptyTable>());
    /// <summary>
    /// 列缓存器
    /// </summary>
    /// <param name="cacher"></param>
    internal class TableCacher(ICacher<string, EmptyTable> cacher)
        : CacheFactoryBase<string, EmptyTable>(cacher)
    {
        /// <inheritdoc/>
        protected override EmptyTable CreateNew(in string key)
            => new(key);
    }
    #endregion
}
