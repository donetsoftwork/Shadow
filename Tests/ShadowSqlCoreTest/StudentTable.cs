using ShadowSql.Identifiers;

namespace ShadowSqlCoreTest;

internal class StudentTable : Table
{
    public StudentTable()
        : base("Students")
    {
        Name = DefineColumn(nameof(Name));
        Score = DefineColumn(nameof(Score));
    }
    public readonly IColumn Score;
    new public readonly IColumn Name;
}
