using Dapper.Shadow;
using Shadow.DDL;

namespace Shadow.DDLTests
{
    public class CreateTableTests : ExecuteTestBase, IDisposable
    {
        public CreateTableTests()
            => DropStudentsTable();
        [Fact]
        public void Create()
        {
            var result = new StudentTable()
                .ToCreate()
                .Execute(SqliteExecutor);
            Assert.Equal(0, result);
        }

        void IDisposable.Dispose()
            => DropStudentsTable();
    }
}