using System.Text;

namespace Shadow.DDL.TypeParse;

/// <summary>
/// 类型解析
/// </summary>
public interface ITypeParser
{
    /// <summary>
    /// 写类型
    /// </summary>
    /// <param name="sql"></param>
    void Write(StringBuilder sql);
    /// <summary>
    /// 写类型和长度
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="length"></param>
    void Write(StringBuilder sql, int length);
    /// <summary>
    /// 写类型和精度
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="precision"></param>
    /// <param name="scale"></param>
    void Write(StringBuilder sql, int precision, int scale);
}