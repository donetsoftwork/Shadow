using System.Text;

namespace Shadow.DDL.TypeParse;

/// <summary>
/// 长度类型解析
/// </summary>
/// <param name="dbType"></param>
/// <param name="defaultLength"></param>
public class LengthTypeParser(string dbType, int defaultLength = 50)
    : TypeParser(dbType)
{
    #region 配置
    private readonly int _defaultLength = defaultLength;
    /// <summary>
    /// 默认字段长度
    /// </summary>
    public int DefaultLength
        => _defaultLength;
    #endregion
    /// <inheritdoc/>
    public override void Write(StringBuilder sql)
    {
        sql.Append(_dbType).Append('(').Append(_defaultLength).Append(')');
    }
    /// <inheritdoc/>
    public override void Write(StringBuilder sql, int length)
    {
        if(length > 0)
            sql.Append(_dbType).Append('(').Append(length).Append(')');
        else if(length == 0)
            sql.Append(_dbType).Append('(').Append(_defaultLength).Append(')');
        else
            sql.Append(_dbType).Append("(MAX)");
    }
    /// <inheritdoc/>
    public override void Write(StringBuilder sql, int length, int _)
        => Write(sql, length);
}
