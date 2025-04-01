//using ShadowSql.Engines;
//using ShadowSql.Identifiers;
//using ShadowSql.SqlVales;
//using System.Text;

//namespace ShadowSql.Assigns;

///// <summary>
///// 对列赋参数
///// </summary>
///// <param name="columnName"></param>
///// <param name="parameterName"></param>
///// <param name="assign"></param>
//public class ColumnAssignParameter(string columnName, AssignSymbol assign, string parameterName)
//    : ColumnAssignBase(columnName, assign)
//{
//    #region 配置
//    private readonly string _parameterName = parameterName;
//    /// <summary>
//    /// 参数名
//    /// </summary>
//    public string ParameterName
//        => Parameter.CheckName(_parameterName, _columnName);
//    /// <summary>
//    /// 参数作为值
//    /// </summary>
//    public override ISqlValue Value
//        => Parameter.Use(ParameterName);
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
//        engine.Parameter(sql, ParameterName);
//        return true;
//    }
//    /// <summary>
//    /// 构造赋值操作
//    /// </summary>
//    /// <param name="view"></param>
//    /// <returns></returns>
//    public override IAssignOperation CreateAssignOperation(ITableView view)
//    {
//        if (view.GetColumn(_columnName) is IColumn column)
//            return new AssignOperation(column, _assign, Parameter.Use(_parameterName, column));

//        return this;
//    }
//}
