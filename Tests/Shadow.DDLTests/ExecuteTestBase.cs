using Dapper;
using Dapper.Shadow;
using Hand.Data;
using Microsoft.Data.Sqlite;
using Shadow.DDL;
using Shadow.DDL.Components;
using Shadow.DDL.Schemas;
using ShadowSql.Components;
using ShadowSql.Engines.Sqlite;
using ShadowSql.Identifiers;
using ShadowSql.Sqlite;
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
        var engine = SqliteSqlType.Engine;
        //#if NET8_0_OR_GREATER

        //#else
        //        var connection = new System.Data.SQLite.SQLiteConnection("Data Source=:memory:;");
        //#endif
        //var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
        var dataSource = new StringDataSource("Data Source=file::memory:;Cache=Shared", conn => new SqliteConnection(conn));
        return new DapperExecutor(engine, new SqlSource(dataSource));
    }

    protected async Task CreateStudentsTable(string tableName = "Students")
    {
        try
        {
            await new StudentTable(tableName)
                .ToCreate()
                .ExecuteAsync(SqliteExecutor);
        }
        catch { }
        //        var sql = @$"CREATE TABLE {tableName} (
        //    Id INTEGER PRIMARY KEY AUTOINCREMENT,
        //    Name TEXT NOT NULL,
        //    Age INTEGER
        //)";
        //        SqliteExecutor.Execute(sql);
    }
    protected async Task DropStudentsTable(string tableName = "Students")
    {
        try
        {
            await new StudentTable(tableName)
            .ToDrop()
            .ExecuteAsync(SqliteExecutor);
        }
        catch { }
        //var sql = DropTable.WriteDropTable(tableName);
        //SqliteExecutor.Execute(sql);
    }
    public static readonly ColumnSchema _id = new("Id", SqliteSqlType.Integer, ColumnType.Identity | ColumnType.Key);
    public static readonly ColumnSchema _name = new("Name", SqliteSqlType.Text);
    public static readonly ColumnSchema _age = new("Age", SqliteSqlType.Integer);


    public class StudentTable(string tableName = "Students")
        : TableSchema(tableName, [_id, ExecuteTestBase._name, _age])
    {
        public readonly IColumn Id = _id;
        new public readonly IColumn Name = ExecuteTestBase._name;
        public readonly IColumn Age = _age;
    }
}
