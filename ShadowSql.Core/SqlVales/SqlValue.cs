using ShadowSql.Engines;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.SqlVales;

/// <summary>
/// 数据库值
/// </summary>
public static class SqlValue
{
    /// <summary>
    /// 包装为数据库值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static ISqlValue From<T>(T value)
    {
        return new SqlValueWraper<T>(value);
    }
    /// <summary>
    /// NULL值
    /// </summary>
    public static readonly ISqlValue Null = new SqlValueWraper<DBNull>(DBNull.Value);
    /// <summary>
    /// True
    /// </summary>
    public static readonly ISqlValue True = new SqlValueWraper<bool>(true);
    /// <summary>
    /// False
    /// </summary>
    public static readonly ISqlValue False = new SqlValueWraper<bool>(false);
    /// <summary>
    /// 包装列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values">值</param>
    /// <returns></returns>
    public static ISqlValue Values<T>(params IEnumerable<T> values)
    {
        return new SqlValuesWraper<T>(values);
    }
    /// <summary>
    /// 数据库值包装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">值</param>
    internal class SqlValueWraper<T>(T value) : ISqlValue
    {
        internal readonly T _value = value;

        /// <inheritdoc/>
        public void Write(ISqlEngine engine, StringBuilder sql)
        {
            var sqlValue = engine.SqlValue(_value);
            sqlValue.Write(engine, sql);
        }
    }
    /// <summary>
    /// 列表包装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values">值</param>
    internal class SqlValuesWraper<T>(IEnumerable<T> values) : ISqlValue
    {
        internal readonly IEnumerable<T> _values = values;

        /// <inheritdoc/>
        public void Write(ISqlEngine engine, StringBuilder sql)
        {
            bool appended = false;
            sql.Append('(');
            foreach (var item in _values)
            {
                if (appended)
                    sql.Append(',');
                var sqlValue = engine.SqlValue(item);
                sqlValue.Write(engine, sql);
                appended = true;
            }
            sql.Append(')');
        }
    }
}
