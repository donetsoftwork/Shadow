namespace ShadowSql.Previews;

/// <summary>
/// 子项预览
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPreview<out T>
{
    /// <summary>
    /// 是否为空
    /// </summary>
    bool IsEmpty { get; }
    /// <summary>
    /// 第一个
    /// </summary>
    T First { get; }
    /// <summary>
    /// 是否含第二个
    /// </summary>
    bool HasSecond { get; }
}
