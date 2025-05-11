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
    /// <param name="column"></param>
    public IPrefixField AddColumn(IColumn column)
    {
        var prefixField = new PrefixField(column, _tablePrefix);
        _prefixFields.Add(prefixField);
        return prefixField;
    }
    /// <summary>
    /// sql拼写
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="builder"></param>
    /// <returns></returns>
    internal override void Write(ISqlEngine engine, StringBuilder builder)
    {
        _target.Write(engine, builder);
        engine.TableAs(builder, _name);
    }
    /// <summary>
    /// 是否匹配
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    internal override bool IsMatch(string name)
    {
        return base.IsMatch(name)
            || _target.IsMatch(name);
    }
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
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
    /// <param name="field"></param>
    /// <returns></returns>
    internal IPrefixField? GetPrefixField(IField field)
        => _prefixFields.FirstOrDefault(c => c.IsMatch(field));
    #region IAliasTable
    ITable IAliasTable.Target
        => _target;
    IPrefixField? IAliasTable.GetPrefixField(string fieldName)
        => GetPrefixField(fieldName);
    IPrefixField? IAliasTable.GetPrefixField(IField field)
        => GetPrefixField(field);
    #endregion
    #region ITableView
    IEnumerable<IField> ITableView.Fields
        => _prefixFields;
    ICompareField ITableView.GetCompareField(string fieldName)
        => GetPrefixField(fieldName) ?? new PrefixField(Column.Use(fieldName), _tablePrefix);
    IField? ITableView.GetField(string fieldName)
        => GetPrefixField(fieldName);
    IField ITableView.NewField(string fieldName)
        => new PrefixField(Column.Use(fieldName), _tablePrefix);
    IPrefixField IAliasTable.NewPrefixField(IColumn column)
        => new PrefixField(column, _tablePrefix);
    #endregion
}
