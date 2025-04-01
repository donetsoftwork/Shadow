using Dapper;
using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using Shadow.DDL;
using Shadow.DDL.Components;
using Shadow.DDL.Schemas;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.SqlVales;
using System.Collections;

namespace Shadow.DDLTests;

/// <summary>
/// 执行sql测试基类
/// </summary>
public abstract class ExecuteTestBase
{
    private readonly Lazy<DapperExecutor> _sqlite = new(CreateSqlite);
    /// <summary>
    /// Sqlite执行器
    /// </summary>
    public DapperExecutor SqliteExecutor
        => _sqlite.Value;


    private static DapperExecutor CreateSqlite()
    {
        var provider = new DictionaryProvider(new Hashtable())
            .AddComponent<IDefineColumComponent>(new DefineSqliteColumComponent());
        var engine = new Sqlite(new SqlValueProvider("1", "0", "NULL"), provider);

        var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
        return new DapperExecutor(engine, connection);
    }

    protected void CreateStudentsTable(string tableName = "Students")
    {
        try
        {
            new StudentTable(tableName)
                .ToCreate()
                .Execute(SqliteExecutor);
        }
        catch { }
        //        var sql = @$"CREATE TABLE {tableName} (
        //    Id INTEGER PRIMARY KEY AUTOINCREMENT,
        //    Name TEXT NOT NULL,
        //    Age INTEGER
        //)";
        //        SqliteExecutor.Execute(sql);
    }
    protected void DropStudentsTable(string tableName = "Students")
    {
        try
        {
            new StudentTable(tableName)
            .ToDrop()
            .Execute(SqliteExecutor);
        }
        catch { }
        //var sql = DropTable.WriteDropTable(tableName);
        //SqliteExecutor.Execute(sql);
    }
    public static readonly ColumnSchema _id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
    public static readonly ColumnSchema _name = new("Name", "TEXT");
    public static readonly ColumnSchema _age = new("Age", "INTEGER");


    public class StudentTable(string tableName = "Students")
        : TableSchema(tableName, [_id, ExecuteTestBase._name, _age])
    {
        public readonly IColumn Id = _id;
        new public readonly IColumn Name = ExecuteTestBase._name;
        public readonly IColumn Age = _age;
    }
}
