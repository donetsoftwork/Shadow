using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ShadowSql.Identifiers;

/// <summary>
/// 表名对象
/// </summary>
/// <param name="tableName">表名</param>
public class Table(string tableName)
    : Identifier(tableName), ITable, IInsertTable, IUpdateTable
{
    #region 配置
    private readonly Dictionary<string, IColumn> _columns = new(StringComparer.OrdinalIgnoreCase);
#if NET9_0_OR_GREATER
    private readonly Lock _columnLock = new();
#endif
    private readonly HashSet<IColumn> _insertIgnores = [];
#if NET9_0_OR_GREATER
    private readonly Lock _insertIgnoreLock = new();
#endif
    private readonly HashSet<IColumn> _updateIgnores = [];
#if NET9_0_OR_GREATER
    private readonly Lock _updateIgnoreLock = new();
#endif
    /// <summary>
    /// 列
    /// </summary>
    public IEnumerable<IColumn> Columns
        => _columns.Values;
    /// <summary>
    /// 插入列
    /// </summary>
    internal IEnumerable<IColumn> InsertColumns
    {
        get
        {
            foreach (var column in Columns)
            {
                if (_insertIgnores.Contains(column))
                    continue;
                yield return column;
            }
        }
    }
    /// <summary>
    /// 修改列
    /// </summary>
    internal IEnumerable<IColumn> UpdateColumns
    {
        get
        {
            foreach (var column in Columns)
            {
                if (_updateIgnores.Contains(column))
                    continue;
                yield return column;
            }
        }
    }
    /// <summary>
    /// 插入忽略的列
    /// </summary>
    public IEnumerable<IColumn> InsertIgnores 
        => _insertIgnores;
    /// <summary>
    /// 修改忽略的列
    /// </summary>
    public IEnumerable<IColumn> UpdateIgnores 
        => _updateIgnores;
    #endregion
    /// <summary>
    /// 查找列
    /// </summary>
    /// <param name="columName">列名</param>
    /// <returns></returns>
    public IColumn? GetColumn(string columName)
    {
        if (_columns.TryGetValue(columName, out var column))
            return column;
        return GetFieldWithTablePrefix(_name, _columns.Values, columName);
    }
    #region DefineColumn
    /// <summary>
    /// 定义列
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    public IColumn DefineColumn(string columnName)
    {
        if (_columns.TryGetValue(columnName, out var column))
            return column;
#if NET9_0_OR_GREATER
        lock (_columnLock)
#else
        lock (_columns)
#endif
        {
            if (_columns.TryGetValue(columnName, out column))
                return column;
            column = Column.Use(columnName);
            _columns[columnName] = column;
        }
        return column;
    }
    /// <summary>
    /// 添加列
    /// </summary>
    /// <param name="column">列</param>
    public void AddColumn(IColumn column)
    {
        var columnName = column.ViewName;
        if (_columns.ContainsKey(columnName))
            return;
#if NET9_0_OR_GREATER
        lock (_columnLock)
#else
        lock (_columns)
#endif
        {
            if (_columns.ContainsKey(columnName))
                return;
            _columns[columnName] = column;
        }
    }
    #endregion
    #region IgnoreInsert
    /// <summary>
    /// 忽略插入的列
    /// </summary>
    /// <param name="column">列</param>
    public void AddInsertIgnore(IColumn column)
    {
        if (_insertIgnores.Contains(column))
            return;
#if NET9_0_OR_GREATER
            lock (_insertIgnoreLock)
#else
        lock (_insertIgnores)
#endif
        {
            if (_insertIgnores.Contains(column))
                return;
            _insertIgnores.Add(column);
        }
    }
    //    /// <summary>
    //    /// 忽略插入的列
    //    /// </summary>
    //    /// <param name="columns">列</param>
    //    /// <returns></returns>
    //    public Table IgnoreInsert(params IEnumerable<IColumn> columns)
    //    {
    //        foreach (var column in columns)
    //        {
    //            if (_insertIgnores.Contains(column))
    //                continue;
    //#if NET9_0_OR_GREATER
    //            lock (_insertIgnoreLock)
    //#else
    //            lock (_insertIgnores)
    //#endif
    //            {
    //                if (_insertIgnores.Contains(column))
    //                    continue;
    //                _insertIgnores.Add(column);
    //            }
    //        }
    //        return this;
    //    }
    #endregion
    #region IgnoreUpdate
    /// <summary>
    /// 忽略修改的列
    /// </summary>
    /// <param name="column">列</param>
    public void AddUpdateIgnore(IColumn column)
    {
        if (_updateIgnores.Contains(column))
            return;
#if NET9_0_OR_GREATER
        lock (_updateIgnoreLock)
#else
        lock (_updateIgnores)
#endif
        {
            if (_updateIgnores.Contains(column))
                return;
            _updateIgnores.Add(column);
        }
    }
    #endregion
    /// <summary>
    /// 获取含表名前缀的列
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <param name="columName">列名</param>
    /// <returns></returns>
    internal static TField? GetFieldWithTablePrefix<TField>(string table, IEnumerable<TField> fields, string columName)
        where TField : class, IField
    {
        if (CheckTablePrefix(table, columName))
        {
            var innerName = columName[(table.Length + 1)..];
            return fields.FirstOrDefault(c => Match(c.ViewName, innerName));
        }
        return null;
    }
    /// <summary>
    /// 判断是否含表名前缀
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columName">列名</param>
    /// <returns></returns>
    public static bool CheckTablePrefix(string table, string columName)
    {
        var prefixIndex = table.Length;
        return columName.Length > prefixIndex
            && columName.StartsWith(table)
            && columName[prefixIndex] == '.';
    }
    #region ITable
    /// <inheritdoc/>
    IEnumerable<IColumn> IInsertTable.InsertColumns
        => InsertColumns;
    /// <inheritdoc/>
    IColumn? IInsertTable.GetInsertColumn(string columnName)
    {
        if (GetColumn(columnName) is IColumn column)
        {
            if (_insertIgnores.Contains(column))
                return null;
            return column;
        }
        return Column.Use(columnName);
    }
    /// <inheritdoc/>
    IEnumerable<IAssignView> IUpdateTable.AssignFields
        => UpdateColumns;
    /// <inheritdoc/>
    IAssignView? IUpdateTable.GetAssignField(string fieldName)
    {
        if (GetColumn(fieldName) is IColumn column)
        {
            if (_updateIgnores.Contains(column))
                return null;
            return column;
        }
        return Column.Use(fieldName);
    }
    #endregion
    #region ITableView
    /// <inheritdoc/>
    IEnumerable<IField> ITableView.Fields
        => Columns;
    /// <inheritdoc/>
    IField? ITableView.GetField(string fieldName)
        => GetColumn(fieldName);
    /// <inheritdoc/>
    ICompareField ITableView.GetCompareField(string fieldName)
        => GetColumn(fieldName) ?? Column.Use(fieldName);
    /// <inheritdoc/>
    IField ITableView.NewField(string fieldName)
        => Column.Use(fieldName);
    #endregion
}
