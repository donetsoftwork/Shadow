using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Expressions.SelectFields;

/// <summary>
/// 筛选字段基类
/// </summary>
/// <typeparam name="TTarget"></typeparam>
/// <param name="target"></param>
public abstract class SelectFieldsBase<TTarget>(ITableView target)
    : SelectFieldsBase, ISelectFields
{
    #region 配置
    /// <summary>
    /// 筛选对象
    /// </summary>
    internal readonly ITableView _target = target;
    /// <summary>
    /// 筛选对象
    /// </summary>
    public ITableView Target
        => _target;
    #endregion
    #region GetFieldBase
    /// <summary>
    /// 获取所有字段
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IField> GetFields()
        => _target.Fields;
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField? GetField(string fieldName)
        => _target.GetField(fieldName);
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField NewField(string fieldName)
        => _target.NewField(fieldName);
    #endregion
    #region ISelectFields
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
        => WriteSelectedCore(engine, sql, false);
    IEnumerable<IColumn> ISelectFields.ToColumns()
        => ToColumnsCore();
    #endregion
}
