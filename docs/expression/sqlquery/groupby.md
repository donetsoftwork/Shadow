# sql分组查询
>* 按列分组并查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[IGroupByView](xref:ShadowSql.Identifiers.IGroupByView)

## 2. 基类
>[GroupByBase](xref:ShadowSql.GroupBy.GroupByBase)

## 3. 类
>[GroupByTableSqlQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableSqlQuery%602)

## 4. SqlGroupBy
>创建[GroupByTableSqlQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableSqlQuery%602)

### 4.1 SqlGroupBy扩展方法
>* 从表创建[GroupByTableSqlQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableSqlQuery%602)
```csharp
GroupByTableSqlQuery<TKey, TEntity> SqlGroupBy<TKey, TEntity>(this ITable table, Expression<Func<TEntity, TKey>> select);
```
```csharp
var query = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.UserId);
// SELECT * FROM [UserRoles] GROUP BY [UserId]
```

### 4.2 SqlGroupBy重载扩展方法
>* 从表和逻辑表达式创建[GroupByTableSqlQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableSqlQuery%602)
```csharp
GroupByTableSqlQuery<TKey, TEntity> SqlGroupBy<TKey, TEntity>(this ITable table, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> select);
```
```csharp
var query = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.Score >= 60, u => u.UserId);
// SELECT * FROM [UserRoles] WHERE [Score]>=60 GROUP BY [UserId]
```

### 4.3 SqlGroupBy重载扩展方法
>* 从[TableSqlQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableSqlQuery%601)创建[GroupByTableSqlQuery\<TTable\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableSqlQuery%602)
```csharp
GroupByTableSqlQuery<TKey, TEntity> SqlGroupBy<TKey, TEntity>(this TableSqlQuery<TEntity> SqlQuery, Expression<Func<TEntity, TKey>> select);
```
```csharp
var query = EmptyTable.Use("UserRoles")
    .ToSqlQuery<UserRole>()
    .Where(u => u.Score >= 60)
    .SqlGroupBy(u => u.UserId);
// SELECT * FROM [UserRoles] WHERE [Score]>=60 GROUP BY [UserId]
```

## 5. Having
### 5.1 Having方法
>* 聚合查询
```csharp
GroupByTableSqlQuery<TKey, TEntity> Having(Expression<Func<IGrouping<TKey, TEntity>, bool>> query);
```
```csharp
var query = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.UserId)
    .Having(g => g.Average(r => r.Score) > 60);
// SELECT * FROM [UserRoles] GROUP BY [UserId] HAVING AVG([Score])>60
```

### 5.1 Havingc重载方法
>* 聚合参数查询
```csharp
GroupByTableSqlQuery<TKey, TEntity> Having<TParameter>(Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query);
```
```csharp
var query = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.UserId)
    .Having<UserRole>((g, p) => g.Average(r => r.Score) > p.Score);
// SELECT * FROM [UserRoles] GROUP BY [UserId] HAVING AVG([Score])>@Score
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByTableSqlQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableSqlQuery%602)的方法部分
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/groupby.md)
