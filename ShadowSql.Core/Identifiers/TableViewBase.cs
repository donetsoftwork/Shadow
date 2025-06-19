using System.Collections.Generic;

namespace ShadowSql.Identifiers;

/// <summary>
/// 视图基类(实现接口)
/// </summary>
public abstract class TableViewBase : ViewBase, ITableView
{
    #region ITableView
    /// <inheritdoc/>
    IEnumerable<IField> ITableView.Fields
        => GetFields();
    /// <inheritdoc/>
    IField? ITableView.GetField(string fieldName)
        => GetField(fieldName);
    /// <inheritdoc/>
    ICompareField ITableView.GetCompareField(string fieldName)
        => GetCompareField(fieldName);
    /// <inheritdoc/>
    IField ITableView.NewField(string fieldName)
        => NewField(fieldName);
    #endregion
}
