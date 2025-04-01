﻿using ShadowSql.Components;
using System.Text;

namespace ShadowSql.Engines.MsSql;

/// <summary>
/// MsSql
/// </summary>
/// <param name="select"></param>
/// <param name="sqlVales"></param>
/// <param name="components"></param>
public class MsSqlEngine(ISelectComponent select, ISqlValueComponent sqlVales, IPluginProvider? components) 
    : EngineBase(select, sqlVales, components), ISqlEngine
{
    /// <summary>
    /// MsSql
    /// </summary>
    public MsSqlEngine()
        : this(new MsSqlSelectComponent(), new SqlValueComponent("1", "0", "NULL"), null)
    {
    }
    /// <summary>
    /// 标识符格式化
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="name"></param>
    public override void Identifier(StringBuilder sql, string name)
    {
        sql.Append('[').Append(name).Append(']');
    }    
    /// <summary>
    /// 插入自增列sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override bool InsertedIdentity(StringBuilder sql)
    {
        sql.Append(";SELECT scope_identity()");
        return true;
    }
}
