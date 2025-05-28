# sql联表查询
>* 联表查询数据
>* 联表由多个别名表联接而成
>* 在[多表sql查询](./multi.md)基础上增加了JoinOn
>* 需要[sql联表关联](./joinon.md)配合使用
>* WHERE查询部分与[多表sql查询](./multi.md)类似

## 1. 接口
>* [IJoinTable](xref:ShadowSql.Identifiers.IJoinTable)
>* [IMultiView](xref:ShadowSql.Identifiers.IMultiView)
>* [IDataSqlQuery](xref:ShadowSql.Queries.IDataSqlQuery)

## 2. 基类
>[MultiTableBase](xref:ShadowSql.Join.MultiTableBase)

## 3. 类
>[JoinTableSqlQuery](xref:ShadowSql.Join.JoinTableSqlQuery)

## 4. 示例
### 4.1 自定义别名表
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var joinOn = JoinOnSqlQuery.Create(c, p)
    .On(c.PostId, p.Id);
JoinTableSqlQuery query = joinOn.Root
    .Where(c.Pick.EqualValue(true))
    .Where(p.Author.EqualValue("张三"));
```
>SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'

### 4.2 默认表名字段名
```csharp
var joinOn = JoinOnSqlQuery.Create("Comments", "Posts")
    .OnColumn("PostId", "Id");
JoinTableSqlQuery query = joinOn.Root
    .Where("t1", c => c.Field("Pick").EqualValue(true))
    .Where("t2", p => p.Field("Author").EqualValue("张三"));
```
>SELECT * FROM [Comments] AS t1,[Posts] AS t2 WHERE t1.[Pick]=1 AND t2.[Author]='张三' AND t1.PostId=t2.Id

### 4.3 使用表名生成别名表 
```csharp
var c = EmptyTable.Use("Comments")
    .As("c");
var p = EmptyTable.Use("Posts")
    .As("p");
var joinOn = JoinOnSqlQuery.Create(c, p)
    .OnColumn("PostId", "Id");
JoinTableSqlQuery query = joinOn.Root
    .Where(c.Field("Pick").EqualValue(true))
    .Where(p.Field("Author").EqualValue("张三"));
```
>SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'

### 4.4 使用默认别名表
```csharp
var joinOn = JoinOnSqlQuery.Create("Comments", "Posts")
    .OnColumn("PostId", "Id");
IAliasTable t1 = joinOn.Left;
IAliasTable t2 = joinOn.Source;
JoinTableSqlQuery query = joinOn.Root
    .Where(t1.Field("Pick").EqualValue(true))
    .Where(t2.Field("Author").EqualValue("张三"));
```
>SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1 AND t2.[Author]='张三'

## 5. 其他相关功能
>* 参看[sql查询简介](./index.md)