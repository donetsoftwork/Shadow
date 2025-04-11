using ShadowSql.SqlVales;

namespace ShadowSql.Components;

/// <summary>
/// 数据库值处理组件
/// </summary>
public interface ISqlValueComponent
{
    /// <summary>
    /// 获取数据库值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    ISqlValue SqlValue<T>(T value);
}
