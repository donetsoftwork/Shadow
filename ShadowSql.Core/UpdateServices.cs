using ShadowSql.Assigns;
using ShadowSql.Identifiers;
using ShadowSql.SqlVales;
using ShadowSql.Update;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;
using System.Linq;

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
    /// <param name="columnName">列名</param>
    /// <param name="op">操作</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static TUpdate Set<TUpdate>(this TUpdate update, string columnName, string op = "=", string parameter = "")
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(CreateOperation(update, columnName, AssignSymbol.Get(op), parameter));
        return update;
    }
    /// <summary>
    /// 按字段修改
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="field">字段</param>
    /// <param name="op">操作</param>
    /// <returns></returns>
    public static TUpdate Set<TUpdate>(this TUpdate update, IAssignView field, string op = "=")
        where TUpdate : UpdateBase, IUpdate
    {        
        update.SetCore(new AssignOperation(field, AssignSymbol.Get(op), Parameter.Use(field.ViewName)));
        return update;
    }
    /// <summary>
    /// 按字段修改
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static TUpdate SetFields<TUpdate>(this TUpdate update, params IEnumerable<IAssignView> fields)
        where TUpdate : UpdateBase, IUpdate
    {
        var equalTo = AssignSymbol.Assign;
        foreach (var field in fields)
            update.SetCore(new AssignOperation(field, equalTo, Parameter.Use(field.ViewName)));
        return update;
    }
    /// <summary>
    /// 按自己的列修改
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <returns></returns>
    public static TUpdate SetSelfFields<TUpdate>(this TUpdate update)
        where TUpdate : UpdateBase, IUpdate
        => update.SetFields(update.Table.AssignFields);
    /// <summary>
    /// 赋值
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="update"></param>
    /// <param name="columnName">列名</param>
    /// <param name="value">值</param>
    /// <param name="op">操作</param>
    /// <returns></returns>
    public static TUpdate SetValue<TUpdate, TValue>(this TUpdate update, string columnName, TValue value, string op)
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
    /// <param name="columnName">列名</param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static TUpdate SetAssign<TUpdate>(this TUpdate update, string columnName, string parameter = "")
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(CreateOperation(update, columnName, AssignSymbol.Assign, parameter));
        return update;
    }
    /// <summary>
    /// 赋值
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="update"></param>
    /// <param name="columnName">列名</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static TUpdate SetValue<TUpdate, TValue>(this TUpdate update, string columnName, TValue value)
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(new AssignOperation(update.GetAssignField(columnName), AssignSymbol.Assign, SqlValue.From(value)));
        return update;
    }

    #endregion
    #region 赋值操作
    /// <summary>
    /// 赋值操作
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="operation">操作</param>
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
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public static MultiTableUpdate Update<TMultiUpdate>(this TMultiUpdate update, string tableName)
        where TMultiUpdate : MultiTableUpdate
    {
        update._table = update.MultiTable.From<IAliasTable<ITable>>(tableName);
        return update;
    }
    /// <summary>
    /// 指定被修改的表
    /// </summary>
    /// <typeparam name="TMultiUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static TMultiUpdate Update<TMultiUpdate>(this TMultiUpdate update, IAliasTable<ITable> table)
        where TMultiUpdate : MultiTableUpdate
    {
        update._table = table;
        return update;
    }
    #endregion
    /// <summary>
    /// 获取被修改字段
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IAssignView? GetAssignField<TTable>(this IAliasTable<TTable> aliasTable, string fieldName)
        where TTable : ITable
    {
        if (aliasTable.GetPrefixField(fieldName) is PrefixField prefixField)
        {
            IAssignView field = prefixField.Target;
            if (aliasTable.Target.AssignFields.Any(c => c == field))
                return prefixField;
        }
        if (aliasTable.Target.GetAssignField(fieldName) is IColumn column)
            return aliasTable.NewPrefixField(column);
        return null;
    }

    private static AssignOperation CreateOperation(UpdateBase update, string columnName, AssignSymbol op, string parameter)
    {
        var field = update.GetAssignField(columnName);
        parameter = Parameter.CheckName(parameter, columnName);
        return new AssignOperation(field, op, Parameter.Use(parameter));
    }
}
