using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowSql;

/// <summary>
/// Update扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    /// <summary>
    /// 获取被修改字段
    /// </summary>
    /// <param name="aliasTable"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<IPrefixField> GetAssignFields<TTable>(this IAliasTable<TTable> aliasTable)
        where TTable : IUpdateTable

    {
        var updateFields = aliasTable.Target.AssignFields.ToList();
        foreach (var field in aliasTable.PrefixFields)
        {
            if (field is PrefixField prefixField)
            {
                IAssignView assignField = prefixField.Target;
                if (updateFields.Any(c => c == field))
                    yield return field;
            }
        }
    }
}
