using System.Collections.Generic;

namespace ShadowSql.Compares;

/// <summary>
/// 比较逻辑运算符管理
/// </summary>
public class CompareManager
{
    /// <summary>
    /// 比较逻辑运算符管理
    /// </summary>
    /// <param name="equal"></param>
    /// <param name="notEqual"></param>
    /// <param name="greater"></param>
    /// <param name="lessEqual"></param>
    /// <param name="less"></param>
    /// <param name="greaterEqual"></param>
    /// <param name="like"></param>
    /// <param name="notlike"></param>
    /// <param name="in"></param>
    /// <param name="notIn"></param>
    /// <param name="isNull"></param>
    /// <param name="notNull"></param>
    /// <param name="between"></param>
    /// <param name="notBetween"></param>
    /// <param name="exists"></param>
    /// <param name="notExists"></param>
    public CompareManager(
    CompareSymbol equal, CompareSymbol notEqual
    , CompareSymbol greater, CompareSymbol lessEqual
    , CompareSymbol less, CompareSymbol greaterEqual
    , CompareSymbol like, CompareSymbol notlike
    , CompareSymbol @in, CompareSymbol notIn
    , CompareSymbol isNull, CompareSymbol notNull
    , CompareSymbol between, CompareSymbol notBetween
    , CompareSymbol exists, CompareSymbol notExists
    )
    {
        _equal = Add(equal);
        _notEqual = Add(notEqual);
        _greater = Add(greater);
        _lessEqual = Add(lessEqual);
        _less = Add(less);
        _greaterEqual = Add(greaterEqual);
        _like = Add(like);
        _notlike = Add(notlike);
        //特殊运算符不加入字典,避免被滥用
        _isNull = isNull;
        _notNull = notNull;
        _notIn = notIn;
        _in = @in;
        _between = between;
        _notBetween = notBetween;
        _exists = exists;
        _notExists = notExists;
    }
    private readonly Dictionary<string, CompareSymbol> _operations = [];

    private readonly CompareSymbol _equal;
    private readonly CompareSymbol _notEqual;
    private readonly CompareSymbol _greater;
    private readonly CompareSymbol _lessEqual;
    private readonly CompareSymbol _less;
    private readonly CompareSymbol _greaterEqual;
    private readonly CompareSymbol _notIn;
    private readonly CompareSymbol _in;
    private readonly CompareSymbol _isNull;
    private readonly CompareSymbol _notNull;
    private readonly CompareSymbol _like;
    private readonly CompareSymbol _notlike;
    private readonly CompareSymbol _between;
    private readonly CompareSymbol _notBetween;
    private readonly CompareSymbol _exists;
    private readonly CompareSymbol _notExists;

    /// <summary>
    ///  相等
    /// </summary>
    public CompareSymbol Equal
        => _equal;
    /// <summary>
    /// 不等
    /// </summary>
    public CompareSymbol NotEqual
        => _notEqual;
    /// <summary>
    /// 大于
    /// </summary>
    public CompareSymbol Greater
        => _greater;
    /// <summary>
    /// 小于等于
    /// </summary>
    public CompareSymbol LessEqual
        => _lessEqual;
    /// <summary>
    /// 小于
    /// </summary>
    public CompareSymbol Less
        => _less;
    /// <summary>
    /// 大于等于
    /// </summary>
    public CompareSymbol GreaterEqual
        => _greaterEqual;
    /// <summary>
    /// IN
    /// </summary>
    public CompareSymbol In
        => _in;
    /// <summary>
    /// NOT IN
    /// </summary>
    public CompareSymbol NotIn
        => _notIn;
    /// <summary>
    /// IS NULL
    /// </summary>
    public CompareSymbol IsNull 
        => _isNull;
    /// <summary>
    /// IS NOT NULL
    /// </summary>
    public CompareSymbol NotNull 
        => _notNull;
    /// <summary>
    /// LIKE
    /// </summary>
    public CompareSymbol Like
        => _like;
    /// <summary>
    /// NOT LIKE
    /// </summary>
    public CompareSymbol NotLike
        => _notlike;
    /// <summary>
    /// BETWEEN
    /// </summary>
    public CompareSymbol Between
        => _between;
    /// <summary>
    /// NOT BETWEEN
    /// </summary>
    public CompareSymbol NotBetween
        => _notBetween;
    /// <summary>
    /// EXISTS
    /// </summary>
    public CompareSymbol Exists
        => _exists;
    /// <summary>
    /// NOT EXISTS
    /// </summary>
    public CompareSymbol NotExists
        => _notExists;

    private CompareSymbol Add(CompareSymbol compare)
    {
        TryAdd(compare.Operation, compare);
        return compare;
    }
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="compare"></param>
    private void TryAdd(string operation, CompareSymbol compare)
    {
        if(_operations.ContainsKey(operation))
            return;
        _operations.Add(operation, compare);
    }
    /// <summary>
    /// 添加别名
    /// </summary>
    /// <param name="compare"></param>
    /// <param name="aliases"></param>
    public void AddAliases(CompareSymbol compare, params IEnumerable<string> aliases)
    {
        foreach (var alias in aliases)
        {
            TryAdd(alias, compare);
        }
    }
    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public CompareSymbol Get(string operation)
    {
        return _operations[operation];
    }
    /// <summary>
    /// 构造操作符
    /// </summary>
    /// <returns></returns>
    public static CompareManager Create()
    {
        (CompareSymbol equal, CompareSymbol notEqual) = CompareSymbol.CreatePair("=", "<>");        
        (CompareSymbol greater, CompareSymbol lessEqual) = CompareSymbol.CreatePair(">", "<=");
        (CompareSymbol less, CompareSymbol greaterEqual) = CompareSymbol.CreatePair("<", ">=");
        (CompareSymbol like, CompareSymbol notlike) = CompareSymbol.CreatePair(" LIKE ", " NOT LIKE ");
        (CompareSymbol @in, CompareSymbol notIn) = CompareSymbol.CreatePair(" IN ", " NOT IN ");
        (CompareSymbol isNull, CompareSymbol notNull) = CompareSymbol.CreatePair(" IS NULL", " IS NOT NULL");
        (CompareSymbol between, CompareSymbol notBetween) = CompareSymbol.CreatePair(" BETWEEN ", " NOT BETWEEN ");
        (CompareSymbol exists, CompareSymbol notExists) = CompareSymbol.CreatePair("EXISTS", "NOT EXISTS");

        var manager = new CompareManager(
            equal, notEqual
            , greater, lessEqual
            , less, greaterEqual
            , like, notlike
            , @in, notIn
            , isNull, notNull
            , between, notBetween
            , exists, notExists
            );
        //设置特殊的反转规则
        equal._reverse = equal;
        notEqual._reverse = notEqual;
        isNull._reverse = isNull;
        notNull._reverse = notNull;
        //设置别名
        manager.AddAliases(equal, "<=>");
        manager.AddAliases(notEqual, "!=", "~=", "^=");
        manager.AddAliases(greaterEqual, "!<");
        manager.AddAliases(lessEqual, "!>");
        //特殊运算符不加入字典,不需要别名
        //manager.AddAliases(@in, "IN", "in");
        //manager.AddAliases(notIn, "NOT IN", "not in", "NOTIN", "notin");
        //manager.AddAliases(isNull, "IS NULL", "is null", "ISNULL", "isnull", "NULL", "null");
        //manager.AddAliases(notNull, "IS NOT NULL", "is not null", "ISNOTNULL", "isnotnull"
        //    , "NOT NULL", "not null", "NOTNULL", "notnull");
        return manager;
    }
}
