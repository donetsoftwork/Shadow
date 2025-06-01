# 单表更新
>更新单表中数据

## 1. 接口
>* [IUpdate](xref:ShadowSql.Update.IUpdate)

## 2. 类
>* [TableUpdate\<TEntity\>](xref:ShadowSql.Expressions.Update.TableUpdate%601)

## 3. ToUpdate
### 3.1 ToUpdate扩展方法
```csharp
TableUpdate<TEntity> ToUpdate<TEntity>(this ITable table, Expression<Func<TEntity, bool>> query);
```
```csharp
var update = new Table("Users")
    .ToUpdate<User>(u => u.Id == 1)
    .Set(u => new User { Age = 18 });
// UPDATE [Users] SET [Age]=18 WHERE [Id]=1
```

### 3.2 ToUpdate重载扩展方法
```csharp
TableUpdate<TEntity> ToUpdate<TEntity, TParameter>(this ITable table, Expression<Func<TEntity, TParameter, bool>> query);
```
```csharp
var update = new Table("Users")
    .ToUpdate<User, User>((u, p) => u.Id == p.Id)
    .Set(u => new User { Age = 18 });
// UPDATE [Users] SET [Age]=18 WHERE [Id]=@Id
```

### 3.3 ToUpdate重载扩展方法
```csharp
TableUpdate<TEntity> ToUpdate<TEntity>(this TableSqlQuery<TEntity> query);
```
```csharp
var update = new Table("Users")
    .ToSqlQuery<User>()
    .Where(u => u.Id == 1)
    .ToUpdate()
    .Set(u => new User { Age = 18 });
// UPDATE [Users] SET [Age]=18 WHERE [Id]=1
```

### 3.3 ToUpdate重载扩展方法
```csharp
TableUpdate<TEntity> ToUpdate<TEntity>(this TableQuery<TEntity> query);
```
```csharp
var update = new Table("Users")
    .ToQuery<User>()
    .And(u => u.Id == 1)
    .ToUpdate()
    .Set(u => new User { Age = 18 });
// UPDATE [Users] SET [Age]=18 WHERE [Id]=1
```

## 4. Set方法
```csharp
TableUpdate<TEntity> Set(Expression<Func<TEntity, TEntity>> operation);
```
```csharp
var update = new Table("Students")
    .ToUpdate<Student>(u => u.Score < 60 && u.Score > 55)
    .Set(u => new Student { Score = u.Score + 5 });
// UPDATE [Students] SET [Score]=([Score]+5) WHERE [Score]<60 AND [Score]>55
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableUpdate\<TEntity\>](xref:ShadowSql.Expressions.Update.TableUpdate%601)的方法和扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/update/table.md)