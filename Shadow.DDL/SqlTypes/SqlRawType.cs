using ShadowSql.Engines;
using System.Text;

namespace Shadow.DDL.SqlTypes;

/// <summary>
/// 数据库原始类型
/// </summary>
/// <param name="rawType"></param>
public class SqlRawType(string rawType)
    : ISqlType
{
    #region 配置
    private readonly string _rawType = rawType;
    /// <summary>
    /// 原始类型
    /// </summary>
    public string RawType 
        => _rawType;
    #endregion
    /// <inheritdoc/>
    public void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_rawType);
    }
}
