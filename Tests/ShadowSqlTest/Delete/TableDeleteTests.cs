using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Queries;

namespace ShadowSqlTest.Delete;

public class TableDeleteTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = DB.Use("MyDB");
    //分数
    static readonly IColumn _score = Column.Use("Score");

    [Fact]
    public void Filter()
    {
        var delete = _db.From("Students")
            .ToDelete(_score.LessValue(60));
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }
    [Fact]
    public void FilterSqlQuery()
    {
        var query = SqlQuery.CreateAndQuery()
            .And("Score<60");
        var delete = _db.From("Students")
            .ToDelete(query);
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE Score<60", sql);
    }
    [Fact]
    public void Query()
    {
        var delete = new StudentTable()
            .ToSqlQuery()
            .Where(table => table.Score.LessValue(60))
            .ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }
    [Fact]
    public void TableQuery()
    {
        var delete = _db.From("Students")
            .ToSqlQuery()
            .Where(table => table.Field("Score").LessValue(60))
            .ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }

    public class StudentTable : Table
    {
        public StudentTable()
            :base("Students")
        {
            Name = DefineColumn(nameof(Name));
            Score = DefineColumn(nameof(Score));
        }
        public readonly IColumn Score;
        new public readonly IColumn Name;
    }
}
