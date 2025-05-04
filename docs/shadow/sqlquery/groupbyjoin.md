# sql联表分组
>* 按列分组并查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[IGroupByView](/api/ShadowSql.Identifiers.IGroupByView.html)

## 2. 基类
>[GroupByBase](/api/ShadowSql.GroupBy.GroupByBase.html)

## 3. 类
>[GroupByMultiSqlQuery](/api/ShadowSql.GroupBy.GroupByMultiSqlQuery.html)

## 4. SqlGroupBy
>从多、联表创建[GroupByMultiSqlQuery](/api/ShadowSql.GroupBy.GroupByMultiSqlQuery.html)
### 4.1 SqlGroupBy扩展方法
```csharp
GroupByMultiSqlQuery SqlGroupBy(this JoinTableSqlQuery multiTable, params IFieldView[] fields);
GroupByMultiSqlQuery SqlGroupBy(this MultiTableSqlQuery multiTable, params IFieldView[] fields);
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var query = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .Root
    .SqlGroupBy(p.Id);
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY p.[Id]
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var query = c.SqlMulti(p)
    .Where(c.PostId.Equal(p.Id))
    .SqlGroupBy(p.Id);
// SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id] GROUP BY p.[Id]
```

### 4.2 SqlGroupBy重载扩展方法
```csharp
GroupByMultiSqlQuery SqlGroupBy(this JoinTableSqlQuery multiTable, params IEnumerable<string> columnNames);
GroupByMultiSqlQuery SqlGroupBy(this MultiTableSqlQuery multiTable, params IEnumerable<string> columnNames);
```
```csharp
var query = _db.From("Employees")
    .SqlJoin(_db.From("Departments"))
    .OnColumn("DepartmentId", "Id")
    .Root
    .SqlGroupBy("Manager");
// SELECT * FROM [Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] GROUP BY [Manager]
```
```csharp
var query = _db.From("Employees")
    .SqlMulti(_db.From("Departments"))
    .Where("t1.DepartmentId=t2.Id")
    .SqlGroupBy("Manager");
// SELECT * FROM [Employees] AS t1,[Departments] AS t2 WHERE t1.DepartmentId=t2.Id GROUP BY [Manager]
```

### 4.3 AliasJoinOnSqlQuery的SqlGroupBy
```csharp
GroupByMultiSqlQuery SqlGroupBy<TLeft, TRight>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, Func<TLeft, TRight, IFieldView[]> select)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>;
```
```csharp
var query = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .SqlGroupBy((c, p) => [c.PostId]);
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId]
```

### 4.4 JoinOnSqlQuery的SqlGroupBy
```csharp
GroupByMultiSqlQuery SqlGroupBy<LTable, RTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, Func<LTable, RTable, IColumn[]> select)
        where LTable : ITable
        where RTable : ITable;
```
```csharp
var query = new CommentTable()
    .SqlJoin(new PostTable())
    .On(c => c.PostId, p => p.Id)
    .SqlGroupBy((c, p) => [c.PostId]);
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t1.[PostId]
```

## 5. HavingAggregate
## 5.1 HavingAggregate方法
>* 先聚合再查询
```csharp
GroupByMultiSqlQuery HavingAggregate<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
         where TAliasTable : IAliasTable;
```
```csharp
var query = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .SqlGroupBy((c, p) => [c.PostId])
    .HavingAggregate<CommentAliasTable>("c", c => c.Pick.Max(), Pick => Pick.GreaterValue(40));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId] HAVING MAX(c.[Pick])>40
```

## 5.2 HavingAggregate重载方法
>* 先定位列再聚合后查询
```csharp
GroupByMultiSqlQuery HavingAggregate<TTable>(string tableName,  Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
        where TTable : ITable;
```
```csharp
var query = new CommentTable()
    .SqlJoin(new PostTable())
    .OnColumn("PostId", "Id")
    .Root
    .SqlGroupBy("PostId")
    .HavingAggregate<CommentTable>("Comments", c => c.Pick, Pick => Pick.Sum(), Pick => Pick.GreaterValue(100));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t1.[PostId] HAVING SUM(t1.[Pick])>100
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByMultiSqlQuery](/api/ShadowSql.GroupBy.GroupByMultiSqlQuery.html)的方法和扩展方法部分
>* 参看[聚合](../../shadowcore/aggregate.md)
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/groupby.md)
