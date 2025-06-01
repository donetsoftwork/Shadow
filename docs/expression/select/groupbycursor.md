# 分组分页
>* 从分组分页获取数据组件
>* 本组件用来处理sql的SELECT分页语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性
>* 本组件通过[分组分页游标](../cursor/groupby.md)来分页

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)
>* [IGroupBySelect](xref:ShadowSql.Expressions.Select.IGroupBySelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>* [GroupByTableCursorSelect\<TKey, TEntity\>](xref:ShadowSql.Expressions.CursorSelect.GroupByTableCursorSelect%602)

## 4. ToSelect扩展方法
>* 从[分组分页游标](../cursor/groupby.md)创建[GroupByTableCursorSelect\<TKey, TEntity\>](xref:ShadowSql.Expressions.CursorSelect.GroupByTableCursorSelect%602)
>* 先分组再分页后筛选
>* 调用路径: GroupBy().ToCursor().ToSelect()
>* 筛选的默认字段为当前分组字段
~~~csharp
GroupByTableCursorSelect<TKey, TEntity> ToSelect<TKey, TEntity>(this GroupByTableCursor<TKey, TEntity> cursor);
~~~
~~~csharp
var select = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.UserId)
    .ToCursor(10, 20)
    .CountDesc()
    .ToSelect();
// SELECT [UserId] FROM [UserRoles] GROUP BY [UserId] ORDER BY COUNT(*) DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 5. Select方法
>* 聚合筛选
>* 调用路径: GroupBy().ToCursor().ToSelect().Select()
~~~csharp
GroupByTableCursorSelect<TKey, TEntity> Select<TProperty>(Expression<Func<IGrouping<TKey, TEntity>, TProperty>> select);
~~~
~~~csharp
var select = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.UserId)
    .ToCursor(10, 20)
    .CountAsc()
    .ToSelect()
    .SelectKey()
    .Select(g => new { Score = g.Max(u => u.Score) });
// SELECT [UserId],MAX([Score]) AS Score FROM [UserRoles] GROUP BY [UserId] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[sql分组](../sqlquery/groupby.md)
>* 参看[逻辑分组](../query/groupby.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)