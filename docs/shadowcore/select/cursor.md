# 分页获取
>* 通过游标分页获取数据组件
>* 本组件用来处理sql的SELECT分页语句

## 1. 接口
>[ISelect](xref:ShadowSql.Select.ISelect)

## 2. 使用方法
### 2.1 从表获取
```csharp
var table = _db.From("Users");
var cursor = new TableCursor(table)
    .OrderBy("Id DESC")
    .Skip(20)
    .Take(10);
// SELECT [PostId],COUNT(*) AS CommentCount FROM [Comments] WHERE [Pick]=1 GROUP BY [PostId]
```

### 2.2 从SqlQuery获取
>[SqlQuery](../sqlquery/table.md)
```csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
    .Where(table.Status.EqualValue(true));
var cursor = new TableCursor(query, 10, 20)
    .Desc(table.Id);
var select = new CursorSelect(cursor)
    .Select(table.Id, table.Name);
// SELECT [Id],[Name] FROM [Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
```

### 2.3 从Query获取
>[Query](../query/table.md)
```csharp
var table = new UserTable();
var query = new TableQuery(table)
    .And(table.Status.EqualValue(true));
var cursor = new TableCursor(query, 10, 20)
    .Desc(table.Id);
var select = new CursorSelect(cursor)
    .Select(table.Id, table.Name);
// SELECT [Id],[Name] FROM [Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
```

### 2.4 从GroupBySqlQuery获取
>[GroupBySqlQuery](../sqlquery/groupby.md)
```csharp
var query = new TableSqlQuery("Users")
    .StrictLessValue("Age", 20);
var groupBy = GroupBySqlQuery.Create(query, "City");
var cursor = new TableCursor(groupBy, 10, 20)
    .Desc(groupBy.Count());
var select = new CursorSelect(cursor)
    .Select("City")
    .Select(groupBy.CountAs("CityCount"));
// SELECT [City],COUNT(*) AS CityCount FROM [Users] WHERE [Age]<20 GROUP BY [City] ORDER BY COUNT(*) DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
```
### 2.5 从GroupByQuery获取
>[GroupByQuery](../query/groupby.md)
```csharp
var table = new CommentTable();
var query = new TableQuery(table)
    .And(table.Pick.EqualValue(true));
var groupBy = GroupByQuery.Create(query, table.PostId);
var cursor = new TableCursor(groupBy, 10, 20)
    .Desc(groupBy.Count());
var select = new CursorSelect(cursor)
    .Select(table.PostId, groupBy.CountAs("CommentCount"))
// SELECT [PostId],COUNT(*) AS CommentCount FROM [Comments] WHERE [Pick]=1 GROUP BY [PostId] ORDER BY COUNT(*) DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
```
