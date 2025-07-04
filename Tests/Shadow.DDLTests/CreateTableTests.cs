using Dapper.Shadow;
using Shadow.DDL;
using Shadow.DDL.Schemas;
using ShadowSql;

namespace Shadow.DDLTests
{
    public class CreateTableTests : ExecuteTestBase, IDisposable
    {
        public CreateTableTests()
            => DropStudentsTable();

        [Fact]
        public void Create()
        {
            ColumnSchema id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
            ColumnSchema name = new("Name", "TEXT");
            TableSchema table = new("Students", [id, name]);
            CreateTable create = new(table);
            var sql = SqliteExecutor.Engine.Sql(create);
            Assert.Equal("CREATE TABLE \"Students\"(\"Id\" INTEGER PRIMARY KEY AUTOINCREMENT,\"Name\" TEXT)", sql);
        }

        [Fact]
        public void Execute()
        {
            ColumnSchema id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
            ColumnSchema name = new("Name", "TEXT");
            var result = new TableSchema("Students", [id, name])
                .ToCreate()
                .Execute(SqliteExecutor);
            Assert.Equal(0, result);
        }
        [Fact]
        public void Build()
        {
            var builder = new TableSchemaBuilder("Students", "tenant1")
                .DefineIdentity("Id", "INTEGER")
                .DefinColumns("TEXT", "Name");
            TableSchema table = builder.Build();
            CreateTable create = new(table);
            var sql = SqliteExecutor.Engine.Sql(create);
            Assert.Equal("CREATE TABLE \"tenant1\".\"Students\"(\"Id\" INTEGER AUTOINCREMENT,\"Name\" TEXT)", sql);
        }
        [Fact]
        public void Build2()
        {
            var builder = new TableSchemaBuilder("Students", "tenant1");
            builder.DefineColumn("Id", "INTEGER")
                .ColumnType = ColumnType.Identity | ColumnType.Key;
            builder.DefineColumn("Name", "TEXT");
            TableSchema table = builder.Build();
            CreateTable create = new(table);
            var sql = SqliteExecutor.Engine.Sql(create);
            Assert.Equal("CREATE TABLE \"tenant1\".\"Students\"(\"Id\" INTEGER PRIMARY KEY AUTOINCREMENT,\"Name\" TEXT)", sql);
        }

        void IDisposable.Dispose()
            => DropStudentsTable();
    }
}