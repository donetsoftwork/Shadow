# 快速上手
>* 精简版快速上手

## 1. 按列查询
### 1.1 常量查询
~~~csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
     .Where(table.Id.LessValue(100))
     .Where(table.Status.EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]<100 AND [Status]=1
~~~

### 1.2 参数查询
~~~csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
     .Where(table.Id.Less("LastId"))
     .Where(table.Status.Equal());
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=@Status
~~~

## 2. 按列名查询
### 2.1 常量查询
~~~csharp
var query = new TableSqlQuery("Users")
    .Where(u => u.Field("Id").LessValue(100))
    .Where(u => u.Field("Status").EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]<100 AND [Status]=1
~~~

### 2.2 参数查询
~~~csharp
var query = new TableSqlQuery("Users")
    .Where(u => u.Field("Id").Less("LastId"))
    .Where(u => u.Field("Status").Equal());
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=@Status
~~~

## 3. 原生sql查询
### 3.1 常量查询
~~~csharp
var query = new TableSqlQuery("Users")
    .Where("Id=1", "Status=1");
// SELECT * FROM [Users] WHERE Id=1 AND Status=1
~~~

### 3.2 参数查询
~~~csharp
var query = new TableSqlQuery("Users")
    .Where("Id=@Id", "Status=@Status");
// SELECT * FROM [Users] WHERE Id=@Id AND Status=@Status
~~~

## 4. 逻辑运算查询
### 4.1 常量查询
~~~csharp
var users = new UserTable();
var query = new TableQuery(users)
    .And(users.Id.LessValue(100) & users.Status.EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]<100 AND [Status]=1
~~~

### 4.2 参数查询
~~~csharp
var users = new UserTable();
var query = new TableQuery(users)
    .And(users.Id.Less("LastId") & users.Status.Equal("Status"));
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=@Status
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
### 6.1 常量删除
~~~csharp
var table = _db.From("Students");
var query = new TableSqlQuery(table)
    .Where(table.Field("Score").LessValue(60));
var delete = new TableDelete(table, query.Filter);
// DELETE FROM [Students] WHERE [Score]<60
~~~

### 6.2 参数删除
~~~csharp
var table = _db.From("Students");
var query = new TableSqlQuery(table)
    .Where(table.Field("Score").Less());
var delete = new TableDelete(table, query.Filter);
// DELETE FROM [Students] WHERE [Score]<@Score
~~~

## 7. 更新
### 7.1 常量更新
~~~csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
    .Where(table.Id.EqualValue(1));
var update = new TableUpdate(table, query.Filter)
    .Set(table.Status.AssignValue(false));
// UPDATE [Users] SET [Status]=0 WHERE [Id]=1
~~~

### 7.2 参数更新
~~~csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
    .Where(table.Id.Equal());
var update = new TableUpdate(table, query.Filter)
    .Set(table.Status.Assign("Status"));
// UPDATE [Users] SET [Status]=@Status WHERE [Id]=@Id
~~~

## 8. 插入
### 8.1 常量插入
~~~csharp
var table = new StudentTable();
var insert = new SingleInsert(table)
    .Insert(table.Name.InsertValue("张三"))
    .Insert(table.Score.InsertValue(90));
// INSERT INTO [Students]([Name],[Score])VALUES('张三',90)
~~~

### 8.2 参数插入
~~~csharp
var table = new StudentTable();
var insert = new SingleInsert(table)
    .Insert(table.Name.Insert("StudentName"))
    .Insert(table.Score.Insert());
// INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,@Score)
~~~