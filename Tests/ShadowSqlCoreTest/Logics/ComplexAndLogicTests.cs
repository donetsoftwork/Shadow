using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSqlCoreTest.Logics;

public class ComplexAndLogicTests
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
    public void Add()
    {
        var orComplex = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        var complex = new ComplexAndLogic()
            .AddOtherCore(orComplex)
            .AndCore(_score.GreaterEqualValue(60));
        var sql = _engine.Sql(complex);
        Assert.Equal("[Score]>=60 AND ([Grade]>4 OR [Pioneer]=1)", sql);
    }

    [Fact]
    public void And2()
    {
        var and = new AndLogic(_score.GreaterEqualValue(60))
              .AndCore(_score.LessValue(70));
        var complex = new ComplexAndLogic()
            .AndCore(and);
        var sql = _engine.Sql(complex);
        Assert.Equal("[Score]>=60 AND [Score]<70", sql);
    }
    [Fact]
    public void AndOr()
    {
        var or = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));

        var complex = new ComplexAndLogic()
            .AndCore(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70))
            .And(or);
        var sql = _engine.Sql(complex);
        Assert.Equal("[Score]>=60 AND [Score]<70 AND ([Grade]>4 OR [Pioneer]=1)", sql);
    }
    [Fact]
    public void Or()
    {
        var or = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));

        var complex = new ComplexAndLogic()
            .AndCore(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70))
            .AndCore(or);
        var logic = complex.ToOrCore()
            .Or(_hobby.GreaterValue(0));
        var sql = _engine.Sql(logic);
        Assert.Equal("[Hobby]>0 OR ([Score]>=60 AND [Score]<70 AND ([Grade]>4 OR [Pioneer]=1))", sql);
    }
    [Fact]
    public void MergeToAnd()
    {
        AndLogic and = new AndLogic(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var or = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));

        ComplexAndLogic complex = new ComplexAndLogic()
            .AndCore(_hobby.GreaterValue(0))
            .AndCore(or)
            .MergeTo(and);
        var sql = _engine.Sql(complex);
        Assert.Equal("[Hobby]>0 AND [Score]>=60 AND [Score]<70 AND ([Grade]>4 OR [Pioneer]=1)", sql);
    }
    [Fact]
    public void MergeToComplexAnd()
    {
        var or = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));

        ComplexAndLogic complex = new ComplexAndLogic()
            .AndCore(_hobby.GreaterValue(0))
            .AndCore(or);
        ComplexAndLogic complex2 = new ComplexAndLogic()
            .AndCore(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var logic = complex.MergeTo(complex2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=60 AND [Score]<70 AND [Hobby]>0 AND ([Grade]>4 OR [Pioneer]=1)", sql);
    }
    [Fact]
    public void MergeToOr()
    {
        OrLogic or = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        ComplexAndLogic complex = new ComplexAndLogic()
            .AndCore(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var logic = complex.MergeToOr(or);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Grade]>4 OR [Pioneer]=1 OR ([Score]>=60 AND [Score]<70)", sql);
    }
    [Fact]
    public void MergeToComplexOr()
    {
        ComplexOrLogic or = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        ComplexAndLogic complex = new ComplexAndLogic()
            .AndCore(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70));
        var logic = complex.MergeToOr(or);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Grade]>4 OR [Pioneer]=1 OR ([Score]>=60 AND [Score]<70)", sql);
    }
    [Fact]
    public void Not()
    {
        OrLogic or = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_hobby.GreaterValue(0));
        ComplexAndLogic complex = new ComplexAndLogic()
            .AndCore(_score.GreaterEqualValue(60))
            .AndCore(_score.LessValue(70))
            .AndCore(or);
        var logic = complex.Not();
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]<60 OR [Score]>=70 OR ([Grade]<=4 AND [Hobby]<=0)", sql);
    }
}
