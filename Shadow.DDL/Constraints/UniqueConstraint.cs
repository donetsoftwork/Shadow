using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace Shadow.DDL.Constraints;

/// <summary>
/// 唯一约束
/// </summary>
/// <param name="name"></param>
/// <param name="columns"></param>
public class UniqueConstraint(string name, params IColumn[] columns)
    : IConstraint
{
    /// <summary>
    /// 唯一约束
    /// </summary>
    /// <param name="name"></param>
    /// <param name="columnNames"></param>
    public UniqueConstraint(string name, params string[] columnNames)
        : this(name, Column.Use(columnNames))
    {
    }
    #region 配置
    private readonly string _name = name;
    private readonly IColumn[] _columns = columns;
    /// <summary>
    /// 
    /// </summary>
    public string Name 
        => _name;
    /// <summary>
    /// 
    /// </summary>
    public IColumn[] Columns 
        => _columns;
    /// <inheritdoc/>
    public bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        return engine.GetDefineColumComponent()
            .WriteUniqueConstraint(engine, sql, _name, _columns);
    }
    #endregion
}
