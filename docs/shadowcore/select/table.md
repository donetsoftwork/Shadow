# 数据获取
>* 数据获取组件
>* 本组件用来处理sql的SELECT语句

## 1. 接口
>[ISelect](/api/ShadowSql.Select.ISelect.html)

## 2. 使用方法
### 2.1 从表获取
```csharp
var select = new TableSelect(_db.From("Users"))
    .Select("Id", "Name");
// SELECT [Id],[Name] FROM [Users]
```

### 2.2 从SqlQuery获取
>[SqlQuery](../sqlquery/table.md)
```csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
    .Where(table.Status.EqualValue(true));
var select = new TableSelect(query)
    .Select(table.Id, table.Name);
// SELECT [Id],[Name] FROM [Users] WHERE [Status]=1
```

### 2.3 从Query获取
>[Query](../query/table.md)
```csharp
var table = new UserTable();
var query = new TableQuery(table)
    .And(table.Status.EqualValue(true));
var select = new TableSelect(query)
    .Select(table.Id, table.Name);
// SELECT [Id],[Name] FROM [Users] WHERE [Status]=1
```

### 2.4 从GroupBySqlQuery获取
>[GroupBySqlQuery](../sqlquery/groupby.md)
```csharp
var table = new CommentTable();
var query = new TableSqlQuery(table)
    .Where(table.Pick.EqualValue(true));
var groupBy = GroupBySqlQuery.Create(query, table.PostId);
var select = new TableSelect(groupBy)
    .Select(table.PostId, groupBy.CountAs("CommentCount"));
// SELECT [PostId],COUNT(*) AS CommentCount FROM [Comments] WHERE [Pick]=1 GROUP BY [PostId]
```

### 2.5 从GroupByQuery获取
>[GroupByQuery](../query/groupby.md)
```csharp
var table = new CommentTable();
var query = new TableQuery(table)
    .And(table.Pick.EqualValue(true));
var groupBy = GroupByQuery.Create(query, table.PostId);
var select = new TableSelect(groupBy)
    .Select(table.PostId, groupBy.CountAs("CommentCount"));
// SELECT [PostId],COUNT(*) AS CommentCount FROM [Comments] WHERE [Pick]=1 GROUP BY [PostId]
```
