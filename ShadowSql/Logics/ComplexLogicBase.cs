using ShadowSql.Engines;
using ShadowSql.Previews;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.Logics;

/// <summary>
/// 复合逻辑基类
/// </summary>
/// <param name="separator"></param>
/// <param name="items"></param>
/// <param name="others"></param>
public abstract class ComplexLogicBase(LogicSeparator separator, List<AtomicLogic> items, List<ComplexLogicBase> others)
    : Logic(separator, items)
{
    /// <summary>
    /// 复合逻辑子项
    /// </summary>
    internal readonly List<ComplexLogicBase> _others = others;
    /// <summary>
    /// 添加复合逻辑子项
    /// </summary>
    /// <param name="other"></param>
    internal void AddOther(ComplexLogicBase other)
        => _others.Add(other);
    /// <summary>
    /// 子逻辑预览
    /// </summary>
    /// <returns></returns>
    internal override IPreview<AtomicLogic> Preview()
        => new ComplexLogicPreview(this);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {        
        var appended = base.TryWrite(engine, sql);

        foreach (ComplexLogicBase item in _others)
        {
            var point = sql.Length;
            if (appended)
                _separator.Write(engine, sql);
            sql.Append('(');
            if (item.TryWrite(engine, sql))
            {
                sql.Append(')');
                appended = true;
            }
            else
            {
                //回滚
                sql.Length = point;
            }
        }

        return appended;  
    }
}
