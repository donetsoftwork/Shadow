# 联表分组
>* 从联表分组查询获取数据组件
>* 依赖[sql联表分组](../sqlquery/groupbyjoin.md)或[逻辑联表分组](../query/groupbyjoin.md)
>* 本组件用来处理sql的SELECT语句
>* 本组件是对ShadowSql.Core同名组件的扩展

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)
>* [IGroupBySelect](xref:ShadowSql.Expressions.Select.IGroupBySelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>* [GroupByMultiSelect\<TKey\>](xref:ShadowSql.Expressions.Select.GroupByMultiSelect%601)

## 4. ToSelect
>* 创建[GroupByMultiSelect\<TKey\>](xref:ShadowSql.Expressions.Select.GroupByMultiSelect%601)

### 4.1 ToSelect扩展方法
>* 从sql联表分组获取
>* 依赖[sql联表分组](../sqlquery/groupbyjoin.md)
>* 筛选的默认字段为当前分组字段
~~~csharp
GroupByMultiSelect<TKey> ToSelect<TKey>(this GroupByMultiSqlQuery<TKey> groupBy);
~~~
~~~csharp
var select = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On(u => u.Id, r => r.UserId)
    .SqlGroupBy((u, r) => r.UserId)
    .ToSelect();
// SELECT t2.[UserId] FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId]
~~~

### 4.2 ToSelect重载扩展方法
>* 从逻辑分组获取
>* 依赖[逻辑联表分组](../query/groupbyjoin.md)
>* 筛选的默认字段为当前分组字段
~~~csharp
GroupByMultiSelect<TKey> ToSelect<TKey>(this GroupByMultiQuery<TKey> groupBy);
~~~
~~~csharp
var select = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .And((u, r) => u.Id == r.UserId)
    .GroupBy((u, r) => r.UserId)
    .ToSelect();
// SELECT t2.[UserId] FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId]
~~~

## 5. Select方法
>* 聚合筛选
>* 从别名表聚合
~~~csharp
GroupByMultiSelect<TKey> Select<TEntity, TProperty>(string table, Expression<Func<IGrouping<TKey, TEntity>, TProperty>> select);
~~~
~~~csharp
var select = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .SqlGroupBy((u, r) => r.UserId)
    .ToSelect()
    .SelectKey()
    .Select<User, object>("t1", g => new { MaxAge = g.Max((User u) => u.Age) });
// SELECT t2.[UserId],MAX(t1.[Age]) AS MaxAge FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId]
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[sql联表分组](../sqlquery/groupbyjoin.md)
>* 参看[逻辑联表分组](../query/groupbyjoin.md)
>* 参看[聚合](../../shadowcore/fields/aggregate.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)