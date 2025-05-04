# 游标简介
>* 本组件用来处理分页和排序,封装了ORDER BY和分页参数
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[ICursor](/api/ShadowSql.Cursors.ICursor.html)

## 2. 基类
>* [CursorBase](/api/ShadowSql.Cursors.CursorBase.html)
>* [GroupByCursorBase](/api/ShadowSql.Cursors.GroupByCursorBase.html)

## 3. 功能类
### 3.1 TableCursor
>* [TableCursor\<TTable\>](/api/ShadowSql.Cursors.TableCursor-1.html)
>* [表游标](./table.md)

### 3.2 MultiTableCursor
>* [MultiTableCursor](/api/ShadowSql.Cursors.MultiTableCursor.html)
>* [联表游标](./join.md)

### 3.3 GroupByTableCursor
>* [GroupByTableCursor\<TTable\>](/api/ShadowSql.Cursors.GroupByTableCursor-1.html)
>* [分组游标](./groupby.md)

### 3.4 GroupByMultiCursor
>* [GroupByMultiCursor](/api/ShadowSql.Cursors.GroupByMultiCursor.html)
>* [联表分组游标](./groupbyjoin.md)

## 4. 排序方法
### 4.1 AggregateAsc扩展方法
>* 聚合正序
```csharp
TGroupByCursor AggregateAsc<TGroupByCursor>(this TGroupByCursor cursor, Func<IGroupByView, IAggregateField> select)
        where TGroupByCursor : GroupByCursorBase;
```

### 4.2 AggregateDesc扩展方法
>* 聚合倒序
```csharp
TGroupByCursor AggregateDesc<TGroupByCursor>(this TGroupByCursor cursor, Func<IGroupByView, IAggregateField> select)
        where TGroupByCursor : GroupByCursorBase;
```
### 4.3 CountAsc扩展方法
>* 计数正序
```csharp
TGroupByCursor CountAsc<TGroupByCursor>(this TGroupByCursor cursor)
        where TGroupByCursor : GroupByCursorBase;
```

### 4.4 CountDesc扩展方法
>* 计数倒序
```csharp
TGroupByCursor CountDesc<TGroupByCursor>(this TGroupByCursor cursor)
        where TGroupByCursor : GroupByCursorBase;
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[ShadowSqlCore相关文档](../../shadowcore/cursor/index.md)
