using Shadow.DDL.Constraints;
using Shadow.DDL.Schemas;

namespace Shadow.DDLTests.Supports;

public class UserRoleTable(string tableName = "UserRoles", string schema = "")
    : TableSchema(tableName, [_id, _userId, _roleId], [_uQ_UserRoles_UserId_RoleId], schema)
{
    private static readonly ColumnSchema _id = new ColumnSchema<int>("Id", ColumnType.Identity);
    private static readonly ColumnSchema _userId = new ColumnSchema<int>("UserId");
    private static readonly ColumnSchema _roleId = new ColumnSchema<int>("RoleId");
    private static readonly IConstraint _uQ_UserRoles_UserId_RoleId = new UniqueConstraint("UQ_UserRoles_UserId_RoleId", _userId, _roleId);
    public ColumnSchema Id => _id;
    public ColumnSchema UserId => _userId;
    public ColumnSchema RoleId => _roleId;
}
