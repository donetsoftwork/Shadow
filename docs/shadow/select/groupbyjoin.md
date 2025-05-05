# 联表分组
>* 从联表分组查询获取数据组件
>* 依赖[sql联表分组](../sqlquery/groupbyjoin.md)或[逻辑联表分组](../query/groupbyjoin.md)
>* 本组件用来处理sql的SELECT语句
>* 本组件是对ShadowSql.Core同名组件的扩展

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)
>* [IGroupBySelect](xref:ShadowSql.Select.IGroupBySelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>[GroupByMultiSelect](xref:ShadowSql.Select.GroupByMultiSelect)

## 4. ToSelect
>创建[GroupByMultiSelect](xref:ShadowSql.Select.GroupByMultiSelect)

### 4.1 ToSelect扩展方法
>* 从sql联表分组获取
>* 依赖[sql联表分组](../sqlquery/groupbyjoin.md)
>* 筛选的默认字段为当前分组字段
~~~csharp
GroupByMultiSelect ToSelect(this GroupByMultiSqlQuery groupBy);
~~~
~~~csharp
var select = _db.From("Employees")
    .SqlJoin(_db.From("Departments"))
    .OnColumn("DepartmentId", "Id")
    .Root
    .SqlGroupBy("Manager")
    .ToSelect();
// SELECT [Manager] FROM [Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] GROUP BY [Manager]
~~~

### 4.2 ToSelect重载扩展方法
>* 从逻辑分组获取
>* 依赖[逻辑联表分组](../query/groupbyjoin.md)
>* 筛选的默认字段为当前分组字段
~~~csharp
GroupByMultiSelect ToSelect(this GroupByMultiQuery groupBy);
~~~
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var select = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .Root
    .SqlGroupBy(p.Id)
    .ToSelect();
// SELECT p.[Id] FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY p.[Id]
~~~

## 5. SelectAggregate
### 5.1 SelectAggregate方法
>* 聚合筛选
>* 从别名表聚合
~~~csharp
GroupByMultiSelect SelectAggregate<TAliasTable>(string tableName, Func<TAliasTable, IAggregateFieldAlias> select)
        where TAliasTable : IAliasTable;
GroupByMultiSelect SelectAggregate<TAliasTable>(string tableName, Func<TAliasTable, IEnumerable<IAggregateFieldAlias>> select)
        where TAliasTable : IAliasTable;
~~~
~~~csharp
var select = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .SqlGroupBy((c, p) => [c.PostId])
    .ToSelect()
    .SelectGroupBy()
    .SelectAggregate<CommentAliasTable>("c", c => c.Pick.SumAs("PickTotal"));
// SELECT c.[PostId],SUM(c.[Pick]) AS PickTotal FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId]
~~~

### 5.2 SelectAggregate重载方法
>* 聚合筛选
>* 从表聚合,先定位再聚合
~~~csharp
GroupByMultiQuery Apply<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<Logic, IAggregateField, Logic> query)
        where TTable : ITable;
~~~
~~~csharp
var select = new CommentTable()
    .SqlJoin(new PostTable())
    .On(c => c.PostId, p => p.Id)
    .SqlGroupBy((c, p) => [c.PostId])
    .ToSelect()
    .SelectGroupBy()
    .SelectAggregate<CommentTable>("Comments", c => c.Pick, Pick => Pick.SumAs("PickTotal"));
// SELECT t1.[PostId],SUM(t1.[Pick]) AS PickTotal FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t1.[PostId]
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[sql联表分组](../sqlquery/groupbyjoin.md)
>* 参看[逻辑联表分组](../query/groupbyjoin.md)
>* 参看[聚合](../../shadowcore/aggregate.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)