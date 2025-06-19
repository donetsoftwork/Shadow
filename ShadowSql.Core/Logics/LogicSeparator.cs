using ShadowSql.Engines;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.Logics;

/// <summary>
/// 逻辑连接(and/or)
/// </summary>
/// <param name="separator">and/or</param>
public abstract class LogicSeparator(string separator)
    : Identifier(separator)
{
    /// <summary>
    /// 分隔符
    /// </summary>
    public abstract string Separator { get; }
    /// <summary>
    /// 反转
    /// </summary>
    /// <returns></returns>
    public abstract LogicSeparator Reverse();
    /// <summary>
    /// AndSeparator
    /// </summary>
    public const string AndSeparator = "AND";
    /// <summary>
    /// OrSeparator
    /// </summary>
    public const string OrSeparator = "OR";
    /// <summary>
    /// And
    /// </summary>
    public static readonly LogicSeparator And = new AndLogicSeparator();
    /// <summary>
    /// Or
    /// </summary>
    public static readonly LogicSeparator Or = new OrLogicSeparator();

    class AndLogicSeparator()
        : LogicSeparator(AndSeparator)
    {
        const string separator = " AND ";
        /// <inheritdoc/>
        internal override void Write(ISqlEngine engine, StringBuilder sql)
        {
            sql.Append(separator);
        }
        /// <inheritdoc/>
        public override LogicSeparator Reverse()
            => Or;
        /// <inheritdoc/>
        public override string Separator
            => separator;
    }

    class OrLogicSeparator()
        : LogicSeparator(OrSeparator)
    {
        const string separator = " OR ";
        /// <inheritdoc/>
        internal override void Write(ISqlEngine engine, StringBuilder sql)
        {
            sql.Append(separator);
        }
        /// <inheritdoc/>
        public override LogicSeparator Reverse()
            => And;
        /// <inheritdoc/>
        public override string Separator
            => separator;
    }
}
