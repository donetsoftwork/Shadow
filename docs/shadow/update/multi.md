# 联表更新
>更新联表中某个表的数据

## 1. 接口
>[IUpdate](/api/shadowcore/update/IUpdate.html)

## 2. 类
>[MultiTableUpdate](/api/shadowcore/update/MultiTableUpdate.html)

## 3. 相关方法
### 3.1 ToUpdate扩展方法
```csharp
MultiTableUpdate ToUpdate(this IMultiView view);
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var update = JoinOnSqlQuery.Create(c, p)
    .On(c.PostId.Equal(p.Id))
    .Root
    .Where(p.Author.EqualValue("张三"))
    .Where(c.Pick.EqualValue(false))
    .ToUpdate()
    .Set(c.Pick.EqualToValue(true));
// UPDATE c SET c.[Pick]=1 FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三' AND c.[Pick]=0
```

### 3.3 Set扩展方法
>此扩展方法联表更新专用
```csharp
TMultiUpdate Set<TMultiUpdate>(this TMultiUpdate update, Func<IAliasTable, IAssignOperation> operation)
    where TMultiUpdate : MultiTableUpdate;
```
```csharp
var joinOn = JoinOnSqlQuery.Create("Comments", "Posts")
    .On((t1, t2) => t1.Field("PostId").Equal(t2.Field("Id")));
var query = joinOn.Root
    .Where("Posts", t2 => t2.Field("Author").EqualValue("张三"))
    .Where("Comments", t1 => t1.Field("Pick").EqualValue(false));
var update = query.ToUpdate()
    .Update("Comments")
    .Set(t1 => t1.Field("Pick").EqualToValue(true));
// UPDATE t1 SET t1.[Pick]=1 FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t2.[Author]='张三' AND t1.[Pick]=0
```

### 3.3 Update扩展方法
>* 可选方法,用于指定更新数据的表
>* 默认更新第一张表的数据
>* 如果更新第一张表的数据可以不指定

```csharp
TMultiUpdate Update<TMultiUpdate>(this TMultiUpdate update, IAliasTable table)
    where TMultiUpdate : MultiTableUpdate;
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var joinOn = JoinOnSqlQuery.Create(c, p)
    .On(c.PostId.Equal(p.Id));
var query = joinOn.Root
    .Where(p.Author.EqualValue("张三"))
    .Where(c.Pick.EqualValue(false));
var update = query.ToUpdate()
    .Update(c)
    .Set(c.Pick.EqualToValue(true));
// UPDATE c SET c.[Pick]=1 FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三' AND c.[Pick]=0
```

### 3.4 Update重载扩展方法
>* 可选方法,用于指定更新数据的表
>* 默认更新第一张表的数据
>* 如果更新第一张表的数据可以不指定

```csharp
MultiTableUpdate Update<TMultiUpdate>(this TMultiUpdate update, string tableName)
    where TMultiUpdate : MultiTableUpdate;
```
```csharp
var joinOn = JoinOnSqlQuery.Create("Comments", "Posts");
var (t1, t2) = (joinOn.Left, joinOn.Source);
joinOn.On(t1.Field("PostId").Equal(t2.Field("Id")));
var query = joinOn.Root
    .Where(t2.Field("Author").EqualValue("张三"))
    .Where(t1.Field("Pick").EqualValue(false));
var update = query.ToUpdate()
    .Update("Comments")
    .Set(t1.Field("Pick").EqualToValue(true));
// UPDATE t1 SET t1.[Pick]=1 FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t2.[Author]='张三' AND t1.[Pick]=0
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[MultiTableUpdate](/api/shadowcore/update/MultiTableUpdate.html)的方法和扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/update/multi.md)