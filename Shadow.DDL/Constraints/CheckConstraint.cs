using ShadowSql.Engines;
using System.Text;

namespace Shadow.DDL.Constraints;

/// <summary>
/// 检查约束
/// </summary>
/// <param name="name"></param>
/// <param name="expression"></param>
public class CheckConstraint(string name, string expression)
    : IConstraint
{
    #region 配置
    private readonly string _name = name;
    private readonly string _expression = expression;

    /// <summary>
    /// 约束名
    /// </summary>
    public string Name 
        => _name;
    /// <summary>
    /// 表达式
    /// </summary>
    public string Expression 
        => _expression;
    #endregion

    /// <inheritdoc/>
    public bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        return engine.GetDefineColumComponent()
            .WriteCheckConstraint(engine, sql, _name, _expression);
    }
}
