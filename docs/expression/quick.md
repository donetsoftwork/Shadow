# 快速上手
>* 表达式版快速上手

## 1. 表达式查询
### 1.1 按常量查询
~~~csharp
var query = new TableSqlQuery<User>("Users")
    .Where(u => u.Name == "张三");
// SELECT * FROM [Users] WHERE [Name]='张三'
~~~

### 1.2 按参数查询
~~~csharp
var query = new TableSqlQuery<User>()
    .Where<UserParameter>((u, p) =>  u.Age > p.Age2);
// SELECT * FROM [User] WHERE [Age]>@Age2
~~~

## 2. 表达式排序
### 2.1 对单个字段排序
~~~csharp
var cursor = new Table("Users")
    .Take<User>(10)
    .Asc(u => u.Id);
// SELECT TOP 10 * FROM [Users] ORDER BY [Id]
~~~

### 2.2 对多个字段排序
~~~csharp
var cursor = new Table("Users")
    .Take<User>(10)
    .Desc(u => new { u.Age, u.Id });
// SELECT TOP 10 * FROM [Users] ORDER BY [Age] DESC,[Id] DESC
~~~

## 3. 联表查询
### 3.1 主外键联表
~~~csharp
var query = new Table("Users")
    .SqlJoin<User, UserRole>(new Table("UserRoles"))
    .On(u => u.Id, r => r.UserId);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]
~~~

### 3.2 逻辑表达式联表
~~~csharp
var query = new Table("Users")
    .SqlJoin<User, UserRole>(new Table("UserRoles"))
    .On((u, r) => u.Id == r.UserId);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]
~~~

## 4. 插入
### 4.1 插入常量值 
~~~csharp
var insert = EmptyTable.Use("Users")
    .ToInsert(() => new User { Name = "张三", Age = 18 });
// INSERT INTO [Users]([Name],[Age])VALUES('张三',18)
~~~

### 4.2 插入参数
~~~csharp
var insert = EmptyTable.Use("Users")
    .ToInsert<UserParameter, User>(p => new User { Name = p.Name2, Age = p.Age2 });
// INSERT INTO [Users]([Name],[Age])VALUES(@Name2,@Age2)
~~~

## 5. 表达式删除
~~~csharp
var delete = new TableSqlQuery<Student>("Students")
    .Where(s => s.Score < 60)
    .ToDelete();
// DELETE FROM [Students] WHERE [Score]<60
~~~

## 6. 表达式更新
### 6.1 常量更新
~~~csharp
var update = EmptyTable.Use("Users")
    .ToUpdate<User>(u => u.Id == 1)
    .Set(u => new User { Age = 18 });
// UPDATE [Users] SET [Age]=18 WHERE [Id]=1
~~~

### 6.2 参数化更新
~~~csharp
var user = new User { Id =1, Age = 18 };
var update = EmptyTable.Use("Users")
    .ToUpdate<User>(u => u.Id == user.Id)
    .Set(u => new User { Age = user.Age });
// UPDATE [Users] SET [Age]=@Age WHERE [Id]=@Id
~~~

### 6.3 原值叠加更新
~~~csharp
var update = EmptyTable.Use("Students")
    .ToUpdate<Student>(u => u.Score < 60 && u.Score > 55)
    .Set(u => new Student { Score = u.Score + 5 });
// UPDATE [Students] SET [Score]=([Score]+5) WHERE [Score]<60 AND [Score]>55
~~~

## 7、表达式获取数据
### 7.1 直接获取全表
>* 调用ToSelect
~~~csharp
var select = _db.From("Users")
    .ToSelect<User>()
    .Select(u => new { u.Id, u.Name });
// SELECT [Id],[Name] FROM [Users]
~~~

### 7.3 从表达式获取
>* 调用ToSelect
~~~csharp
var select = _db.From("Users")
    .ToSelect<User>(u => u.Status)
    .Select(u => u.Id);
// SELECT [Id] FROM [Users] WHERE [Status]=1
~~~

### 7.3 从表查询获取
>* 调用路径:ToSqlQuery().ToSelect()
~~~csharp
var select = _db.From("Users")
    .ToSqlQuery<User>()
    .Where(u => u.Status)
    .ToSelect()
    .Select(u => new { u.Id, u.Name });
// SELECT [Id],[Name] FROM [Users] WHERE [Status]=1
~~~

### 7.4 分页获取
>* 调用路径:ToSqlQuery().Take().ToSelect()
~~~csharp
var select = _db.From("Users")
    .ToSqlQuery<User>()
    .Where(u => u.Status)
    .Take(10, 20)
    .Desc(u => u.Id)
    .ToSelect();
// SELECT * FROM [Users] WHERE [Status]=1 ORDER BY [Id] OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

