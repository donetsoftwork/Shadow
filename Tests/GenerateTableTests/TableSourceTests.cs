using Hand;
using Hand.Sql;
using Hand.Symbols;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ShadowSql.GenerateTable;
using ShadowSql.Identifiers;
using System.Collections.Frozen;
using System.Collections.Immutable;

namespace GenerateTableTests;

public class TableSourceTests
{
    public static TableSource Transform(string source, params Type[] references)
    {
        var driver = SyntaxTreeDriver.CreateDefaultDriver()
            .Reference(typeof(GenerateTableAttribute<>))
            .Reference<MemberRuleParser>()
            .Reference<Table>();
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

        var table = TableTransform.Transform(compilation, type, targetSymbol, attribute);
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
    public void CreateProperty()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;

namespace GenerateTableTests;

public record Product(int Id);

[GenerateTable<Product>]
partial class ProductTable;
";
        var table = Transform(source);
        var columnType = table.ColumnType.ToSyntax();
        var properties0 = table.Properties.Select(static item => item.Name)
            .ToFrozenSet();
        var id = table.SourseProperties.FirstOrDefault(p => p.Name == "Id");
        Assert.NotNull(id);
        var property = TableSource.CreateProperty(columnType, id, properties0);
        var code = property.NormalizeWhitespace().ToString();
        Assert.Equal("public ShadowSql.Identifiers.IColumn Id { get; }", code);
    }
    [Fact]
    public void CreateNewProperty()
    {
        var source = @"
using Hand;
using Hand.Entities;
using Hand.Sql;

namespace GenerateTableTests;

public record Product(string Name);

[GenerateTable<Product>]
partial class ProductTable;
";
        var table = Transform(source);
        var columnType = table.ColumnType.ToSyntax();
        //var properties0 = table.BaseProperties.Select(static item => item.Name)
        //    .ToFrozenSet();
        var name = table.SourseProperties.FirstOrDefault(p => p.Name == "Name");
        Assert.NotNull(name);
        var property = TableSource.CreateProperty(columnType, name, table.BaseMemberNames);
        var code = property.NormalizeWhitespace().ToString();
        Assert.Equal("public new ShadowSql.Identifiers.IColumn Name { get; }", code);
    }    
}
