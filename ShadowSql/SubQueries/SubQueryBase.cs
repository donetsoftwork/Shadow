using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Variants;

namespace ShadowSql.SubQueries;

/// <summary>
/// 没想好怎么实现
/// </summary>
/// <typeparam name="PTable"></typeparam>
/// <param name="parentTable"></param>
/// <param name="query"></param>
public abstract class SubQueryBase<PTable>(TableAlias<PTable> parentTable, IDataSqlQuery query)
    where PTable : ITable
{
    private readonly TableAlias<PTable> _parentTable = parentTable;
    private readonly IDataSqlQuery _query = query;
    //todo: scalar_expression { = | <> | != | > | >= | !> | < | <= | !< } (SELECT Min|Max(Field) FROM ...)
    //todo: scalar_expression { = | <> | != | > | >= | !> | < | <= | !< } {SOME|ANY}(subquery)
    //todo: IN(subquery)
    //todo: EXISTS(subquery)
}
