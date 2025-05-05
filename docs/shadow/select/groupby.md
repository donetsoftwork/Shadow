# 表分组
>* 从表分组查询获取数据组件
>* 依赖[sql分组](../sqlquery/groupby.md)或[逻辑分组](../query/groupby.md)
>* 本组件用来处理sql的SELECT语句
>* 本组件是对ShadowSql.Core同名组件的扩展

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)
>* [IGroupBySelect](xref:ShadowSql.Select.IGroupBySelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>[GroupByTableSelect\<TTable\>](xref:ShadowSql.Select.GroupByTableSelect%601)
~~~csharp
class GroupByTableSelect<TTable> : SelectBase<TTable>
    where TTable : ITable;
~~~

## 4. ToSelect
>创建[GroupByTableSelect\<TTable\>](/api/ShadowSql.Select.GroupByTableSelect-1.html)

### 4.1 ToSelect扩展方法
>* 从sql分组获取
>* 依赖[sql分组](../sqlquery/groupby.md)
>* 筛选的默认字段为当前分组字段
~~~csharp
GroupByTableSelect<TTable> ToSelect<TTable>(this GroupByTableSqlQuery<TTable> source)
        where TTable : ITable;
~~~
~~~csharp
var select = _db.From("Users")
    .SqlGroupBy("City")
    .ToSelect();
// SELECT [City] FROM [Users] GROUP BY [City]
~~~

### 4.2 ToSelect重载扩展方法
>* 从逻辑分组获取
>* 依赖[逻辑分组](../query/groupby.md)
>* 筛选的默认字段为当前分组字段
~~~csharp
GroupByTableSelect<TTable> ToSelect<TTable>(this GroupByTableQuery<TTable> source)
        where TTable : ITable;
~~~
~~~csharp
var select = _db.From("Users")
    .ToSqlQuery()
    .Where("Status=1")
    .GroupBy("City")
    .ToSelect();
// SELECT [City] FROM [Users] WHERE Status=1 GROUP BY [City]
~~~

## 5. SelectAggregate方法
>* 聚合筛选
~~~csharp
GroupByTableSelect<TTable> SelectAggregate(Func<TTable, IAggregateFieldAlias> select);
~~~
~~~csharp
var select = new CommentTable()
    .GroupBy(c => [c.PostId])
    .ToSelect()
    .SelectGroupBy()
    .SelectAggregate(c => c.Pick.SumAs("PickTotal"));
// SELECT [PostId],SUM([Pick]) AS PickTotal FROM [Comments] GROUP BY [PostId]
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[sql分组](../sqlquery/groupby.md)
>* 参看[逻辑分组](../query/groupby.md)
>* 参看[聚合](../../shadowcore/aggregate.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)
