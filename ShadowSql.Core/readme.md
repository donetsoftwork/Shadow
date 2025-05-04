# ShadowSql.Core
>* 拼接sql工具
>* 支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成

以下简单示例的sql是按MsSql数据库

## 1. 按列查询
~~~csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
     .Where(table.Id.Less("LastId"))
     .Where(table.Status.EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1
~~~

## 2. 按列名查询
~~~csharp
var query = new TableSqlQuery("Users")
    .Where(u=> u.Field("Id").Less("LastId"))
    .Where(u => u.Field("Status").EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1
~~~

## 3. 原生sql查询
~~~csharp
var query = new TableSqlQuery("Users")
    .Where("Id=@Id", "Status=@Status");
// SELECT * FROM [Users] WHERE Id=@Id AND Status=@Status
~~~

## 4. 逻辑运算查询
~~~csharp
var users = new UserTable();
var query = new TableQuery(users)
    .And(users.Id.Less("LastId") & users.Status.EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1
~~~

## 5. 分页查询
~~~csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
     .Where(table.Id.Less("LastId"))
     .Where(table.Status.EqualValue(true));
var cursor = new TableCursor(query)
    .Desc(table.Id)
    .Skip(10)
    .Take(10);
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1 ORDER BY [Id] DESC OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 6. 删除
~~~csharp
var table = _db.From("Students");
var query = new TableSqlQuery(table)
    .Where(table.Field("Score").LessValue(60));
var delete = new TableDelete(table, query.Filter);
// DELETE FROM [Students] WHERE [Score]<60
~~~

## 7. 更新
~~~csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
    .Where(table.Id.Equal());
var update = new TableUpdate(table, query.Filter)
    .Set(table.Status.EqualToValue(false));
// UPDATE [Users] SET [Status]=0 WHERE [Id]=@Id
~~~

## 8. 插入
~~~csharp
var table = new StudentTable();
var insert = new SingleInsert(table)
    .Insert(table.Name.Insert("StudentName"))
    .Insert(table.Score.InsertValue(90));
// INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,90)
~~~
