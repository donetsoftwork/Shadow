using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;

namespace ShadowSqlTest.Join;

public class JoinMemberQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    
    [Fact]
    public void Where()
    {
        var e = _db.From("Employees")
            .DefineColums("Id", "Name", "DepartmentId")
            .As("e");
        var d = _db.From("Departments")
            .DefineColums("Id", "Name", "Manager", "ParentId", "RootId")
            .As("d");
  
        var departmentJoin = e.Join(d)
            .OnColumn("DepartmentId", "Id");
        var joinTableQuery = departmentJoin.Root
            .Where(e.Field("Age").GreaterValue(40))
            .Where(d.Column("Manager").NotEqualValue("CEO"));
        var sql = _engine.Sql(joinTableQuery);
        Assert.Equal("[Employees] AS e INNER JOIN [Departments] AS d ON e.[DepartmentId]=d.[Id] WHERE e.[Age]>40 AND d.[Manager]<>'CEO'", sql);
    }
}
