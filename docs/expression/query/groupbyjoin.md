# sql联表分组
>* 按列分组并查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [IGroupByView](xref:ShadowSql.Identifiers.IGroupByView)

## 2. 基类
>* [GroupByBase](xref:ShadowSql.GroupBy.GroupByBase)

## 3. 类
>* [GroupByMultiQuery](xref:ShadowSql.GroupBy.GroupByMultiQuery)

## 4. GroupBy扩展方法
>从多、联表创建[GroupByMultiQuery\<TKey\>](xref:ShadowSql.Expressions.GroupBy.GroupByMultiQuery%601)
```csharp
GroupByMultiQuery<TKey> GroupBy<TLeft, TRight, TKey>(this JoinOnQuery<TLeft, TRight> joinOn, Expression<Func<TLeft, TRight, TKey>> select);
```
```csharp
var query = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .GroupBy((u, r) => r.UserId);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId]
```

## 5. And方法
>* 聚合查询

```csharp
GroupByMultiQuery<TKey> And<TEntity>(string table, Expression<Func<IGrouping<TKey, TEntity>, bool>> query);
```
```csharp
var query = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .GroupBy((u, r) => r.UserId)
    .And<User>("Users", g => g.Average(u => u.Age) > 18);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] HAVING AVG(t1.[Age])>18
```

## 6. Or方法
>* 聚合查询

```csharp
GroupByMultiQuery<TKey> Or<TEntity>(string table, Expression<Func<IGrouping<TKey, TEntity>, bool>> query);
```
```csharp
var query = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .And((u, r) => u.Id == r.UserId)
    .GroupBy((u, r) => r.UserId)
    .Or<User>("Users", g => g.Average(u => u.Age) > 18)
    .Or<UserRole>("UserRoles", g => g.Average(u => u.Score) > 60);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] HAVING AVG(t1.[Age])>18 OR AVG(t2.[Score])>60
```

## 7. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByMultiQuery\<TKey>](xref:ShadowSql.Expressions.GroupBy.GroupByMultiQuery%601)的方法和扩展方法部分
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/query/groupby.md)
