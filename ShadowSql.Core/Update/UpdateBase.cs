using ShadowSql.Assigns;
using ShadowSql.Engines;
using ShadowSql.FieldInfos;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Update;

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
    protected abstract IColumn? GetColumn(string columName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected virtual IField Field(string fieldName)
        => FieldInfo.Use(fieldName);
    /// <summary>
    /// 获取赋值字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    internal IAssignView GetAssignField(string fieldName)
    {
        if (GetColumn(fieldName) is IAssignView assignField)
            return assignField;
        return Field(fieldName);
    }
}

