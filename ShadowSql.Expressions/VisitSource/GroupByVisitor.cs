using ShadowSql.Aggregates;
using ShadowSql.Expressions.Visit;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ShadowSql.Expressions.VisitSource;

/// <summary>
/// 分组表达式解析
/// </summary>
/// <param name="groupBy"></param>
/// <param name="source"></param>
/// <param name="entity"></param>
public class GroupByVisitor(IGroupByView groupBy, ITableView source, Expression entity)
    : GroupByKeyVisitor(groupBy, entity)
{
    /// <summary>
    /// 分组表达式解析
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="entity"></param>
    public GroupByVisitor(IGroupByView groupBy, Expression entity)
        : this(groupBy, groupBy.Source, entity)
    {
    }
    #region 配置
    private readonly ITableView _source = source;
    /// <summary>
    /// 源表
    /// </summary>
    public ITableView Source
        => _source;
    private readonly IField[] _key= [.. groupBy.Fields];
    /// <summary>
    /// 分组键
    /// </summary>
    public IField[] Key
        => _key;
    /// <summary>
    /// 分组键名常量
    /// </summary>
    public const string KEYNAME = nameof(Key);
    #endregion
    #region IFieldProvider
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public override IEnumerable<IField> GetFieldsByMember(MemberExpression member)
    {
        if (member.Expression == _entity)
        {
            var fieldName = member.Member.Name;
            if (_groupBy.GetField(fieldName) is IField field)
                return [field];
            if (fieldName == KEYNAME)
                return _key;
        }
        return [];
    }
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override IField? GetFieldByName(string fieldName)
        => _groupBy.GetField(fieldName);
    #endregion
    /// <summary>
    /// 从聚合方法中获取源表字段
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    private IField? GetSourceFieldByExpression(Expression expression)
    {
        if (expression is LambdaExpression method)
            return GetSourceFieldByExpression(_groupBy, _source, method.Parameters[0], method.Body);
        return null;
    }
    #region IField
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public override IField? GetFieldByExpression(Expression expression)
    {
        if (expression == _entity)
            return TableVisitor.GetField(Fields);
        if (expression is MemberExpression member)
        {
            if (member.Expression == _entity)
            {
                var fieldName = member.Member.Name;
                if (_groupBy.GetField(fieldName) is IField field)
                    return field;
                if (fieldName == KEYNAME)
                    return TableVisitor.GetField(_key);
            }
        }
        return null;
    }
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public override IEnumerable<IField> GetFieldsByExpression(Expression expression)
    {
        if (expression == _entity)
            return Fields;
        if (expression is MemberExpression member)
        {
            if (member.Expression == _entity)
            {
                var fieldName = member.Member.Name;
                if (_groupBy.GetField(fieldName) is IField field)
                    return [field];
                if (fieldName == KEYNAME)
                    return _key;
            }
        }
        return [];
    }
    #endregion
    #region IFieldView
    /// <summary>
    /// 从参数中筛选
    /// </summary>
    /// <param name="argument"></param>
    /// <param name="memberInfo"></param>
    /// <returns></returns>
    public override IEnumerable<IFieldView> SelectFieldsByAssignment(Expression argument, MemberInfo memberInfo)
    {
        if (argument is MethodCallExpression method)
        {
            if (GetAggregateFieldAlias(method, memberInfo) is IAggregateFieldAlias field)
                yield return field;
        }
        else if (argument is MemberExpression member)
        {
            var fieldName = member.Member.Name;
            if (member.Expression is Expression entity)
            {
                if (entity == _entity)
                {
                    if (_groupBy.GetField(fieldName) is IField field)
                    {
                        yield return SelectVisitor.SelectField(field, memberInfo.Name);
                        yield break;
                    }
                    else if (fieldName == KEYNAME)
                    {
                        foreach (var item in _key)
                            yield return item;
                        yield break;
                    }                    
                }
                else if (entity is MemberExpression member2 && member2.Expression == _entity && member2.Member.Name == KEYNAME)
                {
                    if (_groupBy.GetField(fieldName) is IField field)
                    {
                        yield return SelectVisitor.SelectField(field, memberInfo.Name);
                        yield break;
                    }
                }
            }
            yield return SelectVisitor.SelectParameter(member.Member, memberInfo.Name);
        }
        else if (GetCompareFieldByExpression(argument) is ICompareView compare)
        {
            yield return new AliasFieldInfo(compare, memberInfo.Name);
        }
    }
    /// <summary>
    /// 从方法调用中获取比较字段
    /// </summary>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    public override ICompareView? GetCompareFieldByMethodCall(MethodCallExpression methodCall)
        => GetAggregateField(methodCall);
    /// <summary>
    /// 从属性中筛选
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public override IEnumerable<IFieldView> SelectFieldsByMember(MemberExpression member)
    {
        var fieldName = member.Member.Name;
        if (member.Expression is Expression entity)
        {
            if (entity == _entity)
            {
                if (_groupBy.GetField(fieldName) is IField field)
                {
                    yield return field;
                }
                else if (fieldName == KEYNAME)
                {
                    foreach (var item in _key)
                        yield return item;
                }
                yield break;
            }
            else if(entity is MemberExpression member2 && member2.Expression == _entity && member2.Member.Name == KEYNAME)
            {
                if (_groupBy.GetField(fieldName) is IField field)
                {
                    yield return field;
                    yield break;
                }
            }
        }
        yield return SelectVisitor.SelectParameter(member.Member);
    }
    #endregion
    #region ICompareView
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public override ICompareView? GetCompareFieldByExpression(Expression expression)
    {
        if (expression is MethodCallExpression method)
        {
            if (GetAggregateField(method) is IAggregateField field)
                return field;
            return null;
        }
        return base.GetCompareFieldByExpression(expression);
    }
    #endregion    
    /// <summary>
    /// 获取聚合字段
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public IAggregateField? GetAggregateField(MethodCallExpression method)
    {
        var arguments = method.Arguments;
        var count = arguments.Count;
        if (method.Object != null || count == 0)
            throw new NotSupportedException("不支持函数" + method.Method.Name);

        switch (method.Method.Name)
        {
            case AggregateMethod.Count:
                if (count == 1)
                    return CountFieldInfo.Instance;
                else if (count == 2 && GetSourceFieldByExpression(arguments[1]) is IField field)
                    return field.DistinctCount();
                break;
            case AggregateMethod.Sum:
                if (count == 2 && GetSourceFieldByExpression(arguments[1]) is IField field2)
                    return field2.Sum();
                break;
            case AggregateMethod.Avg:
                if (count == 2 && GetSourceFieldByExpression(arguments[1]) is IField field3)
                    return field3.Avg();
                break;
            case AggregateMethod.Min:
                if (count == 2 && GetSourceFieldByExpression(arguments[1]) is IField field4)
                    return field4.Min();
                break;
            case AggregateMethod.Max:
                if (count == 2 && GetSourceFieldByExpression(arguments[1]) is IField field5)
                    return field5.Max();
                break;
            default:
                break;
        }
        throw new NotSupportedException("不支持函数" + method.Method.Name);
    }
    /// <summary>
    /// 获取聚合字段别名
    /// </summary>
    /// <param name="method"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public IAggregateFieldAlias? GetAggregateFieldAlias(MethodCallExpression method, MemberInfo member)
    {
        var arguments = method.Arguments;
        var count = arguments.Count;
        if (method.Object != null || count == 0)
            throw new NotSupportedException("不支持函数" + method.Method.Name);

        switch (method.Method.Name)
        {
            case AggregateMethod.Count:
                if (count == 1)
                    return CountAliasFieldInfo.Use(member.Name);
                else if (count == 2 && GetSourceFieldByExpression(arguments[1]) is IField field)
                    return field.DistinctCountAs(member.Name);
                break;
            case AggregateMethod.Sum:
                if (count == 2 && GetSourceFieldByExpression(arguments[1]) is IField field2)
                    return field2.SumAs(member.Name);
                break;
            case AggregateMethod.Avg:
                if (count == 2 && GetSourceFieldByExpression(arguments[1]) is IField field3)
                    return field3.AvgAs(member.Name);
                break;
            case AggregateMethod.Min:
                if (count == 2 && GetSourceFieldByExpression(arguments[1]) is IField field4)
                    return field4.MinAs(member.Name);
                break;
            case AggregateMethod.Max:
                if (count == 2 && GetSourceFieldByExpression(arguments[1]) is IField field5)
                    return field5.MaxAs(member.Name);
                break;
            default:
                break;
        }
        throw new NotSupportedException("不支持函数" + method.Method.Name);
    }
    #region SelectVisitor
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="fields"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static SelectVisitor Select<TKey, TEntity, T>(IGroupByView groupBy, List<IFieldView> fields, Expression<Func<IGrouping<TKey, TEntity>, T>> expression)
    {
        var visitor = new SelectVisitor(new GroupByVisitor(groupBy, expression.Parameters[0]), fields);
        visitor.Visit(expression.Body);
        return visitor;
    }
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="source"></param>
    /// <param name="fields"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    internal static SelectVisitor Select<TKey, TEntity, T>(IGroupByView groupBy, ITableView source, List<IFieldView> fields, Expression<Func<IGrouping<TKey, TEntity>, T>> expression)
    {
        var visitor = new SelectVisitor(new GroupByVisitor(groupBy, source, expression.Parameters[0]), fields);
        visitor.Visit(expression.Body);
        return visitor;
    }
    #endregion
    #region LogicVisitor
    /// <summary>
    /// 解析查询表达式
    /// </summary>
    /// <typeparam name="TKey">分组键类型</typeparam>
    /// <typeparam name="TEntity">源对象类型</typeparam>
    /// <param name="groupBy"></param>
    /// <param name="logic"></param>
    /// <param name="expression">查询表达式</param>
    /// <returns></returns>
    public static LogicVisitor Having<TKey, TEntity>(IGroupByView groupBy, Logic logic, Expression<Func<IGrouping<TKey, TEntity>, bool>> expression)
    {
        var visitor = new LogicVisitor(new GroupByVisitor(groupBy, expression.Parameters[0]), logic);
        visitor.Visit(expression.Body);
        return visitor;
    }
    /// <summary>
    /// 解析查询表达式
    /// </summary>
    /// <typeparam name="TKey">分组键类型</typeparam>
    /// <typeparam name="TEntity">源对象类型</typeparam>
    /// <typeparam name="TParameter">参数类型</typeparam>
    /// <param name="groupBy"></param>
    /// <param name="logic"></param>
    /// <param name="expression">查询表达式</param>
    /// <returns></returns>
    public static LogicVisitor Having<TKey, TEntity, TParameter>(IGroupByView groupBy, Logic logic, Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> expression)
    {
        var visitor = new LogicVisitor(new GroupByVisitor(groupBy, expression.Parameters[0]), logic);
        visitor.Visit(expression.Body);
        return visitor;
    }
    #endregion
    #region MultiLogicVisitor
    /// <summary>
    /// 解析查询表达式
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="table"></param>
    /// <param name="logic"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    internal static LogicVisitor Having<TKey, TEntity>(IGroupByView groupBy, ITableView table, Logic logic, Expression<Func<IGrouping<TKey, TEntity>, bool>> expression)
    {
        var visitor = new LogicVisitor(new GroupByVisitor(groupBy, table, expression.Parameters[0]), logic);
        visitor.Visit(expression.Body);
        return visitor;
    }
    /// <summary>
    /// 解析查询表达式
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="table"></param>
    /// <param name="logic"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    internal static LogicVisitor Having<TKey, TEntity, TParameter>(IGroupByView groupBy, ITableView table, Logic logic, Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> expression)
    {
        var visitor = new LogicVisitor(new GroupByVisitor(groupBy, table, expression.Parameters[0]), logic);
        visitor.Visit(expression.Body);
        return visitor;
    }
    #endregion
    #region OrderByVisitor
    ///// <summary>
    ///// 解析表达式
    ///// </summary>
    ///// <typeparam name="TKey"></typeparam>
    ///// <typeparam name="TOrder"></typeparam>
    ///// <param name="groupBy"></param>
    ///// <param name="fields"></param>
    ///// <param name="expression"></param>
    ///// <returns></returns>
    //public static OrderByVisitor OrderBy<TKey, TOrder>(IGroupByView groupBy, ICollection<IOrderAsc> fields, Expression<Func<TKey, TOrder>> expression)
    //{
    //    var visitor = new OrderByVisitor(new GroupByVisitor(groupBy, expression.Parameters[0]), fields);
    //    visitor.Visit(expression.Body);
    //    return visitor;
    //}
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TOrder"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="fields"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static OrderByVisitor OrderBy<TKey, TEntity, TOrder>(IGroupByView groupBy, List<IOrderAsc> fields, Expression<Func<IGrouping<TKey, TEntity>, TOrder>> expression)
    {
        var visitor = new OrderByVisitor(new GroupByVisitor(groupBy, expression.Parameters[0]), fields);
        visitor.Visit(expression.Body);
        return visitor;
    }
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TOrder"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="source"></param>
    /// <param name="fields"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    internal static OrderByVisitor OrderBy<TKey, TEntity, TOrder>(IGroupByView groupBy, ITableView source, List<IOrderAsc> fields, Expression<Func<IGrouping<TKey, TEntity>, TOrder>> expression)
    {
        var visitor = new OrderByVisitor(new GroupByVisitor(groupBy, source, expression.Parameters[0]), fields);
        visitor.Visit(expression.Body);
        return visitor;
    }
    #endregion
    #region SourceField
    /// <summary>
    /// 获取源表字段
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="source"></param>
    /// <param name="entity"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static IField? GetSourceFieldByExpression(IGroupByView groupBy, ITableView source, Expression entity, Expression expression)
    {
        if (entity == expression)
            return TableVisitor.GetField(groupBy.Source.Fields);
        if (expression is MemberExpression member)
            return GetSourceFieldByMember(groupBy, source, entity, member);
        return null;
    }
    /// <summary>
    /// 获取源表字段
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="source"></param>
    /// <param name="entity"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    public static IField? GetSourceFieldByMember(IGroupByView groupBy, ITableView source, Expression entity, MemberExpression member)
    {
        if (member.Expression == entity)
        {
            var fieldName = member.Member.Name;
            if (fieldName == KEYNAME)
                return TableVisitor.GetField(groupBy.Fields);
            return TableVisitor.GetFieldByName(source, fieldName);
        }
        return null;
    }
    #endregion

    //#region SourceFields
    ///// <summary>
    ///// 获取源表字段
    ///// </summary>
    ///// <param name="groupBy"></param>
    ///// <param name="entity"></param>
    ///// <param name="expression"></param>
    ///// <returns></returns>
    //public static IEnumerable<IField> GetSourceFieldsByExpression(IGroupByView groupBy, Expression entity, Expression expression)
    //{
    //    if (entity == expression)
    //        return groupBy.Source.Fields;
    //    if(expression is MemberExpression member)
    //        return GetSourceFieldsByMember(groupBy, entity, member);
    //    return [];
    //}
    ///// <summary>
    ///// 获取源表字段
    ///// </summary>
    ///// <param name="groupBy"></param>
    ///// <param name="entity"></param>
    ///// <param name="member"></param>
    ///// <returns></returns>
    //public static IEnumerable<IField> GetSourceFieldsByMember(IGroupByView groupBy, Expression entity, MemberExpression member)
    //{
    //    if (member.Expression == entity)
    //    {
    //        var fieldName = member.Member.Name;
    //        if (fieldName == KEYNAME)
    //            return groupBy.Fields;
    //        return [TableVisitor.GetField(groupBy.Source, fieldName)];
    //    }
    //    return [];
    //}
    //#endregion
}
