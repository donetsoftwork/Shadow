# 逻辑查询简介
>* 基于[Logic](xref:ShadowSql.Logics.Logic)的实现类AndLogic、OrLogic、ComplexAndLogic及ComplexOrLogic等
>* [AndLogic](xref:ShadowSql.Logics.AndLogic)实现AND连接逻辑
>* [OrLogic](xref:ShadowSql.Logics.OrLogic)实现OR连接逻辑
>* [ComplexAndLogic](xref:ShadowSql.Logics.ComplexAndLogic)实现AND连接并支持嵌套OR查询
>* [ComplexOrLogic](xref:ShadowSql.Logics.ComplexOrLogic)实现OR连接并支持嵌套AND查询
>* 按And、Or来操作

## 1. 接口
>IDataQuery

## 2. 方法
### 2.1 And扩展方法
```csharp
Query And<Query>(this Query query, AtomicLogic logic)
	where Query : FilterBase, IDataQuery;
Query And<Query>(this Query query, AndLogic logic)
	where Query : FilterBase, IDataQuery;
Query And<Query>(this Query query, ComplexAndLogic logic)
	where Query : FilterBase, IDataQuery;
Query And<Query>(this Query query, OrLogic logic)
	where Query : FilterBase, IDataQuery;
Query And<Query>(this Query query, ComplexOrLogic logic)
	where Query : FilterBase, IDataQuery;
Query And<Query>(this Query query, Logic logic)
	where Query : FilterBase, IDataQuery;
Query And<Query>(this Query query, Func<ITableView, AtomicLogic> logic)
	where Query : FilterBase, IDataQuery;
Query And<Query>(this Query query, Func<ITableView, OrLogic> logic)
	where Query : FilterBase, IDataQuery
```
```csharp
var users = new UserTable();
var query = new TableQuery(users)
     .And(users.Id.LessValue(100));
// SELECT * FROM [Users] WHERE [Id]<100
```
```csharp
var users = new UserTable();
AndLogic andLogic = users.Id.Equal() & users.Status.Equal("Status");
var query = new TableQuery(users)
    .And(andLogic);
// SELECT * FROM [Users] WHERE [Id]=@Id AND [Status]=@Status
```

### 2.2 Or扩展方法
>如果主要是OR查询,构造函数可以传一个空的Or查询对象来提高性能
```csharp
Query Or<Query>(this Query query, AtomicLogic logic)
    where Query : FilterBase, IDataQuery;
Query Or<Query>(this Query query, AndLogic logic)
    where Query : FilterBase, IDataQuery;
Query Or<Query>(this Query query, ComplexAndLogic logic)
    where Query : FilterBase, IDataQuery;
Query Or<Query>(this Query query, OrLogic logic)
    where Query : FilterBase, IDataQuery;
Query Or<Query>(this Query query, ComplexOrLogic logic)
    where Query : FilterBase, IDataQuery;
Query Or<Query>(this Query query, Logic logic)
    where Query : FilterBase, IDataQuery;
Query Or<Query>(this Query query, Func<ITableView, AtomicLogic> logic)
    where Query : FilterBase, IDataQuery;
Query Or<Query>(this Query query, Func<ITableView, AndLogic> logic)
    where Query : FilterBase, IDataQuery;
```
```csharp
var users = new UserTable();
var query = new TableQuery("Users", new OrLogic())
     .Or(users.Id.Equal())
     .Or(users.Status.Equal("Status"));
// SELECT * FROM [Users] WHERE [Id]=@Id OR [Status]=@Status
```
```csharp
var users = new UserTable();
var query = new TableQuery(users, new OrLogic())
    .Or(users.Id.LessValue(100) | users.Status.EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]<100 OR [Status]=1
```

### 2.3 Apply扩展方法
>* 操作Logic的高阶函数
>* 也可称开窗函数,把Query内部成员ITableView和Logic开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来查询
>* 查询子函数标记static性能更好
```csharp
Query Apply<Query>(this Query query, Func<Logic, Logic> logic)
    where Query : FilterBase, IDataQuery
Query Apply<Query>(this Query query, Func<ITableView, Logic, Logic> logic)
    where Query : FilterBase, IDataQuery;
```
```csharp
var query = new TableQuery("Users")
    .Apply(static (u, q) => q
        .And(u.Field("Id").Equal())
        .And(u.Field("Status").EqualValue(true))
    );
// SELECT * FROM [Users] WHERE [Id]=@Id AND [Status]=1
```
