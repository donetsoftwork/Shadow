using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System;
using System.Linq;
using System.Text;

namespace ShadowSql.Update;

/// <summary>
/// 多表(联表)修改
/// </summary>
/// <param name="multiTable"></param>
/// <param name="target"></param>
public class MultiTableUpdate(IMultiTableQuery multiTable, IAliasTable target)
    : UpdateBase<IAliasTable>(target, multiTable.Filter)
{
    /// <summary>
    /// 多表(联表)修改
    /// </summary>
    /// <param name="multiTable"></param>
    public MultiTableUpdate(IMultiTableQuery multiTable)
        : this(multiTable, multiTable.Tables.First())
    {
    }
    #region 配置
    private readonly IMultiTableQuery _multiTable = multiTable;
    /// <summary>
    /// 多表(联表)视图
    /// </summary>
    public IMultiTableQuery MultiTable 
        => _multiTable;
    #endregion
    /// <summary>
    /// 指定被修改的表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public MultiTableUpdate Update(string tableName)
    {
        if (_multiTable.GetMember(tableName) is IAliasTable table)
            _source = table;
        return this;
    }
    #region ISqlFragment
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.UpdatePrefix(sql);
        sql.Append(_source.Alias);

        //var assigns = GetAssigns();
        sql.Append(" SET ");
        var appended = false;
        foreach (var assign in _assignInfos)
        {
            //var point = sql.Length;
            if (appended)
                sql.Append(',');
            assign.Write(engine, sql);
            appended = true;
            //if (assign.Write(engine, sql))
            //{
            //    appended = true;
            //}
            //else
            //{
            //    //回滚
            //    sql.Length = point;
            //}
        }
        if (appended)
        {
            sql.Append(" FROM ");
            _multiTable.Write(engine, sql);
        }
        else
        {
            throw new InvalidOperationException("没有设置修改信息");
        }
    }
    #endregion
}
