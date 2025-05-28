using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSqlCoreTest.Logics;

public class OrLogicTests
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
        OrLogic or = new OrLogic(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var or2 = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));
        var logic = or.Or(or2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=90 OR [Hobby]>0 OR [Grade]>4 OR [Pioneer]=1", sql);
    }
    [Fact]
    public void OrAnd()
    {
        var or = new OrLogic(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var and = new AndLogic(_grade.GreaterValue(4))
            .AndCore(_pioneer.EqualValue(true));
        var logic = or.Or(and);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]>=90 OR [Hobby]>0 OR ([Grade]>4 AND [Pioneer]=1)", sql);
    }
    [Fact]
    public void And()
    {
        var or = new OrLogic(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var logic = or.ToAndCore()
            .And(_pioneer.EqualValue(true));
        var sql = _engine.Sql(logic);
        Assert.Equal("[Pioneer]=1 AND ([Score]>=90 OR [Hobby]>0)", sql);
    }
    [Fact]
    public void MergeToOr()
    {
        var or = new OrLogic(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var or2 = new OrLogic(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));

        var logic = or.MergeTo(or2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Grade]>4 OR [Pioneer]=1 OR [Score]>=90 OR [Hobby]>0", sql);
    }
    [Fact]
    public void MergeToComplexOr()
    {
        var or = new OrLogic(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var or2 = new ComplexOrLogic()
            .OrCore(_grade.GreaterValue(4))
            .OrCore(_pioneer.EqualValue(true));

        var logic = or.MergeTo(or2);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Grade]>4 OR [Pioneer]=1 OR [Score]>=90 OR [Hobby]>0", sql);
    }
    [Fact]
    public void MergeToAnd()
    {
        var or = new OrLogic(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var and = new AndLogic(_pioneer.EqualValue(true));
        var logic = or.MergeToAnd(and);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Pioneer]=1 AND ([Score]>=90 OR [Hobby]>0)", sql);
    }
    [Fact]
    public void MergeToComplexAnd()
    {
        var or = new OrLogic(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var and = new ComplexAndLogic()
            .AndCore(_pioneer.EqualValue(true));
        var logic = or.MergeToAnd(and);
        var sql = _engine.Sql(logic);
        Assert.Equal("[Pioneer]=1 AND ([Score]>=90 OR [Hobby]>0)", sql);
    }
    [Fact]
    public void Not()
    {
        OrLogic or = new OrLogic(_score.GreaterEqualValue(90))
            .OrCore(_hobby.GreaterValue(0));
        var logic = or.Not();
        var sql = _engine.Sql(logic);
        Assert.Equal("[Score]<90 AND [Hobby]<=0", sql);
    }

}
