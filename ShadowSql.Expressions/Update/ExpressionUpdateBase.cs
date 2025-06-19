using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Update;
using System;
using System.Text;

namespace ShadowSql.Expressions.Update;

/// <summary>
/// 修改基类
/// </summary>
/// <param name="source"></param>
public abstract class ExpressionUpdateBase<TTable>(TTable source)
    : UpdateBase, IUpdate
    where TTable : IUpdateTable
{
    #region 配置
    /// <summary>
    /// 源表
    /// </summary>
    internal TTable _source = source;
    /// <summary>
    /// 源表
    /// </summary>
    public TTable Source
        => _source;
    #endregion  
    #region ISqlEntity   
    /// <inheritdoc/>
    protected override void WriteSource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    #endregion    
    #region UpdateBase
    /// <inheritdoc/>
    /// <exception cref="ArgumentException"></exception>
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

