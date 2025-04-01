﻿using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// Or查询
/// </summary>
public static partial class ShadowSqlServices
{
    #region Not
    /// <summary>
    /// not And为not每个子项的Or
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    public static SqlOrQuery Not(this SqlAndQuery and)
    {
        var or = new SqlOrQuery();
        var conditions = and.Conditions;
        if (conditions.ItemsCount > 0)
            or.AddLogic(conditions.Not());
        var andComplex = and.Complex;
        var orComplex = or.Complex;
        orComplex.NotLogics(andComplex._logics);
        orComplex.NotOthers(andComplex);
        return or;
    }    
    ///// <summary>
    ///// not子项
    ///// </summary>
    ///// <param name="logic"></param>
    ///// <param name="others"></param>
    //internal static void NotOthers(this SqlOrQuery logic, IEnumerable<IComplexOrLogic> others)
    //{
    //    foreach (var item in others)
    //        logic._others.Add(item.Not());
    //}
    #endregion
    #region CopyTo
    /// <summary>
    /// 复制Or查询
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    internal static void CopyTo(this SqlOrQuery source, SqlOrQuery destination)
    {
        source.Complex.CopyTo(destination.Complex);
        source.Conditions.CopyTo(destination.Conditions);
    }
    #endregion
    #region MergeTo
    /// <summary>
    /// SqlOrQuery与SqlOrQuery合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    internal static SqlOrQuery MergeTo(this SqlOrQuery source, SqlOrQuery query)
    {
        source.CopyTo(query);
        return query;
    }
    /// <summary>
    /// SqlOrQuery与ComplexOrLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static SqlOrQuery MergeTo(this SqlOrQuery source, ComplexOrLogic or)
        => or.MergeTo(source);
    /// <summary>
    /// SqlOrQuery与OrLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="or"></param>
    /// <returns></returns>
    internal static SqlOrQuery MergeTo(this SqlOrQuery source, OrLogic or)
        => or.MergeTo(source);
    #endregion
    #region MergeToAnd
    /// <summary>
    /// SqlOrQuery与ComplexAndLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static ComplexAndLogic MergeToAnd(this SqlOrQuery source, ComplexAndLogic and)
    {
        var preview = source.Preview();
        if (preview.IsEmpty)
            return and;
        if (preview.HasSecond)
        {
            var conditions = source.Conditions;
            var complex = source.Complex.Copy();
            if (conditions.ItemsCount > 0)
                complex.AddLogic(conditions);
            and.AddOtherCore(complex);
        }
        else
        {
            and.AddLogic(preview.First);
        }
        return and;
    }
    /// <summary>
    /// SqlOrQuery与AndLogic合并
    /// </summary>
    /// <param name="source"></param>
    /// <param name="and"></param>
    /// <returns></returns>
    internal static SqlAndQuery MergeToAnd(this SqlOrQuery source, AndLogic and)
        => new(and.MergeTo(source.MergeToAnd(new ComplexAndLogic())));
    #endregion
}
