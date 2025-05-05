# sql查询简介
>* 基于SqlQuery的实现类SqlAndQuery和SqlOrQuery
>* 按sql关键字where、having、on来查询
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对各查询和数据类型特殊处理,增强功能、增加易用性

## 1. 接口
>[IDataSqlQuery](xref:ShadowSql.Queries.IDataSqlQuery)

## 2. 按列查询
>* [按列名查询扩展](./columnquery.md)

## 3. 按字段查询
>* [按字段名查询扩展](./fieldquery.md)

## 4. TableSqlQuery
>* [TableSqlQuery\<TTable\>](xref:ShadowSql.Tables.TableSqlQuery%601)
>* [表查询](./table.md)

## 5. MultiTableSqlQuery
>* [MultiTableSqlQuery](xref:ShadowSql.Join.MultiTableSqlQuery)
>* [多表查询](./multi.md)

## 6. JoinOnSqlQuery
>* [JoinOnSqlQuery\<LTable, RTable\>](xref:ShadowSql.Join.JoinOnSqlQuery%602)
>* [表关联查询](./joinon.md)

## 7. AliasJoinOnSqlQuery
>* [AliasJoinOnSqlQuery\<TLeft, TRight\>](xref:ShadowSql.Join.AliasJoinOnSqlQuery%602)
>* [别名表关联查询](./aliasjoinon.md)

## 8. JoinTableSqlQuery
>* [JoinTableSqlQuery](xref:ShadowSql.Join.JoinTableSqlQuery)]
>* [联表查询](./join.md)

## 9. GroupByTableSqlQuery
>* [GroupByTableSqlQuery\<TTable\>](xref:ShadowSql.GroupBy.GroupByTableSqlQuery%601)
>* [分组查询](./groupby.md)

## 10. GroupByMultiSqlQuery
>* [GroupByMultiSqlQuery](xref:ShadowSql.GroupBy.GroupByMultiSqlQuery)
>* [联表分组](./groupbyjoin.md)

## 11. 扩展方法
### 11.1 Where扩展方法
```csharp
TMultiTable Where<TMultiTable>(this TMultiTable multiTable, Func<IMultiView, AtomicLogic> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery;
TMultiTable Where<TMultiTable>(this TMultiTable multiTable, string tableName, Func<IAliasTable, AtomicLogic> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery;
MultiTableSqlQuery Where<TTable>(this MultiTableSqlQuery multiTable, string tableName, Func<TTable, IColumn> select, Func<IColumn, AtomicLogic> query)
        where TTable : ITable;
```

### 11.2 Apply扩展方法
```csharp
TMultiTable Apply<TMultiTable>(this TMultiTable multiTable, string tableName, Func<SqlQuery, IAliasTable, SqlQuery> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery;
```

## 12. 其他通用功能
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/index.md)
