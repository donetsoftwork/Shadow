using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 单列筛选基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TFields"></typeparam>
/// <param name="source"></param>
/// <param name="fields"></param>
public class SingleSelectBase<TSource, TFields>(TSource source, TFields fields)
    : SelectBase<TSource, TFields>(source, fields), ISingleSelect
    where TSource : ITableView
    where TFields : ISelectFields
{
    /// <summary>
    /// 单列
    /// </summary>
    public IFieldView SingleField
        => _fields.Selected.Last();

    /// <summary>
    /// 只返回单列
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<IFieldView> CheckFields()
    {
        yield return SingleField;
    }
    #region ISqlFragment
    /// <summary>
    /// 拼写字段
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    public override bool WriteSelected(ISqlEngine engine, StringBuilder sql)
    {
        var singleField = _fields.Selected.LastOrDefault();
        if (singleField is null)
        {
            //占位符
            sql.Append(1);

        }
        else
        {
            singleField.Write(engine, sql);
        }

        return true;
    }

    #endregion
}
