# sql联表分组
>* 按列分组并查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[IGroupByView](xref:ShadowSql.Identifiers.IGroupByView)

## 2. 基类
>[GroupByBase](xref:ShadowSql.GroupBy.GroupByBase)

## 3. 类
>[GroupByMultiQuery](xref:ShadowSql.GroupBy.GroupByMultiQuery)

## 4. GroupBy
>从多、联表创建[GroupByMultiQuery](xref:ShadowSql.GroupBy.GroupByMultiQuery)
### 4.1 GroupBy扩展方法
```csharp
GroupByMultiQuery GroupBy(this JoinTableQuery multiTable, params IFieldView[] fields);
GroupByMultiQuery GroupBy(this MultiTableQuery multiTable, params IFieldView[] fields);
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var query = c.Join(p)
    .And(c.PostId.Equal(p.Id))
    .Root
    .GroupBy(p.Id);
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY p.[Id]
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var query = c.Multi(p)
    .And(c.PostId.Equal(p.Id))
    .GroupBy(p.Id);
// SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id] GROUP BY p.[Id]
```

### 4.2 GroupBy重载扩展方法
```csharp
GroupByMultiQuery GroupBy(this JoinTableQuery multiTable, params IEnumerable<string> columnNames);
GroupByMultiQuery GroupBy(this MultiTableQuery multiTable, params IEnumerable<string> columnNames);
```
```csharp
var query = _db.From("Employees")
    .SqlJoin(_db.From("Departments"))
    .OnColumn("DepartmentId", "Id")
    .Root
    .GroupBy("Manager");
// SELECT * FROM [Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] GROUP BY [Manager]
```
```csharp
var query = _db.From("Employees")
    .SqlMulti(_db.From("Departments"))
    .Where("t1.DepartmentId=t2.Id")
    .GroupBy("Manager");
// SELECT * FROM [Employees] AS t1,[Departments] AS t2 WHERE t1.DepartmentId=t2.Id GROUP BY [Manager]
```

### 4.3 AliasJoinOnQuery的GroupBy
```csharp
GroupByMultiQuery GroupBy<TLeft, TRight>(this AliasJoinOnQuery<TLeft, TRight> joinOn, Func<TLeft, TRight, IFieldView[]> select)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>;
```
```csharp
var query = new CommentAliasTable("c")
    .Join(new PostAliasTable("p"))
    .Apply(c => c.PostId, p => p.Id, (q, PostId, Id) => q.And(PostId.Equal(Id)))
    .GroupBy((c, p) => [c.PostId]);
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId]
```

### 4.4 JoinOnQuery的GroupBy
```csharp
GroupByMultiQuery GroupBy<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn, Func<LTable, RTable, IColumn[]> select)
        where LTable : ITable
        where RTable : ITable;
```
```csharp
var query = new CommentTable()
    .Join(new PostTable())
    .Apply(c => c.PostId, p => p.Id, (q, PostId, Id) => q.And(PostId.Equal(Id)))
    .GroupBy((c, p) => [p.Id]);
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t2.[Id]
```

## 5. Apply
## 5.1 Apply方法
>* 先聚合再查询
```csharp
GroupByMultiQuery Apply<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> aggregate, Func<Logic, IAggregateField, Logic> query)
         where TAliasTable : IAliasTable;
```
```csharp
var query = new CommentAliasTable("c")
    .Join(new PostAliasTable("p"))
    .Apply((q, c, p) => q.And(c.PostId.Equal(p.Id)))
    .GroupBy((c, p) => [c.PostId])
    .Apply<CommentAliasTable>("c", c => c.Pick.Max(), (q, Pick) => q.And(Pick.GreaterValue(10)));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId] HAVING MAX(c.[Pick])>10
```

## 5.2 Apply重载方法
>* 先定位列再聚合后查询
```csharp
GroupByMultiQuery Apply<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<Logic, IAggregateField, Logic> query)
        where TTable : ITable;
```
```csharp
var query = new CommentTable()
    .Join(new PostTable())
    .Apply(c => c.PostId, p => p.Id, (q, PostId, Id) => q.And(PostId.Equal(Id)))
    .GroupBy((c, p) => [c.PostId])
    .Apply<CommentTable>("Comments", c => c.Pick, Pick => Pick.Max(), (q, Pick) => q.And(Pick.GreaterValue(10)));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t1.[PostId] HAVING MAX(t1.[Pick])>10
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByMultiQuery](xref:ShadowSql.GroupBy.GroupByMultiQuery)的方法和扩展方法部分
>* 参看[聚合](../../shadowcore/aggregate.md)
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/query/groupby.md)
