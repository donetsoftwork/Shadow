using ShadowSql.Expressions.Visit;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.VisitSource;

/// <summary>
/// 从分组键获取字段
/// </summary>
/// <param name="groupBy">分组查询</param>
/// <param name="entity"></param>
public class GroupByKeyVisitor(IGroupByView groupBy, Expression entity)
    : VisitSourceBase(entity), IFieldProvider
{
    #region 配置
    /// <summary>
    /// 分组视图
    /// </summary>
    protected readonly IGroupByView _groupBy = groupBy;
    /// <summary>
    /// 分组视图
    /// </summary>
    public IGroupByView GroupBy
        => _groupBy;
    /// <summary>
    /// 分组字段
    /// </summary>
    public override IEnumerable<IField> Fields
        => _groupBy.Fields;
    #endregion
    #region IFieldProvider
    /// <inheritdoc/>
    public override IEnumerable<IField> GetFieldsByMember(MemberExpression member)
        => GetKeyByMember(_groupBy, _entity, member);
    /// <inheritdoc/>
    public override IField? GetFieldByName(string fieldName)
        => _groupBy.GetField(fieldName);
    #endregion
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="entity"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    public static IEnumerable<IField> GetKeyByMember(IGroupByView groupBy, Expression entity, MemberExpression member)
    {
        if (member == entity)
            return groupBy.Fields;
        if (member.Expression == entity)
        {
            if (groupBy.GetField(member.Member.Name) is IField field)
                return [field];
        }
        return [];
    }
    #region IField
    /// <summary>
    /// 获取分组字段
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="groupBy">分组查询</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static IEnumerable<IField> GetKeys<TKey, T>(IGroupByView groupBy, Expression<Func<TKey, T>> expression)
    {
        var fields = new List<IField>();
        var visitor = new FieldVisitor(new GroupByKeyVisitor(groupBy, expression.Parameters[0]), fields);
        visitor.Visit(expression.Body);
        return fields;
    }
    #endregion
    #region SelectVisitor
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fields">字段</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static SelectVisitor Select<TEntity, T>(IGroupByView groupBy, List<IFieldView> fields, Expression<Func<TEntity, T>> select)
    {
        var visitor = new SelectVisitor(new GroupByKeyVisitor(groupBy, select.Parameters[0]), fields);
        visitor.Visit(select.Body);
        return visitor;
    }
    #endregion
}
