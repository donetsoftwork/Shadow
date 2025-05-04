# sql别名表关联
>* 两个别名表关联查询
>* 联表查询的JOIN ON部分
>* 通过关键字On查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [IJoinOn](/api/ShadowSql.Join.IJoinOn.html)
>* [IDataSqlQuery](/api/ShadowSql.Queries.IDataSqlQuery.html)

## 2. 基类
>[JoinOnBase](/api/ShadowSql.Join.JoinOnBase.html)

## 3. 类
>* [AliasJoinOnSqlQuery\<TLeft, TRight\>](/api/ShadowSql.Join.AliasJoinOnSqlQuery-2.html)

## 4. SqlJoin扩展方法
```csharp
AliasJoinOnSqlQuery<TLeft, TRight> SqlJoin<TLeft, TRight>(this TLeft left, TRight right)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>;
```
```csharp
var joinOn = _db.From("Employees")
    .As("e")
    .SqlJoin(_db.From("Departments").As("d"))
    .OnColumn("DepartmentId", "Id");
// SELECT * FROM [Employees] AS e INNER JOIN [Departments] AS d ON e.[DepartmentId]=d.[Id]
```

## 5. On扩展方法
### 5.1 On扩展方法
>按字段联表
```csharp
AliasJoinOnSqlQuery<TLeft, TRight> On<TLeft, TRight>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, Func<TLeft, IColumn> left, Func<TRight, IColumn> right)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>;
AliasJoinOnSqlQuery<TLeft, TRight> On<TLeft, TRight>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, Func<TLeft, IColumn> left, CompareSymbol compare, Func<TRight, IColumn> right)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>;
```
```csharp
var query = new PostAliasTable("p")
    .SqlJoin(new CommentAliasTable("c"))
    .On(p => p.Id, c => c.PostId);
// SELECT * FROM [Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId]
```

### 5.2 On重载扩展方法
>按表联表
```csharp
AliasJoinOnSqlQuery<TLeft, TRight> On<TLeft, TRight>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, Func<TLeft, TRight, AtomicLogic> query)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>;
```
```csharp
var query = new PostAliasTable("p")
    .SqlJoin(new CommentAliasTable("c"))
    .On((p, c) => p.Id.Equal(c.PostId));
// SELECT * FROM [Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId]
```

## 6. LeftTableJoin扩展方法
```csharp
AliasJoinOnSqlQuery<TLeft, T> LeftTableJoin<TLeft, TRight, T>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, T table)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        where T : IAliasTable<ITable>;
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
UserAliasTable u = new("u");
var query = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .LeftTableJoin(u)
    .On(c.UserId, u.Id);
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]
```

## 7. RightTableJoin扩展方法
```csharp
AliasJoinOnSqlQuery<TRight, T> RightTableJoin<TLeft, TRight, T>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, T table)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        where T : IAliasTable<ITable>;
```
```csharp
var query = new PostTable()
    .SqlJoin(new CommentTable())
    .On(p => p.Id, c => c.PostId)
    .RightTableJoin(new UserTable())
    .On(c => c.UserId, u => u.Id);
// SELECT * FROM [Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]
```

## 8. OnLeft方法
>* 在ON子句单对左表查询
>* 一般只在RIGHT JOIN时使用该方法,否则建议直接在WHERE子句查询
>* 作用是从左表剔除部分数据用NULL填充
```csharp
AliasJoinOnSqlQuery<TLeft, TRight> OnLeft(Func<TLeft, AtomicLogic> query);
```
```csharp
var query = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .AsRightJoin()
    .On(c => c.PostId, p => p.Id)
    .OnLeft(c => c.Pick.EqualValue(true));
// SELECT * FROM [Comments] AS t1 RIGHT JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] AND t1.[Pick]=1
```

## 9. OnRight方法
>* 在ON子句单对右表查询
>* 一般只在LEFT JOIN时使用该方法,否则建议直接在WHERE子句查询
>* 作用是从右表剔除部分数据用NULL填充
```csharp
AliasJoinOnSqlQuery<TLeft, TRight> OnRight(Func<TRight, AtomicLogic> query);
```
```csharp
var query = new PostAliasTable("p")
    .SqlJoin(new CommentAliasTable("c"))
    .AsLeftJoin()
    .On(p => p.Id, c => c.PostId)
    .OnRight(c => c.Pick.EqualValue(true));
// SELECT * FROM [Posts] AS p LEFT JOIN [Comments] AS c ON p.[Id]=c.[PostId] AND c.[Pick]=1
```

## 10. Apply扩展方法
>* 操作SqlQuery的高阶函数
>* 也可称开窗函数,把内部的别名表和SqlQuery开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来关联查询
```csharp
AliasJoinOnSqlQuery<TLeft, TRight> Apply<TLeft, TRight>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, Func<SqlQuery, TLeft, TRight, SqlQuery> query)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>;
```
```csharp
var query = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .AsRightJoin()
    .Apply((q, c, p) => q
        .And(c.PostId.Equal(p.Id))
        .And(c.Pick.EqualValue(true))
    );
// SELECT * FROM [Comments] AS c RIGHT JOIN [Posts] AS p ON c.[PostId]=p.[Id] AND c.[Pick]=1
```

## 11. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[AliasJoinOnSqlQuery\<TLeft, TRight\>](/api/ShadowSql.Join.AliasJoinOnSqlQuery-2.html)的方法和扩展方法部分
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/joinon.md)
