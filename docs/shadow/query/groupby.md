# 逻辑分组查询
>* 按列分组并查询
>* sql的GROUP BY和HAVING部分
>* 按And、Or来操作

## 1. 接口
>[IGroupByView](/api/ShadowSql.Identifiers.IGroupByView.html)

## 2. 基类
>[GroupByBase](/api/ShadowSql.GroupBy.GroupByBase.html)

## 3. 类
>[GroupByTableQuery\<TTable\>](/api/ShadowSql.GroupBy.GroupByTableQuery-1.html)

## 4. GroupBy
>创建[GroupByTableQuery\<TTable\>](/api/ShadowSql.GroupBy.GroupByTableQuery-1.html)
### 4.1 GroupBy扩展方法
>* 从表创建[GroupByTableQuery\<TTable\>](/api/ShadowSql.GroupBy.GroupByTableQuery-1.html)
```csharp
GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, params IFieldView[] fields)
        where TTable : ITable;
GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, Func<TTable, IFieldView[]> select)
        where TTable : ITable;
GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, params IEnumerable<string> columnNames)
        where TTable : ITable;
```
```csharp
var groupBy = _db.From("Users")
    .GroupBy("City");
// SELECT * FROM [Users] GROUP BY [City]
```
```csharp
var table = new CommentTable();
var groupBy = table.GroupBy(table.PostId);
// SELECT * FROM [Comment] GROUP BY [PostId]
```
```csharp
var groupBy = new CommentTable()
    .GroupBy(table => [table.PostId]);
// SELECT * FROM [Comment] GROUP BY [PostId]
```

### 4.2 GroupBy重载扩展方法
>* 从表和查询条件创建[GroupByTableQuery\<TTable\>](/api/ShadowSql.GroupBy.GroupByTableQuery-1.html)
```csharp
GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, ISqlLogic where, params IFieldView[] fields)
        where TTable : ITable;
GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, Func<TTable, ISqlLogic> where, Func<TTable, IFieldView[]> select)
        where TTable : ITable;
GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, ISqlLogic where, params IEnumerable<string> columnNames)
        where TTable : ITable;
```
```csharp
IColumn age = Column.Use("Age");
var groupBy = _db.From("Users")
    .GroupBy(age.GreaterValue(30), "City");
// SELECT * FROM [Users] WHERE [Age]>30 GROUP BY [City]
```
```csharp
var table = new CommentTable();
var groupBy = table.GroupBy(table.UserId.InValue(1, 2, 3), table.PostId);
var sql = _engine.Sql(groupBy);
// SELECT * FROM [Comments] WHERE [UserId] IN (1,2,3) GROUP BY [PostId]
```
```csharp
var groupBy = new CommentTable()
    .GroupBy(table => table.UserId.LessValue(100), table => [table.PostId]);
// SELECT * FROM [Comments] WHERE [UserId]<100 GROUP BY [PostId]
```

### 4.3 GroupBy重载扩展方法
>* 从[TableQuery\<TTable\>](/api/ShadowSql.Tables.TableQuery-1.html)创建[GroupByTableQuery\<TTable\>](/api/ShadowSql.GroupBy.GroupByTableQuery-1.html)
```csharp
GroupByTableQuery<TTable> GroupBy<TTable>(this TableQuery<TTable> query, params IFieldView[] fields)
        where TTable : ITable;
GroupByTableQuery<TTable> GroupBy<TTable>(this TableQuery<TTable> query, Func<TTable, IFieldView[]> select)
        where TTable : ITable;
GroupByTableQuery<TTable> GroupBy<TTable>(this TableQuery<TTable> query, params IEnumerable<string> columnNames)
        where TTable : ITable;
```
```csharp
var table = new CommentTable();
var groupBy = table.ToQuery()
    .And(table.UserId.InValue(1, 2, 3))
    .GroupBy(table.PostId);
// SELECT * FROM [Comments] WHERE [UserId] IN (1,2,3) GROUP BY [PostId]
```
```csharp
var groupBy = new CommentTable()
    .ToQuery()
    .And(table => table.UserId.LessValue(100))
    .GroupBy(table => [table.PostId]);
// SELECT * FROM [Comments] WHERE [UserId]<100 GROUP BY [PostId]
```

## 5. Apply方法
>* 先聚合再查询
>* 操作Logic的高阶函数
>* 也可称开窗函数,把聚合字段和Logic开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来查询
```csharp
GroupByTableQuery<TTable> Apply(Func<TTable, IAggregateField> aggregate, Func<Logic, IAggregateField, Logic> query);
```
```csharp
var groupBy = new CommentTable()
    .GroupBy(static table => [table.PostId])
    .Apply(static table => table.Pick.Sum(), static (q, Pick) => q.And(Pick.GreaterValue(100)));
// SELECT * [Comments] GROUP BY [PostId] HAVING SUM([Pick])>100
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByTableQuery\<TTable\>](/api/ShadowSql.GroupBy.GroupByTableQuery-1.html)的方法和扩展方法部分
>* 参看[聚合](../../shadowcore/aggregate.md)
>* 参看[逻辑查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/Query/groupby.md)