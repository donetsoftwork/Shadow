# sql联表分组
>* 按列分组并查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[IGroupByView](xref:ShadowSql.Identifiers.IGroupByView)

## 2. 基类
>[GroupByBase](xref:ShadowSql.GroupBy.GroupByBase)

## 3. 类
>[GroupByMultiSqlQuery\<TKey\>](xref:ShadowSql.Expressions.GroupBy.GroupByMultiSqlQuery%601)

## 4. SqlGroupBy扩展方法
>从多、联表创建[GroupByMultiSqlQuery\<TKey\>](xref:ShadowSql.Expressions.GroupBy.GroupByMultiSqlQuery%601)
```csharp
GroupByMultiQuery<TKey> SqlGroupBy<TLeft, TRight, TKey>(this JoinOnSqlQuery<TLeft, TRight> joinOn, Expression<Func<TLeft, TRight, TKey>> select);
```
```csharp
var query = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .SqlGroupBy((u, r) => r.UserId);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId]
```

## 5. Having
>* 聚合查询

## 5.1 Having方法
```csharp
GroupByMultiSqlQuery<TKey> Having<TEntity>(string table, Expression<Func<IGrouping<TKey, TEntity>, bool>> query);
```
```csharp
var query = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .SqlGroupBy((u, r) => r.UserId)
    .Having<User>("Users", g => g.Average(u => u.Age) > 18);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] HAVING AVG(t1.[Age])>18
```

## 5.2 Having重载方法
```csharp
GroupByMultiSqlQuery<TKey> Having<TEntity, TParameter>(string table, Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query);
```
```csharp
var query = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .SqlGroupBy((u, r) => r.UserId)
    .Having<User, User>("Users", (g, p) => g.Average(u => u.Age) > p.Age);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] HAVING AVG(t1.[Age])>@Age
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByMultiSqlQuery\<TKey\>](xref:ShadowSql.Expressions.GroupBy.GroupByMultiSqlQuery%601)的方法和扩展方法部分
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/groupby.md)
