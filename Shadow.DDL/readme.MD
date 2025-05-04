# ShadowSql.DDL
>* ShadowSql的DDL组件
>* 主要用来支持CreateTable
>* 搭配Dapper.Shadow.Core或Dapper.Shadow可以直接操作数据库的表

以下以Sqlite例

## 1. CreateTable
~~~csharp
    ColumnSchema id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
    ColumnSchema name = new("Name", "TEXT");
    TableSchema table = new("Students", [id, name]);
    CreateTable create = new(table);
~~~
>
>CREATE TABLE "Students"("Id" INTEGER PRIMARY KEY AUTOINCREMENT,"Name" TEXT)

## 2. 搭配Dapper.Shadow建表
~~~csharp
    ColumnSchema id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
    ColumnSchema name = new("Name", "TEXT");
    var result = new TableSchema("Students", [id, name])
        .ToCreate()
        .Execute(Executor);
~~~
## 3. DropTable
~~~csharp
var drop = new DropTable("Students");
~~~
>DROP TABLE "Students"

## 4. 搭配Dapper.Shadow删掉
~~~csharp
    var result = new StudentTable()
    .ToDrop()
    .Execute(Executor);
~~~
