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
        var employees = _db.From("Employees")
            .DefineColums("Id", "Name", "DepartmentId");
        var departments = _db.From("Departments")
            .DefineColums("Id", "Name", "Manager", "ParentId", "RootId");

        var joinTable = new JoinTableQuery();
        var employeeQuery = joinTable.CreateMemberQuery(employees);
        var departmentJoin = employeeQuery.Join(departments)
            .OnColumn("DepartmentId", "Id");
        var departmentQuery = departmentJoin.ToRightQuery();
        employeeQuery.Where(e => e.Field("Age").GreaterValue(40));
        departmentQuery.Where(d => d.Column("Manager").NotEqualValue("CEO"));
        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t1.[Age]>40 AND t2.[Manager]<>'CEO'", sql);
    }
}
