using Hand;
using Hand.Collections.Grouping;
using Hand.Sql;
using Hand.Symbols;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shadow.DDL.Schemas;
using ShadowSql.GenerateTable;
using System.Collections.Immutable;

namespace GenerateSchemaTableTests;

public class TableSchemaSourceTests
{
    public static TableSchemaSource Transform(string source, params Type[] references)
    {
        var driver = SyntaxTreeDriver.CreateDefaultDriver()
            .Reference(typeof(GenerateTableAttribute<>))
            .Reference<MemberRuleParser>()
            .Reference<TableSchema>();
        foreach (var item in references)
            driver.Reference(item.Assembly);

        var compilation = driver.Compile(source);
        Assert.NotNull(compilation);
        var syntaxTree = compilation.SyntaxTrees.FirstOrDefault();
        Assert.NotNull(syntaxTree);
        var type = syntaxTree.GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .FirstOrDefault(item => item.Modifiers.IsPartial());
        Assert.NotNull(type);
        var semanticModel = compilation.GetSemanticModel(syntaxTree);
        var targetSymbol = semanticModel.GetDeclaredSymbol(type);
        Assert.NotNull(targetSymbol);
        //var tableType = TableTransform.GetTableType(compilation);
        //Assert.NotNull(tableType);
        var attribute = GetGenerateTableAttribute(compilation, targetSymbol.GetAttributes());
        Assert.NotNull(attribute);

        var table = TableTransform.Transform(compilation, type, targetSymbol, attribute) as TableSchemaSource;
        Assert.NotNull(table);
        return table;
    }
    /// <summary>
    /// 获取生成表配置标记
    /// </summary>
    /// <param name="compilation"></param>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public static AttributeData? GetGenerateTableAttribute(Compilation compilation, ImmutableArray<AttributeData> attributes)
    {
        var attributeType = compilation.GetTypeByMetadataName(TableGenerator.Attribute);
        if (attributeType is null)
            return null;
        //var attribute = SymbolAttributeHelper.GetAttributesByType(attributes, attributeType)
        //    .FirstOrDefault();
        var attribute = GetAttributesByType(attributes, attributeType)
            .FirstOrDefault();
        return attribute;
    }
    /// <summary>
    /// 获取标记
    /// </summary>
    /// <param name="attributes"></param>
    /// <param name="attributeSymbol"></param>
    /// <returns></returns>
    public static IEnumerable<AttributeData> GetAttributesByType(IEnumerable<AttributeData> attributes, INamedTypeSymbol attributeSymbol)
    {
        var checker = GetAttributeClassChecker(attributeSymbol);
        foreach (var attribute in attributes)
        {
            // 检查标记
            var attributeClass = attribute.AttributeClass;
            if (attributeClass is null)
                continue;
            if (checker(attributeClass))
                yield return attribute;
        }
    }
    /// <summary>
    /// 获取标记类检查器
    /// </summary>
    /// <param name="attributeSymbol"></param>
    /// <returns></returns>
    public static Predicate<INamedTypeSymbol> GetAttributeClassChecker(INamedTypeSymbol attributeSymbol)
    {
        return attributeSymbol.IsGenericType ? AttributeDefinition : AttributeEquals;

        bool AttributeEquals(INamedTypeSymbol symbol)
            => SymbolTypeDescriptor.CheckEquals(attributeSymbol, symbol);
        bool AttributeDefinition(INamedTypeSymbol symbol)
            => symbol.IsGenericType(attributeSymbol);
    }
    [Fact]
    public void CheckColumnInitializer()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;

namespace GenerateSchemaTableTests;

public record Product(int Id, string Name);

[GenerateTable<Product>]
partial class ProductTable;
";
        var table = Transform(source);
        var id = table.SourseProperties.FirstOrDefault(p => p.Name == "Id");
        Assert.NotNull(id);
        GroupList tableConstraints = new();
        var initializer = table.CheckColumnInitializer(id, id.Type, tableConstraints);
        Assert.NotNull(initializer);
        var code = initializer.NormalizeWhitespace().ToString();
        Assert.Contains("ColumnSchema<int>(\"Id\"", code);
    }
    [Fact]
    public void CreateField()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;

namespace GenerateSchemaTableTests;

public record Product(int Id, string Name);

[GenerateTable<Product>]
partial class ProductTable;
";
        var table = Transform(source);

        var id = table.SourseProperties.FirstOrDefault(p => p.Name == "Id");
        Assert.NotNull(id);
        GroupList tableConstraints = new();
        var initializer = table.CheckColumnInitializer(id, id.Type, tableConstraints);
        Assert.NotNull(initializer);
        var columnType = table.ColumnType.ToSyntax();
        //var baseFileds = table.BaseFields.Select(static item => item.Name)
        //    .ToFrozenSet();
        var field = TableSchemaSource.CreateField("_id", columnType, table.BaseMemberNames, initializer);
        var code = field.NormalizeWhitespace().ToString();
        Assert.Contains("ColumnSchema _id", code);
        Assert.Contains("ColumnSchema<int>(\"Id\"", code);
    }
    [Fact]
    public void Column()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateSchemaTableTests;

public class Product(string name)
{
    [Column(""ProductName"")]
    public string Name { get; } = name;
}

[GenerateTable<Product>]
partial class ProductTable;
";
        var table = Transform(source);

        var name = table.SourseProperties.FirstOrDefault(p => p.Name == "Name");
        Assert.NotNull(name);
        GroupList tableConstraints = new();
        var initializer = table.CheckColumnInitializer(name, name.Type, tableConstraints);
        Assert.NotNull(initializer);
        var columnType = table.ColumnType.ToSyntax();
        //var baseFileds = table.BaseFields.Select(static item => item.Name)
        //    .ToFrozenSet();
        var field = TableSchemaSource.CreateField("_name", columnType, table.BaseMemberNames, initializer);
        var code = field.NormalizeWhitespace().ToString();
        Assert.Contains("ColumnSchema _name", code);
        Assert.Contains("ColumnSchema<string>(\"ProductName\", \"Name\"", code);
    }
    [Fact]
    public void Unique()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;

namespace GenerateSchemaTableTests;

public class Product(string name)
{
    [Unique]
    public string Name { get; } = name;
}

[GenerateTable<Product>]
partial class ProductTable;
";
        var table = Transform(source);

        var name = table.SourseProperties.FirstOrDefault(p => p.Name == "Name");
        Assert.NotNull(name);
        GroupList tableConstraints = new();
        var initializer = table.CheckColumnInitializer(name, name.Type, tableConstraints);
        Assert.NotNull(initializer);
        var columnType = table.ColumnType.ToSyntax();
        //var baseFileds = table.BaseMemberNames;
        var field = TableSchemaSource.CreateField("_name", columnType, table.BaseMemberNames, initializer);
        var code = field.NormalizeWhitespace().ToString();
        Assert.Contains("ColumnSchema _name", code);
        Assert.Contains("ColumnType.Unique", code);
    }
}
