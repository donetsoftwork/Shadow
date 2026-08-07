using System.Text;

namespace Shadow.DDL.TypeParse;

/// <summary>
/// 类型解析
/// </summary>
/// <param name="dbType"></param>
public class TypeParser(string dbType)
    : ITypeParser
{
    #region 配置
    /// <summary>
    /// 数据库类型
    /// </summary>
    protected readonly string _dbType = dbType;
    /// <summary>
    /// 类型名
    /// </summary>
    public string DbType
        => _dbType;
    #endregion
    /// <inheritdoc/>
    public virtual void Write(StringBuilder sql)
    {
        sql.Append(_dbType);
    }
    /// <inheritdoc/>
    public virtual void Write(StringBuilder sql, int length)
    {
        sql.Append(_dbType);
    }
    /// <inheritdoc/>
    public virtual void Write(StringBuilder sql, int precision, int scale)
    {
        sql.Append(_dbType);
    }
}
