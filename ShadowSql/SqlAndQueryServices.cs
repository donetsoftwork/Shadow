﻿using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// And查询
/// </summary>
public static partial class ShadowSqlServices
{
    #region Not
    /// <summary>
    /// not Or为not每个子项的And
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    public static SqlAndQuery Not(this SqlOrQuery or)
    {
        var and = new SqlAndQuery();
        var conditions = or.Conditions;
        if (conditions.ItemsCount > 0)
            and.AddLogic(conditions.Not());
        var andComplex = and.Complex;
        var orComplex = or.Complex;
        andComplex.NotLogics(orComplex._logics);
        andComplex.NotOthers(orComplex);
        return and;
    }    
    ///// <summary>
    ///// not子项
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="others"></param>
    //internal static void NotOthers(this SqlAndQuery logic, IEnumerable<IComplexAndLogic> others)
    //{
    //    foreach (var item in others)
    //        logic._others.Add(item.Not());
    //}
    #endregion
    #region CopyTo
    /// <summary>
    /// 复制And查询
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    internal static void CopyTo(this SqlAndQuery source, SqlAndQuery destination)
    {
        source.Complex.CopyTo(destination.Complex);
        source.Conditions.CopyTo(destination.Conditions);
    }
    #endregion
    #region MergeTo
    /// <summary>
    /// SqlAndQuery与SqlAndQuery合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    internal static SqlAndQuery MergeTo(this SqlAndQuery source, SqlAndQuery query)
    {
        source.CopyTo(query);
        return query;
    }
    /// <summary>
    /// SqlAndQuery与ComplexAndLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static SqlAndQuery MergeTo(this SqlAndQuery source, ComplexAndLogic and)
        => and.MergeTo(source);
    /// <summary>
    /// SqlAndQuery与AndLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static SqlAndQuery MergeTo(this SqlAndQuery source, AndLogic and)
        => and.MergeTo(source);
    #endregion
    #region MergeToOr
    /// <summary>
    /// SqlAndQuery与ComplexOrLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static TComplexOrLogic MergeToOr<TComplexOrLogic>(this SqlAndQuery source, TComplexOrLogic or)
        where TComplexOrLogic : ComplexLogicBase, IOrLogic
    {
        var preview = source.Preview();
        if (preview.IsEmpty)
            return or;
        if (preview.HasSecond)
        {
            var conditions = source.Conditions;
            var complex = source.Complex.Copy();
            if (conditions.ItemsCount > 0)
                complex.AddLogic(conditions);
            or.AddOtherCore(complex);
        }
        else
        {
            or.AddLogic(preview.First);
        }
        return or;
    }
    /// <summary>
    /// SqlAndQuery与OrLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static SqlOrQuery MergeToOr(this SqlAndQuery source, OrLogic or)
        => new(or.MergeTo(source.MergeToOr(new ComplexOrLogic())), new SqlConditionLogic(LogicSeparator.Or));
    #endregion
}
