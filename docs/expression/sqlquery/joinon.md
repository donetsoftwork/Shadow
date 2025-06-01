# sql表关联
>* 两个表关联查询
>* 联表查询的JOIN ON部分
>* 通过关键字On查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [IJoinOn](xref:ShadowSql.Join.IJoinOn)
>* [IDataSqlQuery](xref:ShadowSql.Queries.IDataSqlQuery)

## 2. 基类
>[JoinOnBase](xref:ShadowSql.Join.JoinOnBase)

## 3. 类
>* [JoinOnSqlQuery\<TLeft, TRight\>](xref:ShadowSql.Expressions.Join.JoinOnSqlQuery%602)

## 4. SqlJoin
### 4.1 SqlJoin扩展方法
>* 从表创建[JoinOnSqlQuery\<TLeft, TRight\>](xref:ShadowSql.Expressions.Join.JoinOnSqlQuery%602)和[JoinTableSqlQuery](xref:ShadowSql.Join.JoinTableSqlQuery)
```csharp
JoinOnSqlQuery<TLeft, TRight> SqlJoin<TLeft, TRight>(this ITable main, ITable table);
```
```csharp
var query = new Table("Users")
    .SqlJoin<User, UserRole>(new Table("UserRoles"));
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2
```

### 4.2 SqlJoin重载扩展方法
>* 从别名表创建[JoinOnSqlQuery\<TLeft, TRight\>](xref:ShadowSql.Expressions.Join.JoinOnSqlQuery%602)和[JoinTableSqlQuery](xref:ShadowSql.Join.JoinTableSqlQuery)
```csharp
JoinOnSqlQuery<TLeft, TRight> SqlJoin<TLeft, TRight>(this IAliasTable left, IAliasTable right);
```
```csharp
var query = EmptyTable.Use("Users")
    .As("u")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles").As("r"))
// SELECT * FROM [Users] AS u INNER JOIN [UserRoles] AS r
```

## 5. On
### 5.1 On方法
>* 按主外键联表
```csharp
JoinOnSqlQuery<TLeft, TRight> On<TKey>(Expression<Func<TLeft, TKey>> left, Expression<Func<TRight, TKey>> right);
```
```csharp
var query = new Table("Users")
    .SqlJoin<User, UserRole>(new Table("UserRoles"))
    .On(u => u.Id, r => r.UserId);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]
```

### 5.2 On重载方法
>* 按主外键联表
```csharp
JoinOnSqlQuery<TLeft, TRight> On(Expression<Func<TLeft, TRight, bool>> query);
```
```csharp
var query = new Table("Users")
    .SqlJoin<User, UserRole>(new Table("UserRoles"))
    .On((u, r) => u.Id == r.UserId);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]
```

## 6. LeftTableJoin
### 6.1 LeftTableJoin扩展方法
```csharp
JoinOnSqlQuery<TLeft, TOther> LeftTableJoin<TLeft, TRight, TOther>(this JoinOnSqlQuery<TLeft, TRight> joinOn, ITable table);
```
```csharp
var query = EmptyTable.Use("Comments")
    .SqlJoin<Comment, Post>(EmptyTable.Use("Posts"))
    .On((c, p) => c.PostId == p.Id)
    .LeftTableJoin<Comment, Post, User>(EmptyTable.Use("Users"))
    .On((c, u) => c.UserId == u.Id);
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]
```

### 6.2 LeftTableJoin重载扩展方法
```csharp
JoinOnSqlQuery<TLeft, TOther> LeftTableJoin<TLeft, TRight, TOther>(this JoinOnSqlQuery<TLeft, TRight> joinOn, IAliasTable table);
```
```csharp
var query = EmptyTable.Use("Comments")
    .As("c")
    .SqlJoin<Comment, Post>(EmptyTable.Use("Posts").As("p"))
    .On((c, p) => c.PostId == p.Id)
    .LeftTableJoin<Comment, Post, User>(EmptyTable.Use("Users").As("u"))
    .On((c, u) => c.UserId == u.Id);
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]
```

## 7. RightTableJoin
### 7.1 RightTableJoin扩展方法
```csharp
JoinOnSqlQuery<TRight, TOther> RightTableJoin<TLeft, TRight, TOther>(this JoinOnSqlQuery<TLeft, TRight> joinOn, ITable table);
```
```csharp
var query = EmptyTable.Use("Posts")
    .SqlJoin<Post, Comment>(EmptyTable.Use("Comments"))
    .On((c, p) => c.Id == p.PostId)
    .RightTableJoin<Post, Comment, User>(EmptyTable.Use("Users"))
    .On((c, u) => c.UserId == u.Id);
// SELECT * FROM [Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]
```

### 7.2 RightTableJoin重载扩展方法
```csharp
JoinOnSqlQuery<TRight, TOther> RightTableJoin<TLeft, TRight, TOther>(this JoinOnSqlQuery<TLeft, TRight> joinOn, IAliasTable table);
```
```csharp
var query = EmptyTable.Use("Posts")
    .As("p")
    .SqlJoin<Post, Comment>(EmptyTable.Use("Comments").As("c"))
    .On((c, p) => c.Id == p.PostId)
    .RightTableJoin<Post, Comment, User>(EmptyTable.Use("Users").As("u"))
    .On((c, u) => c.UserId == u.Id);
// SELECT * FROM [Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]
```

## 8. OnLeft方法
>* 在ON子句单对左表查询
>* 一般只在RIGHT JOIN时使用该方法,否则建议直接在WHERE子句查询
>* 作用是从左表剔除部分数据用NULL填充
```csharp
JoinOnSqlQuery<TLeft, TRight> OnLeft(Expression<Func<TLeft, bool>> query);
```
```csharp
var query = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .AsRightJoin()
    .On((u, r) => u.Id == r.UserId)
    .OnLeft(u => u.Status);
// SELECT * FROM [Users] AS t1 RIGHT JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] AND t1.[Status]=1
```

## 9. OnRight
>* 在ON子句单对右表查询
>* 一般只在LEFT JOIN时使用该方法,否则建议直接在WHERE子句查询
>* 作用是从右表剔除部分数据用NULL填充
```csharp
JoinOnSqlQuery<LTable, RTable> OnRight(Func<RTable, IColumn> select, Func<IColumn, AtomicLogic> query);
```
```csharp
var query = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .AsLeftJoin()
    .On((u, r) => u.Id == r.UserId)
    .OnRight(r => r.Score >= 60);
// SELECT * FROM [Users] AS t1 LEFT JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] AND t2.[Score]>=60
```


## 10. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[JoinOnSqlQuery\<LTable, RTable\>](xref:ShadowSql.Join.JoinOnSqlQuery%602)的方法和扩展方法部分
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/joinon.md)
