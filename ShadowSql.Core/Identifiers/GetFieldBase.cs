using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Text;

namespace ShadowSql.Identifiers;

/// <summary>
/// 获取字段基类
/// </summary>
public abstract class GetFieldBase : ISqlEntity
{
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected abstract IField? GetField(string fieldName);
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName"></param>
    protected abstract IField NewField(string fieldName);
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
