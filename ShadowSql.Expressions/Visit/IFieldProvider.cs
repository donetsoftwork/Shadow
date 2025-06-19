using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ShadowSql.Expressions.Visit;

/// <summary>
/// 获取字段接口
/// </summary>
public interface IFieldProvider
{
    #region IField
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    IEnumerable<IField> GetFieldsByMember(MemberExpression member);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    IEnumerable<IField> GetFieldsByExpression(Expression expression);
    ///// <summary>
    ///// 从构造对象中获取字段
    ///// </summary>
    ///// <param name="node"></param>
    ///// <returns></returns>
    //IEnumerable<IField> GetFieldsByNew(NewExpression node);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    IField? GetFieldByName(string fieldName);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    IField? GetFieldByMember(MemberExpression member);
    /// <summary>
    /// 获取字段
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    IField? GetFieldByExpression(Expression expression);
    #endregion
    #region IFieldView
    ///// <summary>
    ///// 从构造对象中筛选
    ///// </summary>
    ///// <param name="node"></param>
    ///// <returns></returns>
    //IEnumerable<IFieldView> SelectFieldsByNew(NewExpression node);
    /// <summary>
    /// 从赋值中筛选
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <param name="info"></param>
    /// <returns></returns>
    IEnumerable<IFieldView> SelectFieldsByAssignment(Expression expression, MemberInfo info);
    /// <summary>
    /// 从属性中筛选
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    IEnumerable<IFieldView> SelectFieldsByMember(MemberExpression member);
    #endregion
    #region ICompareView
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns></returns>
    ICompareView? GetCompareFieldByExpression(Expression expression);
    /// <summary>
    /// 从方法调用中获取比较字段
    /// </summary>
    /// <param name="methodCall"></param>
    /// <returns></returns>
    ICompareView? GetCompareFieldByMethodCall(MethodCallExpression methodCall);
    #endregion
}