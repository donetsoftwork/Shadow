using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using Shadow.DDL.Components;
using Shadow.DDL.Schemas;
using ShadowSql.Components;
using Hand.Data;
using ShadowSql.Engines.Sqlite;
using ShadowSql.Identifiers;
using ShadowSql.Sqlite;
using ShadowSql.Variants;
using System.Collections;

namespace Dapper.ShadowTests;

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
        var engine = new SqliteEngine(new SqliteSelectComponent(), new SqlValueComponent("1", "0", "NULL"), provider);

        var dataSource = new StringDataSource("Data Source=file::memory:;Cache=Shared", conn => new SqliteConnection(conn));
        return new DapperExecutor(engine, new SqlSource(dataSource));
    }
    
    public static readonly ColumnSchema _id = new("Id", "INTEGER", ColumnType.Identity | ColumnType.Key);
    public static readonly ColumnSchema _name = new("Name", "TEXT");
    public static readonly ColumnSchema _age = new("Age", "INTEGER");
    public static readonly ColumnSchema _classId = new("ClassId", "INTEGER");
    public static readonly ColumnSchema _teacher = new("Teacher", "TEXT");



    public class SchoolClassTable(string tableName = "Classes")
        : TableSchema(tableName, [_id, ExecuteTestBase._name, _teacher])
    {
        public readonly IColumn Id = _id;
        new public readonly IColumn Name = ExecuteTestBase._name;
        public readonly IColumn Teacher = _teacher;
    }

    public class SchoolClassAliaTable
      : TableAlias<Table>
    {
        public SchoolClassAliaTable(string tableAlias = "c")
            : this(new Table("Classes"), tableAlias)
        {
        }
        private SchoolClassAliaTable(Table table, string tableAlias)
            : base(table, tableAlias)
        {
            Id = AddColumn(Column.Use(nameof(Id)));
            Name = AddColumn(Column.Use(nameof(Name)));
            Teacher = AddColumn(Column.Use(nameof(Teacher)));
        }

        public readonly IPrefixField Id;
        new public readonly IPrefixField Name;
        public readonly IPrefixField Teacher;
    }

    public class SchoolClass
    {
        /// <summary>
        /// ORM调用
        /// </summary>
        protected SchoolClass()
        {
        }
        public SchoolClass(int id, string name, string teacher)
        {
            Id = id;
            Name = name;
            Teacher = teacher;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
    }
    public class StudentView : Student
    {
        public string ClassName { get; set; }
        public string Teacher { get; set; }
    }
}
