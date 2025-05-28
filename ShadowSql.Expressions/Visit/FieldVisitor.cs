using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 筛选解析基类
/// </summary>
public class FieldVisitor(IFieldProvider source, ICollection<IField> fields)
    : VisitorBase(source)
{
    #region 配置
    private readonly ICollection<IField> _fields = fields;
    /// <summary>
    /// 字段列表
    /// </summary>
    public IEnumerable<IField> Fields
        => _fields;
    #endregion
    /// <summary>
    /// 处理属性
    /// </summary>
    /// <param name="member"></param>
    protected override void CheckMember(MemberExpression member)
    {
        foreach (var field in _source.GetFieldsByMember(member))
            _fields.Add(field);
    }
    /// <summary>
    /// 处理赋值
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="info"></param>
    protected override void CheckAssignment(Expression expression, MemberInfo info)
    {
        if (_source.GetFieldByExpression(expression) is IField field)
        {
            var name = info.Name;
            if (field.IsMatch(name))
                _fields.Add(field);
            else
                throw new ArgumentException("不支持重命名字段");
        }
    }
    #region Visit
    ///// <summary>
    ///// 解析NewExpression
    ///// </summary>
    ///// <param name="node"></param>
    ///// <returns></returns>
    //protected override Expression VisitNew(NewExpression node)
    //{
    //    var members = node.Members;
    //    var arguments = node.Arguments;
    //    if (members is null || arguments is null)
    //        return node;
    //    for (int i = 0; i < arguments.Count; i++)
    //    {
    //        if (_source.GetFieldByExpression(arguments[i]) is IField field)
    //        {
    //            var name = members[i].Name;
    //            if (field.IsMatch(name))
    //                _fields.Add(field);
    //            else
    //                throw new ArgumentException("不支持重命名字段");
    //        }
    //    }
    //    return node;
    //}
    ///// <summary>
    ///// 解析MemberExpression
    ///// </summary>
    ///// <param name="memberExp"></param>
    ///// <returns></returns>
    //protected override Expression VisitMember(MemberExpression memberExp)
    //{
    //    if (_source.GetFieldByMember(memberExp) is IField field)
    //        _fields.Add(field);
    //    return memberExp;
    //}
    #endregion
    //#region 静态方法
    ///// <summary>
    ///// 解析表达式
    ///// </summary>
    ///// <typeparam name="TEntity"></typeparam>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="table"></param>
    ///// <param name="fields"></param>
    ///// <param name="expression"></param>
    ///// <returns></returns>
    //public static FieldVisitor Visit<TEntity, T>(ITableView table, ICollection<IField> fields, Expression<Func<TEntity, T>> expression)
    //{
    //    var visitor = new FieldVisitor(new TableVisitor(table, expression.Parameters[0]), fields);
    //    visitor.Visit(expression.Body);
    //    return visitor;
    //}
    ///// <summary>
    ///// 获取字段
    ///// </summary>
    ///// <typeparam name="TEntity"></typeparam>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="table"></param>
    ///// <param name="select"></param>
    ///// <returns></returns>
    //public static IEnumerable<IField> GetFields<TEntity, T>(ITableView table, Expression<Func<TEntity, T>> select)
    //{
    //    var fields = new List<IField>();
    //    Visit(table, fields, select);
    //    return fields;
    //}
    //#endregion
}
