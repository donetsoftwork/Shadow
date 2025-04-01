using ShadowSql.Engines;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SelectFields;

/// <summary>
/// 筛选字段基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="view"></param>
public abstract class SelectFieldsBase<TSource>(TSource view)
    : SelectFieldsBase, ISelectFields
    where TSource : ITableView
{
    #region 配置
    /// <summary>
    /// 表视图
    /// </summary>
    protected readonly TSource _source = view;
    /// <summary>
    /// 表视图
    /// </summary>
    public TSource View
        => _source;
    #endregion
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public override IColumn? GetColumn(string columnName)
        => _source.GetColumn(columnName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override IField Field(string fieldName)
        => _source.Field(fieldName);
}
/// <summary>
/// 筛选字段基类
/// </summary>
public abstract class SelectFieldsBase
{
    #region 配置
    /// <summary>
    /// 字段信息
    /// </summary>
    protected readonly List<IFieldView> _selected = [];
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
        _selected.Add(new AliasFieldInfo(statement, alias));
    }
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public abstract IColumn? GetColumn(string columnName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract IField Field(string fieldName);
    /// <summary>
    /// 构建列信息
    /// </summary>
    /// <param name="columnName"></param>
    internal IFieldView CheckField(string columnName)
    {
        if (GetColumn(columnName) is IColumn column)
            return column;
        return Field(columnName);
    }
    #endregion

    #region ISelectFields
    /// <summary>
    /// 拼写字段
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public bool WriteSelected(ISqlEngine engine, StringBuilder sql)
    {
        bool appended = false;
        foreach (var field in _selected)
        {
            //var point = sql.Length;
            if (appended)
                sql.Append(',');
            field.Write(engine, sql);
            appended = true;
        }
        return appended;
    }
    #endregion
}
