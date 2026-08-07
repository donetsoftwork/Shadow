using Dapper.Shadow;
using Shadow.DDL;
using Shadow.DDL.Schemas;
using ShadowSql;
using ShadowSql.Sqlite;

namespace Shadow.DDLTests
{
    public class CreateTableTests : ExecuteTestBase, IDisposable
    {
        [Fact]
        public void Create()
        {
            ColumnSchema id = new("Id", SqliteSqlType.Integer,  ColumnType.Identity | ColumnType.Key);
            ColumnSchema name = new("Name", SqliteSqlType.Text);
            TableSchema table = new("Students", [id, name]);
            CreateTable create = new(table);
            var sql = SqliteExecutor.Engine.Sql(create);
            Assert.Equal("CREATE TABLE \"Students\"(\"Id\" INTEGER PRIMARY KEY AUTOINCREMENT,\"Name\" TEXT)", sql);
        }

        [Fact]
        public async Task Execute()
        {
            await DropStudentsTable();
            ColumnSchema id = new("Id", SqliteSqlType.Integer, ColumnType.Identity | ColumnType.Key);
            ColumnSchema name = new("Name", SqliteSqlType.Text);
            var result = await new TableSchema("Students", [id, name])
                .ToCreate()
                .ExecuteAsync(SqliteExecutor);
            Assert.Equal(0, result);
            await DropStudentsTable();
        }
        [Fact]
        public void Build()
        {
            var builder = new TableSchemaBuilder("Students", "tenant1")
                .DefineIdentity("Id", SqliteSqlType.Integer)
                .DefinColumns(SqliteSqlType.Text, "Name");
            TableSchema table = builder.Build();
            CreateTable create = new(table);
            var sql = SqliteExecutor.Engine.Sql(create);
            Assert.Equal("CREATE TABLE \"tenant1\".\"Students\"(\"Id\" INTEGER AUTOINCREMENT,\"Name\" TEXT)", sql);
        }
        [Fact]
        public void Build2()
        {
            var builder = new TableSchemaBuilder("Students", "tenant1");
            builder.DefineColumn("Id", SqliteSqlType.Integer, ColumnType.Identity | ColumnType.Key);
            builder.DefineColumn("Name", SqliteSqlType.Text);
            TableSchema table = builder.Build();
            CreateTable create = new(table);
            var sql = SqliteExecutor.Engine.Sql(create);
            Assert.Equal("CREATE TABLE \"tenant1\".\"Students\"(\"Id\" INTEGER PRIMARY KEY AUTOINCREMENT,\"Name\" TEXT)", sql);
        }

        void IDisposable.Dispose() { }
    }
}