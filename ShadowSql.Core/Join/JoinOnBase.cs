using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联基类
/// </summary>
public abstract class JoinOnBase
    : FilterBase, IMultiView
{
    #region 配置
    private string _joinType = " INNER JOIN ";
    /// <summary>
    /// 联表类型
    /// </summary>
    public string JoinType
        => _joinType;
    /// <summary>
    /// 成员表
    /// </summary>
    protected readonly List<IAliasTable> _tables = [];
    /// <summary>
    /// 成员表
    /// </summary>
    public IEnumerable<IAliasTable> Tables
        => _tables;
    #endregion
    #region JoinType
    private void AsType(string type)
       => _joinType = type;
    /// <summary>
    /// 内联
    /// </summary>
    /// <returns></returns>
    internal void AsInnerJoinCore()
        => AsType(" INNER JOIN ");
    /// <summary>
    /// 外联
    /// </summary>
    /// <returns></returns>
    internal void AsOuterJoinCore()
        => AsType(" OUTER JOIN ");
    /// <summary>
    /// 左联
    /// </summary>
    /// <returns></returns>
    internal void AsLeftJoinCore()
        => AsType(" LEFT JOIN ");
    /// <summary>
    /// 右联
    /// </summary>
    /// <returns></returns>
    internal void AsRightJoinCore()
        => AsType(" RIGHT JOIN ");
    #endregion
    #region Column
    /// <summary>
    /// 获取左边列
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract IPrefixField? GetLeftField(string fieldName);
    /// <summary>
    /// 获取右边列
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract IPrefixField? GetRightField(string fieldName);
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public IPrefixField? GetPrefixField(string fieldName)
    {
        return GetRightField(fieldName)
            ?? GetLeftField(fieldName);
    }
    #endregion
    #region Field
    /// <summary>
    /// 获取右边字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract IField LeftField(string fieldName);
    /// <summary>
    /// 获取右边字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract IField RightField(string fieldName);
    #endregion
    #region ICompareField
    /// <summary>
    /// 左边比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public ICompareView GetLeftCompareField(string fieldName)
    {
        if (GetLeftField(fieldName) is IColumn column)
            return column;
        return LeftField(fieldName);
    }
    /// <summary>
    /// 右边比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public ICompareView GetRightCompareField(string fieldName)
    {
        if (GetRightField(fieldName) is IColumn column)
            return column;
        return RightField(fieldName);
    }
    #endregion
    #region IMultiTable
    /// <summary>
    /// 获取联表成员
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public abstract IAliasTable? GetMember(string tableName);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 筛选前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void FilterPrefix(ISqlEngine engine, StringBuilder sql)
        => engine.JoinOnPrefix(sql);
    /// <summary>
    /// 拼写联接右表
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected abstract void WriteRightSource(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_joinType);
        WriteRightSource(engine, sql);
    }
    #endregion
}
