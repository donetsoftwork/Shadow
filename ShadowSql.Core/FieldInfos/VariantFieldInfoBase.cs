using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 字段变形
/// </summary>
/// <param name="target"></param>
public abstract class VariantFieldInfoBase<TField>(TField target)
    : ISqlEntity
    where TField : ISqlEntity
{
    #region 配置
    /// <summary>
    /// 被去重的字段
    /// </summary>
    protected readonly TField _target = target;
    /// <summary>
    /// 被去重的字段
    /// </summary>
    public TField Target
        => _target;
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected abstract void WriteCore(ISqlEngine engine, StringBuilder sql);
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => WriteCore(engine, sql);
}
