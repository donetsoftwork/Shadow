using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;

namespace Shadow.DDL.Schemas;

/// <summary>
/// 列定义
/// </summary>
/// <param name="name"></param>
/// <param name="sqlType"></param>
public class ColumnSchema(string name, string sqlType = "INT")
    : ColumnBase(name), IColumn, IMatch
{
    private readonly string _sqlType = sqlType.ToUpperInvariant();
    /// <summary>
    /// 数据库类型
    /// </summary>
    public string SqlType
        => _sqlType;

    private string _default = string.Empty;
    /// <summary>
    /// 数据库默认值
    /// </summary>
    public string Default
    {
        get { return _default; }
        set { _default = value; }
    }
    private bool _notNull = false;
    /// <summary>
    /// 是否NotNull
    /// </summary>
    public bool NotNull
    {
        get { return _notNull; }
        set { _notNull = value; }
    }
    private ColumnType _columnType = ColumnType.Normal;
    /// <summary>
    /// 字段类型
    /// </summary>
    public ColumnType ColumnType
    {
        get { return _columnType; }
        set { _columnType = value; }
    }

    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public IPrefixColumn GetPrefixColumn(params string[] prefix)
    {
        return new PrefixColumn(this, prefix);
    }    
    string IView.ViewName
        => _name;
    /// <summary>
    /// 转化为列
    /// </summary>
    /// <returns></returns>
    public IColumn ToColumn()
        => this;
    #region IMatch
    /// <summary>
    /// 是否匹配
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    internal bool IsMatch(string name)
        => Identifier.Match(name, _name);
    bool IMatch.IsMatch(string name)
        => IsMatch(name);
    #endregion
}

/// <summary>
/// 字段类型
/// </summary>
[Flags]
public enum ColumnType
{
    /// <summary>
    /// 空
    /// </summary>
    Empty = 0,
    /// <summary>
    /// 自增列
    /// </summary>
    Identity = 1,
    /// <summary>
    /// 主键之一
    /// </summary>
    Key = 2,
    /// <summary>
    /// 唯一列
    /// </summary>
    Unique = 4,
    /// <summary>
    /// 非空
    /// </summary>
    NOTNULL = 8,
    /// <summary>
    /// 普通
    /// </summary>
    Normal = 16,
    /// <summary>
    /// 计算列
    /// </summary>
    Computed = 32,
    /// <summary>
    /// 逻辑删除
    /// </summary>
    Del = 64,
    /// <summary>
    /// 忽略
    /// </summary>
    Ignore = 128
}
