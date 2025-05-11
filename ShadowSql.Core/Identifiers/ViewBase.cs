using System.Collections.Generic;

namespace ShadowSql.Identifiers;

/// <summary>
/// 视图基类(不实现接口)
/// </summary>
public abstract class ViewBase : GetFieldBase
{
    #region ITableView
    /// <summary>
    /// 所有字段
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<IField> GetFields();

    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected virtual ICompareField GetCompareField(string fieldName)
        => GetField(fieldName) ?? NewField(fieldName);
    #endregion

}
