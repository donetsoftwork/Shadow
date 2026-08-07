using Shadow.DDL.Components;
using Shadow.DDL.Constraints;
using Shadow.DDL.Schemas;
using Shadow.DDL.TypeParse;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Shadow.DDL;

/// <summary>
/// 扩展方法
/// </summary>
public static class DDLServices
{
    /// <summary>
    /// 建表
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columns">列</param>
    /// <param name="constraints">约束</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CreateTable ToCreate(this ITable table, IEnumerable<ColumnSchema> columns, IEnumerable<IConstraint> constraints)
        => new(table, columns, constraints);
    /// <summary>
    /// 建表
    /// </summary>
    /// <param name="table">表</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CreateTable ToCreate(this TableSchema table)
        => new(table, table.Columns, table.Constraints);
    /// <summary>
    /// 删表
    /// </summary>
    /// <param name="table">表</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DropTable ToDrop(this ITable table)
        => new(table);
    /// <summary>
    /// 获取类型解析器
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IDefineColumComponent GetDefineColumComponent(this ISqlEngine engine)
    {
        var provider = engine.PluginProvider
            ?? throw new Exception($"engine.{nameof(engine.PluginProvider)} is null");
        var columComponent = provider.GetPlugin<IDefineColumComponent>()
            ?? throw new Exception($"请先注册注册组件 {nameof(IDefineColumComponent)}");
        return columComponent;
    }
    /// <summary>
    /// 获取类型解析器
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="originalType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ITypeParser GetTypeParser(this ISqlEngine engine, Type originalType)
    {
        return GetDefineColumComponent(engine)
            .GetTypeParser(originalType)
            ?? throw new Exception($"{originalType.FullName} is not supported");
    }
}
