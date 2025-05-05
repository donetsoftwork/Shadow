# 游标
>对数据进行截取,处理分页和排序

## 1. 接口
>[ICursor](xref:ShadowSql.Cursors.ICursor)

## 2. 基类
>[CursorBase](xref:ShadowSql.Cursors.CursorBase)

## 3. 类
>[TableCursor](xref:ShadowSql.Cursors.TableCursor)

## 4. 使用方法
### 4.1 截取table
>使用游标截取table
```csharp
var cursor = new TableCursor(_db.From("Users"))
    .Desc("Age")
    .Skip(20)
    .Take(10);
```

### 4.2 截取SqlQuery
>使用游标截取SqlQuery
```csharp
var query = new TableSqlQuery("Users")
    .Where("Age>30");
var cursor = new TableCursor(query)
    .OrderBy("Id DESC")
    .Skip(20)
    .Take(10);
```

### 4.3 截取Query
>使用游标截取Query
```csharp
var table = new PostTable();
var query = new TableQuery(table)
    .And(table.Author.EqualValue("张三"));
var cursor = new TableCursor(query)
    .Desc(table.Id)
    .Skip(20)
    .Take(10);
```

### 4.4 截取GroupBySqlQuery
>使用游标截取GroupBySqlQuery
```csharp
CommentTable table = new();
var query = new TableSqlQuery(table)
    .Where(table.Pick.EqualValue(true));
var groupBy = GroupBySqlQuery.Create(query, table.PostId)
    .Having(g => g.Count().GreaterValue(10));
var cursor = new TableCursor(groupBy)
    .Asc(table.PostId)
    .Desc(table.Id)
    .Skip(20)
    .Take(10);
```

### 4.5 截取GroupByQuery
>使用游标截取GroupByQuery
```csharp
CommentTable table = new();
var query = new TableQuery(table)
    .And(table.Pick.EqualValue(true));
var groupBy = GroupByQuery.Create(query, table.PostId)
    .And(g => g.Count().GreaterValue(10));
var cursor = new TableCursor(groupBy)
    .Asc(table.PostId)
    .Skip(20)
    .Take(10);
```
