# sql分组查询
>* 按列分组并查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[IGroupByView](xref:ShadowSql.Identifiers.IGroupByView)

## 2. 基类
>[GroupByBase](xref:ShadowSql.GroupBy.GroupByBase)

## 3. 类
>[GroupByTableSqlQuery\<TTable\>](xref:ShadowSql.GroupBy.GroupByTableSqlQuery%601)

## 4. SqlGroupBy
>创建[GroupByTableSqlQuery\<TTable\>](xref:ShadowSql.GroupBy.GroupByTableSqlQuery%601)
### 4.1 SqlGroupBy扩展方法
>* 从表创建[GroupByTableSqlQuery\<TTable\>](xref:ShadowSql.GroupBy.GroupByTableSqlQuery%601)
```csharp
GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, params IField[] fields)
        where TTable : ITable;
GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, Func<TTable, IField[]> select)
        where TTable : ITable;
GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, params IEnumerable<string> columnNames)
        where TTable : ITable;
```
```csharp
var groupBy = _db.From("Users")
    .SqlGroupBy("City");
// SELECT * FROM [Users] GROUP BY [City]
```
```csharp
var table = new CommentTable();
var groupBy = table.SqlGroupBy(table.PostId);
// SELECT * FROM [Comment] GROUP BY [PostId]
```
```csharp
var groupBy = new CommentTable()
    .SqlGroupBy(table => [table.PostId]);
// SELECT * FROM [Comment] GROUP BY [PostId]
```

### 4.2 SqlGroupBy重载扩展方法
>* 从表和查询条件创建[GroupByTableSqlQuery\<TTable\>](xref:ShadowSql.GroupBy.GroupByTableSqlQuery%601)
```csharp
GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, ISqlLogic where, params IField[] fields)
        where TTable : ITable;
GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, Func<TTable, ISqlLogic> where, Func<TTable, IField[]> select)
        where TTable : ITable;
GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, ISqlLogic where, params IEnumerable<string> columnNames)
        where TTable : ITable;
```
```csharp
IColumn age = Column.Use("Age");
var groupBy = _db.From("Users")
    .SqlGroupBy(age.GreaterValue(30), "City");
// SELECT * FROM [Users] WHERE [Age]>30 GROUP BY [City]
```
```csharp
var table = new CommentTable();
var groupBy = table.SqlGroupBy(table.UserId.InValue(1, 2, 3), table.PostId);
var sql = _engine.Sql(groupBy);
// SELECT * FROM [Comments] WHERE [UserId] IN (1,2,3) GROUP BY [PostId]
```
```csharp
var groupBy = new CommentTable()
    .SqlGroupBy(table => table.UserId.LessValue(100), table => [table.PostId]);
// SELECT * FROM [Comments] WHERE [UserId]<100 GROUP BY [PostId]
```

### 4.3 SqlGroupBy重载扩展方法
>* 从[TableSqlQuery\<TTable\>](xref:ShadowSql.Tables.TableSqlQuery%601)创建[GroupByTableSqlQuery\<TTable\>](xref:ShadowSql.GroupBy.GroupByTableSqlQuery%601)
```csharp
GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TableSqlQuery<TTable> query, params IField[] fields)
        where TTable : ITable;
GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TableSqlQuery<TTable> query, Func<TTable, IField[]> select)
        where TTable : ITable;
GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TableSqlQuery<TTable> query, params IEnumerable<string> columnNames)
        where TTable : ITable;
```
```csharp
var groupBy = _db.From("Users")
    .ToSqlQuery()
    .Where("Age>30")
    .SqlGroupBy("City");
// SELECT * FROM [Users] WHERE Age>30 GROUP BY [City]
```
```csharp
var table = new CommentTable();
var groupBy = table.ToSqlQuery()
    .Where(table.UserId.InValue(1, 2, 3))
    .SqlGroupBy(table.PostId);
// SELECT * FROM [Comments] WHERE [UserId] IN (1,2,3) GROUP BY [PostId]
```
```csharp
var groupBy = new CommentTable()
    .ToSqlQuery()
    .Where(table => table.UserId.LessValue(100))
    .SqlGroupBy(table => [table.PostId]);
// SELECT * FROM [Comments] WHERE [UserId]<100 GROUP BY [PostId]
```

## 5. HavingAggregate方法
>* 先聚合再查询
```csharp
GroupByTableSqlQuery<TTable> HavingAggregate(Func<TTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query);
```
```csharp
var groupBy = new CommentTable()
    .SqlGroupBy(table => [table.PostId])
    .HavingAggregate(table => table.Pick.Sum(), Pick => Pick.GreaterValue(100));
// SELECT * FROM [Comments] GROUP BY [PostId] HAVING SUM([Pick])>100
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByTableSqlQuery\<TTable\>](xref:ShadowSql.GroupBy.GroupByTableSqlQuery%601)的方法和扩展方法部分
>* 参看[聚合](../../shadowcore/aggregate.md)
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/groupby.md)
