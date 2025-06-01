# 分组游标
>* 对分组进行截取,处理分页和排序
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [ICursor](xref:ShadowSql.Cursors.ICursor)

## 2. 基类
>* [CursorBase](xref:ShadowSql.Cursors.CursorBase)

## 3. 类
>* [GroupByTableCursor\<TTable\>](xref:ShadowSql.Cursors.GroupByTableCursor%601)
~~~csharp
class GroupByTableCursor<TTable>
    where TTable : ITable;
~~~

## 4. ToCursor
### 4.1 ToCursor扩展方法
>* 从sql分组创建分组游标
~~~csharp
GroupByTableCursor<TTable> ToCursor<TTable>(this GroupByTableSqlQuery<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var cursor = _db.From("Employees")
    .SqlGroupBy("DepartmentId")
    .ToCursor();
    .CountAsc();
// SELECT * FROM [Employees] GROUP BY [DepartmentId] ORDER BY COUNT(*)
~~~

### 4.2 ToCursor重载扩展方法
>* 从逻辑分组创建分组游标
~~~csharp
GroupByTableCursor<TTable> ToCursor<TTable>(this GroupByTableQuery<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var cursor = _db.From("Employees")
    .GroupBy("DepartmentId")
    .ToCursor()
    .AggregateDesc(g => g.Max("Age"));
// SELECT * FROM [Employees] GROUP BY [DepartmentId] ORDER BY MAX([Age]) DESC
~~~

## 5. Take
>* Take方法是ToCursor的平替
### 5.1 Take扩展方法
>* 从sql分组创建分组游标
~~~csharp
GroupByTableCursor<TTable> Take<TTable>(this GroupByTableSqlQuery<TTable> groupBy, int limit, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var cursor = _db.From("Employees")
    .SqlGroupBy("DepartmentId")
    .Take(10);
    .CountAsc();
// SELECT TOP 10 * FROM [Employees] GROUP BY [DepartmentId] ORDER BY COUNT(*)
~~~

### 5.2 Take重载扩展方法
>* 从逻辑分组创建分组游标
~~~csharp
GroupByTableCursor<TTable> Take<TTable>(this GroupByTableQuery<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var cursor = _db.From("Employees")
    .GroupBy("DepartmentId")
    .Take(10)
    .AggregateDesc(g => g.Max("Age"));
// SELECT TOP 10 * FROM [Employees] GROUP BY [DepartmentId] ORDER BY MAX([Age]) DESC
~~~

## 6. AggregateAsc方法
>* 聚合正序
~~~csharp
GroupByTableCursor<TTable> AggregateAsc(Func<TTable, IAggregateField> aggregate);
~~~
~~~csharp
var cursor = new CommentTable()
    .SqlGroupBy(c => [c.PostId])
    .ToCursor()
    .AggregateAsc(c => c.Pick.Sum());
// SELECT * FROM [Comments] GROUP BY [PostId] ORDER BY SUM([Pick])
~~~

## 7. AggregateDesc方法
>* 聚合正序
~~~csharp
GroupByTableCursor<TTable> AggregateDesc(Func<TTable, IAggregateField> aggregate);
~~~
~~~csharp
var cursor = new CommentTable()
    .GroupBy(c => [c.PostId])
    .ToCursor()
    .AggregateDesc(c => c.Pick.Sum());
// SELECT * FROM [Comments] GROUP BY [PostId] ORDER BY SUM([Pick]) DESC
~~~

## 8. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByTableCursor\<TTable\>](xref:ShadowSql.Cursors.GroupByTableCursor%601)的方法和扩展方法部分
>* 参看[游标简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/cursor/index.md)
