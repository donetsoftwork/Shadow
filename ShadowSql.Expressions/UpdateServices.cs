using ShadowSql.Expressions.AliasTables;
using ShadowSql.Expressions.Visit;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Update;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShadowSql.Expressions;

/// <summary>
/// Update扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    /// <summary>
    /// 赋值操作
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="update"></param>
    /// <param name="operation"></param>
    /// <returns></returns>
    public static MultiTableUpdate Set<TEntity>(this MultiTableUpdate update, Expression<Func<TEntity, TEntity>> operation)
    {
        var visitor = new UpdateVisitor(new TableVisitor(operation.Parameters[0], update.CheckTable()), update._assignInfos);
        visitor.Visit(operation.Body);
        return update;
    }
    /// <summary>
    /// 获取被修改字段
    /// </summary>
    /// <param name="aliasTable"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<IPrefixField> GetAssignFields<TTable>(this IAliasTable<TTable> aliasTable)
        where TTable : ITable

    {
        var updateFields = aliasTable.Target.AssignFields.ToList();
        foreach (var field in aliasTable.PrefixFields)
        {
            if (field is PrefixField prefixField)
            {
                IAssignView assignField = prefixField.Target;
                if (updateFields.Any(c => c == field))
                    yield return field;
            }
        }
    }
    /// <summary>
    /// 转化为修改表
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    internal static IUpdateTable AsUpdate(this ITableView view)
    {
        if (view is ITable table)
            return table;
        if (view is IAliasTable<ITable> aliasTable)
            return new AliasUpdateTable(aliasTable);
        throw new ArgumentException("不支持" + view.GetType().ToString());
    }
}
