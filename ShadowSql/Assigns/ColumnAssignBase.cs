//using ShadowSql.Engines;
//using ShadowSql.Identifiers;
//using ShadowSql.SqlVales;
//using System.Text;

//namespace ShadowSql.Assigns;

///// <summary>
///// 列赋值操作基类
///// </summary>
///// <param name="columnName"></param>
///// <param name="assign"></param>
//public abstract class ColumnAssignBase(string columnName, AssignSymbol assign)
//    : /*IAssignInfo, */IAssignOperation
//{
//    /// <summary>
//    /// 列名
//    /// </summary>
//    protected readonly string _columnName = columnName;
//    /// <summary>
//    /// 赋值操作符(默认Equal)
//    /// </summary>
//    protected readonly AssignSymbol _assign = assign;
//    /// <summary>
//    /// 列名
//    /// </summary>
//    public string ColumnName
//        => _columnName;
//    /// <summary>
//    /// 左边列
//    /// </summary>
//    IColumn IAssignOperation.Column
//        => Column.Use(_columnName);
//    /// <summary>
//    /// 右边值(也可以是列)
//    /// </summary>
//    public abstract ISqlValue Value { get; }
//    /// <summary>
//    /// 赋值操作符(默认Equal)
//    /// </summary>
//    public AssignSymbol Assign
//        => _assign;
//    /// <summary>
//    /// 构造赋值操作
//    /// </summary>
//    /// <param name="view"></param>
//    /// <returns></returns>
//    public abstract IAssignOperation CreateAssignOperation(ITableView view);
//    /// <summary>
//    /// 拼写sql
//    /// </summary>
//    /// <param name="engine"></param>
//    /// <param name="sql"></param>
//    /// <returns></returns>
//    public abstract bool Write(ISqlEngine engine, StringBuilder sql);
//}
