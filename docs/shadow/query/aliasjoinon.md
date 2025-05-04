# 逻辑别名表关联
>* 两个别名表关联查询
>* 联表查询的JOIN ON部分
>* 通过关键字And和Or查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [IJoinOn](/api/ShadowSql.Join.IJoinOn.html)
>* [IDataQuery](/api/ShadowSql.Queries.IDataQuery.html)

## 2. 基类
>[JoinOnBase](/api/ShadowSql.Join.JoinOnBase.html)

## 3. 类
>* [AliasJoinOnQuery\<TLeft, TRight\>](/api/ShadowSql.Join.AliasJoinOnQuery-2.html)

## 4. Join扩展方法
>* 从表创建[AliasJoinOnQuery\<TLeft, TRight\>](/api/ShadowSql.Join.JoinOnQuery-2.html)和[JoinTableQuery](/api/ShadowSql.Join.JoinTableQuery.html)
```csharp
AliasJoinOnQuery<TLeft, TRight> Join<TLeft, TRight>(this TLeft left, TRight right)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>;
```
```csharp
var joinOn = _db.From("Employees")
    .As("e")
    .Join(_db.From("Departments").As("d"));
// SELECT * FROM [Employees] AS e INNER JOIN [Departments] AS d
```

## 5. LeftTableJoin扩展方法
```csharp
AliasJoinOnQuery<TLeft, T> LeftTableJoin<TLeft, TRight, T>(this AliasJoinOnQuery<TLeft, TRight> joinOn, T table)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        where T : IAliasTable<ITable>;
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
UserAliasTable u = new("u");
var query = c.Join(p)
    .And(c.PostId.Equal(p.Id))
    .LeftTableJoin(u)
    .And(c.UserId.Equal(u.Id));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]
```

## 6. RightTableJoin扩展方法
```csharp
AliasJoinOnQuery<TRight, T> RightTableJoin<TLeft, TRight, T>(this AliasJoinOnQuery<TLeft, TRight> joinOn, T table)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        where T : IAliasTable<ITable>;
```
```csharp
PostAliasTable p = new("p");
CommentAliasTable c = new("c");
UserAliasTable u = new("u");
var query = p.Join(c)
    .And(p.Id.Equal(c.PostId))
    .RightTableJoin(u)
    .And(c.UserId.Equal(u.Id));
// SELECT * FROM [Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]
```

## 7. Apply扩展方法
>* 操作Logic的高阶函数
>* 也可称开窗函数,把内部的别名表和Logic开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来关联查询
```csharp
AliasJoinOnQuery<TLeft, TRight> Apply<TLeft, TRight>(this AliasJoinOnQuery<TLeft, TRight> joinOn, Func<Logic, TLeft, TRight, Logic> logic)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>;
```
```csharp
var query = new CommentAliasTable("c")
    .Join(new PostAliasTable("p"))
    .AsRightJoin()
    .Apply((q, c, p) => q
        .And(c.PostId.Equal(p.Id))
        .And(c.Pick.EqualValue(true))
    );
// SELECT * FROM [Comments] AS c RIGHT JOIN [Posts] AS p ON c.[PostId]=p.[Id] AND c.[Pick]=1
```

## 8. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[AliasJoinOnQuery\<TLeft, TRight\>](/api/ShadowSql.Join.AliasJoinOnQuery-2.html)的方法和扩展方法部分
>* 参看[逻辑查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/query/joinon.md)
