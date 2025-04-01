using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;

namespace ShadowSqlTest.Join;

public class JoinOnQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void On()
    {
        var join = CreateJoin();
        join.On("t1.DepartmentId=t2.Id");
        var sql = _engine.Sql(join.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.DepartmentId=t2.Id", sql);
    }

    [Fact]
    public void Source()
    {
        var join = CreateJoin()
            .AsLeftJoin()
            .On((t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")))
            //多表同名字段,优先本次被联接的表
            .On(view => view.Column("Id").IsNull());
        var sql = _engine.Sql(join.Root);
        Assert.Equal("[Employees] AS t1 LEFT JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] AND t2.[Id] IS NULL", sql);
    }

    [Fact]
    public void TableLogicInfo()
    {
        var join = CreateJoin();
        var left = join.Left;
        var right = join.Source;

        var departmentId = left.Field("DepartmentId");
        var id = right.Field("Id");
        join.On(departmentId.Equal(id));
        var sql = _engine.Sql(join.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id]", sql);
    }



    private static JoinOnSqlQuery<Table, Table> CreateJoin()
    {
        var employees = _db.From("Employees")
            .DefineColums("Id", "Name", "DepartmentId");
        var departments = _db.From("Departments")
            .DefineColums("Id", "Name", "Manager", "ParentId", "RootId");

        return employees.SqlJoin(departments);
    }
}
