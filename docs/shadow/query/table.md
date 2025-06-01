# 表逻辑查询
>从表中按逻辑查询数据

## 1. 接口
>* [IDataQuery](xref:ShadowSql.Queries.IDataQuery)

## 2. 类
>* [TableQuery\<TTable\>](xref:ShadowSql.Tables.TableQuery%601)

## 3. 从表创建查询
>* 从表创建[TableQuery\<TTable\>](xref:ShadowSql.Tables.TableQuery%601)

### 3.1 ToQuery扩展方法
>* 从表创建TableQuery\<TTable\>的AND查询
```csharp
TableQuery<TTable> ToQuery<TTable>(this TTable table)
        where TTable : ITable;
```
```csharp
var users = new UserTable();
var query = users.ToQuery()
    .And(users.Id.LessValue(100));
// SELECT * FROM [Users] WHERE Id<100
```

### 3.2 ToOrQuery扩展方法
>* 从表创建TableQuery\<TTable\>的OR查询
```csharp
TableQuery<TTable> ToOrQuery<TTable>(this TTable table)
        where TTable : ITable;
```
```csharp
var query = _db.From("Users")
    .ToOrQuery()
    .Or(_id.Equal())
    .Or(_status.Equal("Status"));
// SELECT * FROM [Users] WHERE Id=@Id OR Status=@Status
```

## 4. And方法
```csharp
TableQuery<TTable> And(Func<TTable, AtomicLogic> query);
```
```csharp
var query = new UserTable()
    .ToQuery()
    .And(user => user.Id.Less("LastId"));
// SELECT * FROM [Users] WHERE [Id]<@LastId
```

## 5. Or方法
```csharp
TableQuery<TTable> Or(Func<TTable, AtomicLogic> query);
```
```csharp
var query = new UserTable()
    .ToOrQuery()
    .Or(user => user.Id.Equal())
    .Or(_status.Equal("Status"));
// SELECT * FROM [Users] WHERE Id=@Id OR Status=@Status
```

## 6. Apply方法
>* 操作Logic的高阶函数
>* 也可称开窗函数,把内部的表和Logic开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来关联查询
>* 查询子函数标记static性能更好
```csharp
TableQuery<TTable> Apply(Func<Logic, TTable, Logic> query);
```
```csharp
var query = new UserTable()
    .ToQuery()
    .Apply(static (q, u) => q
        .And(u.Id.Less("LastId"))
        .And(u.Status.EqualValue(true))
    );
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableQuery\<TTable\>](xref:ShadowSql.Tables.TableQuery%601)的方法和扩展方法部分
>* 参看[查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/query/index.md)