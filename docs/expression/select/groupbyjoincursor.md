# 联表分组分页
>* 从联表分组分页获取数据组件
>* 本组件用来处理sql的SELECT分页语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性
>* 本组件通过[联表分组游标](../cursor/groupbyjoin.md)来分页

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)
>* [IGroupBySelect](xref:ShadowSql.Expressions.Select.IGroupBySelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>* [GroupByMultiCursorSelect\<TKey\>](xref:ShadowSql.Expressions.CursorSelect.GroupByMultiCursorSelect%601)

## 4. ToSelect扩展方法
>* 从[联表分组游标](../cursor/groupbyjoin.md)创建[GroupByMultiCursorSelect\<TKey\>](xref:ShadowSql.Expressions.CursorSelect.GroupByMultiCursorSelect%601)
>* 先联表、分组、分页再筛选
>* 调用路径: GroupBy().ToCursor().ToSelect()
>* 筛选的默认字段为当前分组字段
```csharp
GroupByMultiCursorSelect<TKey> ToSelect<TKey>(this GroupByMultiCursor<TKey> cursor);
```
```csharp
var select = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On(u => u.Id, r => r.UserId)
    .SqlGroupBy((u, r) => r.UserId)
    .ToCursor()
    .CountAsc()
    .ToSelect();
// SELECT t2.[UserId] FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*)
```

## 5. Select方法
>* 聚合筛选
>* 从别名表聚合
~~~csharp
GroupByMultiCursorSelect<TKey> Select<TEntity, TProperty>(string table, Expression<Func<IGrouping<TKey, TEntity>, TProperty>> select);
~~~
~~~csharp
var select = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .SqlGroupBy((u, r) => r.UserId)
    .ToCursor()
    .CountDesc()
    .ToSelect()
    .SelectKey()
    .Select<User, object>("t1", g => new { MaxAge = g.Max(u => u.Age) });
// SELECT t2.[UserId],MAX(t1.[Age]) AS MaxAge FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*) DESC
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[联表分组游标](../cursor/groupbyjoin.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)