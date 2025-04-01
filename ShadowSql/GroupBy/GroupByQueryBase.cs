using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 分组sql查询基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
/// <param name="fields"></param>
/// <param name="having"></param>
public abstract class GroupByQueryBase<TSource>(TSource source, IFieldView[] fields, Logic having)
    : GroupByLogicBase(fields, having)
    where TSource : ITableView
{
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    protected readonly TSource _source = source;
    /// <summary>
    /// 数据源表
    /// </summary>
    public TSource Source
        => _source;
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 输出数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteGroupBySource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    #endregion
}
/// <summary>
/// 分组sql查询基类
/// </summary>
/// <param name="fields"></param>
/// <param name="having"></param>
public abstract class GroupByLogicBase(IFieldView[] fields, Logic having)
    : GroupByBase<Logic>(fields, having)
{
    #region FilterBase
    /// <summary>
    /// 增加逻辑
    /// </summary>
    /// <param name="condition"></param>
    internal override void AddLogic(AtomicLogic condition)
        => _having.AddLogic(condition);
    /// <summary>
    /// And查询
    /// </summary>
    internal override void ToAndCore()
        => _having = _having.ToAnd();
    /// <summary>
    /// Or查询
    /// </summary>
    internal override void ToOrCore()
        => _having = _having.ToOr();
    #region Logic
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(AtomicLogic condition)
        => _having = _having.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(AndLogic condition)
        => _having = _having.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(ComplexAndLogic condition)
        => _having = _having.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(OrLogic condition)
        => _having = _having.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(ComplexOrLogic condition)
        => _having = _having.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(Logic condition)
        => _having = _having.And(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(AtomicLogic condition)
        => _having = _having.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(AndLogic condition)
        => _having = _having.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(ComplexAndLogic condition)
        => _having = _having.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(OrLogic condition)
        => _having = _having.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(ComplexOrLogic condition)
        => _having = _having.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(Logic condition)
        => _having = _having.Or(condition);
    #endregion
    #endregion
}
