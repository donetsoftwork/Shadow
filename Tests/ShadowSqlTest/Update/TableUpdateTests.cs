﻿using ShadowSql;
using ShadowSql.Assigns;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using static ShadowSqlTest.Delete.TableDeleteTests;

namespace ShadowSqlTest.Update;

public class TableUpdateTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = DB.Use("MyDB");
    //分数
    static readonly IColumn _score = Column.Use("Score");

    [Fact]
    public void Filter()
    {
        var update = _db.From("Students")
            .ToUpdate(_score.LessValue(60))
            .Set(_score.EqualToValue(60));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Students] SET [Score]=60 WHERE [Score]<60", sql);
    }
    [Fact]
    public void FilterSqlQuery()
    {
        var query = SqlQuery.CreateAndQuery()
            .And("Score<60");
        var update = _db.From("Students")
            .ToUpdate(query)
            .SetValue("Score", 60);
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Students] SET [Score]=60 WHERE Score<60", sql);
    }
    [Fact]
    public void Query()
    {
        var update = new StudentTable()
            .ToUpdate(table => table.Score.LessValue(60))
            .SetValue("Score", AssignSymbol.Add, 10);
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Students] SET [Score]+=10 WHERE [Score]<60", sql);
    }

    [Fact]
    public void TableQuery()
    {
        var update = new StudentTable()
            .ToSqlQuery()
            .Where(table => table.Score.LessValue(60))
            .ToUpdate()
            .Set(table => table.Score.AddValue(10));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Students] SET [Score]+=10 WHERE [Score]<60", sql);
    }
}
