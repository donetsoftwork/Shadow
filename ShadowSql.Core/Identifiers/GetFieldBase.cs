using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Identifiers;

/// <summary>
/// 获取字段基类
/// </summary>
public abstract class GetFieldBase : ISqlEntity
{
    /// <summary>
    /// 所有字段
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<IField> GetFields();
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    protected abstract IField? GetField(string fieldName);
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    protected abstract IField NewField(string fieldName);
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    protected abstract void WriteCore(ISqlEngine engine, StringBuilder sql);
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteCore(engine, sql);
    #endregion
}
