# 多表逻辑查询
>* 从多张表中按逻辑查询数据
>* 是一种简化的联表查询,没有JOIN关键字
>* 多表由多个别名表组成

## 1. 接口
>* [IMultiView](xref:ShadowSql.Identifiers.IMultiView)
>* [IDataQuery](xref:ShadowSql.Queries.IDataQuery)

## 2. 基类
>[MultiTableBase](xref:ShadowSql.Join.MultiTableBase)

## 3. 类
>[MultiTableQuery](xref:ShadowSql.Join.MultiTableQuery)

## 4. 方法
### 4.1 CreateMember扩展方法
>* 该方法适用多表和联表
>* sql和逻辑查询都支持
```csharp
TableAlias<TTable> CreateMember<MultiTable, TTable>(this MultiTable multiTable, TTable table)
        And MultiTable : MultiTableBase, IMultiView
        And TTable : ITable;
```
```csharp
var multiTable = new MultiTableQuery();
var e = multiTable.CreateMember(_db.From("Employees"));
var d = multiTable.CreateMember(_db.From("Departments"));
multiTable.And(e.Field("DepartmentId").Equal(d.Field("Id")));
```
>SELECT * FROM [Employees] AS t1,[Departments] AS t2 And t1.[DepartmentId]=t2.[Id]

### 4.2 AddMembers
#### 4.2.1 AddMembers扩展方法
>* 该方法适用多表和联表
>* sql和逻辑查询都支持
>
```csharp
MultiTable AddMembers<MultiTable>(this MultiTable multiTable, params IEnumerable<IAliasTable> aliasTables)
    And MultiTable : MultiTableBase;
```
```csharp
var e = _db.From("Employees")
    .As("e");
var d = _db.From("Departments")
    .As("d");
var multiTable = new MultiTableQuery()
    .AddMembers(e, d)
    .And(e.Field("DepartmentId").Equal(d.Field("Id")));
```
>SELECT * FROM [Employees] AS e,[Departments] AS d And e.[DepartmentId]=d.[Id]


#### 4.2.2 AddMembers重载扩展方法
>* 该方法适用多表和联表
>* sql和逻辑查询都支持
```csharp
MultiTable AddMembers<MultiTable>(this MultiTable multiTable, params IEnumerable<ITable> tables)
    And MultiTable : MultiTableBase, IMultiView;
```
```csharp
var multiTable = new MultiTableQuery()
    .AddMembers(_db.From("Employees"), _db.From("Departments"))
    .And("t1.DepartmentId=t2.Id");
```
>SELECT * FROM [Employees] AS t1,[Departments] AS t2 And t1.DepartmentId=t2.Id

#### 4.2.3 AddMembers重载扩展方法
>* 该方法适用多表和联表
>* sql和逻辑查询都支持
```csharp
MultiTable AddMembers<MultiTable>(this MultiTable multiTable, params IEnumerable<string> tableNames)
    And MultiTable : MultiTableBase, IMultiView;
```
```csharp
var multiTable = new MultiTableQuery()
    .AddMembers("Employees", "Departments")
    .And("t1.DepartmentId=t2.Id");
```
>SELECT * FROM [Employees] AS t1,[Departments] AS t2 And t1.DepartmentId=t2.Id

### 4.3 Apply
>* 操作Logic的高阶函数
>* 也可称开窗函数,把内部的表和Logic开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来关联查询
>* 查询子函数标记static性能更好
```csharp
TMultiTable Apply<TMultiTable>(this TMultiTable multiTable, Func<IMultiView, Logic, Logic> query)
    where TMultiTable : MultiTableBase, IDataQuery;
TMultiTable Apply<TMultiTable>(this TMultiTable multiTable, string tableName, Func<IAliasTable, Logic, Logic> query)
    where TMultiTable : MultiTableBase, IDataQuery;
```
```csharp
var query = new MultiTableQuery()
    .AddMembers("Comments", "Posts")
    .Apply(static (tables, q) => q.And(tables.From("Comments").Field("PostId").Equal(tables.From("Posts").Field("Id"))))
    .Apply("Comments", static (t1, q) => q.And(t1.Field("Pick").EqualValue(true)))
    .Apply("Posts", static (t2, q) => q.And(t2.Field("Author").EqualValue("张三")));
```
>SELECT * FROM [Comments] AS t1,[Posts] AS t2 WHERE t1.[PostId]=t2.[Id] AND t1.[Pick]=1 AND t2.[Author]='张三'

## 5.示例
### 5.1 自定义别名表
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var multiTable = new MultiTableQuery()
    .AddMembers(c, p)
    .And(c.PostId.Equal(p.Id))
    .And(c.Pick.EqualValue(true))
    .And(p.Author.EqualValue("张三"));
```
>SELECT * FROM [Comments] AS c,[Posts] AS p And c.[PostId]=p.[Id] AND c.[Pick]=1 AND p.[Author]='张三'

### 5.2 自定义表
```csharp
CommentTable c = new();
PostTable p = new();
var multiTable = new MultiTableQuery()
    .AddMembers(c.As("c"), p.As("p"))
    .And("c.PostId=p.Id")
    .And<CommentTable>("c", c => c.Pick, Pick => Pick.EqualValue(true))
    .And<PostTable>("p", p => p.Author, Author => Author.EqualValue("张三"));
```
>SELECT * FROM [Comments] AS c,[Posts] AS p And c.[Pick]=1 AND p.[Author]='张三' AND c.PostId=p.Id

### 5.3 使用表名字段名
### 5.3.1 默认表名字段名
```csharp
var multiTable = new MultiTableQuery()
    .AddMembers("Comments", "Posts")
    .And("t1.PostId=t2.Id")
    .And("t1", c => c.Field("Pick").EqualValue(true))
    .And("t2", p => p.Field("Author").EqualValue("张三"));
```
>SELECT * FROM [Comments] AS t1,[Posts] AS t2 And t1.[Pick]=1 AND t2.[Author]='张三' AND t1.PostId=t2.Id

### 5.3.2 使用表名生成别名表 
```csharp
var c = SimpleTable.Use("Comments")
    .As("c");
var p = SimpleTable.Use("Posts")
    .As("p");
var multiTable = new MultiTableQuery()
    .AddMembers(c, p)
    .And(c.Field("PostId").Equal(p.Field("Id")))
    .And(c.Field("Pick").EqualValue(true))
    .And(p.Field("Author").EqualValue("张三"));
```
>SELECT * FROM [Comments] AS c,[Posts] AS p And c.[PostId]=p.[Id] AND c.[Pick]=1 AND p.[Author]='张三'

### 5.3.3 使用默认别名表
```csharp
var multiTable = new MultiTableQuery()
    .AddMembers("Comments", "Posts");
IAliasTable t1 = multiTable.From("Comments");
IAliasTable t2 = multiTable.From("Posts");
var query = multiTable
    .And(t1.Field("PostId").Equal(t2.Field("Id")))
    .And(t1.Field("Pick").EqualValue(true))
    .And(t2.Field("Author").EqualValue("张三"));
```
>SELECT * FROM [Comments] AS t1,[Posts] AS t2 And t1.[PostId]=t2.[Id] AND t1.[Pick]=1 AND t2.[Author]='张三'

## 6. 其他相关功能
>* 参看[逻辑查询简介](./index.md)