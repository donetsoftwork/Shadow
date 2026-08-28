using Hand;
using Hand.Cachers;
using Hand.Generators;
using Hand.Reflection;
using Hand.Rule;
using Hand.Transform;
using Hand.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Threading;

namespace ShadowSql.GenerateTable;

/// <summary>
/// 表结构转化
/// </summary>
public class TableTransform : IGeneratorTransform<TableSource>
{
    /// <inheritdoc />
    public TableSource? Transform(AttributeContext context, CancellationToken cancellation = default)
    {
        if (cancellation.IsCancellationRequested)
            return null;
        if (context.TargetNode is not TypeDeclarationSyntax type)
            return null;
        if (context.TargetSymbol is not INamedTypeSymbol targetSymbol)
            return null;
        var compilation = context.SemanticModel.Compilation;
        //var attribute = GetGenerateTableAttribute(compilation, context.Attributes);
        var attribute = context.Attributes.FirstOrDefault();
        if (attribute is null)
            return null;
        return Transform(compilation, type, targetSymbol, attribute);
    }
    public static TableSource? Transform(Compilation compilation, TypeDeclarationSyntax type, INamedTypeSymbol targetSymbol, AttributeData attribute)
    {
        var typeSymbols = new TypeSymbolCacher(compilation);
        if (typeSymbols.Get(targetSymbol) is not ComplexTypeInfo targetInfo)
            return null;
        var sourseSymbol = GetFromSymbol(attribute);
        if (sourseSymbol is null || sourseSymbol.Equals(targetSymbol, SymbolEqualityComparer.IncludeNullability))
            return null;
        if (typeSymbols.Get(sourseSymbol) is not ComplexTypeInfo sourseInfo)
            return null;
        INamedTypeSymbol baseSymbol = targetSymbol.BaseType!;
        var tableType = GetTableType(compilation);
        var tableSchemaType = GetTableSchemaType(compilation);
        var generator = SyntaxGenerator.Clone(type);
        if (tableSchemaType is null)
        {
            if (tableType is null)
                return null;
            if (CheckBaseType(generator, ref baseSymbol, tableType))
                return CreateByTable(typeSymbols, generator, targetInfo, sourseInfo, baseSymbol, attribute);
            return null;
        }
        if (tableType is null)
        {
            if (CheckBaseType(generator, ref baseSymbol, tableSchemaType))
                return CreateByTableSchema(typeSymbols, generator, targetInfo, sourseInfo, baseSymbol, attribute);
            return null;
        }
        if (CheckBaseType(generator, ref baseSymbol, tableSchemaType))
            return CreateByTableSchema(typeSymbols, generator, targetInfo, sourseInfo, baseSymbol, attribute);
        if (CheckBaseType(generator, ref baseSymbol, tableType))
            return CreateByTable(typeSymbols, generator, targetInfo, sourseInfo, baseSymbol, attribute);
        return null;
    }
    /// <summary>
    /// 解析源类型符号
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetFromSymbol(AttributeData attribute)
    {
        var attributeType = attribute.AttributeClass;
        if (attributeType == null)
            return null;
        if (attributeType.IsGenericType)
            return attributeType.TypeArguments[0] as INamedTypeSymbol;
        return null;
    }
    /// <summary>
    /// 按TableSchema生成
    /// </summary>
    /// <param name="typeSymbols"></param>
    /// <param name="generator"></param>
    /// <param name="targetInfo"></param>
    /// <param name="sourseInfo"></param>
    /// <param name="baseSymbol"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static TableSchemaSource? CreateByTableSchema(TypeSymbolCacher typeSymbols, SyntaxGenerator generator, ComplexTypeInfo targetInfo, ComplexTypeInfo sourseInfo, INamedTypeSymbol baseSymbol, AttributeData attribute)
    {
        var compilation = typeSymbols.Compilation;
        var columnType = GetColumnSchemaType(compilation) ?? throw new Exception("columnType is null");
        return new TableSchemaSource(generator, typeSymbols, compilation, targetInfo, sourseInfo, baseSymbol, columnType, GetRule(attribute));
    }
    /// <summary>
    /// 按Table生成
    /// </summary>
    /// <param name="typeSymbols"></param>
    /// <param name="generator"></param>
    /// <param name="targetInfo"></param>
    /// <param name="sourseInfo"></param>
    /// <param name="baseSymbol"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static TableSource? CreateByTable(TypeSymbolCacher typeSymbols, SyntaxGenerator generator, ComplexTypeInfo targetInfo, ComplexTypeInfo sourseInfo, INamedTypeSymbol baseSymbol, AttributeData attribute)
    {
        var compilation = typeSymbols.Compilation;
        var columnType = GetColumnType(compilation) ?? throw new Exception("columnType is null");
        return new TableSource(generator, compilation, targetInfo, sourseInfo, baseSymbol, columnType, GetRule(attribute));
    }
    /// <summary>
    /// 检查基类
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static bool CheckBaseType(SyntaxGenerator generator, ref INamedTypeSymbol baseSymbol, INamedTypeSymbol expected)
    {
        if(baseSymbol is null || baseSymbol.IsObject())
        {
            baseSymbol = expected;
            generator.AddBaseType(SyntaxFactory.SimpleBaseType(generator.Display(expected)));
            return true;
        }
        if(baseSymbol.IsBase(expected))
            return true;
        return false;
    }
    /// <summary>
    /// 获取规则
    /// </summary>
    /// <param name="attribute"></param>
    /// <returns></returns>
    public static IValidation<string> GetRule(AttributeData attribute)
    {
        var rule = SymbolAttributeHelper.GetArgumentConstant(attribute, "Rule");
        if (rule is null)
            return Logic.True<string>();
        return MemberRuleParser.Default.Parse(rule.Value.GetPrimitive(string.Empty));
    }
    /// <summary>
    /// 判断是否支持ColumnSchema类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static bool CheckSchema(Compilation compilation)
    {
        var type = compilation.GetTypeByMetadataName("Shadow.DDL.Schemas.TableSchema");
        return type is not null;
    }
    /// <summary>
    /// 获取Table类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetTableType(Compilation compilation)
        => compilation.GetTypeByMetadataName("ShadowSql.Identifiers.Table");
    /// <summary>
    /// 获取TableSchema类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetTableSchemaType(Compilation compilation)
        => compilation.GetTypeByMetadataName("Shadow.DDL.Schemas.TableSchema");
    /// <summary>
    /// 获取IColumn类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetColumnType(Compilation compilation)
        => compilation.GetTypeByMetadataName("ShadowSql.Identifiers.IColumn");
    /// <summary>
    /// 获取ColumnSchema类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetColumnSchemaType(Compilation compilation)
        => compilation.GetTypeByMetadataName("Shadow.DDL.Schemas.ColumnSchema");
    /// <summary>
    /// 单例
    /// </summary>
    public static readonly TableTransform Instance = new();
}
