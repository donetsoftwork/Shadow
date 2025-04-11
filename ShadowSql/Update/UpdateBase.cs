using ShadowSql.Assigns;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
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

