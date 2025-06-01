# 表逻辑查询
>从表中按逻辑查询数据

## 1. 接口
>* [IDataQuery](xref:ShadowSql.Queries.IDataQuery)

## 2. 类
>* [TableQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableQuery%601)

## 3. 从表创建查询
>* 从表创建[TableQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableQuery%601)

### 3.1 ToQuery扩展方法
>* 从表创建[TableQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableQuery%601)的AND查询
```csharp
TableQuery<TEntity> ToQuery<TEntity>(this ITable table);
```
```csharp
var query = new Table("User")
    .ToQuery<User>()
    .And(u => u.Name == "张三");
// SELECT * FROM [User] WHERE [Name]='张三'
```

### 3.2 ToOrQuery扩展方法
>* 从表创建[TableQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableQuery%601)的OR查询
```csharp
TableQuery<TEntity> ToOrQuery<TEntity>(this ITable table);
```
```csharp
var query = new Table("User")
    .ToOrQuery<User>()
    .Or(u => u.Age > 18)
    .Or(u => u.Status);
// SELECT * FROM [User] WHERE [Age]>18 OR [Status]=1
```
## 4. And
### 4.1 And方法
```csharp
TableQuery<TEntity> And(Expression<Func<TEntity, bool>> query);
```
```csharp
var query = new TableQuery<User>()
    .ToQuery<User>()
    .And(u => u.Name == "张三");
// SELECT * FROM [User] WHERE [Name]='张三'
```

### 4.2 And重载方法
```csharp
TableQuery<TEntity> And<TParameter>(Expression<Func<TEntity, TParameter, bool>> query);
```
```csharp
var query = new TableQuery<User>()
    .And<UserParameter>((u, p) => p.Age2 > u.Age);
// SELECT * FROM [User] WHERE @Age2>[Age]
```

## 5. Or
### 5.1 Or方法
```csharp
TableQuery<TEntity> Or(Expression<Func<TEntity, bool>> query);
```
```csharp
var query = new TableQuery<User>()
    .ToOrQuery<User>()
    .Or(u => u.Age > 18)
    .Or(u => u.Status);
// SELECT * FROM [User] WHERE [Age]>18 OR [Status]=1
```

### 5.2 Or重载方法
```csharp
TableQuery<TEntity> Or<TParameter>(Expression<Func<TEntity, TParameter, bool>> query);
```
```csharp
var query = new TableQuery<User>()
    .Or<UserParameter>((u, p) => u.Age > p.Age2 || u.Id == p.Id2);
// SELECT * FROM [User] WHERE [User] WHERE [Age]>@Age2 OR [Id]=@Id2
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableQuery\<TEntity\>](xref:ShadowSql.Expressions.Tables.TableQuery%601)的方法和扩展方法部分
>* 参看[查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/query/index.md)