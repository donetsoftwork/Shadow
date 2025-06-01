# 逻辑联表关联
>* 两个别名表关联查询
>* 联表查询的JOIN ON部分
>* 通过关键字And和Or查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [IJoinOn](xref:ShadowSql.Join.IJoinOn)
>* [IDataQuery](xref:ShadowSql.Queries.IDataQuery)

## 2. 基类
>[JoinOnBase](xref:ShadowSql.Join.JoinOnBase)

## 3. 类
>* [JoinOnQuery\<TLeft, TRight\>](xref:ShadowSql.Expressions.Join.JoinOnQuery%602)

## 4. Join
>* 从表创建[JoinOnQuery\<TLeft, TRight\>](xref:ShadowSql.Expressions.Join.JoinOnQuery%602)和[JoinTableQuery](xref:ShadowSql.Join.JoinTableQuery)

### 4.1 Join扩展方法
```csharp
JoinOnQuery<TLeft, TRight> Join<TLeft, TRight>(this ITable main, ITable table);
```
```csharp
var query = new Table("Users")
    .Join<User, UserRole>(new Table("UserRoles"));
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2
```

### 4.2 Join重载扩展方法
```csharp
JoinOnQuery<TLeft, TRight> Join<TLeft, TRight>(this IAliasTable left, IAliasTable right);
```
```csharp
var query = EmptyTable.Use("Users")
    .As("u")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles").As("r"));
// SELECT * FROM [Users] AS u INNER JOIN [UserRoles] AS r
```

## 5. LeftTableJoin
### 5.1 LeftTableJoin扩展方法
```csharp
JoinOnQuery<TLeft, TOther> LeftTableJoin<TLeft, TRight, TOther>(this JoinOnQuery<TLeft, TRight> joinOn, ITable table);
```
```csharp
var query = EmptyTable.Use("Comments")
    .Join<Comment, Post>(EmptyTable.Use("Posts"))
    .And((c, p) => c.PostId == p.Id)
    .LeftTableJoin<Comment, Post, User>(EmptyTable.Use("Users"))
    .And((c, u) => c.UserId == u.Id);
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]
```

### 5.2 LeftTableJoin重载扩展方法
```csharp
JoinOnQuery<TLeft, TOther> LeftTableJoin<TLeft, TRight, TOther>(this JoinOnQuery<TLeft, TRight> joinOn, IAliasTable table);
```
```csharp
var query = EmptyTable.Use("Comments")
    .As("c")
    .Join<Comment, Post>(EmptyTable.Use("Posts").As("p"))
    .And((c, p) => c.PostId == p.Id)
    .LeftTableJoin<Comment, Post, User>(EmptyTable.Use("Users").As("u"))
    .And((c, u) => c.UserId == u.Id);
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]
```


## 6. RightTableJoin
### 6.1 RightTableJoin扩展方法
```csharp
JoinOnQuery<TRight, TOther> RightTableJoin<TLeft, TRight, TOther>(this JoinOnQuery<TLeft, TRight> joinOn, ITable table);
```
```csharp
var query = EmptyTable.Use("Posts")
    .Join<Post, Comment>(EmptyTable.Use("Comments"))
    .And((c, p) => c.Id == p.PostId)
    .RightTableJoin<Post, Comment, User>(EmptyTable.Use("Users"))
    .And((c, u) => c.UserId == u.Id);
// SELECT * FROM [Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]
```

### 6.2 RightTableJoin重载扩展方法
```csharp
JoinOnQuery<TRight, TOther> RightTableJoin<TLeft, TRight, TOther>(this JoinOnQuery<TLeft, TRight> joinOn, IAliasTable table);
```
```csharp
var query = EmptyTable.Use("Posts")
    .Join<Post, Comment>(EmptyTable.Use("Comments"))
    .And((c, p) => c.Id == p.PostId)
    .RightTableJoin<Post, Comment, User>(EmptyTable.Use("Users"))
    .And((c, u) => c.UserId == u.Id);
// SELECT * FROM [Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]
```

## 7. And方法
```csharp
JoinOnQuery<TLeft, TRight> And(Expression<Func<TLeft, TRight, bool>> query);
```
```csharp
var query = EmptyTable.Use("Comments")
    .Join<Comment, Post>(EmptyTable.Use("Posts"))
    .And((c, p) => c.PostId == p.Id);
// SELECT * FROM [Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId]
```

## 8. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[JoinOnQuery\<TLeft, TRight\>](xref:ShadowSql.Expressions.Join.JoinOnQuery%602)的方法和扩展方法部分
>* 参看[逻辑查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/query/joinon.md)