using ShadowSql.SqlVales;
using System;

namespace ShadowSql.Components;

/// <summary>
/// 数据库值处理组件
/// </summary>
/// <param name="trueExpression"></param>
/// <param name="falseExpression"></param>
/// <param name="nullExpression"></param>
public class SqlValueComponent(string trueExpression, string falseExpression, string nullExpression)
    : ISqlValueComponent
{
    private readonly ISqlValue _true = new StraightValue(trueExpression);
    private readonly ISqlValue _false = new StraightValue(falseExpression);
    private readonly ISqlValue _null = new StraightValue(nullExpression);

    /// <summary>
    /// true
    /// </summary>
    public ISqlValue True 
        => _true;
    /// <summary>
    /// false
    /// </summary>
    public ISqlValue False 
        => _false;
    /// <summary>
    /// null
    /// </summary>
    public ISqlValue Null 
        => _null;
    /// <summary>
    /// 获取数据库值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ISqlValue SqlValue<T>(T value)
    {
        if (value is bool boolValue)
            return boolValue ? _true : _false;
        else if (value is null)
            return _null;
#pragma warning disable CS8604
        else if (value is int or long or short
            or uint or ulong or ushort
            or byte or sbyte
            or float or double or decimal)
            return new StraightValue(value.ToString());
        else if (value is DateTime)
            return new StringValue(value.ToString());
        else
            return new UnSafeValue(value.ToString());
#pragma warning restore CS8604
    }
}
