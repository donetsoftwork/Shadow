using ShadowSql.Identifiers;

namespace ShadowSqlTest;

internal class Users : Table
{
    public Users()
        : base(nameof(Users))
    {
        Id = DefineColumn(nameof(Id));
        Status = DefineColumn(nameof(Status));
    }
    #region Columns
    public IColumn Id { get; private set; }
    public IColumn Status { get; private set; }
    #endregion
}
