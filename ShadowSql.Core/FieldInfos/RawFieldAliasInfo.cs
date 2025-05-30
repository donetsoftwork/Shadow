﻿using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 别名字段信息
/// </summary>
/// <param name="statement"></param>
/// <param name="alias"></param>
public class RawFieldAliasInfo(string statement, string alias)
     : IdentifierBase(alias), IFieldAlias
{
    private readonly string _statement = statement;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _name;
    /// <summary>
    /// 语句
    /// </summary>
    public string Statement
        => _statement;

    string IView.ViewName
        => _name;
    IColumn IFieldView.ToColumn()
        => Column.Use(_name);
    IFieldAlias IFieldView.As(string alias)
        => new RawFieldAliasInfo(_statement, alias);
    #region ISqlEntity
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    internal override void Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(_statement);
        engine.ColumnAs(sql, _name);
    }
    #endregion
}
