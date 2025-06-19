using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 修改基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="table">表</param>
public abstract class UpdateBase<TSource>(TSource table)
    : UpdateBase, IUpdate
    where TSource : IUpdateTable
{
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    internal TSource _source = table;
    /// <summary>
    /// 源表
    /// </summary>
    public TSource Source
        => _source;
    #endregion  
    #region ISqlEntity   
    /// <inheritdoc/>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    #endregion    
    #region UpdateBase
    /// <inheritdoc/>
    internal override IAssignView GetAssignField(string fieldName)
        => _source.GetAssignField(fieldName)
        ?? throw new ArgumentException(fieldName + "字段不存在", nameof(fieldName));
    #endregion
    #region IUpdate
    /// <inheritdoc/>
    IUpdateTable IUpdate.Table
        => _source;
    #endregion
}

