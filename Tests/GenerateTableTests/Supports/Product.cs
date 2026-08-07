using Hand.Sql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateTableTests.Supports;

[Table("Products")]
public class Product(int id, string name)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; } = id;
    [Unique]
    [Column("ProductName")]
    public string Name { get; } = name;
    [Unique("CategoryModel")]
    public int CategoryId { get; set; }
    [Unique("CategoryModel")]
    public string Model { get; set; }
}
