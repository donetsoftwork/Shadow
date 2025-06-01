# sql查询简介
>* 基于SqlQuery的实现类SqlAndQuery和SqlOrQuery
>* 按sql关键字where、having、on来查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对各查询和数据类型特殊处理,增强功能、增加易用性

## 1. 接口
>[IDataSqlQuery](xref:ShadowSql.Queries.IDataSqlQuery)

## 2. TableSqlQuery
>* [TableSqlQuery\<TTable\>](xref:ShadowSql.Expressions.Tables.TableSqlQuery%601)
>* [表查询](./table.md)

## 3. JoinOnSqlQuery
>* [JoinOnSqlQuery\<LTable, RTable\>](xref:ShadowSql.Expressions.Join.JoinOnSqlQuery%602)
>* [表关联查询](./joinon.md)

## 4. GroupByTableSqlQuery
>* [GroupByTableSqlQuery\<TKey, TEntity\>](xref:ShadowSql.Expressions.GroupBy.GroupByTableSqlQuery%602)
>* [分组查询](./groupby.md)

## 5. GroupByMultiSqlQuery
>* [GroupByMultiSqlQuery\<TKey\>](xref:ShadowSql.Expressions.GroupBy.GroupByMultiSqlQuery%601)
>* [联表分组](./groupbyjoin.md)

## 6. 扩展方法
```csharp
MultiTableSqlQuery Where<TEntity>(this MultiTableSqlQuery multiTable, string table, Expression<Func<TEntity, bool>> query);
MultiTableSqlQuery Where<TEntity, TParameter>(this MultiTableSqlQuery multiTable, string table, Expression<Func<TEntity, TParameter, bool>> query);
MultiTableSqlQuery Where<TEntity>(this MultiTableSqlQuery multiTable, Expression<Func<TEntity, bool>> query);
MultiTableSqlQuery Where<TEntity, TParameter>(this MultiTableSqlQuery multiTable, Expression<Func<TEntity, TParameter, bool>> query);
```

## 7. 其他通用功能
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/index.md)
