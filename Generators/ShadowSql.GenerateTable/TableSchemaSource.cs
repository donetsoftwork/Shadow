using Hand;
using Hand.Collections;
using Hand.Collections.Grouping;
using Hand.Reflection;
using Hand.Rule;
using Hand.Symbols;
using Hand.Words;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;

namespace ShadowSql.GenerateTable;

/// <summary>
/// 表结构源对象
/// </summary>
/// <param name="type"></param>
/// <param name="compilation"></param>
/// <param name="typeSymbol"></param>
/// <param name="baseTypeSymbol"></param>
/// <param name="sourseSymbol"></param>.
/// <param name="columnType"></param>
/// <param name="rule"></param>
public class TableSchemaSource(TypeDeclarationSyntax type, Compilation compilation, INamedTypeSymbol typeSymbol, INamedTypeSymbol baseTypeSymbol, INamedTypeSymbol sourseSymbol, INamedTypeSymbol columnType, IValidation<string> rule)
    : TableSource(type, compilation, typeSymbol, baseTypeSymbol, sourseSymbol, columnType, rule)
{
    #region 配置
    private readonly Compilation _compilation = compilation;
    private readonly INamedTypeSymbol? _requiredAttributeType = GetRequiredAttributeType(compilation);
    private readonly INamedTypeSymbol? _precisionAttributeType = GetPrecisionAttributeType(compilation);
    private readonly INamedTypeSymbol? _maxLengthAttributeType = GetMaxLengthAttributeType(compilation);
    private readonly INamedTypeSymbol? _stringLengthAttributeType = GetStringLengthAttributeType(compilation);
    private readonly INamedTypeSymbol? _defaultValueAttributeType = GetDefaultValueAttributeType(compilation);
    private readonly INamedTypeSymbol? _uniqueAttributeType = GetUniqueAttributeType(compilation);
    #endregion
    /// <summary>
    /// 定义构造函数
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    private ConstructorDeclarationSyntax CreateConstructor(IdentifierNameSyntax tableName, IdentifierNameSyntax schema)
    {
        var (schemaDefaultValue, tableNameDefaultValue) = GetTableDefaultValue();
        var constructor = _type.Constructor(Parameter(tableName.Identifier, tableNameDefaultValue), Parameter(schema.Identifier, schemaDefaultValue))
            .Public();
        return constructor;
    }
    /// <summary>
    /// 定义构造函数
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public override ConstructorDeclarationSyntax CreateConstructor(SyntaxGenerator builder, IdentifierNameSyntax tableName)
    {
        var schema = SyntaxFactory.IdentifierName("schema");
        var constructor = CreateConstructor(tableName, schema)
            .ToBuilder();
        var tableConstraints = new GroupList<string, string>(new ConcurrentDictionary<string, ICollection<string>>());
        var columns = new List<ExpressionSyntax>();
        foreach(var column in CreateColumns(builder, tableConstraints))
        {
            columns.Add(SyntaxFactory.IdentifierName(column));
        }
        var constraints = new List<ExpressionSyntax>();
        foreach (var constraint in CreateConstraints(builder, tableConstraints))
        {
            constraints.Add(SyntaxFactory.IdentifierName(constraint));
        }
        return constructor.End()
             // TableSchema(string tableName, ColumnSchema[] columns, string schema = "")
             .WithBaseInitializer(tableName, SyntaxGenerator.Collection([.. columns]), SyntaxGenerator.Collection([.. constraints]), schema);
    }
    public IEnumerable<string> CreateConstraints(SyntaxGenerator builder, IGroupCollection<string, string> tableConstraints)
    {
        var constraintType = SyntaxFactory.IdentifierName("global::Shadow.DDL.Constraints.UniqueConstraint");
        foreach (var name in tableConstraints.Keys)
        {
            var columnNames = tableConstraints.GetValues(name)
                .ToArray();
            var count = columnNames.Length;
            if (count == 0)
                continue;
            var fieldName = UnderWordRule.UnderLower(name);
            // 新字段与原字段不能重名
            if (_memberNames.Contains(fieldName))
                continue;
            var constraintInitializer = CheckConstraintInitializer(constraintType, name, columnNames);
            var field = CreateField(fieldName, constraintType, _baseMemberNames, constraintInitializer);
            builder.AddField(field);
            yield return fieldName;
        }
    }
    public static ExpressionSyntax CheckConstraintInitializer(TypeSyntax constraintType, string name, string[] columnNames)
    {
        var arguments = new List<ExpressionSyntax>(columnNames.Length + 1)
        { 
            SyntaxGenerator.Literal(name)
        };
        foreach (var column in columnNames)
            arguments.Add(SyntaxGenerator.Literal(column));
        return constraintType.New(arguments);
    }
    public IEnumerable<string> CreateColumns(SyntaxGenerator builder, IGroupCollection<string, string> tableConstraints)
    {
        var columnType = _columnType.ToSyntax();
        foreach (var property in _sourseProperties)
        {
            var fieldName = CreateColumn(builder, tableConstraints, columnType, property, property.Type);
            if (string.IsNullOrEmpty(fieldName))
                continue;
            yield return fieldName;
        }
        foreach (var field in _sourseFields)
        {
            var fieldName = CreateColumn(builder, tableConstraints, columnType, field, field.Type);
            if (string.IsNullOrEmpty(fieldName))
                continue;
            yield return fieldName;
        }
    }
    /// <summary>
    /// 构造列
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tableConstraints"></param>
    /// <param name="columnType"></param>
    /// <param name="baseProperties"></param>
    /// <param name="baseFields"></param>
    /// <param name="properties0"></param>
    /// <param name="fileds0"></param>
    /// <param name="member"></param>
    /// <param name="memberType"></param>
    /// <returns></returns>
    private string CreateColumn(SyntaxGenerator builder, IGroupCollection<string, string> tableConstraints, TypeSyntax columnType, ISymbol member, ITypeSymbol memberType)
    {
        var memberName = member.Name;
        // 新属性与原属性不能重名
        if (_memberNames.Contains(memberName))
            return string.Empty;
        var fieldName = UnderWordRule.UnderLower(memberName);
        // 新字段与原字段不能重名
        if (_memberNames.Contains(fieldName))
            return string.Empty;
        // 生成字段 
        var columnInitializer = CheckColumnInitializer(member, memberType, tableConstraints);
        var field = CreateField(fieldName, columnType, _baseMemberNames, columnInitializer);
        builder.AddField(field);
        // 生成属性
        var property = CreateProperty(columnType, member, fieldName, _baseMemberNames);
        builder.AddProperty(property);
        return fieldName;
    }

    /// <summary>
    /// 构造列属性
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="columnType"></param>
    /// <param name="member"></param>
    /// <param name="fieldName"></param>
    /// <param name="baseMembers"></param>
    /// <returns></returns>
    private static PropertyDeclarationSyntax CreateProperty(TypeSyntax columnType, ISymbol member,string fieldName, FrozenSet<string> baseMembers)
    {
        var memberName = member.Name;
        var property = columnType.Property(memberName, SyntaxFactory.IdentifierName(fieldName))
            .Public();
        // 父类重名需要加new
        if (baseMembers.Contains(memberName))
            return property.New();
        return property;
    }
    /// <summary>
    /// 获取列配置
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    private (string columnName, string memberName, string? typeName) GetColumn(ISymbol symbol)
    {
        var memberName = symbol.Name;
        if (_columnAttributeType is null)
            return (memberName, memberName, null);
        var attribute = SymbolAttributeHelper.GetAttributesByType(symbol, _columnAttributeType)
            .FirstOrDefault();
        if (attribute is null)
            return (memberName, memberName, null);
        var columnName = SymbolAttributeHelper.GetArgumentValue<string>(attribute, 0);
        var typeName = SymbolAttributeHelper.GetArgumentValue<string>(attribute, "TypeName");
        if (string.IsNullOrWhiteSpace(columnName))
            return (memberName, memberName, typeName);
        return (columnName!, memberName, typeName);
    }
    /// <summary>
    /// 构造字段
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fieldType"></param>
    /// <param name="baseFields"></param>
    /// <param name="initializer"></param>
    /// <returns></returns>
    public static FieldDeclarationSyntax CreateField(string name, TypeSyntax fieldType, ISet<string> baseFields, ExpressionSyntax initializer)
    {
        var field = fieldType.Field(name, initializer)
            .Private();
        // 父类重名需要加new
        if (baseFields.Contains(name))
            field = field.New();
        return field.Static().ReadOnly();
    }
    /// <summary>
    /// 列初始化(new)
    /// </summary>
    /// <param name="member"></param>
    /// <param name="fieldType"></param>
    /// <param name="tableConstraints"></param>
    /// <returns></returns>
    public ExpressionSyntax CheckColumnInitializer(ISymbol member, ITypeSymbol memberType, IGroupCollection<string, string> tableConstraints)
    {
        const string columnTypeName = "global::Shadow.DDL.Schemas.ColumnSchema";
        var (fieldType, nullable) = CheckMemberType(member, memberType);
        var defaultValue = CheckInitializer(member);
        var (columnName, memberName, typeName) = GetColumn(member);
        List<ExpressionSyntax> arguments = CheckColumnArguments(columnName, memberName);
        var columnTypeArgument = CheckColumnTypes(columnName, member, nullable, tableConstraints);
        if (!string.IsNullOrWhiteSpace(typeName))
        {
            arguments.Add(SyntaxGenerator.Literal(typeName!));
            if (columnTypeArgument is not null)
                arguments.Add(columnTypeArgument);
            return SyntaxFactory.IdentifierName(columnTypeName).New(arguments, defaultValue);
        }
        if (columnTypeArgument is not null)
            arguments.Add(columnTypeArgument);
        var columnType = SyntaxGenerator.Generic(columnTypeName, fieldType.ToSyntax());
        if (_precisionAttributeType is not null && SymbolAttributeHelper.GetAttributesByType(member, _precisionAttributeType).FirstOrDefault() is AttributeData precisionAttribute)
        {
            // 获取字段精度配置
            var precision = SymbolAttributeHelper.GetArgumentValue<int>(precisionAttribute, 0);
            if (precision > 0)
            {
                arguments.Add(SyntaxGenerator.Literal(precision));
                var scale = SymbolAttributeHelper.GetArgumentValue<int>(precisionAttribute, 1);
                if (scale < 0 || scale > precision)
                    scale = 0;
                arguments.Add(SyntaxGenerator.Literal(scale));
            }
            return columnType.New(arguments, defaultValue);
        }
        var length = GetFieldLength(member);
        if (length != 0)
            arguments.Add(SyntaxGenerator.Literal(length));
        return columnType.New(arguments, defaultValue);
    }
    /// <summary>
    /// 检查列参数
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static List<ExpressionSyntax> CheckColumnArguments(string columnName, string memberName)
    {
        if (columnName == memberName)
            return [SyntaxGenerator.Literal(columnName)];
        return [SyntaxGenerator.Literal(columnName), SyntaxGenerator.Literal(memberName)];
    }
    /// <summary>
    /// 获取字段长度配置
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public int GetFieldLength(ISymbol member)
    {
        if (_maxLengthAttributeType is not null && SymbolAttributeHelper.GetAttributesByType(member, _maxLengthAttributeType).FirstOrDefault() is AttributeData maxLengthAttribute)
        {
            var length = SymbolAttributeHelper.GetArgumentValue<int>(maxLengthAttribute, 0);
            if (length == 0)
                return length;
        }
        if (_stringLengthAttributeType is not null && SymbolAttributeHelper.GetAttributesByType(member, _stringLengthAttributeType).FirstOrDefault() is AttributeData stringLengthAttribute)
        {
            var length = SymbolAttributeHelper.GetArgumentValue<int>(stringLengthAttribute, 0);
            if (length == 0)
                return length;
        }
        return 0;
    }
    /// <summary>
    /// 构造列枚举类型表达式
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="member"></param>
    /// <param name="nullable"></param>
    /// <param name="tableConstraints"></param>
    /// <returns></returns>
    public ExpressionSyntax? CheckColumnTypes(string columnName, ISymbol member, bool nullable, IGroupCollection<string, string> tableConstraints)
    {
        var columnTypes = GetColumnTypes(columnName, member, nullable, tableConstraints)
            .ToArray();
        var count = columnTypes.Length;
        if (count == 0)
            return null;
        var columnType = SyntaxFactory.IdentifierName("global::Shadow.DDL.Schemas.ColumnType");
        ExpressionSyntax expression = columnType.Access(columnTypes[0]);
        for (var i = 1; i < count; i++)
        {
            expression = expression.Or(columnType.Access(columnTypes[i]));
        }
        return expression;
    }
    public IEnumerable<string> GetColumnTypes(string columnName, ISymbol member, bool nullable, IGroupCollection<string, string> tableConstraints)
    {
        var generated = GetDatabaseGenerated(member);
        if (generated == "Identity")
        {
            yield return "Identity";
        }            
        else if (generated == "Computed")
        {
            yield return "Computed";
            yield break;
        }
        if (_keyAttributeType is not null && SymbolAttributeHelper.GetAttributesByType(member, _keyAttributeType).Any())
            yield return "Key";
        if(_uniqueAttributeType is not null && SymbolAttributeHelper.GetAttributesByType(member, _uniqueAttributeType).FirstOrDefault() is AttributeData attribute)
        {
            var name = SymbolAttributeHelper.GetArgumentValue<string>(attribute, 0);
            if (string.IsNullOrWhiteSpace(name))
            {
                yield return "Unique";
            }
            else
            {
                tableConstraints.Add(name, columnName);
            }
        }
            
        if (!nullable)
            yield return "NotNull";
    }
    public string GetDatabaseGenerated(ISymbol member)
    {
        if (_databaseGeneratedAttributeType is null)
            return string.Empty;
        var attribute = SymbolAttributeHelper.GetAttributesByType(member, _databaseGeneratedAttributeType)
            .FirstOrDefault();
        if (attribute is null)
            return string.Empty;
        var option = attribute.ConstructorArguments[0];
        var type = option.Type ?? throw new ArgumentException("缺少枚举类型");
        var value = option.Value as IComparable ?? throw new ArgumentException("枚举值无效");
        var field = SymbolReflection.GetEnumField(type, value)
            ?? throw new ArgumentException($"类型: {type.Name},无效的枚举值: {value}");
        return field.Name;
    }
    public InitializerExpressionSyntax? CheckInitializer(ISymbol member)
    {
        var defaultValue = GetDefaultValue(member);
        if (defaultValue is null)
            return null;
        var assign = SyntaxFactory.IdentifierName("DefaultValue").Assign(defaultValue);
        return SyntaxFactory.InitializerExpression(SyntaxKind.ObjectInitializerExpression, SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(assign));
    }
    /// <summary>
    /// 获取默认值表达式
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public ExpressionSyntax? GetDefaultValue(ISymbol member)
    {
        if (_defaultValueAttributeType is null)
            return null;
        if (SymbolAttributeHelper.GetAttributesByType(member, _defaultValueAttributeType)
            .FirstOrDefault()
            ?.ConstructorArguments
           .FirstOrDefault() is TypedConstant argument)
            return argument.ToExpression();
        return null;
    }
    /// <summary>
    /// 检查成员属性类型
    /// </summary>
    /// <param name="member"></param>
    /// <param name="nullable"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public (INamedTypeSymbol memberType, bool nullable) CheckMemberType(ISymbol member, ITypeSymbol type)
    {
        var checkType = SymbolReflection.CheckOriginalSymbol(_compilation, type)
            ?? throw new Exception("propertyType not supported");
        var nullable = checkType.IsNullable();
        if (nullable && checkType.TypeArguments[0] is INamedTypeSymbol namedType)
            checkType = namedType;
        if (_requiredAttributeType is not null)
        {
            if (SymbolAttributeHelper.GetAttributesByType(member, _requiredAttributeType).Any())
                nullable = false;
        }
        return (checkType, nullable);
    }
    /// <summary>
    /// 获取表名默认值
    /// </summary>
    /// <returns></returns>
    private (string schema, string tableName) GetTableDefaultValue()
    {
        string schema = string.Empty;
        string? tableName = null;
        var attribute = GetTableAttribute();
        if (attribute is not null)
        {
            schema = SymbolAttributeHelper.GetArgumentValue<string>(attribute, "Schema") ?? string.Empty;
            tableName = SymbolAttributeHelper.GetArgumentValue<string>(attribute, 0);
        }

        if (string.IsNullOrWhiteSpace(tableName))
            return (schema, _sourseSymbol.Name);
        return (schema, tableName!);
    }
    #region AttributeType
    /// <summary>
    /// 获取Required特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetRequiredAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("System.ComponentModel.DataAnnotations.RequiredAttribute");
    /// <summary>
    /// 获取精度特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetPrecisionAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("Hand.DataAnnotations.PrecisionAttribute");
    /// <summary>
    /// 获取MaxLength特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetMaxLengthAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("System.ComponentModel.DataAnnotations.MaxLengthAttribute");
    /// <summary>
    /// 获取StringLength特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetStringLengthAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("System.ComponentModel.DataAnnotations.StringLengthAttribute");
    /// <summary>
    /// 获取DefaultValue特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetDefaultValueAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("System.ComponentModel.DefaultValueAttribute");
    /// <summary>
    /// 获取Unique特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetUniqueAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("Hand.Sql.UniqueAttribute");
    #endregion
}
