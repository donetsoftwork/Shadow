using ShadowSql.Identifiers;

namespace TestSupports;

public class StudentTable : Table
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
