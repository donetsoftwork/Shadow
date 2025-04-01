using ShadowSql.Generators;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System.Collections.Generic;
using System.Linq;

namespace ShadowSql.Join;

/// <summary>
/// 多(联)表基类
/// </summary>
/// <param name="aliasGenerator"></param>
/// <param name="filter"></param>
public abstract class MultiTableBase(IIdentifierGenerator aliasGenerator, SqlQuery filter)
    : MultiQueryBase(filter)
{
    #region 配置
    /// <summary>
    /// 别名生成器
    /// </summary>
    protected readonly IIdentifierGenerator _aliasGenerator = aliasGenerator;
    /// <summary>
    /// 成员表
    /// </summary>
    protected readonly List<IAliasTable> _tables = [];
    /// <summary>
    /// 成员表
    /// </summary>
    public override IEnumerable<IAliasTable> Tables
        => _tables;
    #endregion
    /// <summary>
    /// 构造表成员
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public TableAlias<TTable> CreateMember<TTable>(TTable table)
        where TTable : ITable
    {
        var aliasTable = table.As(_aliasGenerator.NewName());
        AddMemberCore(aliasTable);
        return aliasTable;
    }
    /// <summary>
    /// 添加表成员
    /// </summary>
    /// <param name="aliasTable"></param>
    internal void AddMemberCore(IAliasTable aliasTable)
        => _tables.Add(aliasTable);
    /// <summary>
    /// 获取列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public override IPrefixColumn? GetPrefixColumn(string columName)
    {
        foreach (var member in _tables)
        {
            if (member.GetPrefixColumn(columName) is IPrefixColumn prefixColumn)
                return prefixColumn;
        }
        return null;
    }
    /// <summary>
    /// 获取联表成员
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public override IAliasTable? GetMember(string tableName)
        => _tables.FirstOrDefault(m => m.IsMatch(tableName));
    
    #region IDataView
    /// <summary>
    /// 列
    /// </summary>
    public override IEnumerable<IPrefixColumn> PrefixColumns
        => _tables.SelectMany(m => m.PrefixColumns);
    #endregion
    /// <summary>
    /// 获取比较列
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public override ICompareField GetCompareField(string fieldName)
    {
        if (GetPrefixColumn(fieldName) is ICompareField prefixColumn)
            return prefixColumn;
        return Field(fieldName);
    }
}
