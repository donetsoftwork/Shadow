# 逻辑分组查询
>* 按列分组并查询
>* sql的GROUP BY和HAVING部分
>* 按And、Or来操作

## 1. 接口
>* [IGroupByView](xref:ShadowSql.Identifiers.IGroupByView)

## 2. 基类
>* [GroupByBase](xref:ShadowSql.GroupBy.GroupByBase)

## 3. 类
>* [GroupByTableQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableQuery%602)

## 4. GroupBy
>创建[GroupByTableQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableQuery%602)

### 4.1 GroupBy扩展方法
>* 从表创建[GroupByTableQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableQuery%602)
```csharp
GroupByTableQuery<TKey, TEntity> GroupBy<TKey, TEntity>(this ITable table, Expression<Func<TEntity, TKey>> select);
```
```csharp
var query = EmptyTable.Use("UserRoles")
    .GroupBy<int, UserRole>(u => u.UserId);
// SELECT * FROM [UserRoles] GROUP BY [UserId]
```

### 4.2 GroupBy重载扩展方法
>* 从表和逻辑表达式创建[GroupByTableQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableQuery%602)
```csharp
GroupByTableQuery<TKey, TEntity> GroupBy<TKey, TEntity>(this ITable table, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> select);
```
```csharp
var query = EmptyTable.Use("UserRoles")
    .GroupBy<int, UserRole>(u => u.Score >= 60, u => u.UserId);
// SELECT * FROM [UserRoles] WHERE [Score]>=60 GROUP BY [UserId]
```

### 4.3 GroupBy重载扩展方法
>* 从[TableQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableQuery%601)创建[GroupByTableQuery\<TTable\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableQuery%602)
```csharp
GroupByTableQuery<TKey, TEntity> GroupBy<TKey, TEntity>(this TableQuery<TEntity> Query, Expression<Func<TEntity, TKey>> select);
```
```csharp
var query = EmptyTable.Use("UserRoles")
    .ToQuery<UserRole>()
    .And(u => u.Score >= 60)
    .GroupBy(u => u.UserId);
// SELECT * FROM [UserRoles] WHERE [Score]>=60 GROUP BY [UserId]
```

## 5. And方法
```csharp
GroupByTableQuery<TKey, TEntity> And(Expression<Func<IGrouping<TKey, TEntity>, bool>> query);
```
```csharp
var query = EmptyTable.Use("UserRoles")
    .GroupBy<int, UserRole>(u => u.UserId)
    .And(g => g.Average(r => r.Score) > 60);
// SELECT * FROM [UserRoles] GROUP BY [UserId] HAVING AVG([Score])>60
```

## 6. Or方法
```csharp
GroupByTableQuery<TKey, TEntity> Or(Expression<Func<IGrouping<TKey, TEntity>, bool>> query);
```
```csharp
var query = EmptyTable.Use("Users")
    .GroupBy<string?, User>(u => u.Belief)
    .Or(g => g.Min(r => r.Age) < 18 || g.Max(r => r.Age) > 60);
// SELECT * FROM [Users] GROUP BY [Belief] HAVING MIN([Age])<18 OR MAX([Age])>60
```

## 7. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByTableQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableQuery%602)的方法和扩展方法部分
>* 参看[逻辑查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/query/groupby.md)