using ShadowSql.Assigns;
using ShadowSql.Identifiers;
using ShadowSql.SqlVales;
using ShadowSql.Update;
using System;

namespace ShadowSql;

/// <summary>
/// Update扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region columnName
    /// <summary>
    /// 赋参数
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="columnName"></param>
    /// <param name="op"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TUpdate Set<TUpdate>(this TUpdate update, string columnName, string op = "=", string parameter = "")
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(CreateOperation(update, columnName, AssignSymbol.Get(op), parameter));
        return update;
    }
    /// <summary>
    /// 赋值
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="update"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <param name="op"></param>
    /// <returns></returns>
    public static TUpdate SetValue<TUpdate, TValue>(this TUpdate update, string columnName, TValue value, string op = "=")
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(new AssignOperation(update.GetAssignField(columnName), AssignSymbol.Get(op), SqlValue.From(value)));
        return update;
    }
    /// <summary>
    /// 赋参数
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="columnName"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static TUpdate SetEqualTo<TUpdate>(this TUpdate update, string columnName, string parameter = "")
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(CreateOperation(update, columnName, AssignSymbol.EqualTo, parameter));
        return update;
    }
    /// <summary>
    /// 赋值
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="update"></param>
    /// <param name="columnName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TUpdate SetEqualToValue<TUpdate, TValue>(this TUpdate update, string columnName, TValue value)
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(new AssignOperation(update.GetAssignField(columnName), AssignSymbol.EqualTo, SqlValue.From(value)));
        return update;
    }
    #endregion
    #region 赋值操作
    /// <summary>
    /// 赋值操作
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="operation"></param>
    /// <returns></returns>
    public static TUpdate Set<TUpdate>(this TUpdate update, IAssignInfo operation)
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(operation);
        return update;
    }
    #endregion
    #region 赋值sql
    /// <summary>
    /// 赋值原生sql
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="assignSql"></param>
    /// <returns></returns>
    public static TUpdate SetRaw<TUpdate>(this TUpdate update, string assignSql)
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(RawAssignInfo.Use(assignSql));
        return update;
    }
    #endregion
    #region MultiTableUpdate
    /// <summary>
    /// 指定被修改的表
    /// </summary>
    /// <typeparam name="TMultiUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static MultiTableUpdate Update<TMultiUpdate>(this TMultiUpdate update, string tableName)
        where TMultiUpdate : MultiTableUpdate
    {
        update._table = update.MultiTable.From(tableName);
        return update;
    }
    /// <summary>
    /// 指定被修改的表
    /// </summary>
    /// <typeparam name="TMultiUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static TMultiUpdate Update<TMultiUpdate>(this TMultiUpdate update, IAliasTable table)
        where TMultiUpdate : MultiTableUpdate
    {
        update._table = table;
        return update;
    }
    /// <summary>
    /// 赋值操作
    /// </summary>
    /// <typeparam name="TMultiUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="operation"></param>
    /// <returns></returns>
    public static TMultiUpdate Set<TMultiUpdate>(this TMultiUpdate update, Func<IAliasTable, IAssignInfo> operation)
        where TMultiUpdate : MultiTableUpdate
    {
        update.SetCore(operation(update._table));
        return update;
    }
    #endregion

    private static AssignOperation CreateOperation(UpdateBase update, string columnName, AssignSymbol op, string parameter)
    {
        var field = update.GetAssignField(columnName);
        parameter = Parameter.CheckName(parameter, columnName);
        return new AssignOperation(field, op, Parameter.Use(parameter));
    }
}
