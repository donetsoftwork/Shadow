using ShadowSql.Assigns;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 修改基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
/// <param name="filter"></param>
public abstract class UpdateBase<TSource>(TSource source, ISqlLogic filter)
    : UpdateBase(filter), IUpdate
    where TSource : ITableView
{
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    protected TSource _source = source;
    /// <summary>
    /// 源表
    /// </summary>
    public TSource Source
        => _source;
    #endregion  
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public override IColumn? GetColumn(string columName)
        => _source.GetColumn(columName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override IField Field(string fieldName)
        => _source.Field(fieldName);
    #region IUpdate
    IUpdate IUpdate.Set(IAssignOperation operation)
    {
        SetCore(operation);
        return this;
    }
    ITableView IUpdate.Table
        => _source;
    #endregion
}

/// <summary>
/// 修改基类
/// </summary>
/// <param name="filter"></param>
public abstract class UpdateBase(ISqlLogic filter)
{
    #region 配置
    /// <summary>
    /// 过滤条件
    /// </summary>
    protected readonly ISqlLogic _filter = filter;
    /// <summary>
    /// 过滤条件
    /// </summary>
    public ISqlLogic Filter
        => _filter;
    /// <summary>
    /// 修改操作
    /// </summary>
    protected readonly List<IAssignOperation> _assignInfos = [];
    /// <summary>
    /// 赋值操作
    /// </summary>
    public IEnumerable<IAssignOperation> AssignInfos
        => _assignInfos;
    #endregion
    #region Set
    /// <summary>
    /// 添加修改信息
    /// </summary>
    /// <param name="operation"></param>
    internal void SetCore(IAssignOperation operation)
    {
        _assignInfos.Add(operation);
    }
    #endregion    

    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public abstract void Write(ISqlEngine engine, StringBuilder sql);
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public abstract IColumn? GetColumn(string columName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public abstract IField Field(string fieldName);
    /// <summary>
    /// 获取赋值字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public IAssignView GetAssignField(string fieldName)
    {
        if (GetColumn(fieldName) is IAssignView assignField)
            return assignField;
        return Field(fieldName);
    }
}
