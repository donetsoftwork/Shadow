using ShadowSql.Engines;
using ShadowSql.FieldInfos;
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
    : SqlAlias<TTable>, IAliasTable
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
        _prefixColumns = [.. GetPrefixColumns(_tablePrefix, target.Columns)];
    }
    #region 配置
    private readonly string[] _tablePrefix;
    //内联的展开运算符“..”
    /// <summary>
    /// 表前缀包装列
    /// </summary>
    private readonly List<IPrefixColumn> _prefixColumns;
    /// <summary>
    /// 表前缀包装的列
    /// </summary>
    public IEnumerable<IPrefixColumn> PrefixColumns
        => _prefixColumns;
    #endregion
    /// <summary>
    /// 添加列
    /// </summary>
    /// <param name="column"></param>
    public IPrefixColumn AddColumn(IColumn column)
    {
        var prefixColumn = column.GetPrefixColumn(_tablePrefix);
        _prefixColumns.Add(prefixColumn);
        return prefixColumn;
    }

    /// <summary>
    /// 转化联表字段
    /// </summary>
    /// <param name="tablePrefix"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    public static IEnumerable<IPrefixColumn> GetPrefixColumns(string[] tablePrefix, IEnumerable<IColumn> columns)
    {
        foreach (var column in columns)
            yield return column.GetPrefixColumn(tablePrefix);
    }
    /// <summary>
    /// sql拼写
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="builder"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder builder)
    {
        _target.Write(engine, builder);
        engine.TableAs(builder, _name);
    }
    /// <summary>
    /// 是否匹配
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public override bool IsMatch(string name)
    {
        return base.IsMatch(name)
            || _target.IsMatch(name);
    }
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public IPrefixColumn? GetPrefixColumn(string columName)
    {

        //return _columns.FirstOrDefault(c => c.IsMatch(columName));
        return _prefixColumns.FirstOrDefault(c => Identifier.Match(c.ViewName, columName))
            ?? Table.GetColumnWithTablePrefix(_name, _prefixColumns, columName)
            ?? Table.GetColumnWithTablePrefix(_target.Name, _prefixColumns, columName);
    }
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    public IPrefixColumn? GetPrefixColumn(IColumn column)
        => _prefixColumns.FirstOrDefault(c => c.IsMatch(column));
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public IField Field(string fieldName)
        => new AliasTableFieldInfo(this, fieldName);
    ITableView IAliasTable.Target
        => _target;
    #region ITableView
    IEnumerable<IColumn> ITableView.Columns
        => _prefixColumns;
    IColumn? ITableView.GetColumn(string columName)
        => GetPrefixColumn(columName);
    #endregion
}
