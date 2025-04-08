using ShadowSql.Assigns;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 修改基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
public abstract class UpdateBase<TSource>(TSource source)
    : UpdateBase, IUpdate
    where TSource : ITableView
{
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    internal TSource _source = source;
    /// <summary>
    /// 源表
    /// </summary>
    public TSource Source
        => _source;
    #endregion  
    #region ISqlEntity   
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
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
public abstract class UpdateBase : ISqlEntity
{
    #region 配置
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
    #region ISqlEntity
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => Write(engine, sql);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    protected virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        WriteUpdate(engine, sql);
        WriteSet(engine, sql);
        WriteSource(engine, sql);
    }
    /// <summary>
    /// 拼写Update子句
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected virtual void WriteUpdate(ISqlEngine engine, StringBuilder sql)
        => engine.UpdatePrefix(sql);
    /// <summary>
    /// 拼写Set子句
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <exception cref="InvalidOperationException"></exception>
    protected void WriteSet(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(" SET ");
        var appended = false;
        foreach (var assign in _assignInfos)
        {
            if (appended)
                sql.Append(',');
            assign.Write(engine, sql);
            appended = true;
        }
        if (appended)
            return;
        throw new InvalidOperationException("没有设置修改信息");
    }
    /// <summary>
    /// 拼写数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected abstract void WriteSource(ISqlEngine engine, StringBuilder sql);
    #endregion
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

