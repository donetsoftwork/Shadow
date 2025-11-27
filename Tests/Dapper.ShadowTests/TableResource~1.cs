using Dapper;
using Dapper.Shadow;
using Shadow.DDL;
using Shadow.DDL.Schemas;
using ShadowSql;
using ShadowSql.Insert;

namespace Dapper.ShadowTests;

/// <summary>
/// 数据表资源
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="table"></param>
internal class TableResource<TTable>(DapperExecutor executor, TTable table)
    : IAsyncDisposable
    where TTable : TableSchema
{
    public TableResource(DapperExecutor executor)
        : this(executor, Activator.CreateInstance<TTable>())
    {
    }
    protected readonly DapperExecutor _executor = executor;
    protected readonly TTable _table = table;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    public virtual async ValueTask InitAsync()
    {
        try
        {
            await _table.ToCreate()
                .ExecuteAsync(_executor);
        }
        catch { }
    }
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            await _table.ToDrop()
                .ExecuteAsync(_executor);
        }
        catch { }
    }
}

internal class StudentTableResource(DapperExecutor executor, StudentTable table)
    : TableResource<StudentTable>(executor, table)
{
    public StudentTableResource(DapperExecutor executor)
        : this(executor, new StudentTable("Students"))
    {
    }
    public override async ValueTask InitAsync()
    {
        try
        {
            await _table.ToCreate()
                .ExecuteAsync(_executor);
            var insert = new SingleInsert(_table)
                .InsertSelfColumns();
            await insert.ExecuteAsync(_executor, new Student(1, "张三", 10, 1));
            await insert.ExecuteAsync(_executor, new Student(2, "李四", 11, 1));
        }
        catch { }
    }
}

public class Student
{
    /// <summary>
    /// ORM调用
    /// </summary>
    protected Student()
    {
    }
    public Student(int id, string name, int age, int classId)
    {
        Id = id;
        Name = name;
        Age = age;
        ClassId = classId;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int ClassId { get; set; }
}