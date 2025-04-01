using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSqlTest.Operator;

/// <summary>
/// 运算符重载测试
/// </summary>
public class AtomicLogicTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    //分数
    static readonly IColumn _score = Column.Use("Score");
    //年级
    static readonly IColumn _grade = Column.Use("Grade");
    //少先队员
    static readonly IColumn _pioneer = Column.Use("Pioneer");
    //爱好
    static readonly IColumn _hobby = Column.Use("Hobby");

    [Fact]
    public void AndTest()
    {
        // Arrange
        AtomicLogic source = _score.GreaterEqualValue(60);
        AtomicLogic logic1 = _grade.Greater("LowGrade");
        EmptyLogic logic2 = EmptyLogic.Instance;
        var logic3 = _pioneer.EqualValue(true) & _hobby.GreaterValue(0);
        var logic4 = _pioneer.EqualValue(true) | _hobby.GreaterValue(0);
        // Act
        var and1 = source & logic1;
        var and2 = source & logic2;
        var and3 = source & logic3;
        var and4 = source & logic4;

        var sql1 = _engine.Sql(and1);
        var sql2 = _engine.Sql(and2);
        var sql3 = _engine.Sql(and3);
        var sql4 = _engine.Sql(and4);

        // Assert
        Assert.Equal("[Score]>=60 AND [Grade]>@LowGrade", sql1);
        Assert.Equal("[Score]>=60", sql2);
        Assert.Equal("[Pioneer]=1 AND [Hobby]>0 AND [Score]>=60", sql3);
        Assert.Equal("[Score]>=60 AND ([Pioneer]=1 OR [Hobby]>0)", sql4);
    }
    [Fact]
    public void OrTest()
    {
        // Arrange
        AtomicLogic source = _score.GreaterEqualValue(60);
        AtomicLogic logic1 = _grade.Greater("LowGrade");
        EmptyLogic logic2 = EmptyLogic.Instance;
        AndLogic logic3 = _pioneer.EqualValue(true) & _hobby.GreaterValue(0);
        OrLogic logic4 = _pioneer.EqualValue(true) | _hobby.GreaterValue(0);
        // Act
        var or1 = source | logic1;
        var or2 = source | logic2;
        var or3 = source | logic3;
        var or4 = source | logic4;

        var sql1 = _engine.Sql(or1);
        var sql2 = _engine.Sql(or2);
        var sql3 = _engine.Sql(or3);
        var sql4 = _engine.Sql(or4);

        // Assert
        Assert.Equal("[Score]>=60 OR [Grade]>@LowGrade", sql1);
        Assert.Equal("[Score]>=60", sql2);
        Assert.Equal("[Score]>=60 OR ([Pioneer]=1 AND [Hobby]>0)", sql3);
        Assert.Equal("[Pioneer]=1 OR [Hobby]>0 OR [Score]>=60", sql4);
    }
    [Fact]
    public void NotTest()
    {
        // Arrange
        AtomicLogic source = _score.GreaterEqualValue(60);
        // Act
        var not = source.Not();
        var sql = _engine.Sql(not);
        // Assert
        Assert.Equal("[Score]<60", sql);
    }
}