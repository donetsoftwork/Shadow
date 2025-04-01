using System.Threading;

namespace ShadowSql.Generators;

/// <summary>
/// Id自增标识生成器
/// </summary>
/// <param name="prefix"></param>
/// <param name="id"></param>
/// <param name="step"></param>
public class IdIncrementGenerator(string prefix, int id = 0, int step = 1)
    : IIdentifierGenerator
{
    private readonly string _prefix = prefix;
    private int _currentId = id;
    private readonly int _step = step;

    /// <summary>
    /// 前缀
    /// </summary>
    public string Prefix
        => _prefix;
    /// <summary>
    /// 当前Id
    /// </summary>
    public int CurrentId
        => _currentId;
    /// <summary>
    /// 步长
    /// </summary>
    public int Step 
        => _step;

    /// <summary>
    /// 生成新标识
    /// </summary>
    /// <returns></returns>
    public string NewName()
    {
        var id = Interlocked.Add(ref _currentId, _step);
        return _prefix + id.ToString();
    }
}
