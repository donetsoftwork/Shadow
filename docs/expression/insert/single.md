# 插入单条
>* 本组件用来组装sql的INSERT语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [ISingleInsert](xref:ShadowSql.Insert.ISingleInsert)

## 2. 基类
>* [SingleInsertBase](xref:ShadowSql.Insert.SingleInsertBase)

## 3. 类型
>* [SingleInsert\<TEntity\>](xref:ShadowSql.Expressions.Insert.SingleInsert%601)

## 4. ToInsert
### 4.1 ToInsert扩展方法
>从表创建SingleInsert
```csharp
SingleInsert<TEntity> ToInsert<TEntity>(this ITable table);
```
```csharp
var insert = EmptyTable.Use("Users")
    .ToInsert<User>()
    .Insert(() => new User { Name = "张三", Age = 18 });
// INSERT INTO [Users]([Name],[Age])VALUES('张三',18)
```

### 4.2 ToInsert重载扩展方法
>从表创建SingleInsert
```csharp
SingleInsert<TEntity> ToInsert<TEntity>(this ITable table, Expression<Func<TEntity>> select);
```
```csharp
var insert = EmptyTable.Use("Users")
    .ToInsert(() => new User { Name = "张三", Age = 18 });
// INSERT INTO [Users]([Name],[Age])VALUES('张三',18)
```

### 4.3 ToInsert重载扩展方法
>从表创建SingleInsert
```csharp
SingleInsert<TEntity> ToInsert<TParameter, TEntity>(this ITable table, Expression<Func<TParameter, TEntity>> select);
```
```csharp
var insert = EmptyTable.Use("Users")
    .ToInsert<UserParameter, User>(p => new User { Name = p.Name2, Age = p.Age2 });
// INSERT INTO [Users]([Name],[Age])VALUES(@Name,@Age)
```

## 5. Insert
### 5.1 Insert方法
```csharp
SingleInsert<TEntity> Insert(Expression<Func<TEntity>> select);
```
```csharp
var insert = EmptyTable.Use("Users")
    .ToInsert<User>()
    .Insert(() => new User { Name = "张三", Age = 18 });
// INSERT INTO [Users]([Name],[Age])VALUES('张三',18)
```

### 5.2 Insert方法
```csharp
SingleInsert<TEntity> Insert<TParameter>(Expression<Func<TParameter, TEntity>> select);
```
```csharp
var insert = EmptyTable.Use("Users")
    .ToInsert<User>()
    .Insert<User>(u => new User { Name = u.Name, Age = u.Age });
// INSERT INTO [Users]([Name],[Age])VALUES(@Name,@Age)
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[SingleInsert\<TEntity\>](xref:ShadowSql.Expressions.Insert.SingleInsert%601)的方法和扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/insert/single.md)