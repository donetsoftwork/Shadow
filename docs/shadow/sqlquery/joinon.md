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
>* [JoinOnSqlQuery\<LTable, RTable\>](xref:ShadowSql.Join.JoinOnSqlQuery%602)

## 4. SqlJoin扩展方法
>* 从表创建[JoinOnSqlQuery\<LTable, RTable\>](xref:ShadowSql.Join.JoinOnSqlQuery%602)和[JoinTableSqlQuery](xref:ShadowSql.Join.JoinTableSqlQuery)
```csharp
JoinOnSqlQuery<LTable, RTable> SqlJoin<LTable, RTable>(this LTable main, RTable table)
        where LTable : ITable
        where RTable : ITable;
```
```csharp
var joinOn = _db.From("Employees")
    .SqlJoin(_db.From("Departments"))
    .OnColumn("DepartmentId", "Id");
// SELECT * FROM [Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id]
```

## 5. On扩展方法
>按字段联表
```csharp
JoinOnSqlQuery<LTable, RTable> On<LTable, RTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, Func<LTable, IColumn> left, Func<RTable, IColumn> right)
        where LTable : ITable
        where RTable : ITable;
JoinOnSqlQuery<LTable, RTable> On<LTable, RTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, Func<LTable, IColumn> left, CompareSymbol compare, Func<RTable, IColumn> right)
        where LTable : ITable
        where RTable : ITable;
```
```csharp
var joinOn = new CommentTable()
    .SqlJoin(new PostTable())
    .On(c => c.PostId, p => p.Id);
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]
```

## 6. LeftTableJoin扩展方法
```csharp
JoinOnSqlQuery<LTable, TTable> LeftTableJoin<LTable, RTable, TTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, TTable table)
        where LTable : ITable
        where RTable : ITable
        where TTable : ITable;
```
```csharp
var query = new CommentTable()
    .SqlJoin(new PostTable())
    .On(c => c.PostId, p => p.Id)
    .LeftTableJoin(new UserTable())
    .On(c => c.UserId, u => u.Id);
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]
```

## 7. RightTableJoin扩展方法
```csharp
JoinOnSqlQuery<RTable, TTable> RightTableJoin<LTable, RTable, TTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, TTable table)
        where LTable : ITable
        where RTable : ITable
        where TTable : ITable;
```
```csharp
var query = new PostTable()
    .SqlJoin(new CommentTable())
    .On(p => p.Id, c => c.PostId)
    .RightTableJoin(new UserTable())
    .On(c => c.UserId, u => u.Id);
// SELECT * FROM [Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]
```

## 8. OnLeft方法
>* 在ON子句单对左表查询
>* 一般只在RIGHT JOIN时使用该方法,否则建议直接在WHERE子句查询
>* 作用是从左表剔除部分数据用NULL填充
```csharp
JoinOnSqlQuery<LTable, RTable> OnLeft(Func<LTable, IColumn> select, Func<IColumn, AtomicLogic> query);
```
```csharp
var joinOn = new CommentTable()
    .SqlJoin(new PostTable())
    .AsRightJoin()
    .On(c => c.PostId, p => p.Id)
    .OnLeft(c => c.Pick, Pick => Pick.EqualValue(true));
// SELECT * FROM [Comments] AS t1 RIGHT JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] AND t1.[Pick]=1
```


## 9. OnRight
>* 在ON子句单对右表查询
>* 一般只在LEFT JOIN时使用该方法,否则建议直接在WHERE子句查询
>* 作用是从右表剔除部分数据用NULL填充
```csharp
JoinOnSqlQuery<LTable, RTable> OnRight(Func<RTable, IColumn> select, Func<IColumn, AtomicLogic> query);
```
```csharp
var joinOn = new CommentTable()
    .SqlJoin(new PostTable())
    .AsLeftJoin()
    .On(c => c.PostId, p => p.Id)
    .OnRight(p => p.Author, Author => Author.NotEqualValue("张三"));
// SELECT * FROM [Comments] AS t1 LEFT JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] AND t2.[Author]<>'张三'
```

## 10. Apply扩展方法
>* 操作SqlQuery的高阶函数
>* 也可称开窗函数,把内部的字段和SqlQuery开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来关联查询
```csharp
JoinOnSqlQuery<LTable, RTable> Apply<LTable, RTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, Func<LTable, IColumn> left, Func<RTable, IColumn> right, Func<SqlQuery, IColumn, IColumn, SqlQuery> query)
        where LTable : ITable
        where RTable : ITable;
```
```csharp
var joinOn = new CommentTable()
    .SqlJoin(new PostTable())
    .Apply(
        left => left.PostId, 
        right => right.Id, 
        (q, PostId, Id) => q.And(PostId.Equal(Id))
    );
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]
```

## 11. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[JoinOnSqlQuery\<LTable, RTable\>](xref:ShadowSql.Join.JoinOnSqlQuery%602)的方法和扩展方法部分
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/joinon.md)
