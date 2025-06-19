using ShadowSql.Expressions.Select;
using ShadowSql.Expressions.Visit;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.VisitSource;

/// <summary>
/// 表查询表达式解析
/// </summary>
public class TableVisitor : VisitSourceBase, IFieldProvider
{
    internal TableVisitor(Expression entity, ITableView table)
        : base(entity)
    {
        _table = table;
    }
    /// <summary>
    /// 解析表
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="entity"></param>
    public TableVisitor(ITable table, Expression entity)
        : this(entity, table)
    {
    }
    /// <summary>
    /// 解析别名表
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="entity"></param>
    public TableVisitor(IAliasTable table, Expression entity)
        : this(entity, table)
    {
    }
    #region 配置
    private readonly ITableView _table;
    /// <summary>
    /// 表视图
    /// </summary>
    public ITableView Table
        => _table;
    /// <inheritdoc/>
    public override IEnumerable<IField> Fields
        => _table.Fields;
    #endregion
    #region IFieldProvider
    /// <inheritdoc/>
    public override IEnumerable<IField> GetFieldsByMember(MemberExpression member)
        => GetFields(_entity, member, _table);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    public override IField? GetFieldByName(string fieldName)
        => GetFieldByName(_table, fieldName);
    #endregion
    #region GetField
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="table">表</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static IField? GetFieldByExpression<TEntity, T>(ITable table, Expression<Func<TEntity, T>> expression)
        => GetFieldByExpression(expression, table);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="table">表</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static IField? GetFieldByExpression<TEntity, T>(IAliasTable table, Expression<Func<TEntity, T>> expression)
        => GetFieldByExpression(expression, table);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    internal static IField GetFieldByName(ITableView table, string fieldName)
        => table.GetField(fieldName) ?? table.NewField(fieldName);
    ///// <summary>
    ///// 获取表字段
    ///// </summary>
    ///// <param name="table">表</param>
    ///// <param name="member"></param>
    ///// <returns></returns>
    //public static IField GetField(ITable table, MemberExpression member)
    //    => GetField(table, member);
    ///// <summary>
    ///// 获取别名表字段
    ///// </summary>
    ///// <param name="table">表</param>
    ///// <param name="member"></param>
    ///// <returns></returns>
    //public static IField GetField(IAliasTable table, MemberExpression member)
    //    => GetField(table, member);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static IField? GetField(IEnumerable<IField> list)
    {
        var fields = list.ToArray();
        if (fields.Length == 1)
            return fields[0];
        return null;
    }
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="member"></param>
    /// <param name="table">表</param>
    /// <returns></returns>
    internal static IField? GetFieldByMember(Expression entity, MemberExpression member, ITableView table)
    {
        if (entity == member)
            return GetField(table.Fields);
        if (member.Expression == entity)
            return GetFieldByName(table, member.Member.Name);
        return null;
    }
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TField"></typeparam>
    /// <param name="table">表</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    internal static IField? GetFieldByExpression<TEntity, TField>(Expression<Func<TEntity, TField>> expression, ITableView table)
    {
        if (expression.Body is MemberExpression member)
            return GetFieldByMember(expression.Parameters[0], member, table);
        return null;
    }
    #endregion
    #region GetFields
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="table">表</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static IEnumerable<IField> GetFieldsByExpression<TEntity, T>(ITable table, Expression<Func<TEntity, T>> expression)
        => GetFieldsByExpression(expression, table);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="table">表</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static IEnumerable<IField> GetFieldsByExpression<TEntity, T>(IAliasTable table, Expression<Func<TEntity, T>> expression)
        => GetFieldsByExpression(expression, table);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TField"></typeparam>
    /// <param name="table">表</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    internal static IEnumerable<IField> GetFieldsByExpression<TEntity, TField>(Expression<Func<TEntity, TField>> expression, ITableView table)
    {
        //不只是MemberExpression一种情况
        //if(expression.Body is MemberExpression member)
        //    return GetFields(expression.Parameters[0], member, table);
        //return [];
        var fields = new List<IField>();
        var visitor = new FieldVisitor(new TableVisitor(expression.Parameters[0], table), fields);
        visitor.Visit(expression.Body);
        return fields;
    }
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="member"></param>
    /// <param name="table">表</param>
    /// <returns></returns>
    private static IEnumerable<IField> GetFields(Expression entity, MemberExpression member, ITableView table)
    {
        if (entity == member)
            return table.Fields;
        if (member.Expression == entity)
            return [GetFieldByName(table, member.Member.Name)];
        return [];
    }
    #endregion
    #region SelectVisitor
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static SelectVisitor Select<TEntity, T>(TableSelect<TEntity> select, Expression<Func<TEntity, T>> expression)
        => Select(select._selected, expression, select._target);
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static SelectVisitor Select<TEntity, T>(ITable table, List<IFieldView> fields, Expression<Func<TEntity, T>> expression)
        => Select(fields, expression, table);
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static SelectVisitor Select<TEntity, T>(IAliasTable table, List<IFieldView> fields, Expression<Func<TEntity, T>> expression)
        => Select(fields, expression, table);
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    internal static SelectVisitor Select<TEntity, T>(List<IFieldView> fields, Expression<Func<TEntity, T>> expression, ITableView table)
    {
        var visitor = new SelectVisitor(new TableVisitor(expression.Parameters[0], table), fields);
        visitor.Visit(expression.Body);
        return visitor;
    }
    #endregion
    #region LogicVisitor
    #region ITable
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="logic">查询逻辑</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static LogicVisitor Where<TEntity>(ITable table, Logic logic, Expression<Func<TEntity, bool>> expression)
        => Where(logic, expression, table);
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="table">表</param>
    /// <param name="logic">查询逻辑</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static LogicVisitor Where<TEntity, TParameter>(ITable table, Logic logic, Expression<Func<TEntity, TParameter, bool>> expression)
        => Where(logic, expression, table);
    #endregion
    #region IAliasTable
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="logic">查询逻辑</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static LogicVisitor Where<TEntity>(IAliasTable table, Logic logic, Expression<Func<TEntity, bool>> expression)
        => Where(logic, expression, table);
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="table">表</param>
    /// <param name="logic">查询逻辑</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static LogicVisitor Where<TEntity, TParameter>(IAliasTable table, Logic logic, Expression<Func<TEntity, TParameter, bool>> expression)
        => Where(logic, expression, table);
    #endregion
    #region ITableView
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="logic">查询逻辑</param>
    /// <param name="expression">表达式</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    internal static LogicVisitor Where<TEntity>(Logic logic, Expression<Func<TEntity, bool>> expression, ITableView table)
    {
        var visitor = new LogicVisitor(new TableVisitor(expression.Parameters[0], table), logic);
        visitor.Visit(expression.Body);
        return visitor;
    }
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="logic">查询逻辑</param>
    /// <param name="expression">表达式</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    internal static LogicVisitor Where<TEntity, TParameter>(Logic logic, Expression<Func<TEntity, TParameter, bool>> expression, ITableView table)
    {
        var visitor = new LogicVisitor(new TableVisitor(expression.Parameters[0], table), logic);
        visitor.Visit(expression.Body);
        return visitor;
    }
    #endregion
    #endregion
    #region OrderByVisitor
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TOrder"></typeparam>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static OrderByVisitor OrderBy<TEntity, TOrder>(ITable table, List<IOrderAsc> fields, Expression<Func<TEntity, TOrder>> expression)
       => OrderBy(fields, expression, table);
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TOrder"></typeparam>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    public static OrderByVisitor OrderBy<TEntity, TOrder>(IAliasTable table, List<IOrderAsc> fields, Expression<Func<TEntity, TOrder>> expression)
        => OrderBy(fields, expression, table);
    /// <summary>
    /// 解析表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TOrder"></typeparam>
    /// <param name="fields">字段</param>
    /// <param name="expression">表达式</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    internal static OrderByVisitor OrderBy<TEntity, TOrder>(List<IOrderAsc> fields, Expression<Func<TEntity, TOrder>> expression, ITableView table)
    {
        var visitor = new OrderByVisitor(new TableVisitor(expression.Parameters[0], table), fields);
        visitor.Visit(expression.Body);
        return visitor;
    }
    #endregion
}
