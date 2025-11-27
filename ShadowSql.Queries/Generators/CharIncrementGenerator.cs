using Hand.Creational;

namespace ShadowSql.Generators;

/// <summary>
/// 字符自增标识生成器
/// </summary>
/// <param name="start"></param>
public class CharIncrementGenerator(char start = 'a')
    : ICreator<string>
{
    private char _current = start;
    /// <summary>
    /// 当前标识
    /// </summary>
    public char Current
        => _current;
    /// <summary>
    /// 生成新标识
    /// </summary>
    /// <returns></returns>
    public string Create()
    {
        var identifier = _current++;
        return identifier.ToString();
    }
}
