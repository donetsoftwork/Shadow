# sql表查询
>* 从表中按sql查询数据
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [IDataSqlQuery](xref:ShadowSql.Queries.IDataSqlQuery)
>* [IWhere](xref:ShadowSql.Filters.IWhere)

## 2. 类
>*[TableSqlQuery\<TTable\>](xref:ShadowSql.Tables.TableSqlQuery%601)

## 3. 从表创建查询
>* 从表创建[TableSqlQuery\<TTable\>](xref:ShadowSql.Tables.TableSqlQuery%601)

### 3.1 ToSqlQuery扩展方法
>* 从表创建TableSqlQuery\<TTable\>的AND查询
```csharp
TableSqlQuery<TTable> ToSqlQuery<TTable>(this TTable table)
        where TTable : ITable;
```
```csharp
var users = new UserTable();
var query = users.ToSqlQuery()
    .Where(users.Id.LessValue(100));
// SELECT * FROM [Users] WHERE Id<100
```

### 3.2 ToSqlOrQuery扩展方法
>* 从表创建TableSqlQuery\<TTable\>的OR查询
```csharp
TableSqlQuery<TTable> ToSqlOrQuery<TTable>(this TTable table)
        where TTable : ITable;
```
```csharp
var query = _db.From("Users")
    .ToSqlOrQuery()
    .Where("Id=@Id", "Status=@Status");
// SELECT * FROM [Users] WHERE Id=@Id OR Status=@Status
```

## 4. Where方法
```csharp
TableSqlQuery<TTable> Where(Func<TTable, AtomicLogic> query);
```
```csharp
var query = new UserTable()
    .ToSqlQuery()
    .Where(user => user.Id.Less("LastId"));
// SELECT * FROM [Users] WHERE [Id]<@LastId
```

## 5. Apply方法
>* 操作Logic的高阶函数
>* 也可称开窗函数,把内部的表和Logic开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来关联查询
>* 查询子函数标记static性能更好
```csharp
TableSqlQuery<TTable> Apply(Func<SqlQuery, TTable, SqlQuery> query);
```
```csharp
var query = new UserTable()
    .ToSqlQuery()
    .Apply(static (q, u) => q
        .And(u.Id.Less("LastId"))
        .And(u.Status.EqualValue(true))
    );
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableSqlQuery\<TTable\>](xref:ShadowSql.Tables.TableSqlQuery%601)的方法和扩展方法部分
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/index.md)
