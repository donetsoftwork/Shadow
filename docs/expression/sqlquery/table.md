# sql表查询
>* 从表中按sql查询数据
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [IDataSqlQuery](xref:ShadowSql.Queries.IDataSqlQuery)
>* [IWhere](xref:ShadowSql.Filters.IWhere)

## 2. 类
>[TableSqlQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableSqlQuery%601)

## 3. 从表创建查询
>* 从表创建[TableSqlQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableSqlQuery%601)
### 3.1 ToSqlQuery扩展方法
>* 从表创建TableSqlQuery\<TEntity\>的AND查询
```csharp
TableSqlQuery<TEntity> ToSqlQuery<TEntity>(this ITable table);
```
```csharp
var query = new Table("User")
    .ToSqlQuery<User>()
    .Where(u => u.Name == "张三");
// SELECT * [User] WHERE [Name]='张三'
```

### 3.2 ToSqlOrQuery扩展方法
>* 从表创建TableSqlQuery\<TEntity\>的OR查询
```csharp
TableSqlQuery<TEntity> ToSqlOrQuery<TEntity>(this ITable table);
```
```csharp
var query = new Table("User")
    .ToSqlOrQuery<User>()
    .Where(u => u.Age > 18)
    .Where(u => u.Status);
// SELECT * FROM [User] WHERE [Age]>18 OR [Status]=1
```

## 4. Where
### 4.1 Where方法
```csharp
TableSqlQuery<TEntity> Where(Expression<Func<TEntity, bool>> query);
```
```csharp
var query = new TableSqlQuery<User>()
    .Where(u => u.Name == "张三");
// SELECT * [User] WHERE [Name]='张三'
```

### 4.2 Where重载方法
```csharp
TableSqlQuery<TEntity> Where<TParameter>(Expression<Func<TEntity, TParameter, bool>> query);
```
```csharp
var query = new TableSqlQuery<User>()
    .Where<UserParameter>((u, p) =>  u.Age > p.Age2);
// SELECT * [User] WHERE [Age]>@Age2
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableSqlQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableSqlQuery%601)的方法部分
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/index.md)
