# 游标简介
>* 本组件用来处理分页和排序,封装了ORDER BY和分页参数
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[ICursor](xref:ShadowSql.Cursors.ICursor)

## 2. 基类
>* [CursorBase](xref:ShadowSql.Cursors.CursorBase)
>* [GroupByCursorBase](xref:ShadowSql.Expressions.Cursors.GroupByCursorBase)

## 3. 功能类
### 3.1 TableCursor
>* [TableCursor\<TTable\>](xref:ShadowSql.Expressions.Cursors.TableCursor%601)
>* [表游标](./table.md)

### 3.2 MultiTableCursor
>* [MultiTableCursor](xref:ShadowSql.Expressions.Cursors.MultiTableCursor)
>* [联表游标](./join.md)

### 3.3 GroupByTableCursor
>* [GroupByTableCursor\<TKey, TEntity\>](xref:ShadowSql.Expressions.Cursors.GroupByTableCursor%602)
>* [分组游标](./groupby.md)

### 3.4 GroupByMultiCursor
>* [GroupByMultiCursor\<TKey\>](xref:ShadowSql.Expressions.Cursors.GroupByMultiCursor%601)
>* [联表分组游标](./groupbyjoin.md)

## 4. 排序方法
### 4.1 CountAsc扩展方法
>* 计数正序
```csharp
TGroupByCursor CountAsc<TGroupByCursor>(this TGroupByCursor cursor)
        where TGroupByCursor : GroupByCursorBase;
```

### 4.2 CountDesc扩展方法
>* 计数倒序
```csharp
TGroupByCursor CountDesc<TGroupByCursor>(this TGroupByCursor cursor)
        where TGroupByCursor : GroupByCursorBase;
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[ShadowSqlCore相关文档](../../shadowcore/cursor/index.md)
