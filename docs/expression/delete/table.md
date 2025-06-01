# 表删除
>* 从表中删除数据

## 1. 接口
>[IDelete](xref:ShadowSql.Delete.IDelete)

## 2. 类
>[TableDelete](xref:ShadowSql.Delete.TableDelete)

## 3. 相关方法
### 3.1 ToDelete扩展方法
```csharp
TableDelete ToDelete<TEntity>(this ITable table, Expression<Func<TEntity, bool>> query);
```
```csharp
var delete = EmptyTable.Use("Posts")
    .ToDelete<Post>(p => p.Id == 1);
// DELETE FROM [Posts] WHERE [Id]=1
```

### 3.2 ToDelete重载扩展方法
```csharp
TableDelete ToDelete<TEntity, TParameter>(this ITable table, Expression<Func<TEntity, TParameter, bool>> query);
```
```csharp
var delete = EmptyTable.Use("Users")
    .ToDelete<User, User>((u, p) => u.Id == p.Id);
// DELETE FROM [Users] WHERE [Id]=@Id
```

### 3.3 ToDelete扩展方法
```csharp
TableDelete ToDelete<TEntity>(this TableSqlQuery<TEntity> query);
```
```csharp
var delete = new TableSqlQuery<Student>("Students")
    .Where(s => s.Score < 60)
    .ToDelete();
// DELETE FROM [Students] WHERE [Score]<60
```

### 3.4 ToDelete扩展方法
```csharp
TableDelete ToDelete<TEntity>(this TableQuery<TEntity> query);
```
```csharp
var delete = new TableQuery<Student>("Students")
    .And(s => s.Score < 60)
    .ToDelete();
// DELETE FROM [Students] WHERE [Score]<60
```

## 4. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableDelete](xref:ShadowSql.Delete.TableDelete)的扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/delete/table.md)
