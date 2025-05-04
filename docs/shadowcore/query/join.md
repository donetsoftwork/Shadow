# 联表逻辑查询
>* 联表查询数据
>* 联表由多个别名表联接而成
>* 在[多表逻辑查询](./multi.md)基础上增加了JoinOn
>* 需要[逻辑联表关联](./joinon.md)配合使用
>* WHERE查询部分与[多表逻辑查询](./multi.md)类似

## 1. 接口
>* [IJoinTable](/api/ShadowSql.Identifiers.IJoinTable.html)
>* [IMultiView](/api/ShadowSql.Identifiers.IMultiView.html)
>* [IDataQuery](/api/ShadowSql.Queries.IDataQuery.html)

## 2. 基类
>[MultiTableBase](/api/ShadowSql.Join.MultiTableBase.html)

## 3. 类
>[JoinTableQuery](/api/ShadowSql.Join.JoinTableQuery.html)

## 4. 示例
### 4.1 自定义别名表
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var joinOn = JoinOnQuery.Create(c, p)
    .And(c.PostId.Equal(p.Id));
JoinTableQuery query = joinOn.Root
    .And(c.Pick.EqualValue(true))
    .And(p.Author.EqualValue("张三"));
```
>SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'

### 4.2 默认表名字段名
```csharp
var joinOn = JoinOnQuery.Create("Comments", "Posts")
    .Apply((t1, t2, logic) => logic.And(t1.Field("PostId").Equal(t2.Field("Id"))));
JoinTableQuery query = joinOn.Root
    .Apply("t1", static (c, q) => q.And(c.Field("Pick").EqualValue(true)))
    .Apply("t2", static (p, q) => q.And(p.Field("Author").EqualValue("张三")));
```
>SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1 AND t2.[Author]='张三'

### 4.3 使用表名生成别名表
```csharp
var c = SimpleTable.Use("Comments")
    .As("c");
var p = SimpleTable.Use("Posts")
    .As("p");
var joinOn = JoinOnQuery.Create(c, p)
    .And(c.Field("PostId").Equal(p.Field("Id")));
JoinTableQuery query = joinOn.Root
    .And(c.Field("Pick").EqualValue(true))
    .And(p.Field("Author").EqualValue("张三"));
```
>SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'

## 5. 其他相关功能
>* 参看[逻辑查询简介](./index.md)