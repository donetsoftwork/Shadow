using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System.Collections.Generic;

namespace ShadowSql.Insert;

/// <summary>
/// 插入
/// </summary>
public interface IInsert : IExecuteSql
{
    /// <summary>
    /// 表
    /// </summary>
    IInsertTable Table { get; }
}

/// <summary>
/// 插入单条
/// </summary>
public interface ISingleInsert : IInsert
{
    /// <summary>
    /// 插入的值列表
    /// </summary>
    IEnumerable<IInsertValue> Items { get; }
    /// <summary>
    /// 插入单值
    /// </summary>
    /// <param name="value"></param>
    void InsertCore(IInsertValue value);
    ///// <summary>
    ///// 插入单值
    ///// </summary>
    ///// <param name="select"></param>
    //void InsertCore(Func<ITable, IInsertValue> select);
}

/// <summary>
/// 多条插入
/// </summary>
public interface IMultiInsert : IInsert
{
    /// <summary>
    /// 插入的值列表
    /// </summary>
    IEnumerable<IInsertValues> Items { get; }
    /// <summary>
    /// 插入多值
    /// </summary>
    /// <param name="value"></param>
    void InsertCore(IInsertValues value);
    ///// <summary>
    ///// 插入多值
    ///// </summary>
    ///// <param name="select"></param>
    //void InsertCore(Func<ITable, IInsertValues> select);
}

/// <summary>
/// 插入Select子查询
/// </summary>
public interface ISelectInsert : IInsert
{
    /// <summary>
    /// 列
    /// </summary>
    IColumn[] Columns { get; }
    /// <summary>
    /// Select子查询
    /// </summary>
    ISelect Select { get; }
}