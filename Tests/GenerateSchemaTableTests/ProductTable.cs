using GenerateSchemaTableTests.Supports;
using Hand.Sql;
using ShadowSql.Identifiers;

namespace GenerateSchemaTableTests;

[GenerateTable<Product>]
partial class ProductTable
{
    public IColumn Status { get; } = Column.Use("Status");
}
