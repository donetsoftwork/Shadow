using Shadow.DDL.Schemas;
using Shadow.DDL.TypeParse;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;

namespace Shadow.DDL.Components;

/// <summary>
/// 定义列组件
/// </summary>
/// <param name="typeManager"></param>
public abstract class DefineColumComponent(TypeManager typeManager)
    : IDefineColumComponent
{
    #region 配置
    private readonly TypeManager _typeManager = typeManager;
    /// <summary>
    /// 类型管理
    /// </summary>
    public TypeManager TypeManager
        => _typeManager;
    #endregion

    /// <inheritdoc/>
    public ITypeParser? GetTypeParser(Type originalType)
        => _typeManager.GetTypeParser(originalType);
    /// <inheritdoc/>
    public abstract bool WriteColumnSchema(ISqlEngine engine, StringBuilder sql, ColumnSchema column);
    /// <inheritdoc/>
    public virtual bool WriteUniqueConstraint(ISqlEngine engine, StringBuilder sql, string name, IColumn[] columns)
    {
        // CONSTRAINT uc_PersonID UNIQUE (Id,Name)
        if (string.IsNullOrWhiteSpace(name) || columns.Length == 0)
            return false;
        sql.Append("CONSTRAINT ").Append(name);
        WriteUnique(engine, sql, columns);
        return true;
    }
    /// <summary>
    /// 拼写唯一约束
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="columns"></param>
    protected static void WriteUnique(ISqlEngine engine, StringBuilder sql, IColumn[] columns)
    {
        sql.Append(" UNIQUE (");
        columns[0].Write(engine, sql);
        for (var i = 1; i < columns.Length; i++)
        {
            sql.Append(',');
            columns[i].Write(engine, sql);
        }
        sql.Append(')');
    }
    /// <inheritdoc/>
    public virtual bool WriteCheckConstraint(ISqlEngine engine, StringBuilder sql, string name, string expression)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(expression))
            return false;
        // CONSTRAINT chk_Person CHECK ([Amount]>0)
        sql.Append("CONSTRAINT ").Append(name).Append(" CHECK (").Append(expression).Append(')');
        return true;
    }
}
