using ShadowSql.Engines;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SelectFields;

/// <summary>
/// 筛选字段基类
/// </summary>
public abstract class SelectFieldsBase : GetFieldBase, ISelectFields
{
    #region 配置
    /// <summary>
    /// 字段信息
    /// </summary>
    private readonly List<IFieldView> _selected = [];
    /// <summary>
    /// 字段信息
    /// </summary>
    public IEnumerable<IFieldView> Selected
        => _selected;
    #endregion
    #region 功能
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="fields"></param>
    internal void SelectCore(params IEnumerable<IFieldView> fields)
    {
        foreach (var field in fields)
            _selected.Add(field);
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="field"></param>
    internal void SelectCore(IFieldView field)
    {
        _selected.Add(field);
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    internal void SelectCore(params IEnumerable<string> columns)
    {
        foreach (var columnName in columns)
            _selected.Add(CheckField(columnName));
    }
    /// <summary>
    /// 筛选别名
    /// </summary>
    /// <param name="alias"></param>
    /// <param name="statement"></param>
    /// <returns></returns>
    internal void AliasCore(string alias, string statement)
    {
        _selected.Add(new RawFieldAliasInfo(statement, alias));
    }
    /// <summary>
    /// 构建列信息
    /// </summary>
    /// <param name="fieldName"></param>
    internal IFieldView CheckField(string fieldName)
    {
        if (GetField(fieldName) is IField field)
            return field;
        return NewField(fieldName);
    }
    #endregion
    #region ISelectFields
    bool ISelectFields.WriteSelected(ISqlEngine engine, StringBuilder sql)
        => WriteSelectedCore(engine, sql, false);
    IEnumerable<IColumn> ISelectFields.ToColumns()
        => ToColumnsCore();
    /// <summary>
    /// 拼写筛选字段列表
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="appended"></param>
    /// <returns></returns>
    protected virtual bool WriteSelectedCore(ISqlEngine engine, StringBuilder sql, bool appended)
        => WriteSelectFields(engine, sql, _selected, appended);
    /// <summary>
    /// 拼写筛选字段列表
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="fields"></param>
    /// <param name="appended"></param>
    /// <returns></returns>
    protected static bool WriteSelectFields(ISqlEngine engine, StringBuilder sql, IEnumerable<IFieldView> fields, bool appended)
    {
        foreach (var field in fields)
        {
            if (appended)
                sql.Append(',');
            field.Write(engine, sql);
            appended = true;
        }
        return appended;
    }
    /// <summary>
    /// 获取列
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerable<IColumn> ToColumnsCore()
    {
        foreach (var field in _selected)
            yield return field.ToColumn();
    }
    #endregion
}
