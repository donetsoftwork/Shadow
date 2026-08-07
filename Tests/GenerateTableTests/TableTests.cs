using Hand;
using Hand.Sql;
using ShadowSql.GenerateTable;
using ShadowSql.Identifiers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateTableTests;

public class TableTests
{
    [Fact]
    public void Generate()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;

namespace GenerateTableTests;

public record Product(int Id, string Name);

[GenerateTable<Product>]
partial class ProductTable;
";
        var service = SyntaxTreeDriver.CreateDefaultDriver()
            .Reference(typeof(GenerateTableAttribute<>))
            .Reference<MemberRuleParser>()
            .Reference<Table>();
        var result = service.Generate<TableGenerator>(source)
            .GetRunResult();
        var syntaxTree = result.GeneratedTrees.FirstOrDefault();
        Assert.NotNull(syntaxTree);
        var code = syntaxTree.GetText().ToString();
        Assert.Contains(" Id ", code);
        Assert.Contains(" Name ", code);
    }
    [Fact]
    public void Table()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateTableTests;

[Table(""Products"")]
public record Product(int Id, string Name);

[GenerateTable<Product>]
partial class ProductTable;
";
        var service = SyntaxTreeDriver.CreateDefaultDriver()
            .Reference(typeof(GenerateTableAttribute<>))
            .Reference<TableAttribute>()
            .Reference<MemberRuleParser>()
            .Reference<Table>();
        var result = service.Generate<TableGenerator>(source)
            .GetRunResult();
        var syntaxTree = result.GeneratedTrees.FirstOrDefault();
        Assert.NotNull(syntaxTree);
        var code = syntaxTree.GetText().ToString();
        Assert.Contains(" Id ", code);
        Assert.Contains(" Name ", code);
        Assert.Contains("Products", code);
    }
    [Fact]
    public void Table2()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateTableTests;

public record Product(int Id, string Name);

[GenerateTable<Product>]
[Table(""ProductTable"")]
partial class ProductTable;
";
        var service = SyntaxTreeDriver.CreateDefaultDriver()
            .Reference(typeof(GenerateTableAttribute<>))
            .Reference<TableAttribute>()
            .Reference<MemberRuleParser>()
            .Reference<Table>();
        var result = service.Generate<TableGenerator>(source)
            .GetRunResult();
        var syntaxTree = result.GeneratedTrees.FirstOrDefault();
        Assert.NotNull(syntaxTree);
        var code = syntaxTree.GetText().ToString();
        Assert.Contains(" Id ", code);
        Assert.Contains(" Name ", code);
        Assert.Contains("ProductTable", code);
    }
    [Fact]
    public void Key()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;
using System.ComponentModel.DataAnnotations;

namespace GenerateTableTests;

public class Product(int id, string name)
{
    [Key]
    public int Id { get; } = id;
    public string Name { get; } = name;
}
[GenerateTable<Product>]
partial class ProductTable;
";
        var service = SyntaxTreeDriver.CreateDefaultDriver()
            .Reference(typeof(GenerateTableAttribute<>))
            .Reference<KeyAttribute>()
            .Reference<MemberRuleParser>()
            .Reference<Table>();
        var result = service.Generate<TableGenerator>(source)
            .GetRunResult();
        var syntaxTree = result.GeneratedTrees.FirstOrDefault();
        Assert.NotNull(syntaxTree);
        var code = syntaxTree.GetText().ToString();
        Assert.Contains(" Id ", code);
        Assert.Contains(" Name ", code);
        Assert.Contains("Ignore(Id)", code);
    }
    [Fact]
    public void DatabaseGenerated()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateTableTests;

public class Product(int id)
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; } = id;
}
[GenerateTable<Product>]
partial class ProductTable;
";
        var service = SyntaxTreeDriver.CreateDefaultDriver()
            .Reference(typeof(GenerateTableAttribute<>))
            .Reference<DatabaseGeneratedAttribute>()
            .Reference<MemberRuleParser>()
            .Reference<Table>();
        var result = service.Generate<TableGenerator>(source)
            .GetRunResult();
        var syntaxTree = result.GeneratedTrees.FirstOrDefault();
        Assert.NotNull(syntaxTree);
        var code = syntaxTree.GetText().ToString();
        Assert.Contains(" Id ", code);
        Assert.Contains("Ignore(Id)", code);
    }
    [Fact]
    public void NotMapped()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateTableTests;

public class Product(int id, string name)
{
    public int Id { get; } = id;
    [NotMapped]
    public string Name { get; } = name;
}
[GenerateTable<Product>]
partial class ProductTable;
";
        var service = SyntaxTreeDriver.CreateDefaultDriver()
            .Reference(typeof(GenerateTableAttribute<>))
            .Reference<NotMappedAttribute>()
            .Reference<MemberRuleParser>()
            .Reference<Table>();
        var result = service.Generate<TableGenerator>(source)
            .GetRunResult();
        var syntaxTree = result.GeneratedTrees.FirstOrDefault();
        Assert.NotNull(syntaxTree);
        var code = syntaxTree.GetText().ToString();
        Assert.Contains(" Id ", code);
        Assert.DoesNotContain(" Name ", code);
    }

    [Fact]
    public void Column()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateTableTests;

public class Product(string name)
{
    [Column(""ProductName"")]
    public string Name { get; } = name;
}
[GenerateTable<Product>]
partial class ProductTable;
";
        var service = SyntaxTreeDriver.CreateDefaultDriver()
            .Reference(typeof(GenerateTableAttribute<>))
            .Reference<ColumnAttribute>()
            .Reference<MemberRuleParser>()
            .Reference<Table>();
        var result = service.Generate<TableGenerator>(source)
            .GetRunResult();
        var syntaxTree = result.GeneratedTrees.FirstOrDefault();
        Assert.NotNull(syntaxTree);
        var code = syntaxTree.GetText().ToString();
        Assert.Contains("ProductName", code);
    }
}
