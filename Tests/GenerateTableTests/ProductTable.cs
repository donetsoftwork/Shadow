using GenerateTableTests.Supports;
using Hand.Sql;
using ShadowSql.Identifiers;

namespace GenerateTableTests;

[GenerateTable<Product>]
public partial class ProductTable
{
    //public IColumn Status { get; } = Column.Use("Status");
}
