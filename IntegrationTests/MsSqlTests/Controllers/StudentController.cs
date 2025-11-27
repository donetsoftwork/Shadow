using Dapper.Shadow;
using Microsoft.AspNetCore.Mvc;
using ShadowApiTests.Models;
using ShadowSql;
using ShadowSql.Expressions;
using ShadowSql.Identifiers;

namespace MsSqlTests.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController(IDB db, IExecutor executor) : ControllerBase
{
    private readonly IDB _db = db;
    private readonly IExecutor _executor = executor;

    [HttpGet]
    public async Task<IEnumerable<Student>> Get(CancellationToken token)
    {
        var list = await _db.From("Students")
            .Take<Student>(10)
            .ToSelect()
            .GetAsync<Student>(_executor, null, token);
        return list;
    }
    [HttpPost(nameof(Add))]
    public async Task<int> Add([FromBody] Student student, CancellationToken token)
    {
        var state = await _db.From("Students")
            .ToInsert<Student>()
            .InsertSelfColumns()
            .ExecuteAsync(_executor, student, token);
        return state;
    }
    [HttpPost(nameof(Delete))]
    public async Task<int> Delete([FromBody] int id, CancellationToken token)
    {
        var state = await _db.From("Students")
            .ToDelete<Student>(s => s.Id == id)
            .ExecuteAsync(_executor, null, token);
        return state;
    }
    [HttpPatch("{id}")]
    public async Task<int> Update(int id, [FromBody] Student student, CancellationToken token)
    {
        var state = await _db.From("Students")
            .ToUpdate<Student>(s => s.Id == student.Id)
            .ExecuteAsync(_executor, student, token);
        return state;
    }

}
