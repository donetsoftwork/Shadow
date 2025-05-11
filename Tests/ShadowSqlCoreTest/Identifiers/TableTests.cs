using ShadowSql;
using ShadowSql.Delete;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Select;
using ShadowSql.Tables;
using ShadowSql.Update;
using TestSupports;

namespace ShadowSqlCoreTest.Identifiers;

public class TableTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void AddColums()
    {
        var name = Column.Use("Name");
        var age = Column.Use("Age");
        var table = new Table("Students")
            .AddColums(name, age);
        Assert.Equal(name, table.GetColumn("Name"));
        Assert.Equal(age, table.GetColumn("Age"));
    }
    [Fact]
    public void DefineColums()
    {
        var table = new Table("Students")
            .DefineColums("Name", "Age");
        Assert.NotNull(table.GetColumn("Name"));
        Assert.NotNull(table.GetColumn("Age"));
    }
    [Fact]
    public void IgnoreInsert()
    {
        var table = new Table("Users")
            .DefineColums("Id","Name", "Age")
            .IgnoreInsert("Id");
        var insert = new SingleInsert(table)
            .InsertSelfColumns();
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Users]([Name],[Age])VALUES(@Name,@Age)", sql);
    }
    [Fact]
    public void IgnoreUpdate()
    {
        var id = Column.Use("Id");
        var name = Column.Use("Name");
        var age = Column.Use("Age");
        var table = new Table("Users")
            .AddColums(id, name, age)
            .IgnoreUpdate(id);
        var update = new TableUpdate(table, id.Equal())
            .SetSelfFields();
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Name]=@Name,[Age]=@Age WHERE [Id]=@Id", sql);
    }
    [Fact]
    public void Copy()
    {
        var table = new Table("Users")
            .DefineColums("Id", "Name", "Age")
            .IgnoreInsert("Id");
        var backup = table.Copy("Users2025");
        var insert = new SingleInsert(backup)
            .InsertSelfColumns();
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Users2025]([Name],[Age])VALUES(@Name,@Age)", sql);
    }
    [Fact]
    public void Delete()
    {
        var id = Column.Use("Id");
        var delete = new TableDelete("Users", id.EqualValue(1));
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Users] WHERE [Id]=1", sql);
    }
    [Fact]
    public void Update()
    {
        var id = Column.Use("Id");
        var age = Column.Use("Age");
        var update = new TableUpdate("Users", id.EqualValue(1))
            .Set(age.AddValue(1));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Age]+=1 WHERE [Id]=1", sql);
    }
    [Fact]
    public void Select()
    {
        var select = new TableSelect("Users")
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users]", sql);
    }
    [Fact]
    public void Define()
    {
        var table = new CommentTable();
        var query = new TableSqlQuery(table)
            .Where(table.Pick.EqualValue(true));
        var select = new TableSelect(query)
            .Select(table.Id, table.Content);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Content] FROM [Comments] WHERE [Pick]=1", sql);
    }
}
