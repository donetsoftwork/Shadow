using ShadowSql.Identifiers;

namespace ShadowSqlCoreTest;

internal class UserTable : Table
{
    public UserTable()
        : base("Users")
    {
        Id = DefineColumn(nameof(Id));
        Status = DefineColumn(nameof(Status));
    }
    #region Columns
    public IColumn Id { get; private set; }
    public IColumn Status { get; private set; }
    #endregion
}
