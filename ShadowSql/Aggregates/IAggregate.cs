﻿using ShadowSql.Fragments;
using ShadowSql.Identifiers;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合(计算)
/// </summary>
public interface IAggregate : ISqlEntity
{
    /// <summary>
    /// 聚合方法名
    /// </summary>
    string Aggregate { get; }
}
/// <summary>
/// 聚合(计算)列
/// </summary>
public interface IAggregateField : IAggregate, ICompareView
{
    /// <summary>
    /// 被计算的列名
    /// </summary>
    string TargetName { get; }
}
/// <summary>
/// 聚合列别名
/// </summary>
public interface IAggregateFieldAlias : IAggregate, IFieldAlias;