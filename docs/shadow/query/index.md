# 逻辑查询简介
>* 基于[Logic](/api/ShadowSql.Logics.Logic.html)的实现类AndLogic、OrLogic、ComplexAndLogic及ComplexOrLogic等
>* 按And、Or来操作
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对各查询和数据类型特殊处理,增强功能、增加易用性

## 1. 接口
>IDataQuery

## 2. TableQuery
>* [TableQuery\<TTable\>](/api/ShadowSql.Tables.TableQuery-1.html)
>* [表查询](./table.md)

## 3. MultiTableQuery
>* [MultiTableQuery](/api/ShadowSql.Join.MultiTableQuery.html)
>* [多表查询](./multi.md)

## 4. JoinOnQuery
>* [JoinOnQuery\<LTable, RTable\>](/api/ShadowSql.Join.JoinOnQuery-2.html)
>* [联表关联查询](./joinon.md)

## 5. AliasJoinOnQuery
>* [AliasJoinOnQuery\<TLeft, TRight\>](/api/ShadowSql.Join.AliasJoinOnQuery-2.html)
>* [别名表关联查询](./aliasjoinon.md)

## 6. JoinTableQuery
>* [JoinTableQuery](/api/ShadowSql.Join.JoinTableQuery.html)]
>* [联表查询](./join.md)

## 7. GroupByTableQuery
>* [GroupByTableQuery\<TTable\>](/api/ShadowSql.GroupBy.GroupByTableQuery-1.html)
>* [分组查询](./groupby.md)

## 8. GroupByMultiQuery
>* [GroupByMultiQuery](/api/ShadowSql.GroupBy.GroupByMultiQuery.html)
>* [联表分组](./groupbyjoin.md)

## 9. Apply扩展方法
```csharp
TMultiTable Apply<TMultiTable>(this TMultiTable multiTable, string tableName, Func<Logic, IAliasTable, Logic> logic)
        where TMultiTable : MultiTableBase, IDataQuery;
```

## 10. 其他通用功能
>* [参看ShadowSqlCore相关文档](../../shadowcore/query/index.md)
