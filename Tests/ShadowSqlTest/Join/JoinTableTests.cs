﻿using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.Join;

public class JoinTableTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void Join()
    {
        var employees = _db.From("Employees")
            .DefineColums("Id", "Name", "DepartmentId");
        var departments = _db.From("Departments")
            .DefineColums("Id", "Name", "Manager", "ParentId", "RootId");

        var joinOn = employees.Join(departments);
        joinOn.And(joinOn.Left.Column("DepartmentId").Equal(joinOn.Source.Column("Id")));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id]", sql);
    }
    [Fact]
    public void SelfJoin()
    {
        var departments = _db.From("Departments");
        var departmentJoinOn = departments.Join(departments)
            .AsLeftJoin();
        var t1 = departmentJoinOn.Left;
        var t2 = departmentJoinOn.Source;
        departmentJoinOn.And(t1.Field("ParentId").Equal(t2.Field("Id")));
        var sql = _engine.Sql(departmentJoinOn.Root);
        Assert.Equal("[Departments] AS t1 LEFT JOIN [Departments] AS t2 ON t1.[ParentId]=t2.[Id]", sql);
    }
    [Fact]
    public void Where()
    {
        var employees = _db.From("Employees");
        var departments = _db.From("Departments");

        var departmentJoinOn = employees.SqlJoin(departments)
            .On(static (t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var joinTable = departmentJoinOn.Root
            .Where("Manager='CEO'");

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE Manager='CEO'", sql);
    }
    [Fact]
    public void Logic()
    {
        var employees = _db.From("Employees");
        var departments = _db.From("Departments");

        var departmentJoinOn = employees.SqlJoin(departments)
            .On(static (t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var manager = Column.Use("Manager");
        var joinTable = departmentJoinOn.Root
            .Where(manager.EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE [Manager]='CEO'", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        var joinOn = SimpleDB.From("Employees").SqlJoin(SimpleDB.From("Departments"))
            .On(static (t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")))
            .WhereLeft(t1 => t1.Field("Age").GreaterValue(30))
            .WhereRight(t2 => t2.Field("Manager").EqualValue("CEO"));
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t1.[Age]>30 AND t2.[Manager]='CEO'", sql);
    }
    [Fact]
    public void SqlQuery2()
    {
        var joinOn = SimpleDB.From("Employees").SqlJoin(SimpleDB.From("Departments"));
        var (t1, t2) = (joinOn.Left, joinOn.Source);
        joinOn.On(t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var joinTable = joinOn.Root
            .Where(t1.Field("Age").GreaterValue(30))
            .Where(t2.Field("Manager").EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t1.[Age]>30 AND t2.[Manager]='CEO'", sql);
    }
    [Fact]
    public void SqlQuery3()
    {
        var e = SimpleDB.From("Employees").As("e");
        var d = SimpleDB.From("Departments").As("d");
        var joinOn = e.SqlJoin(d)
            .On(e.Field("DepartmentId").Equal(d.Field("Id")));
        var joinTable = joinOn.Root
            .Where(e.Field("Age").GreaterValue(30))
            .Where(d.Field("Manager").EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS e INNER JOIN [Departments] AS d ON e.[DepartmentId]=d.[Id] WHERE e.[Age]>30 AND d.[Manager]='CEO'", sql);
    }
    [Fact]
    public void Query()
    {
        var joinOn = SimpleDB.From("Employees").Join(SimpleDB.From("Departments"));
        var (t1, t2) = (joinOn.Left, joinOn.Source);
        joinOn.And(t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var joinTable = joinOn.Root
            .And(t1.Field("Age").GreaterValue(30))
            .And(t2.Field("Manager").EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t1.[Age]>30 AND t2.[Manager]='CEO'", sql);
    }
    [Fact]
    public void Query2()
    {
        var e = SimpleDB.From("Employees").As("e");
        var d = SimpleDB.From("Departments").As("d");
        var joinOn = e.Join(d)
            .And(e.Field("DepartmentId").Equal(d.Field("Id")));
        var joinTable = joinOn.Root
            .And(e.Field("Age").GreaterValue(30))
            .And(d.Field("Manager").EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS e INNER JOIN [Departments] AS d ON e.[DepartmentId]=d.[Id] WHERE e.[Age]>30 AND d.[Manager]='CEO'", sql);
    }
    [Fact]
    public void MultiTable()
    {
        var employees = _db.From("Employees");
        var departments = _db.From("Departments");

        var departmentJoinOn = employees.SqlJoin(departments)
            .On(static (t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var joinTable = departmentJoinOn.Root
            .Where((join, query) => query
                .And(join.From("Employees").Field("Age").EqualValue(40))
                .And(join.From("t2").Field("Manager").EqualValue("CEO"))
            );

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t1.[Age]=40 AND t2.[Manager]='CEO'", sql);
    }



    [Fact]
    public void Table()
    {
        var employees = _db.From("Employees");
        var departments = _db.From("Departments");

        var departmentJoinOn = employees.SqlJoin(departments)
            .On(static (t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var joinTable = departmentJoinOn.Root
            .Where("t1", (employee, query) => query.And(employee.Field("Age").GreaterEqualValue(40)));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t1.[Age]>=40", sql);
    }
    [Fact]
    public void Atomic()
    {
        var employees = _db.From("Employees");
        var departments = _db.From("Departments");

        var departmentJoinOn = employees.SqlJoin(departments)
            .On(static (t1, t2) => t1.Field("DepartmentId").Equal(t2.Field("Id")));
        var joinTable = departmentJoinOn.Root
            .Where(join => join.From("t2").Field("Manager").EqualValue("CEO"));

        var sql = _engine.Sql(joinTable);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] WHERE t2.[Manager]='CEO'", sql);
    }


}
