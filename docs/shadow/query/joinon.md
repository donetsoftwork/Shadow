# 逻辑联表关联
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
>* [JoinOnQuery\<LTable, RTable\>](/api/ShadowSql.Join.JoinOnQuery-2.html)

## 4. Join扩展方法
>* 从表创建[JoinOnQuery\<LTable, RTable\>](/api/ShadowSql.Join.JoinOnQuery-2.html)和[JoinTableQuery](/api/ShadowSql.Join.JoinTableQuery.html)
```csharp
JoinOnQuery<LTable, RTable> Join<LTable, RTable>(this LTable main, RTable table)
        where LTable : ITable
        where RTable : ITable;
```
```csharp
var joinOn = _db.From("Employees")
    .SqlJoin(_db.From("Departments"));
// SELECT * FROM [Employees] AS t1 INNER JOIN [Departments] AS t2
```

## 5. LeftTableJoin扩展方法
```csharp
JoinOnQuery<LTable, TTable> LeftTableJoin<LTable, RTable, TTable>(this JoinOnQuery<LTable, RTable> joinOn, TTable table)
        where LTable : ITable
        where RTable : ITable
        where TTable : ITable;
```
```csharp
var query = new CommentTable()
    .Join(new PostTable())
    .Apply(
        c => c.PostId,
        p => p.Id,
        (q, PostId, Id) => q.And(PostId.Equal(Id))
    )
    .LeftTableJoin(new UserTable())
    .Apply(
        c => c.UserId,
        u => u.Id,
        (q, UserId, Id) => q.And(UserId.Equal(Id))
    );
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]
```


## 6. RightTableJoin扩展方法
```csharp
JoinOnQuery<RTable, TTable> RightTableJoin<LTable, RTable, TTable>(this JoinOnQuery<LTable, RTable> joinOn, TTable table)
        where LTable : ITable
        where RTable : ITable
        where TTable : ITable;
```
```csharp
var query = new PostTable()
    .Join(new CommentTable())
    .Apply(
        p => p.Id,
        c => c.PostId,
        (q, Id, PostId) => q.And(Id.Equal(PostId))
    )
    .RightTableJoin(new UserTable())
    .Apply(
        c => c.UserId,
        u => u.Id,
        (q, UserId, Id) => q.And(UserId.Equal(Id))
    );
// SELECT * FROM [Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]
```

## 7. Apply扩展方法
>* 操作Logic的高阶函数
>* 也可称开窗函数,把内部的字段和Logic开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来关联查询
```csharp
JoinOnQuery<LTable, RTable> Apply<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn, Func<LTable, IColumn> left, Func<RTable, IColumn> right, Func<Logic, IColumn, IColumn, Logic> logic)
        where LTable : ITable
        where RTable : ITable;
```
```csharp
var joinOn = new CommentTable()
    .Join(new PostTable())
    .Apply(
        left => left.PostId, 
        right => right.Id, 
        (q, PostId, Id) => q.And(PostId.Equal(Id))
    );
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]
```

## 8. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[JoinOnQuery\<LTable, RTable\>](/api/ShadowSql.Join.JoinOnQuery-2.html)的方法和扩展方法部分
>* 参看[逻辑查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/query/joinon.md)