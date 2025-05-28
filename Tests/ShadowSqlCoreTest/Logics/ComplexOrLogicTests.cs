using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSqlCoreTest.Logics;

public class ComplexOrLogicTests
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
    public void Or2()
    {
        ComplexOrLogic or = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        var or2 = new OrLogic(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var logic = or.Or(or2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Grade]>4 OR [Pioneer]=1 OR [Score]>=90 OR [Hobby]>0", sql);
    }
    [Fact]
    public void OrAnd()
    {
        ComplexOrLogic or = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        var and = new AndLogic(_score.GreaterEqualValue(90))
            .AndCore(_hobby.GreaterValue(0));
        var logic = or.Or(and);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Grade]>4 OR [Pioneer]=1 OR ([Score]>=90 AND [Hobby]>0)", sql);
    }
    [Fact]
    public void And()
    {
        ComplexOrLogic or = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        var logic = or.ToAndCore()
            .And(_score.GreaterEqualValue(90))
            .And(_hobby.GreaterValue(0));
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=90 AND [Hobby]>0 AND ([Grade]>4 OR [Pioneer]=1)", sql);
    }
    [Fact]
    public void MergeToOr()
    {
        ComplexOrLogic or = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        OrLogic or2 = new OrLogic(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var logic = or.MergeTo(or2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Grade]>4 OR [Pioneer]=1 OR [Score]>=90 OR [Hobby]>0", sql);
    }
    [Fact]
    public void MergeToComplexOr()
    {
        ComplexOrLogic or = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        ComplexOrLogic or2 = new ComplexOrLogic()
            .OrCore(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var logic = or.MergeTo(or2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=90 OR [Hobby]>0 OR [Grade]>4 OR [Pioneer]=1", sql);
    }
    [Fact]
    public void MergeToAnd()
    {
        ComplexOrLogic or = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        AndLogic and = new AndLogic(_score.GreaterEqualValue(90))
            .AndCore(_hobby.GreaterValue(0));
        var logic = or.MergeToAnd(and);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=90 AND [Hobby]>0 AND ([Grade]>4 OR [Pioneer]=1)", sql);
    }
    [Fact]
    public void MergeToComplexAnd()
    {
        ComplexOrLogic or = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        ComplexAndLogic and = new ComplexAndLogic()
            .AndCore(_score.GreaterEqualValue(90))
            .AndCore(_hobby.GreaterValue(0));
        var logic = or.MergeToAnd(and);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=90 AND [Hobby]>0 AND ([Grade]>4 OR [Pioneer]=1)", sql);
    }
    [Fact]
    public void Not()
    {
        ComplexOrLogic or = new ComplexOrLogic()
            .OrCore(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var logic = or.Not();
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]<90 AND [Hobby]<=0", sql);
    }
}
