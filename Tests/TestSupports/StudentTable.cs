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
    public IColumn Score { get; }
    public new IColumn Name { get; }
}
