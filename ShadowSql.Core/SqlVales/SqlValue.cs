using ShadowSql.Engines;
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
    /// <param name="value"></param>
    /// <returns></returns>
    public static ISqlValue From<T>(T value)
    {
        return new SqlValueWraper<T>(value);
    }
    /// <summary>
    /// 包装数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static ISqlValue Values<T>(params T[] values)
    {
        return new SqlValuesWraper<T>(values);
    }
    /// <summary>
    /// 数据库值包装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    class SqlValueWraper<T>(T value) : ISqlValue
    {
        private readonly T _value = value;

        /// <summary>
        /// 拼写sql
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public void Write(ISqlEngine engine, StringBuilder sql)
        {
            var sqlValue = engine.SqlValue(_value);
            sqlValue.Write(engine, sql);
        }
    }
    class SqlValuesWraper<T>(T[] values) : ISqlValue
    {
        private readonly T[] _values = values;

        /// <summary>
        /// 拼写sql
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
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
