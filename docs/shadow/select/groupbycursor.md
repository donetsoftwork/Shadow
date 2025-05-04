# 分组分页
>* 从分组分页获取数据组件
>* 本组件用来处理sql的SELECT分页语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性
>* 本组件通过[分组分页游标](../cursor/groupby.md)来分页

## 1. 接口
>[ISelect](/api/ShadowSql.Select.ISelect.html)

## 2. 基类
>* [SelectFieldsBase](/api/ShadowSql.Select.SelectFieldsBase.html)

## 3. 类
>[GroupByTableCursorSelect\<TTable\>](/api/ShadowSql.CursorSelect.GroupByTableCursorSelect-1.html)

## 4. ToSelect扩展方法
>* 从[分组分页游标](../cursor/groupby.md)创建[GroupByTableCursorSelect\<TTable\>](/api/ShadowSql.CursorSelect.GroupByTableCursorSelect-1.html)
>* 先分组再分页后筛选
>* 调用路径: GroupBy().ToCursor().ToSelect()
>* 筛选的默认字段为当前分组字段
~~~csharp
GroupByTableCursorSelect<TTable> ToSelect<TTable>(this GroupByTableCursor<TTable> cursor)
        where TTable : ITable;
~~~
~~~csharp
var select = _db.From("Users")
    .GroupBy("City")
    .ToCursor(10, 20)
    .CountAsc()
    .ToSelect();
// SELECT [City] FROM [Users] GROUP BY [City] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~


## 5. SelectAggregate方法
>* 聚合筛选
>* 调用路径: GroupBy().ToCursor().ToSelect().SelectAggregate()
~~~csharp
GroupByTableCursorSelect<TTable> SelectAggregate(Func<TTable, IAggregateFieldAlias> select);
~~~
~~~csharp
var select = new CommentTable()
    .GroupBy(c => [c.PostId])
    .ToCursor(10, 20)
    .CountAsc()
    .ToSelect()
    .SelectGroupBy()
    .SelectAggregate(c => c.Pick.SumAs("PickTotal"));
// SELECT [PostId],SUM([Pick]) AS PickTotal FROM [Comments] GROUP BY [PostId] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[sql分组](../sqlquery/groupby.md)
>* 参看[逻辑分组](../query/groupby.md)
>* 参看[聚合](../../shadowcore/aggregate.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)

