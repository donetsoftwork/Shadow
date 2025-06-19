using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.Variants;

/// <summary>
/// 表别名
/// </summary>
/// <typeparam name="TTable"></typeparam>
public class TableAlias<TTable>
    : SqlAlias<TTable>, IAliasTable<TTable>
    where TTable : ITable
{
    /// <summary>
    /// 表别名
    /// </summary>
    /// <param name="target"></param>
    /// <param name="tableAlias"></param>
    public TableAlias(TTable target, string tableAlias)
        :base(target, tableAlias)
    {
        _tablePrefix = [tableAlias, "."];
        //内联的展开运算符“..”
        _prefixFields = [.. PrefixField.GetFields(_tablePrefix, target.Columns)];
    }
    #region 配置
    private readonly string[] _tablePrefix;
    /// <summary>
    /// 表前缀包装列
    /// </summary>
    private readonly List<IPrefixField> _prefixFields;
    /// <summary>
    /// 表前缀包装的列
    /// </summary>
    public IEnumerable<IPrefixField> PrefixFields
        => _prefixFields;
    #endregion
    /// <summary>
    /// 添加列
    /// </summary>
    /// <param name="column">列</param>
    public IPrefixField AddColumn(IColumn column)
    {
        var prefixField = new PrefixField(column, _tablePrefix);
        _prefixFields.Add(prefixField);
        return prefixField;
    }
    /// <inheritdoc/>
    internal override void Write(ISqlEngine engine, StringBuilder builder)
    {
        _target.Write(engine, builder);
        engine.TableAs(builder, _name);
    }
    /// <inheritdoc/>
    internal override bool IsMatch(string name)
    {
        return base.IsMatch(name)
            || _target.IsMatch(name);
    }
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName">列名</param>
    /// <returns></returns>
    internal IPrefixField? GetPrefixField(string columName)
    {
        if(_prefixFields.Count == 0)
            return null;
        return _prefixFields.FirstOrDefault(c => Identifier.Match(c.ViewName, columName))
            ?? Table.GetFieldWithTablePrefix(_name, _prefixFields, columName)
            ?? Table.GetFieldWithTablePrefix(_target.Name, _prefixFields, columName);
    }
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="field">字段</param>
    /// <returns></returns>
    internal IPrefixField? GetPrefixField(IField field)
        => _prefixFields.FirstOrDefault(c => c.IsMatch(field));
    #region IAliasTable
    /// <inheritdoc/>
    ITable IAliasTable.Target
        => _target;
    /// <inheritdoc/>
    IPrefixField? IAliasTable.GetPrefixField(string fieldName)
        => GetPrefixField(fieldName);
    /// <inheritdoc/>
    IPrefixField? IAliasTable.GetPrefixField(IField field)
        => GetPrefixField(field);
    #endregion
    #region ITableView
    /// <inheritdoc/>
    IEnumerable<IField> ITableView.Fields
        => _prefixFields;
    /// <inheritdoc/>
    ICompareField ITableView.GetCompareField(string fieldName)
        => GetPrefixField(fieldName) ?? new PrefixField(Column.Use(fieldName), _tablePrefix);
    /// <inheritdoc/>
    IField? ITableView.GetField(string fieldName)
        => GetPrefixField(fieldName);
    /// <inheritdoc/>
    IField ITableView.NewField(string fieldName)
        => new PrefixField(Column.Use(fieldName), _tablePrefix);
    /// <inheritdoc/>
    IPrefixField IAliasTable.NewPrefixField(IColumn column)
        => new PrefixField(column, _tablePrefix);
    #endregion
}
