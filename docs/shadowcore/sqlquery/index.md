# sql查询简介
>* 基于[SqlQuery](/api/ShadowSql.Queries.SqlQuery.html)的实现类SqlAndQuery和SqlOrQuery
>* [SqlAndQuery](/api/ShadowSql.Queries.SqlAndQuery.html)实现AND连接查询
>* [SqlOrQuery](/api/ShadowSql.Queries.SqlOrQuery.html)实现OR连接查询
>* ToOr从AND切换到OR
>* ToAnd从OR切换到AND
>* 按sql关键字where、having、on来操作
>* 支持按原生sql查询

## 1. 接口
>[IDataSqlQuery](/api/ShadowSql.Queries.IDataSqlQuery.html)

## 2. Where查询
>* Where扩展方法
>* 与sql关键字where相同,用于对表、多表、联表等进行查询
>* 用于实现了IWhere接口的类
```csharp
Query Where<Query>(this Query query, params IEnumerable<string> conditions)
	where Query : IDataSqlQuery, IWhere;
Query Where<Query>(this Query query, AtomicLogic logic)
	where Query : IDataSqlQuery, IWhere;
Query Where<Query>(this Query query, Func<ITableView, AtomicLogic> logic)
	where Query : IDataSqlQuery, IWhere;
Query Where<Query>(this Query query, Func<SqlQuery, SqlQuery> where)
	where Query : IDataSqlQuery, IWhere;
```

## 3. Having查询
>* Having扩展方法
>* 与sql关键字having相同,用于分组查询
```csharp
TGroupBy Having<TGroupBy>(this TGroupBy groupBy, params IEnumerable<string> conditions)
	where TGroupBy : GroupByBase, IDataSqlQuery;
TGroupBy Having<TGroupBy>(this TGroupBy groupBy, AtomicLogic logic)
	where TGroupBy : GroupByBase, IDataSqlQuery;
TGroupBy Having<TGroupBy>(this TGroupBy groupBy, Func<IGroupByView, AtomicLogic> query)
	where TGroupBy : GroupByBase, IDataSqlQuery;
TGroupBy Having<TGroupBy>(this TGroupBy groupBy, Func<SqlQuery, SqlQuery> query)
	where TGroupBy : GroupByBase, IDataSqlQuery;
TGroupBy Having<TGroupBy>(this TGroupBy groupBy, Func<IGroupByView, SqlQuery, SqlQuery> query)
	where TGroupBy : GroupByBase, IDataSqlQuery;
TGroupBy HavingAggregate<TGroupBy>(this TGroupBy groupBy, Func<IGroupByView, IAggregateField> select, Func<IAggregateField, AtomicLogic> query)
	where TGroupBy : GroupByBase, IDataSqlQuery;
TGroupBy HavingAggregate<TGroupBy>(this TGroupBy groupBy, string aggregate, string columnName, Func<IAggregateField, AtomicLogic> query)
	where TGroupBy : GroupByBase, IDataSqlQuery;
```

## 4. On查询
>与sql关键字on相同,用于联表Join On查询
### 4.1 On扩展方法
```csharp
TJoinOn On<TJoinOn>(this TJoinOn joinOn, params IEnumerable<string> conditions)
	where TJoinOn : JoinOnBase, IDataSqlQuery;
TjoinOn On<TjoinOn>(this TjoinOn joinOn, AtomicLogic logic)
	where TjoinOn : JoinOnBase, IDataSqlQuery;
TjoinOn On<TjoinOn>(this TjoinOn joinOn, Func<IJoinOn, AtomicLogic> query)
	where TjoinOn : JoinOnBase, IJoinOn, IDataSqlQuery;
TjoinOn On<TjoinOn>(this TjoinOn joinOn, Func<IAliasTable, IAliasTable, AtomicLogic> query)
	where TjoinOn : JoinOnBase, IJoinOn, IDataSqlQuery;
TJoinOn On<TJoinOn>(this TJoinOn joinOn, Func<SqlQuery, SqlQuery> query)
	where TJoinOn : JoinOnBase, IDataSqlQuery;
TJoinOn On<TJoinOn>(this TJoinOn joinOn, Func<IJoinOn, SqlQuery, SqlQuery> query)
	where TJoinOn : JoinOnBase, IDataSqlQuery, IJoinOn;
TJoinOn On<TJoinOn>(this TJoinOn joinOn, IPrefixColumn left, IPrefixColumn right)
	where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery;
TJoinOn On<TJoinOn>(this TJoinOn joinOn, IPrefixColumn left, CompareSymbol compare, IPrefixColumn right)
	where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery;
TJoinOn On<TJoinOn>(this TJoinOn joinOn, IColumn left, IColumn right)
	where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery;
TJoinOn On<TJoinOn>(this TJoinOn joinOn, IColumn left, CompareSymbol compare, IColumn right)
	where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery;
```
### 4.2 OnColumn扩展方法
```csharp
TJoinOn OnColumn<TJoinOn>(this TJoinOn joinOn, string leftColumn, string rightColumn)
	where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery;
TJoinOn OnColumn<TJoinOn>(this TJoinOn joinOn, string leftColumn, string op, string rightColumn)
	where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery;
```

## 5. AND/OR查询
>* 实现AND/OR切换
>* 实现AND/OR嵌套
## 5.1 ToOr扩展方法
>* 用于切换为Or
>* 用于Or嵌套And
>* 如果前面是And连接,调用ToOr后切换为Or
```csharp
Query ToOr<Query>(this Query query)
	where Query : IDataSqlQuery;
```

## 5.2 ToAnd扩展方法
>* 用于切换为And
>* 用于And嵌套Or
>* 如果前面是Or连接,调用ToAnd后切换为And
```csharp
Query And<Query>(this Query query, AtomicLogic logic)
	where Query : FilterBase, IDataQuery;
```

## 6. Apply扩展方法
>* 操作SqlQuery的高阶函数
>* 也可称开窗函数,把Query内部成员ITableView和SqlQuery开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来查询
>* 查询子函数标记static性能更好
```csharp
Query Apply<Query>(this Query query, Func<SqlQuery, SqlQuery> where)
	where Query : IDataSqlQuery;
Query Apply<Query>(this Query query, Func<ITableView, SqlQuery, SqlQuery> where)
	where Query : IDataSqlQuery;
TMultiTable Apply<TMultiTable>(this TMultiTable multiTable, Func<SqlQuery, IMultiView, SqlQuery> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery;
TMultiTable Apply<TMultiTable>(this TMultiTable multiTable, string tableName, Func<SqlQuery, IAliasTable, SqlQuery> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery;
TJoinOn Apply<TJoinOn>(this TJoinOn joinOn, Func<SqlQuery, IJoinOn, SqlQuery> query)
        where TJoinOn : JoinOnBase, IDataSqlQuery, IJoinOn;
JoinOnSqlQuery Apply(this JoinOnSqlQuery query, Func<SqlQuery, IAliasTable, IAliasTable,  SqlQuery> on);
TGroupBy Apply<TGroupBy>(this TGroupBy groupBy, Func<SqlQuery, IGroupByView, SqlQuery> query)
        where TGroupBy : GroupByBase, IDataSqlQuery;
```
```csharp
var query = new TableSqlQuery("Users")
    .Apply(static (u, q) => q
        .And(u.Field("Id").Less("LastId"))
        .And(u.Field("Status").EqualValue(true))
    );
// SELECT * FROM [Users] WHERE [Id]=@Id AND [Status]=1
```
