# ShadowSql
>* 拼接sql工具,支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成

以下简单示例的sql是按MsSql数据库

## 1. 按列名查询
~~~csharp
var query = _db.From("Users")
    .ToSqlQuery()
    .ColumnParameter("Id", "<", "LastId")
    .ColumnValue("Status", true);
~~~
>
>SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1

### 2. 按列查询
~~~csharp
var query = new UserTable()
    .ToSqlQuery()
     .Where(table => table.Id.Less("LastId"))
     .Where(table => table.Status.EqualValue(true));
~~~
>
>SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1

### 3. 原生sql查询
~~~csharp
var query = _db.From("Users")
    .ToSqlQuery()
    .Where("Id=@Id", "Status=@Status");
~~~
>
>SELECT * FROM [Users] WHERE Id=@Id AND Status=@Status

### 4. 逻辑运算查询
~~~csharp
var users = new UserTable();
var query = users.ToQuery()
    .And(users.Id.Less("LastId") & users.Status.EqualValue(true));
~~~
>
>SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1

### 5. 分页查询
~~~csharp
var query = new UserTable()
    .ToSqlQuery()
    .Where(table => table.Id.Less("LastId"))
    .Where(table => table.Status.EqualValue(true))
    .ToCursor()
    .Desc(table => table.Id)
    .Skip(10)
    .Take(10);
~~~
>
>SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1 ORDER BY [Id] DESC OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY

### 6. 连接查询
~~~csharp
var e = SimpleDB.From("Employees").As("e");
var d = SimpleDB.From("Departments").As("d");
var joinOn = e.SqlJoin(d)
    .On(e.Field("DepartmentId").Equal(d.Field("Id")));
var joinTable = joinOn.Root
    .Where(e.Field("Age").GreaterValue(30))
    .Where(d.Field("Manager").EqualValue("CEO"));
~~~
>
>SELECT * FROM [Employees] AS e INNER JOIN [Departments] AS d ON e.[DepartmentId]=d.[Id] WHERE e.[Age]>30 AND d.[Manager]='CEO'

## 7. 删除
~~~csharp
var delete = new StudentTable()
    .ToSqlQuery()
    .Where(table => table.Score.LessValue(60))
    .ToDelete();
~~~
>
>DELETE FROM [Students] WHERE [Score]<60

## 8. 更新
~~~csharp
var update = new StudentTable()
    .ToSqlQuery()
    .Where(table => table.Score.LessValue(60))
    .ToUpdate()
    .Set(table => table.Score.AddValue(10));
~~~
>
>UPDATE [Students] SET [Score]+=10 WHERE [Score]<60

## 9. 插入
~~~csharp
var insert = new StudentTable()
    .ToInsert()
    .Insert(student => student.Name.Insert("StudentName"))
    .Insert(student => student.Score.InsertValue(90));
~~~
>
~~~sql
INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,90)
~~~
