# 逻辑查询简介
>* 基于[Logic](xref:ShadowSql.Logics.Logic)的实现类AndLogic、OrLogic、ComplexAndLogic及ComplexOrLogic等
>* 按And、Or来操作
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对各查询和数据类型特殊处理,增强功能、增加易用性

## 1. 接口
>IDataQuery

## 2. TableQuery
>* [TableQuery\<TTable\>](xref:ShadowSql.Tables.TableQuery%601)
>* [表查询](./table.md)

## 3. MultiTableQuery
>* [MultiTableQuery](xref:ShadowSql.Join.MultiTableQuery)
>* [多表查询](./multi.md)

## 4. JoinOnQuery
>* [JoinOnQuery\<LTable, RTable\>](xref:ShadowSql.Join.JoinOnQuery%602)
>* [联表关联查询](./joinon.md)

## 5. AliasJoinOnQuery
>* [AliasJoinOnQuery\<TLeft, TRight\>](xref:ShadowSql.Join.AliasJoinOnQuery%602)
>* [别名表关联查询](./aliasjoinon.md)

## 6. JoinTableQuery
>* [JoinTableQuery](xref:ShadowSql.Join.JoinTableQuery)]
>* [联表查询](./join.md)

## 7. GroupByTableQuery
>* [GroupByTableQuery\<TTable\>](xref:ShadowSql.GroupBy.GroupByTableQuery%601)
>* [分组查询](./groupby.md)

## 8. GroupByMultiQuery
>* [GroupByMultiQuery](xref:ShadowSql.GroupBy.GroupByMultiQuery)
>* [联表分组](./groupbyjoin.md)

## 9. Apply扩展方法
```csharp
TMultiTable Apply<TMultiTable>(this TMultiTable multiTable, string tableName, Func<Logic, IAliasTable, Logic> logic)
        where TMultiTable : MultiTableBase, IDataQuery;
```

## 10. 其他通用功能
>* [参看ShadowSqlCore相关文档](../../shadowcore/query/index.md)
