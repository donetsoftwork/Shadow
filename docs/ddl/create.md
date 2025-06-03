# 建表

## 1. 接口
>[IExecuteSql](xref:ShadowSql.Fragments.IExecuteSql)

## 2. 类
>[CreateTable](xref:Shadow.DDL.CreateTable)

## 3. CreateTable
~~~csharp
    ColumnSchema id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
    ColumnSchema name = new("Name", "TEXT");
    TableSchema table = new("Students", [id, name]);
    CreateTable create = new(table);
// CREATE TABLE "Students"("Id" INTEGER PRIMARY KEY AUTOINCREMENT,"Name" TEXT)
~~~


## 4. 搭配Dapper.Shadow建表
~~~csharp
    ColumnSchema id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
    ColumnSchema name = new("Name", "TEXT");
    var result = new TableSchema("Students", [id, name])
        .ToCreate()
        .Execute(Executor);
~~~