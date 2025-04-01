using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using Shadow.DDL;
using Shadow.DDL.Components;
using Shadow.DDL.Schemas;
using ShadowSql.Components;
using ShadowSql.Engines.Sqlite;
using ShadowSql.Identifiers;
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

        var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
        return new DapperExecutor(engine, connection);
    }

    protected StudentTable CreateStudentTable(string tableName = "Students")
    {
        var table = new StudentTable(tableName);
        try
        {
            table.ToCreate()
                .Execute(SqliteExecutor);
        }
        catch { }
        return table;
    }
    protected SchoolClassTable CreateSchoolClassTable(string tableName = "Classes")
    {
        var table = new SchoolClassTable(tableName);
        try
        {
            table.ToCreate()
                .Execute(SqliteExecutor);
        }
        catch { }
        return table;
    }
    protected void DropStudentTable(string tableName = "Students")
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
    protected void DropSchoolClassTable(string tableName = "Classes")
    {
        try
        {
            new SchoolClassTable(tableName)
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
    public static readonly ColumnSchema _classId = new("ClassId", "INTEGER");
    public static readonly ColumnSchema _teacher = new("Teacher", "TEXT");


    public class StudentTable(string tableName = "Students")
        : TableSchema(tableName, [_id, ExecuteTestBase._name, _age, _classId])
    {
        public readonly IColumn Id = _id;
        new public readonly IColumn Name = ExecuteTestBase._name;
        public readonly IColumn Age = _age;
        public readonly IColumn ClassId = _classId;
    }
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

        public readonly IPrefixColumn Id;
        new public readonly IPrefixColumn Name;
        public readonly IPrefixColumn Teacher;
    }
    public class StudentAliasTable
        : TableAlias<Table>
    {
        public StudentAliasTable(string tableAlias = "s")
         : this(new Table("Students"), tableAlias)
        {
        }
        private StudentAliasTable(Table table, string tableAlias)
            : base(table, tableAlias)
        {
            Id = AddColumn(Column.Use(nameof(Id)));
            Name = AddColumn(Column.Use(nameof(Name)));
            Age = AddColumn(Column.Use(nameof(Age)));
            ClassId = AddColumn(Column.Use(nameof(ClassId)));
        }
        public readonly IPrefixColumn Id;
        new public readonly IPrefixColumn Name;
        public readonly IPrefixColumn Age;
        public readonly IPrefixColumn ClassId;
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
    public class Student
    {
        /// <summary>
        /// ORM调用
        /// </summary>
        protected Student()
        {
        }
        public Student(int id, string name, int age, int classId)
        {
            Id = id;
            Name = name;
            Age = age;
            ClassId = classId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int ClassId { get; set; }
    }

    public class StudentView : Student
    {
        public string ClassName { get; set; }
        public string Teacher { get; set; }
    }
}
