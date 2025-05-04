# 逻辑分组查询
>* 按列分组并查询
>* sql的GROUP BY和HAVING部分
>* 按And、Or来操作

## 1. 接口
>[IGroupByView](/api/ShadowSql.Identifiers.IGroupByView.html)

## 2. 基类
>[GroupByBase](/api/ShadowSql.GroupBy.GroupByBase.html)

## 3. 类
>[GroupByQuery](/api//api/ShadowSql.GroupBy.GroupByQuery.html)

## 4. 方法
### 4.1 Create静态方法
#### 4.1.1 对table分组 
```csharp
GroupByQuery Create(ITable table, params IFieldView[] fields);
GroupByQuery Create(ITable table, params IEnumerable<string> columnNames);
```
```csharp
var table = _db.From("Users");
var groupBy = GroupByQuery.Create(table, "City");
// SELECT * FROM [Users] GROUP BY [City]
```

#### 4.1.2 对tableName分组 
```csharp
GroupByQuery Create(string tableName, params IFieldView[] fields);
GroupByQuery Create(string tableName, params IEnumerable<string> columnNames);
```
```csharp
var groupBy = GroupByQuery.Create("Users", "City");
// SELECT * FROM [Users] GROUP BY [City]
```

#### 4.1.3 先查询再分组
```csharp
GroupByQuery Create(IDataFilter filter, params IFieldView[] fields);
GroupByQuery Create(IDataFilter filter, params IEnumerable<string> columnNames);
```
```csharp
CommentTable table = new();
var query = new TableQuery(table)
    .And(table.Pick.EqualValue(true));
var groupBy = GroupByQuery.Create(query, table.PostId)
    .And(g => g.Count().GreaterValue(10));
// SELECT * FROM [Comments] WHERE [Pick]=1 GROUP BY [PostId] HAVING COUNT(*)>10
```

#### 4.1.4 先联表查询再分组
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var joinOn = JoinOnQuery.Create(c, p)
    .And(c.PostId.Equal(p.Id));
JoinTableQuery query = joinOn.Root
    .And(c.Pick.EqualValue(true))
    .And(p.Author.EqualValue("张三"));
var groupBy = GroupByQuery.Create(query, c.PostId)
    .And(g => g.Count().GreaterValue(10));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三' GROUP BY c.[PostId] HAVING COUNT(*)>10
```

### 4.2 And
### 4.2.1 And扩展方法
>* 用于基于分组查询
>* 主要用来对接聚合查询
```csharp
Query And<Query>(this Query query, Func<IGroupByView, AtomicLogic> logic)
    where Query : GroupByBase, IDataQuery;
```
```csharp
var groupBy = GroupByQuery.Create("Users", "City")
    .And(g => g.Count().GreaterValue(10));
// SELECT * FROM [Users] GROUP BY [City] HAVING COUNT(*)>10
```

### 4.2.2 And重载扩展方法
>* 用于基于分组查询
>* 主要用来对接聚合查询
>* 用于AND嵌套OR查询
```csharp
Query And<Query>(this Query query, Func<IGroupByView, OrLogic> logic)
    where Query : GroupByBase, IDataQuery;
```
```csharp
var groupBy = GroupByQuery.Create("Comments", "PostId")
    .And(g => g.Count().GreaterValue(10))
    .And(g => (g.Sum("Hits").GreaterValue(1000) | g.Max("Recommend").GreaterValue(10)));
// SELECT * FROM [Comments] GROUP BY [PostId] HAVING COUNT(*)>10 AND (SUM([Hits])>1000 OR MAX([Recommend])>10)
```

### 4.3 Or
### 4.3.1 Or扩展方法
>* 用于基于分组查询
>* 主要用来对接聚合查询
```csharp
Query Or<Query>(this Query query, Func<IGroupByView, AtomicLogic> logic)
    where Query : GroupByBase, IDataQuery;
```
```csharp
var groupBy = GroupByQuery.Create("Comments", "PostId")
    .Or(g => g.Sum("Hits").GreaterValue(1000))
    .Or(g => g.Max("Recommend").GreaterValue(10));
// SELECT * FROM [Comments] GROUP BY [PostId] HAVING SUM([Hits])>1000 OR MAX([Recommend])>10
```

### 4.3.2 Or重载扩展方法
>* 用于基于分组查询
>* 主要用来对接聚合查询
>* 用于OR嵌套AND查询
```csharp
Query Or<Query>(this Query query, Func<IGroupByView, AndLogic> logic)
    where Query : GroupByBase, IDataQuery;
```
```csharp
var groupBy = GroupByQuery.Create("Comments", "PostId")
    .Or(g => g.Count().GreaterValue(10))
    .Or(g => (g.Sum("Hits").GreaterValue(1000) & g.Max("Recommend").GreaterValue(10)));
// SELECT * FROM [Comments] GROUP BY [PostId] HAVING COUNT(*)>10 OR (SUM([Hits])>1000 AND MAX([Recommend])>10)
```

### 4.4 Apply扩展方法
>* 操作Logic的高阶函数
>* 也可称开窗函数,把IGroupByView和Logic开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来查询
>* 查询子函数标记static性能更好
```csharp
TGroupBy Apply<TGroupBy>(this TGroupBy groupBy, Func<IGroupByView, Logic, Logic> logic)
    where TGroupBy : GroupByBase, IDataSqlQuery;
```
```csharp
var groupBy = GroupBySqlQuery.Create("Users", "CityId")
    .Apply(static (g, q) => q
        .And(g.Count().GreaterValue(100))
        .And(g.Max("Level").GreaterValue(9))
    );
// SELECT * [Users] GROUP BY [CityId] HAVING COUNT(*)>100 AND MAX([Level])>9
```

## 5. 其他相关功能
>* 参看[逻辑查询简介](./index.md)
>* 参看[聚合](../aggregate.md)
