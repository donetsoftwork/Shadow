using Shadow.DDL.Schemas;

namespace Shadow.DDLTests.Supports;

public class StudentTable(string tableName = "Students", string schema = "")
    : TableSchema(tableName, [Defines.Id, Defines.Name, Defines.Score], schema)
{
    public readonly ColumnSchema Id = Defines.Id;
    new public readonly ColumnSchema Name = Defines.Name;
    public readonly ColumnSchema Score = Defines.Score;
    class Defines
    {
        public static readonly ColumnSchema Id = new("Id") { ColumnType = ColumnType.Key };
        public static readonly ColumnSchema Name = new("Name");
        public static readonly ColumnSchema Score = new("Score");
    }
}
