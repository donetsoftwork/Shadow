using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;

namespace ShadowSqlTest.Join;

public class MultiTableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void CreateMember()
    {
        var multiTable = new MultiTableQuery();
        multiTable.CreateMemberQuery(_db.From("Employees"));
        multiTable.CreateMemberQuery(_db.From("Departments"));
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Employees] AS t1,[Departments] AS t2", sql);
    }
    [Fact]
    public void SelfJoin()
    {
        var multiTable = new MultiTableQuery();
        multiTable.CreateMemberQuery(_db.From("Departments"));
        multiTable.CreateMemberQuery(_db.From("Departments"));

        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Departments] AS t1,[Departments] AS t2", sql);
    }
    [Fact]
    public void Where()
    {
        var multiTable = new MultiTableQuery();
        multiTable.CreateMemberQuery(_db.From("Departments"));
        multiTable.CreateMemberQuery(_db.From("Departments"));
        multiTable.Where("t1.DepartmentId=t2.Id");
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Departments] AS t1,[Departments] AS t2 WHERE t1.DepartmentId=t2.Id", sql);
    }
}
