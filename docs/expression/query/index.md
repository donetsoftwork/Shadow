# 逻辑查询简介
>* 基于[Logic](xref:ShadowSql.Logics.Logic)
>* 按And、Or来操作
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对各查询和数据类型特殊处理,增强功能、增加易用性

## 1. 接口
>IDataQuery

## 2. TableQuery
>* [TableQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableQuery%601)
>* [表查询](./table.md)

## 3. JoinOnQuery
>* [JoinOnQuery\<TLeft, TRight\>](xref:ShadowSql.Expressions.Join.JoinOnQuery%602)
>* [联表关联查询](./joinon.md)

## 4. GroupByTableQuery
>* [GroupByTableQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableQuery%602)
>* [分组查询](./groupby.md)

## 5. GroupByMultiQuery
>* [GroupByMultiQuery\<TKey>](xref:ShadowSql.Expressions.GroupBy.GroupByMultiQuery%601)
>* [联表分组](./groupbyjoin.md)

## 6. And扩展方法
```csharp
JoinTableQuery And<TEntity>(this JoinTableQuery multiTable, string table, Expression<Func<TEntity, bool>> query);
MultiTableQuery And<TEntity>(this MultiTableQuery multiTable, string table, Expression<Func<TEntity, bool>> query);
JoinTableQuery And<TEntity>(this JoinTableQuery multiTable, Expression<Func<TEntity, bool>> query);
MultiTableQuery And<TEntity>(this MultiTableQuery multiTable, Expression<Func<TEntity, bool>> query);
```

## 7. Or扩展方法
```csharp
JoinTableQuery Or<TEntity>(this JoinTableQuery multiTable, string table, Expression<Func<TEntity, bool>> query);
MultiTableQuery Or<TEntity>(this MultiTableQuery multiTable, string table, Expression<Func<TEntity, bool>> query);
JoinTableQuery Or<TEntity>(this JoinTableQuery multiTable, Expression<Func<TEntity, bool>> query);
MultiTableQuery Or<TEntity>(this MultiTableQuery multiTable, Expression<Func<TEntity, bool>> query);
```

## 8. 其他通用功能
>* [参看ShadowSqlCore相关文档](../../shadowcore/query/index.md)
