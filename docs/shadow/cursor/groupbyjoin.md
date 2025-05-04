# 联表分组游标
>* 对联表分组进行截取,处理分页和排序
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[ICursor](/api/ShadowSql.Cursors.ICursor.html)

## 2. 基类
>[CursorBase](/api/ShadowSql.Cursors.CursorBase.html)

## 3. 类
>[GroupByMultiCursor](/api/ShadowSql.Cursors.GroupByMultiCursor.html)


## 4. ToCursor
### 4.1 ToCursor扩展方法
>从sql联表分组创建游标
~~~csharp
GroupByMultiCursor ToCursor(this GroupByMultiSqlQuery groupBy, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = _db.From("Employees")
    .SqlJoin(_db.From("Departments"))
    .OnColumn("DepartmentId", "Id")
    .Root
    .SqlGroupBy("Manager")
    .ToCursor()
    .CountAsc();
// SELECT * FROM [Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] GROUP BY [Manager] ORDER BY COUNT(*)
~~~

### 4.2 ToCursor重载扩展方法
>从逻辑联表分组创建游标
~~~csharp
GroupByMultiCursor ToCursor(this GroupByMultiQuery groupBy, int limit = 0, int offset = 0);
~~~
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var cursor = c.Join(p)
    .And(c.PostId.Equal(p.Id))
    .Root
    .GroupBy(p.Id)
    .ToCursor()
    .CountDesc();
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY p.[Id] ORDER BY COUNT(*) DESC
~~~

## 5. 按表聚合排序
### 5.1 AggregateAsc方法
>* 聚合正序
~~~csharp
GroupByMultiCursor AggregateAsc<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
        where TTable : ITable;
~~~
~~~csharp

// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t2.[Id]
~~~

### 5.2 AggregateDesc方法
>* 聚合倒序
~~~csharp
GroupByMultiCursor AggregateDesc<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
        where TTable : ITable;
~~~
~~~csharp

// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t2.[Id] DESC
~~~

## 6. 按别名表聚合排序
### 6.1 AggregateAsc方法
>* 聚合正序
~~~csharp
GroupByMultiCursor AggregateAsc<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> select)
        where TAliasTable : IAliasTable;
~~~
~~~csharp

// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Id]
~~~

### 6.2 AggregateDesc方法
>* 聚合倒序
~~~csharp
GroupByMultiCursor AggregateDesc<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> select)
        where TAliasTable : IAliasTable;
~~~
~~~csharp

// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Pick] DESC
~~~

## 7. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByMultiCursor](/api/ShadowSql.Cursors.GroupByMultiCursor.html)的方法和扩展方法部分
>* 参看[游标简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/cursor/index.md)
