using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 字段变形
/// </summary>
/// <param name="target"></param>
public abstract class VariantFieldInfoBase(IFieldView target)
    : ISqlEntity
{
    #region 配置
    /// <summary>
    /// 被去重的字段
    /// </summary>
    protected readonly IFieldView _target = target;
    /// <summary>
    /// 被去重的字段
    /// </summary>
    public IFieldView Target
        => _target;
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected abstract void Write(ISqlEngine engine, StringBuilder sql);

    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => Write(engine, sql);
}
