using Hand.Structural;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Text;

namespace ShadowSql.Variants;

/// <summary>
/// 字段变形
/// </summary>
/// <param name="original">原始字段</param>
public abstract class VariantFieldInfoBase<TField>(TField original)
    : ISqlEntity, IWrapper<TField>
    where TField : ISqlEntity
{
    #region 配置
    /// <summary>
    /// 原始字段
    /// </summary>
    protected readonly TField _original = original;
    /// <summary>
    /// 原始字段
    /// </summary>
    public TField Original
        => _original;
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
