# 联表分组游标
>* 对联表分组进行截取,处理分页和排序
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [ICursor](xref:ShadowSql.Cursors.ICursor)

## 2. 基类
>* [CursorBase](xref:ShadowSql.Cursors.CursorBase)

## 3. 类
>* [GroupByMultiCursor\<TKey\>](xref:ShadowSql.Expressions.Cursors.GroupByMultiCursor%601)

## 4. ToCursor
### 4.1 ToCursor扩展方法
>从sql联表分组创建游标
~~~csharp
GroupByMultiCursor ToCursor(this GroupByMultiSqlQuery groupBy, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On(u => u.Id, r => r.UserId)
    .SqlGroupBy((u, r) => r.UserId)
    .ToCursor()
    .CountAsc();
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*)
~~~

### 4.2 ToCursor重载扩展方法
>从逻辑联表分组创建游标
~~~csharp
GroupByMultiCursor ToCursor(this GroupByMultiQuery groupBy, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .SqlGroupBy((u, r) => r.UserId)
    .ToCursor()
    .CountDesc();
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*) DESC
~~~

## 5. Take
>* Take方法是ToCursor的平替
### 5.1 Take扩展方法
>从sql联表分组创建游标
~~~csharp
GroupByMultiCursor Take(this GroupByMultiSqlQuery groupBy, int limit, int offset = 0);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On(u => u.Id, r => r.UserId)
    .SqlGroupBy((u, r) => r.UserId)
    .Take()
    .CountAsc();
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*)
~~~

### 5.2 Take重载扩展方法
>从逻辑联表分组创建游标
~~~csharp
GroupByMultiCursor Take(this GroupByMultiQuery groupBy, int limit, int offset = 0);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .SqlGroupBy((u, r) => r.UserId)
    .Take()
    .CountDesc();
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*) DESC
~~~

## 6. 按表聚合排序
### 6.1 Asc方法
>* 聚合正序
~~~csharp
GroupByMultiCursor<TKey> Asc<TEntity, TOrder>(string table, Expression<Func<IGrouping<TKey, TEntity>, TOrder>> select);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .And((u, r) => u.Id == r.UserId)
    .GroupBy((u, r) => r.UserId)
    .ToCursor()
    .Asc<UserRole, int>("UserRoles", g => g.Sum(r => r.Score));
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY SUM(t2.[Score])
~~~

### 6.2 Desc方法
>* 聚合倒序
~~~csharp
GroupByMultiCursor<TKey> Desc<TEntity, TOrder>(string table, Expression<Func<IGrouping<TKey, TEntity>, TOrder>> select);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .And((u, r) => u.Id == r.UserId)
    .GroupBy((u, r) => r.UserId)
    .ToCursor()
    .Desc<UserRole, int>("UserRoles", g => g.Sum(r => r.Score));
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY SUM(t2.[Score]) DESC
~~~

## 8. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByMultiCursor\<TKey\>](xref:ShadowSql.Expressions.Cursors.GroupByMultiCursor%601)的方法和扩展方法部分
>* 参看[游标简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/cursor/index.md)
