# 别名表删除
>* 从别名表中删除数据

## 1. 接口
>[IDelete](xref:ShadowSql.Delete.IDelete)

## 2. 类
>[AliasTableDelete](xref:ShadowSql.Delete.AliasTableDelete)

## 3. 相关方法
### 3.1 ToDelete扩展方法
```csharp
AliasTableDelete ToDelete<TEntity>(this IAliasTable table, Expression<Func<TEntity, bool>> query);
```
```csharp
var delete = EmptyTable.Use("Posts")
    .As("p")
    .ToDelete<Post>(p => p.Id == 1);
// DELETE FROM [Posts] AS p WHERE p.[Id]=1
```

### 3.2 ToDelete重载扩展方法
```csharp
AliasTableDelete ToDelete<TEntity, TParameter>(this IAliasTable table, Expression<Func<TEntity, TParameter, bool>> query);
```
```csharp
var delete = EmptyTable.Use("Users")
    .As("u")
    .ToDelete<User, User>((u, p) => u.Id == p.Id);
// DELETE FROM [Users]  AS u WHERE u.[Id]=@Id
```

### 3.3 ToDelete扩展方法
```csharp
AliasTableDelete ToDelete<TEntity>(this AliasTableSqlQuery<TEntity> query);
```
```csharp
var delete = EmptyTable.Use("Students")
    .As("s")
    .ToSqlQuery<Student>()
    .Where(s => s.Score < 60)
    .ToDelete();
// DELETE FROM [Students] AS s WHERE s.[Score]<60
```

### 3.4 ToDelete扩展方法
```csharp
AliasTableDelete ToDelete<TEntity>(this AliasTableQuery<TEntity> query);
```
```csharp
var delete = EmptyTable.Use("Students")
    .As("s")
    .ToQuery<Student>()
    .And(s => s.Score < 60)
    .ToDelete();
// DELETE FROM [Students] AS s WHERE [Score]<60
```

## 4. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[AliasTableDelete](xref:ShadowSql.Delete.AliasTableDelete)的扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/delete/alias.md)
