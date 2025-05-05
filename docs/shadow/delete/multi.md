# 联表删除
>* 从联表中某个表删除数据

## 1. 接口
>[IDelete](xref:ShadowSql.Delete.IDelete)

## 2. 类
>[MultiTableDelete](xref:ShadowSql.Delete.MultiTableDelete)

## 3. 相关方法
### 3.1 ToDelete扩展方法
```csharp
MultiTableDelete ToDelete(this IMultiView view);
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var delete = JoinOnSqlQuery.Create(c, p)
    .On(c.PostId.Equal(p.Id))
    .Root
    .Where(p.Author.EqualValue("张三"))
    .ToDelete();
// DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三'
```

### 3.2 Delete扩展方法
>* 可选方法,用于指定删除数据的表
>* 默认删除第一张表的数据
>* 如果删除第一张表的数据可以不指定

```csharp
TDelete Delete<TDelete>(this TDelete delete, IAliasTable table)
    where TDelete : MultiTableDelete;
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var delete = JoinOnSqlQuery.Create(c, p)
    .On(c.PostId.Equal(p.Id))
    .Root
    .Where(p.Author.EqualValue("张三"))
    .ToDelete()
    .Delete(c);
// DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三'
```

### 3.3 Delete重载扩展方法
>* 可选方法,用于指定删除数据的表
>* 默认删除第一张表的数据
>* 如果删除第一张表的数据可以不指定

```csharp
TDelete Delete<TDelete>(this TDelete delete, string tableName)
    where TDelete : MultiTableDelete;
```
```csharp
var delete = JoinOnSqlQuery.Create("Comments", "Posts")
    .On((c,p) => c.Field("PostId").Equal(p.Field("Id")))
    .Root
    .Where("Posts", p =>  p.Field("Author").EqualValue("张三"))
    .ToDelete()
    .Delete("Comments");
// DELETE t1 FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t2.[Author]='张三'
```

## 4. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[MultiTableDelete](xref:ShadowSql.Delete.MultiTableDelete)的扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/delete/multi.md)