namespace ShadowSql.Identifiers;

/// <summary>
/// 视图
/// </summary>
public interface IView : IMatch
{
    /// <summary>
    /// 视图名
    /// </summary>
    string ViewName { get; }
}
