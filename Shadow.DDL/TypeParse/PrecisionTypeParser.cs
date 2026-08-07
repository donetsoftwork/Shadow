using System.Text;

namespace Shadow.DDL.TypeParse;

/// <summary>
/// 精度类型解析
/// </summary>
/// <param name="dbType"></param>
public class PrecisionTypeParser(string dbType)
    : TypeParser(dbType)
{
    /// <inheritdoc/>
    public override void Write(StringBuilder sql, int precision)
    {
        sql.Append(_dbType).Append('(').Append(precision).Append(')');
    }
    /// <inheritdoc/>
    public override void Write(StringBuilder sql, int precision, int scale)
    {
        sql.Append(_dbType).Append('(').Append(precision).Append(',').Append(scale).Append(')');
    }
}
