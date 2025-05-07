using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Identifiers;

/// <summary>
/// 视图基类(不实现接口)
/// </summary>
public abstract class ViewBase : ISqlEntity
{
    #region ITableView
    /// <summary>
    /// 所有字段
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<IField> GetFields();
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected abstract IField? GetField(string fieldName);
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected virtual ICompareField GetCompareField(string fieldName)
        => GetField(fieldName) ?? NewField(fieldName);
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName"></param>
    protected abstract IField NewField(string fieldName);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected abstract void WriteCore(ISqlEngine engine, StringBuilder sql);
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteCore(engine, sql);
    #endregion
}
