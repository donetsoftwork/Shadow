using ShadowSql.Logics;

namespace ShadowSql.Previews;

/// <summary>
/// 预览Sql查询
/// </summary>
/// <param name="conditions"></param>
/// <param name="logics"></param>
public class SqlQueryPreview(SqlConditionLogic conditions, ComplexLogicBase logics)
    : ComplexLogicPreview(logics)
{
    private readonly SqlConditionLogic _conditions = conditions;
    /// <summary>
    /// Sql查询条件
    /// </summary>
    public SqlConditionLogic Conditions
        => _conditions;
    /// <inheritdoc/>
    protected override void Init()
    {
        if (_conditions.ItemsCount > 0)
        {
            First = _conditions;
            ExpandSingle(_complex);
        }
        else
        {
            ExpandTwo(_complex);
        }
    }
}
