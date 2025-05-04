# sql分组查询
>* 按列分组并查询
>* sql的GROUP BY和HAVING部分
>* 使用关键字Having查询

## 1. 接口
>[IGroupByView](/api/ShadowSql.Identifiers.IGroupByView.html)

## 2. 基类
>[GroupByBase](/api/ShadowSql.GroupBy.GroupByBase.html)

## 3. 类
>[GroupBySqlQuery](/api//api/ShadowSql.GroupBy.GroupBySqlQuery.html)

## 4. 方法
### 4.1 Create静态方法
#### 4.1.1 对table分组 
```csharp
GroupBySqlQuery Create(ITable table, params IFieldView[] fields);
GroupBySqlQuery Create(ITable table, params IEnumerable<string> columnNames);
```
```csharp
var table = _db.From("Users");
var groupBy = GroupBySqlQuery.Create(table, "City");
// SELECT * FROM [Users] GROUP BY [City]
```

#### 4.1.2 对tableName分组 
```csharp
GroupBySqlQuery Create(ITable table, params IFieldView[] fields);
GroupBySqlQuery Create(ITable table, params IEnumerable<string> columnNames);
```
```csharp
var groupBy = GroupBySqlQuery.Create("Users", "City");
// SELECT * FROM [Users] GROUP BY [City]
```

#### 4.1.3 先查询再分组
```csharp
GroupBySqlQuery Create(IDataFilter filter, params IFieldView[] fields);
GroupBySqlQuery Create(IDataFilter filter, params IEnumerable<string> columnNames);
```
```csharp
var query = new TableSqlQuery("Users")
    .ColumnEqualValue("Age", 20);
var groupBy = GroupBySqlQuery.Create(query, "City");
// SELECT * FROM [Users] WHERE [Age]=20 GROUP BY [City]
```

#### 4.1.4 先联表查询再分组
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var joinOn = JoinOnSqlQuery.Create(c, p)
    .On(c.PostId, p.Id);
JoinTableSqlQuery query = joinOn.Root
    .Where(c.Pick.EqualValue(true))
    .Where(p.Author.EqualValue("张三"));
var groupBy = GroupBySqlQuery.Create(query, c.PostId)
    .Having(g => g.Count().GreaterValue(10));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三' GROUP BY c.[PostId] HAVING COUNT(*)>10
```

### 4.2 Having扩展方法
```csharp
TGroupBy Having<TGroupBy>(this TGroupBy groupBy, params IEnumerable<string> conditions)
        where TGroupBy : GroupByBase, IDataSqlQuery;
TGroupBy Having<TGroupBy>(this TGroupBy groupBy, AtomicLogic logic)
        where TGroupBy : GroupByBase, IDataSqlQuery;
TGroupBy Having<TGroupBy>(this TGroupBy groupBy, Func<IGroupByView, AtomicLogic> query)
        where TGroupBy : GroupByBase, IDataSqlQuery;
```
```csharp
var groupBy = GroupBySqlQuery.Create("Users", "City")
    .Having("COUNT(*)>10");
// SELECT * FROM [Users] GROUP BY [City] HAVING COUNT(*)>10
```
```csharp
var level = Column.Use("Level");
var groupBy = GroupBySqlQuery.Create("Users", "CityId")
    .Having(level.Max().GreaterValue(9));
// SELECT * FROM [Users] GROUP BY [CityId] HAVING MAX([Level])>9
```
```csharp
var query = new TableSqlQuery("Users")
    .ColumnEqualValue("Age", 20);
var groupBy = GroupBySqlQuery.Create(query, "CityId")
    .Having( static g => g.Max("Level").GreaterValue(9));
// SELECT * FROM [Users] WHERE [Age]=20 GROUP BY [CityId] HAVING MAX([Level])>9
```

### 4.3 HavingAggregate扩展方法
```csharp
TGroupBy HavingAggregate<TGroupBy>(this TGroupBy groupBy, string aggregate, string columnName, Func<IAggregateField, AtomicLogic> query)
    where TGroupBy : GroupByBase, IDataSqlQuery;
```
```csharp
var groupBy = GroupBySqlQuery.Create("Users", "CityId")
    .HavingAggregate("MAX", "Level", static level => level.GreaterValue(9));
// SELECT * FROM [Users] GROUP BY [CityId] HAVING MAX([Level])>9
```

### 4.4 Apply扩展方法
>* 操作SqlQuery的高阶函数
>* 也可称开窗函数,把IGroupByView和SqlQuery开放给用户直接使用
>* 以便于使用更直接、通用的逻辑来查询
>* 查询子函数标记static性能更好
```csharp
TGroupBy Apply<TGroupBy>(this TGroupBy groupBy, Func<IGroupByView, SqlQuery, SqlQuery> query)
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
>* 参看[sql查询简介](./index.md)
>* 参看[聚合](../aggregate.md)
