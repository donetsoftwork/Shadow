# sql多表查询
>* 从多张表中按sql查询数据
>* 是一种简化的联表查询,没有JOIN关键字
>* 多表由多个别名表组成

## 1. 接口
>* [IMultiView](xref:ShadowSql.Identifiers.IMultiView)
>* [IDataSqlQuery](xref:ShadowSql.Queries.IDataSqlQuery)

## 2. 基类
>[MultiTableBase](xref:ShadowSql.Join.MultiTableBase)

## 3. 类
>[MultiTableSqlQuery](xref:ShadowSql.Join.MultiTableSqlQuery)

## 4. 方法
### 4.1 CreateMember扩展方法
>* 该方法适用多表和联表
>* sql和逻辑查询都支持
```csharp
TableAlias<TTable> CreateMember<MultiTable, TTable>(this MultiTable multiTable, TTable table)
        where MultiTable : MultiTableBase, IMultiView
        where TTable : ITable;
```
```csharp
var multiTable = new MultiTableSqlQuery();
var e = multiTable.CreateMember(_db.From("Employees"));
var d = multiTable.CreateMember(_db.From("Departments"));
multiTable.Where(e.Field("DepartmentId").Equal(d.Field("Id")));
```
>SELECT * FROM [Employees] AS t1,[Departments] AS t2 WHERE t1.[DepartmentId]=t2.[Id]

### 4.2 AddMembers
#### 4.2.1 AddMembers扩展方法
>* 该方法适用多表和联表
>* sql和逻辑查询都支持
>
```csharp
MultiTable AddMembers<MultiTable>(this MultiTable multiTable, params IEnumerable<IAliasTable> aliasTables)
    where MultiTable : MultiTableBase;
```
```csharp
var e = _db.From("Employees")
    .As("e");
var d = _db.From("Departments")
    .As("d");
var multiTable = new MultiTableSqlQuery()
    .AddMembers(e, d)
    .Where(e.Field("DepartmentId").Equal(d.Field("Id")));
```
>SELECT * FROM [Employees] AS e,[Departments] AS d WHERE e.[DepartmentId]=d.[Id]

#### 4.2.2 AddMembers重载扩展方法
>* 该方法适用多表和联表
>* sql和逻辑查询都支持
```csharp
MultiTable AddMembers<MultiTable>(this MultiTable multiTable, params IEnumerable<ITable> tables)
    where MultiTable : MultiTableBase, IMultiView;
```
```csharp
var multiTable = new MultiTableSqlQuery()
    .AddMembers(_db.From("Employees"), _db.From("Departments"))
    .Where("t1.DepartmentId=t2.Id");
```
>SELECT * FROM [Employees] AS t1,[Departments] AS t2 WHERE t1.DepartmentId=t2.Id

#### 4.2.3 AddMembers重载扩展方法
>* 该方法适用多表和联表
>* sql和逻辑查询都支持
```csharp
MultiTable AddMembers<MultiTable>(this MultiTable multiTable, params IEnumerable<string> tableNames)
    where MultiTable : MultiTableBase, IMultiView;
```
```csharp
var multiTable = new MultiTableSqlQuery()
    .AddMembers("Employees", "Departments")
    .Where("t1.DepartmentId=t2.Id");
```
>SELECT * FROM [Employees] AS t1,[Departments] AS t2 WHERE t1.DepartmentId=t2.Id

## 5.示例
### 5.1 自定义别名表
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var multiTable = new MultiTableSqlQuery()
    .AddMembers(c, p)
    .Where(c.PostId.Equal(p.Id))
    .Where(c.Pick.EqualValue(true))
    .Where(p.Author.EqualValue("张三"));
```
>SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id] AND c.[Pick]=1 AND p.[Author]='张三'

### 5.2 自定义表
```csharp
CommentTable c = new();
PostTable p = new();
var multiTable = new MultiTableSqlQuery()
    .AddMembers(c.As("c"), p.As("p"))
    .Where("c.PostId=p.Id")
    .Where<CommentTable>("c", c => c.Pick, Pick => Pick.EqualValue(true))
    .Where<PostTable>("p", p => p.Author, Author => Author.EqualValue("张三"));
```
>SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[Pick]=1 AND p.[Author]='张三' AND c.PostId=p.Id

### 5.3 使用表名字段名
### 5.3.1 默认表名字段名
```csharp
var multiTable = new MultiTableSqlQuery()
    .AddMembers("Comments", "Posts")
    .Where("t1.PostId=t2.Id")
    .Where("t1", c => c.Field("Pick").EqualValue(true))
    .Where("t2", p => p.Field("Author").EqualValue("张三"));
```
>SELECT * FROM [Comments] AS t1,[Posts] AS t2 WHERE t1.[Pick]=1 AND t2.[Author]='张三' AND t1.PostId=t2.Id

### 5.3.2 使用表名生成别名表 
```csharp
var c = SimpleTable.Use("Comments")
    .As("c");
var p = SimpleTable.Use("Posts")
    .As("p");
var multiTable = new MultiTableSqlQuery()
    .AddMembers(c, p)
    .Where(c.Field("PostId").Equal(p.Field("Id")))
    .Where(c.Field("Pick").EqualValue(true))
    .Where(p.Field("Author").EqualValue("张三"));
```
>SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id] AND c.[Pick]=1 AND p.[Author]='张三'

### 5.3.3 使用默认别名表
```csharp
var multiTable = new MultiTableSqlQuery()
    .AddMembers("Comments", "Posts");
IAliasTable t1 = multiTable.From("Comments");
IAliasTable t2 = multiTable.From("Posts");
var query = multiTable
    .Where(t1.Field("PostId").Equal(t2.Field("Id")))
    .Where(t1.Field("Pick").EqualValue(true))
    .Where(t2.Field("Author").EqualValue("张三"));
```
>SELECT * FROM [Comments] AS t1,[Posts] AS t2 WHERE t1.[PostId]=t2.[Id] AND t1.[Pick]=1 AND t2.[Author]='张三'

## 6. 其他相关功能
>* 参看[sql查询简介](./index.md)