using System.Collections.Generic;

namespace ShadowSql.Identifiers;

/// <summary>
/// 视图基类(实现接口)
/// </summary>
public abstract class TableViewBase : ViewBase, ITableView
{
    #region ITableView
    IEnumerable<IField> ITableView.Fields
        => GetFields();
    IField? ITableView.GetField(string fieldName)
        => GetField(fieldName);
    ICompareField ITableView.GetCompareField(string fieldName)
        => GetCompareField(fieldName);
    IField ITableView.NewField(string fieldName)
        => NewField(fieldName);
    #endregion
}
