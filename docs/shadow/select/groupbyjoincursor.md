# 联表分组分页
>* 从联表分组分页获取数据组件
>* 本组件用来处理sql的SELECT分页语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性
>* 本组件通过[联表分组游标](../cursor/groupbyjoin.md)来分页

## 1. 接口
>[ISelect](xref:ShadowSql.Select.ISelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>[GroupByMultiCursorSelect](xref:ShadowSql.CursorSelect.GroupByMultiCursorSelect)

## 4. ToSelect扩展方法
>* 从[联表分组游标](../cursor/groupbyjoin.md)创建[GroupByMultiCursorSelect](xref:ShadowSql.CursorSelect.GroupByMultiCursorSelect)
>* 先联表、分组、分页再筛选
>* 调用路径: GroupBy().ToCursor().ToSelect()
>* 筛选的默认字段为当前分组字段
```csharp
GroupByMultiCursorSelect ToSelect(this GroupByMultiCursor cursor);
```
```csharp
var select = _db.From("Employees")
    .SqlJoin(_db.From("Departments"))
    .OnColumn("DepartmentId", "Id")
    .Root
    .SqlGroupBy("Manager")
    .ToCursor()
    .CountAsc()
    .ToSelect()
    .SelectGroupBy()
    .SelectCount("ManagerCount");
// SELECT [Manager],COUNT(*) AS ManagerCount FROM [Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] GROUP BY [Manager] ORDER BY COUNT(*)
```

## 5. SelectAggregate
### 5.1 SelectAggregate方法
>* 聚合筛选
>* 从别名表聚合
~~~csharp
GroupByMultiCursorSelect SelectAggregate<TAliasTable>(string tableName, Func<TAliasTable, IAggregateFieldAlias> select)
        where TAliasTable : IAliasTable;
GroupByMultiCursorSelect SelectAggregate<TAliasTable>(string tableName, Func<TAliasTable, IEnumerable<IAggregateFieldAlias>> select)
        where TAliasTable : IAliasTable;
~~~
~~~csharp
var select = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .SqlGroupBy((c, p) => [c.PostId])
    .ToCursor(10, 20)
    .CountAsc()
    .ToSelect()
    .SelectGroupBy()
    .SelectAggregate<CommentAliasTable>("c", c => c.Pick.SumAs("PickTotal"));
// SELECT c.[PostId],SUM(c.[Pick]) AS PickTotal FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

### 5.2 SelectAggregate重载方法
>* 聚合筛选
>* 从表聚合,先定位再聚合
~~~csharp
GroupByMultiCursorSelect SelectAggregate<TTable>(string tableName, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateFieldAlias> aggregate)
        where TTable : ITable;
~~~
~~~csharp
var select = new CommentTable()
    .SqlJoin(new PostTable())
    .On(c => c.PostId, p => p.Id)
    .SqlGroupBy((c, p) => [c.PostId])
    .ToCursor(10, 20)
    .CountAsc()
    .ToSelect()
    .SelectGroupBy()
    .SelectAggregate<CommentTable>("Comments", c => c.Pick, Pick => Pick.SumAs("PickTotal"));
// SELECT t1.[PostId],SUM(t1.[Pick]) AS PickTotal FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t1.[PostId] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[联表分组游标](../cursor/groupbyjoin.md)
>* 参看[聚合](../../shadowcore/fields/aggregate.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)