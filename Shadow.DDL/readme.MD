# ShadowSql.DDL
>* ShadowSql的DDL组件
>* 主要用来支持CreateTable
>* 搭配Dapper.Shadow.Core或Dapper.Shadow可以直接操作数据库的表

以下以Sqlite例

## 1. Build
>* 构造TableSchema对象
>* 通过TableSchemaBuilder类的DefineIdentity、DefineColumn、DefinColumns、DefineKeys等方法来定义表的列和主键等信息
```csharp
var builder = new TableSchemaBuilder("Students", "tenant1")
    .DefineIdentity("Id", "INTEGER")
    .DefinColumns("TEXT", "Name");
TableSchema table = builder.Build();
```
```csharp
var builder = new TableSchemaBuilder("Students", "tenant1");
builder.DefineColumn("Id", "INTEGER")
    .ColumnType = ColumnType.Identity | ColumnType.Key;
builder.DefineColumn("Name", "TEXT");
TableSchema table = builder.Build();
```
```csharp
var builder = new TableSchemaBuilder("user_role", "tenant1")
    .DefineKeys("INT", "UserId", "RoleId")
    .DefinColumns("DATETIME", "CreateTime");
TableSchema table = builder.Build();
```

## 2. CreateTable
~~~csharp
    ColumnSchema id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
    ColumnSchema name = new("Name", "TEXT");
    TableSchema table = new("Students", [id, name]);
    CreateTable create = new(table);
// CREATE TABLE "Students"("Id" INTEGER PRIMARY KEY AUTOINCREMENT,"Name" TEXT)
~~~

## 3. 搭配Dapper.Shadow建表
~~~csharp
    ColumnSchema id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
    ColumnSchema name = new("Name", "TEXT");
    var result = new TableSchema("Students", [id, name])
        .ToCreate()
        .Execute(Executor);
~~~
## 4. DropTable
~~~csharp
var drop = new DropTable("Students");
// DROP TABLE "Students"
~~~

## 5. 搭配Dapper.Shadow删掉
~~~csharp
    var result = new StudentTable()
    .ToDrop()
    .Execute(Executor);
~~~
