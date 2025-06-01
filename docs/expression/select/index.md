# 获取简介
>* Select数据获取组件
>* 本组件用来组装sql的SELECT语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对各查询和数据类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)
>* [IMultiSelect](xref:ShadowSql.Expressions.Select.IMultiSelect)
>* [IGroupBySelect](xref:ShadowSql.Expressions.Select.IGroupBySelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)
>* [SelectBase\<TSource, TTarget\>](xref:ShadowSql.Select.SelectBase%602)
>* [MultiSelectBase\<TSource\>](xref:ShadowSql.Select.MultiSelectBase%601)
>* [GroupBySelectBase\<TSource, TGroupSource\>](xref:ShadowSql.Select.GroupBySelectBase%602)
>* [CursorSelectBase\<TTarget\>](xref:ShadowSql.CursorSelect.CursorSelectBase%601)
>* [MultiCursorSelectBase](xref:ShadowSql.CursorSelect.MultiCursorSelectBase)
>* [GroupCursorBySelectBase\<TGroupSource\>](xref:ShadowSql.CursorSelect.GroupCursorBySelectBase%601)

## 3. 功能类
### 3.1 TableSelect
>* [TableSelect\<TEntity\>](xref:ShadowSql.Expressions.Select.TableSelect%601)
>* [获取表](./table.md)

### 3.2 TableCursorSelect
>* [TableCursorSelect\<TEntity\>](xref:ShadowSql.Expressions.CursorSelect.TableCursorSelect%601)
>* [分页获取表](./tablecursor.md)

### 3.3 MultiTableSelect
>* [MultiTableSelect](xref:ShadowSql.Expressions.Select.MultiTableSelect)
>* [获取联表](./join.md)

### 3.4 MultiTableCursorSelect
>* [MultiTableCursorSelect](xref:ShadowSql.Expressions.CursorSelect.MultiTableCursorSelect)
>* [分页获取联表](./joincursor.md)

### 3.5 GroupByTableSelect
>* [GroupByTableSelect\<TKey, TEntity\>](xref:ShadowSql.Expressions.Select.GroupByTableSelect%602)
>* [获取分组](./groupby.md)

### 3.6 GroupByTableCursorSelect
>* [GroupByTableCursorSelect\<TKey, TEntity\>](xref:ShadowSql.Expressions.CursorSelect.GroupByTableCursorSelect%602)
>* [分页获取分组](./groupbycursor.md)

### 3.7 GroupByMultiSelect
>* [GroupByMultiSelect\<TKey\>](xref:ShadowSql.Expressions.Select.GroupByMultiSelect%601)
>* [获取联表分组](./groupbyjoin.md)

### 3.8 GroupByMultiCursorSelect
>* [GroupByMultiCursorSelect\<TKey\>](xref:ShadowSql.Expressions.CursorSelect.GroupByMultiCursorSelect%601)
>* [分页获取联表分组](./groupbyjoincursor.md)

## 4. 分组筛选扩展方法
### 4.1 SelectGroupBy扩展方法
>* 筛选当前分组字段
```csharp
TSelect SelectKey<TSelect>(this TSelect select)
        where TSelect : SelectFieldsBase, IGroupBySelect;
```

### 4.2 SelectCount扩展方法
>* 筛选计数
```csharp
TSelect SelectCount<TSelect>(this TSelect select, string alias = "Count")
        where TSelect : SelectFieldsBase, IGroupBySelect;
```

## 5. 联表筛选扩展方法
### 5.1 SelectTable扩展方法
>* 筛选表
~~~csharp
TMultiTableSelect SelectTable<TMultiTableSelect>(this TMultiTableSelect multiSelect, IAliasTable aliasTable)
        where TMultiTableSelect : MultiSelectBase;
TMultiTableSelect SelectTable<TMultiTableSelect>(this TMultiTableSelect multiSelect, string tableName)
        where TMultiTableSelect : MultiSelectBase;
~~~

## 6. 其他通用功能
>* [参看ShadowSqlCore相关文档](../../shadowcore/select/index.md)
