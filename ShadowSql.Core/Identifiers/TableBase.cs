using ShadowSql.FieldInfos;
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
    public abstract IEnumerable<IColumn> InsertColumns { get; }
    /// <summary>
    /// 修改列
    /// </summary>
    public abstract IEnumerable<IColumn> UpdateColumns { get; }
    /// <summary>
    /// 列
    /// </summary>
    public abstract IEnumerable<IColumn> Columns { get; }
    #endregion
    /// <summary>
    /// 查找列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public abstract IColumn? GetColumn(string columName);
    #endregion  
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public IField Field(string fieldName)
        => FieldInfo.Use(fieldName);
    IEnumerable<IColumn> IUpdateTable.UpdateColumns
        => UpdateColumns;
    #region ITableView
    string IView.ViewName
        => _name;
    IEnumerable<IColumn> ITableView.Columns
        => Columns;
    /// <summary>
    /// 查找列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    IColumn? ITableView.GetColumn(string columName)
        => GetColumn(columName);
    ICompareField ITableView.GetCompareField(string fieldName)
        => this.GetCompareField(fieldName);
    #endregion
}
