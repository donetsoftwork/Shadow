# ShadowSql.Core
>* 拼接sql工具,支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成

以下简单示例的sql是按MsSql数据库

## 1. 按列名查询
~~~csharp
var query = new TableSqlQuery("Users")
    .ColumnParameter("Id", "<", "LastId")
    .ColumnValue("Status", true);
~~~
>
>SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1

### 2. 按列查询
~~~csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
     .Where(table.Id.Less("LastId"))
     .Where(table.Status.EqualValue(true));
~~~
>
>SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1

### 3. 原生sql查询
~~~csharp
var query = new TableSqlQuery("Users")
    .Where("Id=@Id", "Status=@Status");
~~~
>
>SELECT * FROM [Users] WHERE Id=@Id AND Status=@Status

### 4. 逻辑运算查询
~~~csharp
var users = new UserTable();
var query = new TableQuery(users)
    .And(users.Id.Less("LastId") & users.Status.EqualValue(true));
~~~
>
>SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1

### 5. 分页查询
~~~csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
     .Where(table.Id.Less("LastId"))
     .Where(table.Status.EqualValue(true));
var cursor = new TableCursor(query)
    .Desc(table.Id)
    .Skip(10)
    .Take(10);
~~~
>
>SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1 ORDER BY [Id] DESC OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY
