using ShadowSql.Assigns;
using ShadowSql.Identifiers;
using ShadowSql.SqlVales;
using ShadowSql.Update;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// Update扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region 赋参数
    /// <summary>
    /// 赋参数
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="columnName"></param>
    /// <param name="assign"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    public static TUpdate Set<TUpdate>(this TUpdate update, string columnName, AssignSymbol assign, string parameterName = "")
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(new AssignOperation(update.GetAssignField(columnName), assign, Parameter.Use(parameterName, columnName)));
        return update;
    }
    /// <summary>
    /// 赋参数
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="columnName"></param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    public static TUpdate Set<TUpdate>(this TUpdate update, string columnName, string parameterName = "")
        where TUpdate : UpdateBase, IUpdate
        => Set(update, columnName, AssignSymbol.EqualTo, parameterName);
    /// <summary>
    /// 赋参数
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static TUpdate Set<TUpdate>(this TUpdate update, params IEnumerable<string> columnNames)
        where TUpdate : UpdateBase, IUpdate
    {
        foreach (var columnName in columnNames)
            Set(update, columnName);
        return update;
    }
    #endregion
    #region 赋值
    /// <summary>
    /// 赋值
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="update"></param>3.
    /// <param name="columnName"></param>
    /// <param name="assign"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TUpdate SetValue<TUpdate, TValue>(this TUpdate update, string columnName, AssignSymbol assign, TValue value)
        where TUpdate : UpdateBase, IUpdate
    {
        update.Set(new AssignOperation(update.GetAssignField(columnName), assign, SqlValue.From(value)));
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
    public static TUpdate SetValue<TUpdate, TValue>(this TUpdate update, string columnName, TValue value)
        where TUpdate : UpdateBase, IUpdate
        => SetValue(update, columnName, AssignSymbol.EqualTo, value);
    #endregion
    #region 赋值操作
    /// <summary>
    /// 赋值操作
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="operation"></param>
    /// <returns></returns>
    public static TUpdate Set<TUpdate>(this TUpdate update, IAssignOperation operation)
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(operation);
        return update;
    }
    #endregion
}
