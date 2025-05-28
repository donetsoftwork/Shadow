using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSqlCoreTest.Logics;

public class AndLogicTests
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
    public void AndLogic()
    {
        var logic = _score.GreaterEqualValue(60);
        var and = new AndLogic(_score.GreaterEqualValue(60));
        var sql = _engine.Sql(logic);
        var sql2 = _engine.Sql(and);
        Assert.Equal(sql, sql2);
    }
    [Fact]
    public void AndAtomic()
    {
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var sql = _engine.Sql(and);
        Assert.Equal("[Score]>=60 AND [Score]<70", sql);
    }

    [Fact]
    public void And2()
    {
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var and2 = new AndLogic(_grade.GreaterValue(1))
            .AndCore(_grade.LessValue(6));
        var logic =  and.AndCore(and2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=60 AND [Score]<70 AND [Grade]>1 AND [Grade]<6", sql);
    }
    [Fact]
    public void AndOr()
    {
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var or = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        var logic = and.AndCore(or);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=60 AND [Score]<70 AND ([Grade]>4 OR [Pioneer]=1)", sql);
    }
    [Fact]
    public void AndOr2()
    {
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var or = new OrLogic(_grade.GreaterValue(4));
        var logic = and.AndCore(or);
        var or2 = new OrLogic(_pioneer.EqualValue(true));
        logic = logic.And(or2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=60 AND [Score]<70 AND [Grade]>4 AND [Pioneer]=1", sql);
    }
    [Fact]
    public void AndOr3()
    {
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var or = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        var logic = and.AndCore(or);
        var or2 = new OrLogic(_hobby.EqualValue(2))
            .OrCore(_hobby.EqualValue(3));
        logic = logic.And(or2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=60 AND [Score]<70 AND ([Grade]>4 OR [Pioneer]=1) AND ([Hobby]=2 OR [Hobby]=3)", sql);
    }
    [Fact]
    public void Or()
    {
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var or = and.ToOrCore()
            .Or(_pioneer.EqualValue(true));
        var sql = _engine.Sql(or);
        Assert.Equal("[Pioneer]=1 OR ([Score]>=60 AND [Score]<70)", sql);
    }

    [Fact]
    public void MergeToAnd()
    {
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var and2 = new AndLogic(_grade.GreaterValue(1))
            .AndCore(_grade.LessValue(6));
        var logic = and.MergeTo(and2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Grade]>1 AND [Grade]<6 AND [Score]>=60 AND [Score]<70", sql);
    }
    [Fact]
    public void MergeToComplexAnd()
    {
        ComplexAndLogic complex = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true))
            .MergeToAnd(new ComplexAndLogic());
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        complex = and.MergeTo(complex);
        var sql = _engine.Sql(complex);
        Assert.Equal("[Score]>=60 AND [Score]<70 AND ([Grade]>4 OR [Pioneer]=1)", sql);
    }
    [Fact]
    public void MergeToOr()
    {
        var or = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var logic = and.MergeToOr(or);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Grade]>4 OR [Pioneer]=1 OR ([Score]>=60 AND [Score]<70)", sql);
    }
    [Fact]
    public void MergeToComplexOr()
    {
        ComplexOrLogic complex = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var logic = and.MergeToOr(complex);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Grade]>4 OR [Pioneer]=1 OR ([Score]>=60 AND [Score]<70)", sql);
    }
    [Fact]
    public void Not()
    {
        var and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var logic = and.Not();
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]<60 OR [Score]>=70", sql);
    }
}
