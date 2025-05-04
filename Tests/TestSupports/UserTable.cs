using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace TestSupports;

public class UserTable : Table
{
    public UserTable()
        : base("Users")
    {
        Id = DefineColumn(nameof(Id));
        Name = DefineColumn(nameof(Name));
        Status = DefineColumn(nameof(Status));
    }
    #region Columns
    public IColumn Id { get; private set; }
    new public IColumn Name { get; private set; }
    public IColumn Status { get; private set; }
    #endregion
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
    public IPrefixColumn Id { get; private set; }
    new public IPrefixColumn Name { get; private set; }
    public IPrefixColumn Status { get; private set; }
}