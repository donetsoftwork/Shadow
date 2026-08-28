using Hand;
using Hand.Builders;
using Hand.Reflection;
using Hand.Rule;
using Hand.Sources;
using Hand.Symbols;
using Hand.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;

namespace ShadowSql.GenerateTable;

/// <summary>
/// 表结构源对象
/// </summary>
/// <param name="generator"></param>
/// <param name="compilation"></param>
/// <param name="targetInfo"></param>
/// <param name="sourseInfo"></param>
/// <param name="baseSymbol"></param>
/// <param name="columnType"></param>
/// <param name="rule"></param>
/// <param name="notMappedAttributeType"></param>
public class TableSource(SyntaxGenerator generator, Compilation compilation, ComplexTypeInfo targetInfo, ComplexTypeInfo sourseInfo, INamedTypeSymbol baseSymbol, INamedTypeSymbol columnType, IValidation<string> rule, INamedTypeSymbol? notMappedAttributeType)
    : IGeneratorSource
{
    public TableSource(SyntaxGenerator generator, Compilation compilation, ComplexTypeInfo typeSymbol, ComplexTypeInfo sourseInfo, INamedTypeSymbol baseSymbol, INamedTypeSymbol columnType, IValidation<string> rule)
        : this(generator, compilation, typeSymbol, sourseInfo, baseSymbol, columnType, rule, GetNotMappedAttributeType(compilation))
    {
    }
    #region 配置
    /// <summary>
    /// 类型语法
    /// </summary>
    protected readonly SyntaxGenerator _generator = generator;
    protected readonly ComplexTypeInfo _targetInfo = targetInfo;
    protected readonly INamedTypeSymbol _baseSymbol = baseSymbol;
    protected readonly ComplexTypeInfo _sourseInfo = sourseInfo;
    protected readonly INamedTypeSymbol _columnType = columnType;
    protected readonly TypeSyntax _columnSyntax = generator.Display(columnType);
    /// <summary>
    /// 原属性
    /// </summary>
    protected readonly IPropertySymbol[] _properties = [.. SymbolReflection.GetProperties(targetInfo.Symbol)];
    ///// <summary>
    ///// 基类属性
    ///// </summary>
    //protected readonly IPropertySymbol[] _baseProperties = [.. SymbolReflection.GetNotPrivateMembersWithBase(baseTypeSymbol)];
    /// <summary>
    /// 基类成员名
    /// </summary>
    protected readonly FrozenSet<string> _baseMemberNames = SymbolReflection.GetNotPrivateMembersWithBase(baseSymbol)
        .Select(static item => item.Name)
        .ToFrozenSet();
    /// <summary>
    /// 原成员名
    /// </summary>
    protected readonly FrozenSet<string> _memberNames = targetInfo.Symbol.GetMembers()
        .Select(static item => item.Name)
        .ToFrozenSet();
    /// <summary>
    /// 源属性
    /// </summary>
    protected readonly IPropertySymbol[] _sourseProperties = [.. GetSourseProperties(sourseInfo.Symbol, rule, notMappedAttributeType)];
    /// <summary>
    /// 源字段
    /// </summary>
    protected readonly IFieldSymbol[] _sourseFields = [.. GetSourseFields(sourseInfo.Symbol, rule, notMappedAttributeType)];
    private readonly INamedTypeSymbol? _tableAttributeType = GetTableAttributeType(compilation);
    /// <summary>
    /// DatabaseGenerated标记类型
    /// </summary>
    protected readonly INamedTypeSymbol? _databaseGeneratedAttributeType = GetDatabaseGeneratedAttributeType(compilation);
    /// <summary>
    /// 主键标记类型
    /// </summary>
    protected readonly INamedTypeSymbol? _keyAttributeType = GetKeyAttributeType(compilation);
    /// <summary>
    /// 列标记类型
    /// </summary>
    protected readonly INamedTypeSymbol? _columnAttributeType = GetColumnAttributeType(compilation);
    ///// <summary>
    ///// 类型
    ///// </summary>
    //public TypeDeclarationSyntax Type
    //    => _type;
    /// <summary>
    /// 反射信息
    /// </summary>
    public ComplexTypeInfo Symbol
        => _targetInfo;
    /// <summary>
    /// 列类型
    /// </summary>
    public INamedTypeSymbol ColumnType
        => _columnType;
    /// <summary>
    /// 原属性
    /// </summary>
    public IPropertySymbol[] Properties
        => _properties;
    public ISet<string> BaseMemberNames
        => _baseMemberNames;
    public ISet<string> MemberNames
        => _memberNames;
    /// <summary>
    /// 源属性
    /// </summary>
    public IPropertySymbol[] SourseProperties
        => _sourseProperties;
    /// <summary>
    /// 源字段
    /// </summary>
    public IFieldSymbol[] SourseFields
        => _sourseFields;
    /// <inheritdoc />
    public string GenerateFileName
        => $"{_targetInfo.Symbol.ToDisplayString()}.g.cs";
    #endregion
    /// <inheritdoc />
    public SyntaxGenerator Generate()
    {
        //var builder = SyntaxGenerator.Clone(_type);
        var tableName = SyntaxFactory.IdentifierName("tableName");
        //_generator.AddBaseType(CreateBaseType(_baseTypeSymbol));
        var constructor = CreateConstructor(_generator, tableName);
        _generator.AddConstructor(constructor);

        return _generator;
    }
    /// <summary>
    /// 定义构造函数
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    private ConstructorDeclarationSyntax CreateConstructor(IdentifierNameSyntax tableName)
    {
        var defaultValue = GetTableDefaultValue();
        var constructor = _generator.Type.Constructor(Parameter(tableName.Identifier, defaultValue))
            // Table(string tableName)
            .WithBaseInitializer(tableName)
            .Public();
        return constructor;
    }
    /// <summary>
    /// 定义构造函数
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public virtual ConstructorDeclarationSyntax CreateConstructor(SyntaxGenerator builder, IdentifierNameSyntax tableName)
    {
        var constructor = CreateConstructor(tableName)
            .ToBuilder();
        var updateIgnores = new List<string>();
        var defineColumns = CreateColumns(builder, updateIgnores);
        DefineColumn(constructor, defineColumns);
        var addColumns = GetColumnProperties(updateIgnores);
        AddColumn(constructor, addColumns);
        if (updateIgnores.Count > 0)
            UpdateIgnoreColumn(constructor, updateIgnores);
        return constructor.End();
    }
    /// <summary>
    /// 定义新列
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="columns"></param>
    private void DefineColumn(MethodBodyBuilder<ConstructorDeclarationSyntax> builder,  IEnumerable<ISymbol> members)
    {
        var defineColumn = SyntaxFactory.IdentifierName("DefineColumn");
        foreach (var member in members)
        {
            var name = member.Name;
            var columnName = GetColumnName(member);
            var column = SyntaxFactory.IdentifierName(name);
            var arguments = GetColumnArguments(name, columnName);
            // eg: Id = DefineColumn(nameof(Id))
            var expression = column.Assign(defineColumn.Invocation(arguments));
            builder.AddExpression(expression);
        }
    }
    /// <summary>
    /// 获取列参数
    /// </summary>
    /// <param name="memberName"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    private static IEnumerable<ExpressionSyntax> GetColumnArguments(string memberName, string? columnName)
    {
        yield return SyntaxGenerator.Literal(memberName);
        // 列名一般忽略大小写
        if (columnName is not null && !string.Equals(columnName, memberName, StringComparison.OrdinalIgnoreCase))
            yield return SyntaxGenerator.Literal(columnName);
    }
    /// <summary>
    /// 添加已有列
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="columns"></param>
    private static void AddColumn(MethodBodyBuilder<ConstructorDeclarationSyntax> builder, IEnumerable<string> columns)
    {
        var addColumn = SyntaxFactory.IdentifierName("AddColumn");
        foreach (var columnName in columns)
        {
            var column = SyntaxFactory.IdentifierName(columnName);
            // eg: AddColumn(Name)
            var expression = addColumn.Invocation([column]);
            builder.AddExpression(expression);
        }
    }
    /// <summary>
    /// 更新忽略列
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="columns"></param>
    private static void UpdateIgnoreColumn(MethodBodyBuilder<ConstructorDeclarationSyntax> builder, IEnumerable<string> columns)
    {
        var insertIgnore = SyntaxFactory.IdentifierName("AddInsertIgnore");
        var updateIgnore = SyntaxFactory.IdentifierName("AddUpdateIgnore");
        foreach(var columnName in columns)
        {
            var arguments = SyntaxGenerator.ArgumentList(SyntaxFactory.IdentifierName(columnName));
            // eg: AddInsertIgnore(Id)
            builder.AddExpression(SyntaxFactory.InvocationExpression(insertIgnore, arguments));
            // eg: AddUpdateIgnore(Id)
            builder.AddExpression(SyntaxFactory.InvocationExpression(updateIgnore, arguments));
        }
    }
    /// <summary>
    /// 构造参数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static ParameterSyntax Parameter(SyntaxToken name, string defaultValue)
        => SyntaxGenerator.StringType.Parameter(name, SyntaxGenerator.Literal(defaultValue));
    /// <summary>
    /// 获取表名默认值
    /// </summary>
    /// <returns></returns>
    private string GetTableDefaultValue()
    {
        string? tableName = null;
        var attribute = GetTableAttribute();
        if (attribute is not null)
            tableName = SymbolAttributeHelper.GetArgumentValue<string>(attribute, 0);
        if (string.IsNullOrWhiteSpace(tableName))
            return _sourseInfo.Symbol.Name;
        return tableName!;
    }
    private string? GetColumnName(ISymbol symbol)
    {
        if (_columnAttributeType is null)
            return null;
        var attribute = SymbolAttributeHelper.GetAttributesByType(symbol, _columnAttributeType)
            .FirstOrDefault();
        if (attribute is null)
            return null;
        return SymbolAttributeHelper.GetArgumentValue<string>(attribute, 0);
    }
    /// <summary>
    /// 获取Table标记
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="tableAttributeType"></param>
    /// <returns></returns>
    public AttributeData? GetTableAttribute()
    {
        if (_tableAttributeType is null)
            return null;
        return SymbolAttributeHelper.GetAttributesByType(_targetInfo.Symbol, _tableAttributeType).FirstOrDefault() ??
            SymbolAttributeHelper.GetAttributesByType(_sourseInfo.Symbol, _tableAttributeType).FirstOrDefault();
    }
    ///// <summary>
    ///// 获取源类型属性
    ///// </summary>
    ///// <returns></returns>
    //public IEnumerable<IPropertySymbol> GetSourseProperties()
    //{
    //    if (_notMappedAttributeType is null)
    //    {
    //        return SymbolReflection.GetProperties(_typeSymbol)
    //            .Where(property => _rule.Validate(property.Name));
    //    }
    //    // 过滤掉标记为NotMapped的属性
    //    return Filter(SymbolReflection.GetProperties(_typeSymbol), _notMappedAttributeType, _rule);
    //}
    /// <summary>
    /// 过滤掉标记为NotMapped的属性
    /// </summary>
    /// <param name="symbols"></param>
    /// <param name="notMappedType"></param>
    /// <returns></returns>
    public static IEnumerable<TSymbol> Filter<TSymbol>(IEnumerable<TSymbol> symbols, INamedTypeSymbol notMappedType)
        where TSymbol : ISymbol
    {
        foreach (var item in symbols)
        {
            if (SymbolAttributeHelper.GetAttributesByType(item, notMappedType).Any())
                continue;
            yield return item;
        }
    }
    ///// <summary>
    ///// 获取原列属性
    ///// </summary>
    ///// <returns></returns>
    //public IEnumerable<string> GetColumnProperties()
    //{
    //    foreach (var property in _properties)
    //    {
    //        if (CheckColumnType(property.Type, _columnType))
    //            yield return property.Name;
    //    }
    //}
    /// <summary>
    /// 获取原列属性
    /// </summary>
    /// <param name="updateIgnores"></param>
    /// <returns></returns>
    private IEnumerable<string> GetColumnProperties(List<string> updateIgnores)
    {
        foreach (var property in _properties)
        {
            if (CheckColumnType(property.Type, _columnType))
            {
                var name = property.Name;
                if (CheckUpdateIgnores(property))
                    updateIgnores.Add(name);
                yield return name;
            }
        }
    }
    /// <summary>
    /// 是否修改忽略
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    private bool CheckUpdateIgnores(ISymbol property)
    {
        // 如果为null,两者同时为ull
        if (_keyAttributeType is null || _databaseGeneratedAttributeType is null)
            return false;
        return SymbolAttributeHelper.GetAttributesByType(property, _keyAttributeType).Any() ||
            SymbolAttributeHelper.GetAttributesByType(property, _databaseGeneratedAttributeType).Any();
    }
    /// <summary>
    /// 构造列属性
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="updateIgnores"></param>
    /// <returns></returns>
    private IEnumerable<ISymbol> CreateColumns(SyntaxGenerator builder, List<string> updateIgnores)
    {
        //var columnType = _columnType.ToSyntax();

        //// 原属性名
        //var properties0 = _properties.Select(static item => item.Name)
        //    .ToFrozenSet();
        //// 基类属性
        //var baseProperties = _baseProperties.Select(static item => item.Name)
        //    .ToFrozenSet();
        foreach (var property in _sourseProperties)
        {
            var name = property.Name;
            // 新属性与原成员不能重名
            if (_memberNames.Contains(name))
                continue;
            // 生成新属性
            var column = CreateProperty(_columnSyntax, property, _baseMemberNames);
            builder.AddProperty(column);
            if (CheckUpdateIgnores(property))
                updateIgnores.Add(name);
            yield return property;
        }
        foreach (var field in _sourseFields)
        {
            var name = field.Name;
            // 新属性与原属性不能重名
            if (_memberNames.Contains(name))
                continue;
            // 生成新属性
            var column = CreateProperty(_columnSyntax, field, _baseMemberNames);
            builder.AddProperty(column);
            if (CheckUpdateIgnores(field))
                updateIgnores.Add(name);
            yield return field;
        }
    }
    /// <summary>
    /// 构造列属性
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="columnType"></param>
    /// <param name="member"></param>
    /// <param name="baseProperties"></param>
    /// <returns></returns>
    public static PropertyDeclarationSyntax CreateProperty(TypeSyntax columnType, ISymbol member, ISet<string> baseProperties)
    {
        var memberName = member.Name;
        var property = columnType.GetOnlyProperty(memberName)
            .Public();
        // 父类重名需要加new
        if (baseProperties.Contains(memberName))
            return property.New();
        return property;
    }
    /// <summary>
    /// 检查列类型
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="columnType"></param>
    /// <returns></returns>
    public static bool CheckColumnType(ITypeSymbol symbol, INamedTypeSymbol columnType)
    {
        if (SymbolTypeDescriptor.CheckEquals(columnType, symbol))
            return true;
        if (symbol is INamedTypeSymbol namedTypeSymbol)
        {
            var baseType = namedTypeSymbol.BaseType;
            if (baseType is not null && SymbolTypeDescriptor.CheckEquals(columnType, baseType))
                return true;
            return namedTypeSymbol.AllInterfaces.Any(item => SymbolTypeDescriptor.CheckEquals(columnType, item));
        }
        return false;
    }
    #region CreateBaseType
    /// <summary>
    /// 构造基类
    /// </summary>
    /// <returns></returns>
    public virtual BaseTypeSyntax CreateBaseType(INamedTypeSymbol baseTypeSymbol)
        => SyntaxFactory.SimpleBaseType(baseTypeSymbol.ToSyntax());
    ///// <summary>
    ///// 构造基类
    ///// </summary>
    ///// <param name="baseTypeName"></param>
    ///// <returns></returns>
    //public static BaseTypeSyntax CreateBaseType(string baseTypeName)
    //    => SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName(baseTypeName));
    /// <summary>
    /// 基类初始化
    /// </summary>
    /// <param name="constructor"></param>
    /// <param name="arguments"></param>
    /// <returns></returns>
    public static ConstructorDeclarationSyntax BaseInit(ConstructorDeclarationSyntax constructor, params ExpressionSyntax[] arguments)
        => constructor.WithInitializer(SyntaxFactory.ConstructorInitializer(SyntaxKind.BaseConstructorInitializer, SyntaxGenerator.ArgumentList(arguments)));
    #endregion
    /// <summary>
    /// 获取public属性
    /// </summary>
    /// <param name="sourseSymbol"></param>
    /// <param name="rule"></param>
    /// <param name="notMappedType"></param>
    /// <returns></returns>
    public static IEnumerable<IPropertySymbol> GetSourseProperties(INamedTypeSymbol sourseSymbol, IValidation<string> rule, INamedTypeSymbol? notMappedType)
    {
        // 跨类型只获取public属性
        var properties = SymbolReflection.GetPublicPropertiesWithBase(sourseSymbol)
            .Where(p => rule.Validate(p.Name));
        if (notMappedType is null)
            return properties;
        return Filter(properties, notMappedType);
    }
    ///// <summary>
    ///// 获取非私有成员（字段和属性）包括基类的
    ///// </summary>
    ///// <param name="type"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static IEnumerable<ISymbol> GetNotPrivateMembersWithBase(INamedTypeSymbol type)
    //{
    //    return SymbolReflection.GetNotPrivateMembers<IFieldSymbol>(type, SymbolKind.Field)
    //        .Concat<ISymbol>(SymbolReflection.GetNotPrivateMembers<IPropertySymbol>(type, SymbolKind.Property));
    //}
    /// <summary>
    /// 获取public字段
    /// </summary>
    /// <param name="sourseSymbol"></param>
    /// <param name="rule"></param>
    /// <param name="notMappedType"></param>
    /// <returns></returns>
    public static IEnumerable<IFieldSymbol> GetSourseFields(INamedTypeSymbol sourseSymbol, IValidation<string> rule, INamedTypeSymbol? notMappedType)
    {
        // 跨类型只获取public字段
        var fields = SymbolReflection.GetPublicFieldsWithBase(sourseSymbol)
            .Where(p => rule.Validate(p.Name));
        if (notMappedType is null)
            return fields;
        return Filter(fields, notMappedType);
    }
    #region AttributeType
    /// <summary>
    /// 获取DatabaseGenerated特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetTableAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("System.ComponentModel.DataAnnotations.Schema.TableAttribute");
    /// <summary>
    /// 获取Key特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetKeyAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("System.ComponentModel.DataAnnotations.KeyAttribute");
    /// <summary>
    /// 获取DatabaseGenerated特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetDatabaseGeneratedAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute");
    /// <summary>
    /// 获取NotMapped特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetNotMappedAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute");
    /// <summary>
    /// 获取Column特性类型
    /// </summary>
    /// <param name="compilation"></param>
    /// <returns></returns>
    public static INamedTypeSymbol? GetColumnAttributeType(Compilation compilation)
        => compilation.GetTypeByMetadataName("System.ComponentModel.DataAnnotations.Schema.ColumnAttribute");
    #endregion
}
