using ShadowSql.Expressions.Visit;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.VisitSource;

/// <summary>
/// 联表表查询表达式解析
/// </summary>
/// <param name="joinOn">联接</param>
/// <param name="leftEntity"></param>
/// <param name="rightEntity"></param>
public class JoinOnVisitor(IJoinOn joinOn, Expression leftEntity, Expression rightEntity)
     : VisitSourceBase(rightEntity), IFieldProvider
{
    #region 配置
    private readonly IJoinOn _joinOn = joinOn;
    private readonly IAliasTable _leftTable = joinOn.Left;
    private readonly IAliasTable _rightTable = joinOn.JoinSource;
    private readonly Expression _leftEntity = leftEntity;
    /// <summary>
    /// 联表对象
    /// </summary>
    public IJoinOn JoinOn
        => _joinOn;
    /// <summary>
    /// 左表
    /// </summary>
    public IAliasTable LeftTable
        => _leftTable;
    /// <summary>
    /// 右表
    /// </summary>
    public IAliasTable RightTable
    => _rightTable;
    /// <summary>
    /// 左对象
    /// </summary>
    public Expression LeftEntity
        => _leftEntity;
    /// <inheritdoc/>
    public override IEnumerable<IField> Fields
        => _rightTable.Fields;
    #endregion
    #region IFieldProvider
    /// <inheritdoc/>
    public override IEnumerable<IField> GetFieldsByMember(MemberExpression member)
    {
        var entity = member.Expression;
        if (entity == _leftEntity)
            return [TableVisitor.GetFieldByName(_leftTable, member.Member.Name)];
        else if (entity == _entity)
            return [TableVisitor.GetFieldByName(_rightTable, member.Member.Name)];
        return [];
    }
    /// <inheritdoc/>
    public override IField? GetFieldByName(string fieldName)
        => _joinOn.GetField(fieldName);
    #endregion
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TField"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static IEnumerable<IField> GetFieldsByExpression<TLeft, TRight, TField>(IJoinOn joinOn, Expression<Func<TLeft, TRight, TField>> expression)
    {
        var fields = new List<IField>();
        var visitor = new FieldVisitor(new JoinOnVisitor(joinOn, expression.Parameters[0], expression.Parameters[1]), fields);
        visitor.Visit(expression.Body);
        return fields;
    }
    #region LogicVisitor
    /// <summary>
    /// 解析联表表达式
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="logic">查询逻辑</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static LogicVisitor On<TLeft, TRight>(IJoinOn joinOn, Logic logic, Expression<Func<TLeft, TRight, bool>> expression)
    {
        var visitor = new LogicVisitor(new JoinOnVisitor(joinOn, expression.Parameters[0], expression.Parameters[1]), logic);
        visitor.Visit(expression.Body);
        return visitor;
    }
    #endregion
}
