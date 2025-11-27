using Dapper.Shadow;
using Microsoft.Data.SqlClient;
using Shadow.DDL;
using Shadow.DDL.Components;
using Shadow.DDL.Schemas;
using ShadowSql.Components;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using System.Collections;

var builder = WebApplication.CreateBuilder(args);

RegistServices(builder.Configuration, builder.Services);
builder.AddServiceDefaults();
var app = builder.Build();

app.MapDefaultEndpoints();
app.MapControllers();

app.MapGet("/", () => "Hello World!");
await DBInit(app.Services);
await app.RunAsync();

static void RegistServices(IConfiguration configuration, IServiceCollection services)
{
    services.AddControllers();
    RegistDB(configuration, services);
    // Register any additional services here
    // Example: services.AddSingleton
}

static void RegistDB(IConfiguration configuration, IServiceCollection services)
{
    var tableName = "Students";
    TableSchema studentTable = new(tableName,
        [
            new ColumnSchema("Id", "int"){ ColumnType = ColumnType.Unique | ColumnType.Key },
            new ColumnSchema("Name", "nvarchar(100)"){ ColumnType = ColumnType.Unique },
            new ColumnSchema("Age", "int"),
            new ColumnSchema("Score", "int")
        ]
    );
    services.AddKeyedSingleton(tableName, studentTable);
    var provider = new DictionaryProvider(new Hashtable())
        .AddComponent<IDefineColumComponent>(new DefineMsSqlColumComponent());
    ISqlEngine engine = new MsSqlEngine(new MsSqlSelectComponent(), new SqlValueComponent("1", "0", "NULL"), provider);
    var connectionString = configuration.GetConnectionString("TestDB");
    Console.WriteLine($"Connection String: {connectionString}");
    var connection = new SqlConnection(connectionString!);
    var executor = new DapperExecutor(engine, connection);
    services.AddSingleton<IExecutor>(executor);
    DBSchema db = new(engine, "TestDB");
    db.AddTable(studentTable);
    services.AddSingleton<IDB>(db);
}
static async Task DBInit(IServiceProvider provider)
{
    var executor = provider.GetRequiredService<IExecutor>();
    var studentTable = provider.GetRequiredKeyedService<TableSchema>("Students");
    try
    {
        await studentTable.ToCreate()
            .ExecuteAsync(executor);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating table: {ex.Message}");
    }
}