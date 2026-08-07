using Hand.Structural;
using System.Text;

namespace Shadow.DDL.TypeParse;

/// <summary>
/// 后缀类型
/// </summary>
/// <param name="original"></param>
/// <param name="suffix"></param>
public class SuffixTypeParser(ITypeParser original, string suffix)
    : ITypeParser, IWrapper<ITypeParser>
{
    #region 配置
    private readonly ITypeParser _original = original;
    private readonly string _suffix = suffix;
    /// <inheritdoc/>
    public ITypeParser Original 
        => _original;
    /// <summary>
    /// 后缀
    /// </summary>
    public string Suffix 
        => _suffix;
    #endregion
    /// <inheritdoc/>
    public void Write(StringBuilder sql)
    {
        _original.Write(sql);
        sql.Append(_suffix);
    }
    /// <inheritdoc/>
    public void Write(StringBuilder sql, int length)
    {
        _original.Write(sql, length);
        sql.Append(_suffix);
    }
    /// <inheritdoc/>
    public void Write(StringBuilder sql, int precision, int scale)
    {
        _original.Write(sql, precision, scale);
        sql.Append(_suffix);
    }
}
