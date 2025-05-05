# 逻辑联表关联
>* 两个别名表关联查询
>* 联表查询的JOIN ON部分
>* 使用And和Or查询

## 1. 接口
>* [IJoinOn](xref:ShadowSql.Join.IJoinOn)
>* [IDataQuery](xref:ShadowSql.Queries.IDataQuery)

## 2. 基类
>[JoinOnBase](xref:ShadowSql.Join.JoinOnBase)

## 3. 类
>[JoinOnQuery](xref:ShadowSql.Join.JoinOnQuery)

## 4. 方法
### 4.1 Create
>构造两表关联
#### 4.1.1 Create静态方法
```csharp
JoinOnQuery Create(IAliasTable t1, IAliasTable t2);
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var joinOn = JoiSqlQuery.Create(c, p)
    .And(c.PostId.Equal(p.Id));
```
>SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]

#### 4.1.2 Create重载静态方法
```csharp
JoinOnSqlQuery Create(ITable t1, ITable t2);
```
```csharp
var joinOn = JoinOnQuery.Create(_db.From("Comments"), _db.From("Posts"))
    .Apply((t1, t2, logic) => logic.And(t1.Field("PostId").Equal(t2.Field("Id"))));
```
>SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]

#### 4.1.3 Create重载静态方法
```csharp
JoinOnSqlQuery Create(string t1, string t2);
```
```csharp
var joinOn = JoinOnQuery.Create("Comments", "Posts")
    .Apply((t1, t2, logic) => logic.And(t1.Field("PostId").Equal(t2.Field("Id"))));
```
>SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]

### 4.2 修改联表类型扩展方法
>* 默认INNER JOIN
>* sql和逻辑联表都支持
```csharp
TjoinOn AsInnerJoin<TjoinOn>(this TjoinOn joinOn)
    where TjoinOn : JoinOnBase;
TjoinOn AsOuterJoin<TjoinOn>(this TjoinOn joinOn)
    where TjoinOn : JoinOnBase;
TjoinOn AsLeftJoin<TjoinOn>(this TjoinOn joinOn)
    where TjoinOn : JoinOnBase;
TjoinOn AsRightJoin<TjoinOn>(this TjoinOn joinOn)
    where TjoinOn : JoinOnBase
```

### 4.3 LeftTableJoin
>构造左表与第三表关联
### 4.3.1 LeftTableJoin扩展方法
```csharp
JoinOnQuery LeftTableJoin(this JoinOnQuery joinOn, IAliasTable table);
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var joinPosts = JoinOnQuery.Create(c, p)
    .And(c.PostId.Equal(p.Id));
UserAliasTable u = new("u");
var joinUsers = joinPosts.LeftTableJoin(u)
    .And(c.UserId.Equal(u.Id));
```
>SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]

### 4.3.2 LeftTableJoin重载扩展方法
```csharp
JoinOnQuery LeftTableJoin(this JoinOnQuery joinOn, ITable table);
```
```csharp
var joinPosts = JoinOnQuery.Create(_db.From("Comments"), _db.From("Posts"))
    .Apply((t1, t2, logic) => logic.And(t1.Field("PostId").Equal(t2.Field("Id"))));
var joinUsers = joinPosts.LeftTableJoin(_db.From("Users"))
    .Apply((t1, t2, logic) => logic.And(t1.Field("UserId").Equal(t2.Field("Id"))));
```
>SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]

### 4.3.3 LeftTableJoin重载扩展方法
```csharp
JoinOnSqlQuery LeftTableJoin(this JoinOnSqlQuery joinOn, string tableName);
```
```csharp
var joinPosts = JoinOnSqlQuery.Create("Comments", "Posts")
    .Apply((t1, t2, logic) => logic.And(t1.Field("PostId").Equal(t2.Field("Id"))));
var joinUsers = joinPosts.LeftTableJoin("Users")
    .Apply((t1, t2, logic) => logic.And(t1.Field("UserId").Equal(t2.Field("Id"))));
```
>SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]

### 4.4 RightTableJoin
>构造右表与第三表关联
### 4.4.1 RightTableJoin扩展方法
```csharp
JoinOnQuery RightTableJoin(this JoinOnQuery joinOn, IAliasTable table);
```
```csharp
PostAliasTable p = new("p");
CommentAliasTable c = new("c");        
var joinComments = JoinOnQuery.Create(p, c)
    .And(p.Id.Equal(c.PostId));
UserAliasTable u = new("u");
var joinUsers = joinComments.RightTableJoin(u)
    .And(c.UserId.Equal(u.Id));
```
>SELECT * FROM [Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]

### 4.4.2 RightTableJoin重载扩展方法
```csharp
JoinOnQuery RightTableJoin(this JoinOnQuery joinOn, ITable table);
```
```csharp
var joinComments = JoinOnQuery.Create( _db.From("Posts"), _db.From("Comments"))
    .Apply((t1, t2, logic) => logic.And(t1.Field("Id").Equal(t2.Field("PostId"))));
var joinUsers = joinComments.RightTableJoin(_db.From("Users"))
    .Apply((t1, t2, logic) => logic.And(t1.Field("UserId").Equal(t2.Field("Id"))));
```
>SELECT * FROM [Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]

### 4.4.3 RightTableJoin重载扩展方法
```csharp
JoinOnSqlQuery RightTableJoin(this JoinOnSqlQuery joinOn, string tableName);
```
```csharp
var joinComments = JoinOnSqlQuery.Create("Posts", "Comments")
    .Apply((t1, t2, logic) => logic.And(t1.Field("Id").Equal(t2.Field("PostId"))));
var joinUsers = joinComments.RightTableJoin("Users")
    .Apply((t1, t2, logic) => logic.And(t1.Field("UserId").Equal(t2.Field("Id"))));
```
>SELECT * FROM [Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]

### 4.5 Apply扩展方法
>* 操作Logic的高阶函数
>* 也可称开窗函数,把JoinOnQuery内部的字段和Logic开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来关联查询
>* 查询子函数标记static性能更好
```csharp
TJoinOn Apply<TJoinOn>(this TJoinOn joinOn, string left, string right, Func<Logic, ICompareView, ICompareView, Logic> logic)
        where TJoinOn: JoinOnBase, IDataQuery;
```
```csharp
var joinOn = JoinOnQuery.Create("Comments", "Posts")
    .Apply("PostId", "Id",static (q, PostId, Id) => q.And(PostId.Equal(Id)));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]
```

## 5. 其他相关功能
>* 参看[逻辑查询简介](./index.md)