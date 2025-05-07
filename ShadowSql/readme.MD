# ShadowSql
>* .net拼接sql工具
>* 支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成

以下简单示例的sql是按MsSql数据库

## 1. 按列名查询
~~~csharp
var query = _db.From("Users")
    .ToSqlQuery()
    .StrictParameter("Id", "<", "LastId")
    .StrictValue("Status", true);
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1
~~~

### 2. 按列查询
~~~csharp
var query = new UserTable()
    .ToSqlQuery()
    .Where(table => table.Id.Less("LastId"))
    .Where(table => table.Status.EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1
~~~

### 3. 原生sql查询
~~~csharp
var query = _db.From("Users")
    .ToSqlQuery()
    .Where("Id=@Id", "Status=@Status");
// SELECT * FROM [Users] WHERE Id=@Id AND Status=@Status
~~~

### 4. 逻辑运算查询
~~~csharp
var users = new UserTable();
var query = users.ToQuery()
    .And(users.Id.Less("LastId") & users.Status.EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1
~~~

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
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1 ORDER BY [Id] DESC OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY
~~~

### 6. 联表查询
~~~csharp
var joinOn = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .WhereRight(p => p.Author.NotEqualValue("张三"));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]<>'张三'
~~~

## 7. 插入
~~~csharp
var insert = new StudentTable()
    .ToInsert()
    .Insert(student => student.Name.Insert("StudentName"))
    .Insert(student => student.Score.InsertValue(90));
// INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,90)
~~~

## 8. 删除
~~~csharp
var delete = new StudentTable()
    .ToSqlQuery()
    .Where(table => table.Score.LessValue(60))
    .ToDelete();
// DELETE FROM [Students] WHERE [Score]<60
~~~

## 9. 更新
~~~csharp
var update = new StudentTable()
    .ToSqlQuery()
    .Where(table => table.Score.LessValue(60))
    .ToUpdate()
    .Set(table => table.Score.AddValue(10));
// UPDATE [Students] SET [Score]+=10 WHERE [Score]<60
~~~

## 10、获取数据
### 10.1 直接获取全表
>* 调用ToSelect
~~~csharp
UserTable table = new();
var select = table.ToSelect()
    .Select(table.Id, table.Name);
// SELECT [Id],[Name] FROM [Users]
~~~

### 10.2 从表查询获取
>* 调用路径:ToSqlQuery().ToSelect()
~~~csharp
var table = new UserTable();
var select = table.ToSqlQuery()
    .Where(table.Status.EqualValue(true))
    .ToSelect()
    .Select(table.Id, table.Name);;
// SELECT [Id],[Name] FROM [Users]
~~~

### 10.3 分页获取
>* 调用路径:ToSqlQuery().ToCursor().ToSelect()
~~~csharp
var table = new UserTable();
var select = table.ToSqlQuery()
    .Where(table.Status.EqualValue(true))
    .ToCursor(10, 20)
    .Asc(table.Id)
    .ToSelect()
    .Select(table.Id, table.Name);
// SELECT [Id],[Name] FROM [Users] WHERE [Status]=1 ORDER BY [Id] OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~