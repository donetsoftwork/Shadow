using ShadowSql;
using ShadowSql.SqlVales;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
#if NET9_0_OR_GREATER
using System.Threading;
#endif

namespace ShadowSql.Expressions.Services;

/// <summary>
/// 数据库值服务
/// </summary>
public static class SqlValueService
{
    /// <summary>
    /// 包装为数据库值
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ISqlValue From(Type type, object? value)
        => GetFrom(type)
            .Invoke(value);
    /// <summary>
    /// 包装列表
    /// </summary>
    /// <param name="type"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    public static ISqlValue Values(Type type, IEnumerable objects)
        => GetValues(type)
            .Invoke(objects);
    private static Func<object?, ISqlValue> GetFrom(Type type)
    {
        if (_fromCacher.TryGetValue(type, out Func<object?, ISqlValue>? func))
            return func;
#if NET9_0_OR_GREATER
        lock (_fromLock)
#else
        lock (_fromCacher)
#endif
        {
            if (_fromCacher.TryGetValue(type, out func))
                return func;
            if (type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                && Nullable.GetUnderlyingType(type) is Type underlyingType)
            {
                func = CreateFromFunc(underlyingType);
            }
            else
            {
                func = CreateFromFunc(type);
            }
            _fromCacher.TryAdd(type, func);
        }
        return func;
    }
    private static Func<IEnumerable, ISqlValue> GetValues(Type type)
    {
        if (_valuesCacher.TryGetValue(type, out Func<IEnumerable, ISqlValue>? func))
            return func;
#if NET9_0_OR_GREATER
        lock (_valuesLock)
#else
        lock (_valuesCacher)
#endif
        {
            if (_valuesCacher.TryGetValue(type, out func))
                return func;
            func = CreateValuesFunc(type);
            _valuesCacher.TryAdd(type, func);
        }
        return func;
    }
    /// <summary>
    /// Expression调用SqlValue.From
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static Func<object?, ISqlValue> CreateFromFunc(Type type)
    {
        // 创建参数表达式
        ParameterExpression param = Expression.Parameter(typeof(object), "value");
        // 创建对泛型方法的引用
        MethodInfo method = typeof(SqlValue).GetMethod(nameof(SqlValue.From))!.MakeGenericMethod(type);
        var value = Expression.Convert(param, type);
        // 创建调用表达式
        Expression callExpr = Expression.Call(null, method, value);
        // 编译表达式为委托
        return Expression.Lambda<Func<object?, ISqlValue>>(callExpr, param).Compile();
    }
    /// <summary>
    /// Expression调用SqlValue.Values
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static Func<IEnumerable, ISqlValue> CreateValuesFunc(Type type)
    {
        // 创建参数表达式
        ParameterExpression param = Expression.Parameter(typeof(IEnumerable), "values");
        var valuesType = typeof(IEnumerable<>).MakeGenericType(type);
        // 创建对泛型方法的引用
        MethodInfo method = typeof(SqlValue).GetMethod(nameof(SqlValue.Values))!.MakeGenericMethod(valuesType);
        var value = Expression.Convert(param, type);
        // 创建调用表达式
        Expression callExpr = Expression.Call(null, method, value);
        // 编译表达式为委托
        return Expression.Lambda<Func<IEnumerable, ISqlValue>>(callExpr, param).Compile();
    }

#if NET9_0_OR_GREATER
    private static readonly Lock _fromLock = new();
    private static readonly Lock _valuesLock = new();
#endif
    private static readonly ConcurrentDictionary<Type, Func<object?, ISqlValue>> _fromCacher = [];
    private static readonly ConcurrentDictionary<Type, Func<IEnumerable, ISqlValue>> _valuesCacher = [];
}
