using Shadow.DDL.Schemas;
using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace Shadow.DDLTests.Supports;

public class UserTable(string tableName = "Users", string schema = "")
    : TableSchema(tableName, [Defines.Id, Defines.Name, Defines.Status], schema)
{
    #region Columns
    public readonly ColumnSchema Id = Defines.Id;
    new public readonly ColumnSchema Name  = Defines.Name;
    public readonly ColumnSchema Status = Defines.Status;
    #endregion

    class Defines
    {
        public static readonly ColumnSchema Id = new("Id") { ColumnType = ColumnType.Key };
        public static readonly ColumnSchema Name = new("Name");
        public static readonly ColumnSchema Status = new("Status");
    }
}

public class UserAliasTable : TableAlias<UserTable>
{
    public UserAliasTable(string tableAlias)
        : this(new UserTable(), tableAlias)
    {
    }
    private UserAliasTable(UserTable table, string tableAlias)
        : base(table, tableAlias)
    {
        Id = AddColumn(table.Id);
        Name = AddColumn(table.Name);
        Status = AddColumn(table.Status);
    }
    public IPrefixField Id { get; private set; }
    new public IPrefixField Name { get; private set; }
    public IPrefixField Status { get; private set; }
}