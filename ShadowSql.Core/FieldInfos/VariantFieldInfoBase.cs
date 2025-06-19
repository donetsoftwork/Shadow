using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 字段变形
/// </summary>
/// <param name="field">字段</param>
public abstract class VariantFieldInfoBase<TField>(TField field)
    : ISqlEntity
    where TField : ISqlEntity
{
    #region 配置
    /// <summary>
    /// 被去重的字段
    /// </summary>
    protected readonly TField _target = field;
    /// <summary>
    /// 被去重的字段
    /// </summary>
    public TField Target
        => _target;
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    protected abstract void WriteCore(ISqlEngine engine, StringBuilder sql);
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteCore(engine, sql);
}
