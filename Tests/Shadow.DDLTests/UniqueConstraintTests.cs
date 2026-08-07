using Shadow.DDL;
using Shadow.DDL.Constraints;
using Shadow.DDL.Schemas;
using ShadowSql;
using ShadowSql.Engines.MsSql;
using ShadowSql.MsSql;

namespace Shadow.DDLTests;

public class UniqueConstraintTests
{
    private readonly MsSqlEngine _engine = MsSqlType.Engine;

    [Fact]
    public void Single()
    {
        var column = new ColumnSchema<string>("Email", 100);
        const string constraintName = "UQ_User_Email";
        var constraint = new UniqueConstraint(constraintName, column);
        var table = new TableSchema("Users", [column], [constraint]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains("CONSTRAINT ", sql);
        Assert.Contains(constraintName, sql);
    }
    [Fact]
    public void Multiple()
    {
        var id = new ColumnSchema<int>("Id");
        var userId = new ColumnSchema<int>("UserId");
        var roleId = new ColumnSchema<int>("RoleId");
        const string constraintName = "UQ_UserRoles_UserId_RoleId";
        var constraint = new UniqueConstraint(constraintName, userId, roleId);
        var table = new TableSchema("UserRoles", [id, userId, roleId], [constraint]);
        var sql = _engine.Sql(table.ToCreate());
        Assert.Contains("CONSTRAINT ", sql);
        Assert.Contains(constraintName, sql);
    }
}
