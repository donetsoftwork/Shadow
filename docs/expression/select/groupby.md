# 表分组
>* 从表分组查询获取数据组件
>* 依赖[sql分组](../sqlquery/groupby.md)或[逻辑分组](../query/groupby.md)
>* 本组件用来处理sql的SELECT语句
>* 本组件是对ShadowSql.Core同名组件的扩展

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)
>* [IGroupBySelect](xref:ShadowSql.Expressions.Select.IGroupBySelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>* [GroupByTableSelect\<TKey, TEntity\>](xref:ShadowSql.Expressions.Select.GroupByTableSelect%602)

## 4. ToSelect
>* 创建[GroupByTableSelect\<TKey, TEntity\>](xref:ShadowSql.Expressions.Select.GroupByTableSelect%602)

### 4.1 ToSelect扩展方法
>* 从sql分组获取
>* 依赖[sql分组](../sqlquery/groupby.md)
>* 筛选的默认字段为当前分组字段
~~~csharp
GroupByTableSelect<TKey, TEntity> ToSelect<TKey, TEntity>(this GroupByTableSqlQuery<TKey, TEntity> source);
~~~
~~~csharp
var select = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.UserId)
    .ToSelect();
// SELECT [UserId] FROM [UserRoles] GROUP BY [UserId]
~~~

### 4.2 ToSelect重载扩展方法
>* 从逻辑分组获取
>* 依赖[逻辑分组](../query/groupby.md)
>* 筛选的默认字段为当前分组字段
~~~csharp
GroupByTableSelect<TKey, TEntity> ToSelect<TKey, TEntity>(this GroupByTableQuery<TKey, TEntity> source);
~~~
~~~csharp
var select = EmptyTable.Use("UserRoles")
    .GroupBy<int, UserRole>(u => u.UserId)
    .ToSelect();
// SELECT [UserId] FROM [UserRoles] GROUP BY [UserId]
~~~

## 5. Select方法
>* 聚合筛选
~~~csharp
GroupByTableSelect<TKey, TEntity> Select<TProperty>(Expression<Func<IGrouping<TKey, TEntity>, TProperty>> select);
~~~
~~~csharp
var select = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.UserId)
    .ToSelect()
    .SelectKey()
    .Select(g => new { Score = g.Max(u => u.Score) });
// SELECT [UserId],MAX([Score]) AS Score FROM [UserRoles] GROUP BY [UserId]
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[sql分组](../sqlquery/groupby.md)
>* 参看[逻辑分组](../query/groupby.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)
