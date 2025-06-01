# 分组游标
>* 对分组进行截取,处理分页和排序
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [ICursor](xref:ShadowSql.Cursors.ICursor)

## 2. 基类
>* [CursorBase](xref:ShadowSql.Cursors.CursorBase)

## 3. 类
>* [GroupByTableCursor\<TKey, TEntity\>](xref:ShadowSql.Expressions.Cursors.GroupByTableCursor%602)

## 4. ToCursor
### 4.1 ToCursor扩展方法
>* 从sql分组创建分组游标
~~~csharp
GroupByTableCursor<TKey, TEntity> ToCursor<TKey, TEntity>(this GroupByTableSqlQuery<TKey, TEntity> groupBy, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.UserId)
    .ToCursor()
    .CountAsc();
// SELECT * FROM [UserRoles] GROUP BY [UserId] ORDER BY COUNT(*)
~~~

### 4.2 ToCursor重载扩展方法
>* 从逻辑分组创建分组游标
~~~csharp
GroupByTableCursor<TTable> ToCursor<TTable>(this GroupByTableQuery<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var cursor = EmptyTable.Use("UserRoles")
    .GroupBy<int, UserRole>(u => u.UserId)
    .ToCursor()
    .CountAsc();
// SELECT * FROM [UserRoles] GROUP BY [UserId] ORDER BY COUNT(*)
~~~

## 5. Take
>* Take方法是ToCursor的平替
### 5.1 ToCursor扩展方法
>* 从sql分组创建分组游标
~~~csharp
GroupByTableCursor<TKey, TEntity> ToCursor<TKey, TEntity>(this GroupByTableSqlQuery<TKey, TEntity> groupBy, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.UserId)
    .Take()
    .CountAsc();
// SELECT * FROM [UserRoles] GROUP BY [UserId] ORDER BY COUNT(*)
~~~

### 5.2 ToCursor重载扩展方法
>* 从逻辑分组创建分组游标
~~~csharp
GroupByTableCursor<TTable> ToCursor<TTable>(this GroupByTableQuery<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var cursor = EmptyTable.Use("UserRoles")
    .GroupBy<int, UserRole>(u => u.UserId)
    .Take()
    .CountAsc();
// SELECT * FROM [UserRoles] GROUP BY [UserId] ORDER BY COUNT(*)
~~~

## 6. Asc方法
>* 聚合正序
~~~csharp
GroupByTableCursor<TKey, TEntity> Asc<TOrder>(Expression<Func<IGrouping<TKey, TEntity>, TOrder>> select);
~~~
~~~csharp
var cursor = EmptyTable.Use("UserRoles")
    .SqlGroupBy<int, UserRole>(u => u.UserId)
    .ToCursor()
    .Asc(g => g.Max(r => r.Score));
// SELECT * FROM [UserRoles] GROUP BY [UserId] ORDER BY MAX([Score])
~~~

## 7. Desc方法
>* 聚合正序
~~~csharp
GroupByTableCursor<TKey, TEntity> Desc<TOrder>(Expression<Func<IGrouping<TKey, TEntity>, TOrder>> select);
~~~
~~~csharp
var cursor = new CommentTable()
    .GroupBy(c => [c.PostId])
    .ToCursor()
    .AggregateDesc(c => c.Pick.Sum());
// SELECT * FROM [UserRoles] GROUP BY [UserId] ORDER BY MAX([Score]) DESC
~~~

## 8. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[GroupByTableCursor\<TKey, TEntity\>](xref:ShadowSql.Expressions.Cursors.GroupByTableCursor%602)的方法和扩展方法部分
>* 参看[游标简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/cursor/index.md)
