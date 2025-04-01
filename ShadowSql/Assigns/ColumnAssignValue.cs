//using ShadowSql.Engines;
//using ShadowSql.Identifiers;
//using ShadowSql.SqlVales;
//using System.Text;

//namespace ShadowSql.Assigns;

///// <summary>
///// 对列赋值
///// </summary>
///// <param name="columnName"></param>
///// <param name="assign"></param>
///// <param name="value"></param>
//public class ColumnAssignValue(string columnName, AssignSymbol assign, ISqlValue value)
//     : ColumnAssignBase(columnName, assign)
//{
//    #region 配置
//    private readonly ISqlValue _value = value;
//    /// <summary>
//    /// 右边值
//    /// </summary>
//    public override ISqlValue Value
//        => _value;
//    #endregion

//    /// <summary>
//    /// 拼写sql
//    /// </summary>
//    /// <param name="engine"></param>
//    /// <param name="sql"></param>
//    /// <returns></returns>
//    public override bool Write(ISqlEngine engine, StringBuilder sql)
//    {
//        sql.Append(_columnName);
//        _assign.Write(engine, sql);
//        if (_value.Write(engine, sql))
//            return true;
//        return false;
//    }
//    /// <summary>
//    /// 构造赋值操作
//    /// </summary>
//    /// <param name="view"></param>
//    /// <returns></returns>
//    public override IAssignOperation CreateAssignOperation(ITableView view)
//    {
//        if (view.GetColumn(_columnName) is IColumn column)
//            return new AssignOperation(column, _assign, _value);
//        return this;
//    }
//}
