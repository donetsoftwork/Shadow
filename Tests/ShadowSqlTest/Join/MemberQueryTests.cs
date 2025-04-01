using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Generators;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Queries;


namespace ShadowSqlTest.Join;

public class MemberQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");
    [Fact]
    public void GetColumn()
    {
        var employees = _db.From("Employees")
            .DefineColums("Id", "Name", "DepartmentId");
        var multiTable = new MultiTableSqlQuery(new CharIncrementGenerator('a'), SqlQuery.CreateAndQuery());
        var member = multiTable.CreateMember(employees);
        var column = member.GetPrefixColumn("Id");
        Assert.NotNull(column);
        var column2 = multiTable.GetPrefixColumn("a.Id");
        Assert.StrictEqual(column, column2);
        var column3 = multiTable.GetPrefixColumn("Employees.Id");
        Assert.StrictEqual(column, column3);
    }
    [Fact]
    public void Where()
    {
        var employees = _db.From("Employees")
            .DefineColums("Id", "Name", "DepartmentId");
        var departments = _db.From("Departments")
            .DefineColums("Id", "Name", "Manager", "ParentId", "RootId");

        var multiTable = new MultiTableSqlQuery();
        var employee = multiTable.CreateMember(employees);
        var department = multiTable.CreateMember(departments);
        //employeeQuery.Where(e => e.Field("Age").LessValue(40));
        //departmentQuery.Where(d => d.Column("Manager").EqualValue("CEO"));
        multiTable.Where(employee.Field("Age").LessValue(40));
        multiTable.Where(department.Column("Manager").EqualValue("CEO"));
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Employees] AS t1,[Departments] AS t2 WHERE t1.[Age]<40 AND t2.[Manager]='CEO'", sql);
    }

}
