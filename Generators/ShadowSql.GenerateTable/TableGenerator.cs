using Hand.Executors;
using Hand.Filters;
using Hand.Generators;
using Microsoft.CodeAnalysis;

namespace ShadowSql.GenerateTable;

/// <summary>
/// 生成表结构
/// </summary>
[Generator(LanguageNames.CSharp)]
public class TableGenerator()
    : ValuesGenerator<TableSource>(
        Attribute,
        new SyntaxFilter(),
        TableTransform.Instance,
        new GeneratorExecutor<TableSource>())
{
    /// <summary>
    /// Attribute标记
    /// </summary>
    public const string Attribute = "Hand.Sql.GenerateTableAttribute`1";
}
