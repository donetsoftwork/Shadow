using ShadowSql.Logics;

namespace ShadowSqlCoreTest.Logics;

public class NotRegexTests
{
    [Fact]
    public void Match()
    {
        var regex = NotStatementLogic.NotRegex();
        var match = regex.Match("NOT age > 25");
        Assert.True(match.Success);
    }

    [Fact]
    public void MatchSpace()
    {
        var regex = NotStatementLogic.NotRegex();
        var match = regex.Match(" \t Not\tage > 25");
        Assert.True(match.Success);
    }
    [Fact]
    public void MatchLow()
    {
        var regex = NotStatementLogic.NotRegex();
        var match = regex.Match(" \t not age > 25");
        Assert.True(match.Success);
    }

    [Fact]
    public void MatchFalse()
    {
        var regex = NotStatementLogic.NotRegex();
        var match = regex.Match(" notage > 25");
        Assert.False(match.Success);
    }
    [Fact]
    public void CheckNotLength()
    {
        var statement = " NOT age > 25";
        var len = NotStatementLogic.CheckNotLength(statement);
        Assert.Equal(5, len);
        var logic = statement.Substring(len);
        Assert.Equal("age > 25", logic);
    }
}
