var builder = DistributedApplication.CreateBuilder(args);

var mssql = builder.AddSqlServer("MsSql")
    .AddDatabase("TestDB");

builder.AddProject<Projects.MsSqlTests>("mssqltests")
    .WithReference(mssql)
    .WaitFor(mssql);

builder.Build().Run();
