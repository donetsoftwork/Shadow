using System.Collections.Generic;

namespace ShadowSql.Identifiers;

/// <summary>
/// 表对象基类
/// </summary>
/// <param name="name"></param>
public abstract class TableBase(string name)
    : Identifier(name), ITable, IInsertTable, IUpdateTable, ITableView
{
    #region 基础
    #region 配置
    /// <summary>
    /// 插入列
    /// </summary>
    internal abstract IEnumerable<IColumn> InsertColumns { get; }
    /// <summary>
    /// 修改列
    /// </summary>
    internal abstract IEnumerable<IColumn> UpdateColumns { get; }
    /// <summary>
    /// 列
    /// </summary>
    public abstract IEnumerable<IColumn> Columns { get; }
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName"></param>
    protected virtual IField NewField(string fieldName)
        => Column.Use(fieldName);
    #endregion
    /// <summary>
    /// 查找列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public abstract IColumn? GetColumn(string columName);
    #endregion
    #region ITable
    IEnumerable<IColumn> IInsertTable.InsertColumns
        => InsertColumns;
    IEnumerable<IAssignView> IUpdateTable.UpdateColumns
        => UpdateColumns;
    #endregion
    #region ITableView
    string IView.ViewName
        => _name;
    IEnumerable<IField> ITableView.Fields
        => Columns;
    /// <summary>
    /// 查找列
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    IField? ITableView.GetField(string fieldName)
        => GetColumn(fieldName);
    ICompareField ITableView.GetCompareField(string fieldName)
        => GetColumn(fieldName) ?? Column.Use(fieldName);
    IField ITableView.NewField(string fieldName)
        => NewField(fieldName);
    #endregion
}
