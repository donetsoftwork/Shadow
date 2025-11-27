using Shadow.DDL.Schemas;
using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace Dapper.ShadowTests;

public class StudentTable(string tableName = "Students")
    : TableSchema(tableName, [ExecuteTestBase._id, ExecuteTestBase._name, ExecuteTestBase._age, ExecuteTestBase._classId])
{
    public readonly IColumn Id = ExecuteTestBase._id;
    new public readonly IColumn Name = ExecuteTestBase._name;
    public readonly IColumn Age = ExecuteTestBase._age;
    public readonly IColumn ClassId = ExecuteTestBase._classId;
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
    public readonly IPrefixField Id;
    new public readonly IPrefixField Name;
    public readonly IPrefixField Age;
    public readonly IPrefixField ClassId;
}