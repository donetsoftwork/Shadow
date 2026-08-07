using Hand;
using Hand.Generators;
using Hand.Reflection;
using Hand.Rule;
using Hand.Symbols;
using Hand.Transform;
using Microsoft.CodeAnalysis;
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
        if (context.TargetSymbol is not INamedTypeSymbol symbol)
            return null;
        var compilation = context.SemanticModel.Compilation;
        //var attribute = GetGenerateTableAttribute(compilation, context.Attributes);
        var attribute = context.Attributes.FirstOrDefault();
        if (attribute is null)
            return null;
        var sourseSymbol = GetFromSymbol(attribute);
        if (sourseSymbol is null)
            return null;
        // 优先TableSchema
        return CreateByTableSchema(compilation, type, symbol, sourseSymbol, attribute) ??
            CreateByTable(compilation, type, symbol, sourseSymbol, attribute);
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
    /// <param name="compilation"></param>
    /// <param name="type"></param>
    /// <param name="symbol"></param>
    /// <param name="sourseSymbol"></param>
    /// <param name="attribute"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static TableSchemaSource? CreateByTableSchema(Compilation compilation, TypeDeclarationSyntax type, INamedTypeSymbol symbol, INamedTypeSymbol sourseSymbol, AttributeData attribute)
    {
        var tableSchemaType = GetTableSchemaType(compilation);
        if (tableSchemaType is null || !CheckBaseType(symbol, tableSchemaType)) 
            return null;
        var columnType = GetColumnSchemaType(compilation) ?? throw new Exception("columnType is null");
        return new TableSchemaSource(type, compilation, symbol, tableSchemaType, sourseSymbol, columnType, GetRule(attribute));
    }
    public static TableSource? CreateByTable(Compilation compilation, TypeDeclarationSyntax type, INamedTypeSymbol symbol, INamedTypeSymbol sourseSymbol, AttributeData attribute)
    {
        var tableType = GetTableType(compilation);
        if (tableType is null || !CheckBaseType(symbol, tableType))
            return null;
        var columnType = GetColumnType(compilation) ?? throw new Exception("columnType is null");
        return new TableSource(type, compilation, symbol, tableType, sourseSymbol, columnType, GetRule(attribute));
    }
    /// <summary>
    /// 检查基类
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static bool CheckBaseType(INamedTypeSymbol symbol, INamedTypeSymbol expected)
    {
        var baseSymbol = symbol.BaseType;
        if (baseSymbol is null || baseSymbol.IsObject())
            return true;
        return SymbolTypeDescriptor.CheckEquals(expected, baseSymbol);
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
