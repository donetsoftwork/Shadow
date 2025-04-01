//using ShadowSql.Engines;
//using ShadowSql.Fragments;
//using ShadowSql.Identifiers;
//using System.Text;

//namespace ShadowSql.FieldInfos;

//public class TableAllFieldInfo : IFieldView
//{
//    internal TableAllFieldInfo(ITableView table)
//    {
//        _table = table;
//    }
//    #region 配置
//    private readonly ITableView _table;
//    /// <summary>
//    /// 表
//    /// </summary>
//    public ITableView Table
//        => _table;

//    string IView.ViewName { get; }

//    IColumn IFieldView.ToColumn()
//    {
//        throw new System.NotImplementedException();
//    }

//    bool IMatch.IsMatch(string name)
//    {
//        throw new System.NotImplementedException();
//    }

//    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
//    {
//        _table.Write(engine, sql);
//        sql.Append('.').Append('*');
//    }
//    #endregion
//}
