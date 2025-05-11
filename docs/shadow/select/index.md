# 获取简介
>* Select数据获取组件
>* 本组件用来组装sql的SELECT语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对各查询和数据类型特殊处理,增强功能、增加易用性

## 1. 接口
>[ISelect](xref:ShadowSql.Select.ISelect)

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
>* [TableSelect\<TTable\>](xref:ShadowSql.Select.TableSelect%601)
>* [获取表](./table.md)

### 3.2 TableCursorSelect
>* [TableCursorSelect\<TTable\>](xref:ShadowSql.CursorSelect.TableCursorSelect%601)
>* [分页获取表](./tablecursor.md)

### 3.3 MultiTableSelect
>* [MultiTableSelect](xref:ShadowSql.Select.MultiTableSelect)
>* [获取联表](./join.md)

### 3.4 MultiTableSelect
>* [MultiTableCursorSelect](xref:ShadowSql.CursorSelect.MultiTableCursorSelect)
>* [分页获取联表](./joincursor.md)

### 3.5 GroupByTableSelect
>* [GroupByTableSelect\<TTable\>](xref:ShadowSql.Select.GroupByTableSelect%601)
>* [获取分组](./groupby.md)

### 3.6 GroupByTableCursorSelect
>* [GroupByTableCursorSelect\<TTable\>](xref:ShadowSql.CursorSelect.GroupByTableCursorSelect%601)
>* [分页获取分组](./groupbycursor.md)

### 3.7 GroupByMultiSelect
>* [GroupByMultiSelect](/api/ShadowSql.Select.GroupByMultiSelect)
>* [获取联表分组](./groupbyjoin.md)

### 3.8 GroupByMultiCursorSelect
>* [GroupByMultiCursorSelect](xref:ShadowSql.CursorSelect.GroupByMultiCursorSelect)
>* [分页获取联表分组](./groupbyjoincursor.md)

## 4. ToSelect扩展方法
>* 用于把相关对象导航到对应的Select组件
>* 增加易用性
### 4.1 ToSelect
#### 4.1.1 导航到TableSelect
>* 获取表
```csharp
TableSelect<TTable> ToSelect<TTable>(this TTable table)
        where TTable : ITable;
TableSelect<TTable> ToSelect<TTable>(this TTable table, ISqlLogic filter)
        where TTable : ITable;
TableSelect<TTable> ToSelect<TTable>(this TableQuery<TTable> query)
        where TTable : ITable;
TableSelect<TTable> ToSelect<TTable>(this TableSqlQuery<TTable> query)
        where TTable : ITable;
```

#### 4.1.2 导航到TableCursorSelect
>* 获取表分页
```csharp
TableCursorSelect<TTable> ToSelect<TTable>(this TableCursor<TTable> cursor)
        where TTable : ITable;
```

#### 4.1.3 导航到MultiTableSelect
>* 获取联表查询
>* MultiTableSelect的ToSelect虽然只有一个,但可以处理多表、联表的sql及逻辑查询4种情况
>* [MultiTableSqlQuery](xref:ShadowSql.Join.MultiTableSqlQuery)
>* [MultiTableQuery](xref:ShadowSql.Join.MultiTableQuery)
>* [JoinTableSqlQuery](xref:ShadowSql.Join.JoinTableSqlQuery)
>* [JoinTableQuery](xref:ShadowSql.Join.JoinTableQuery)
>* 4种情况都继承了[IMultiView](xref:ShadowSql.Identifiers.IMultiView)接口
```csharp
MultiTableSelect ToSelect(this IMultiView table);
```

#### 4.1.4 导航到MultiTableCursorSelect
>* 获取联表分页
```csharp
MultiTableCursorSelect ToSelect(this MultiTableCursor cursor);
```

#### 4.1.5 导航到GroupByTableSelect
>* 获取分组查询
```csharp
GroupByTableSelect<TTable> ToSelect<TTable>(this GroupByTableSqlQuery<TTable> source)
        where TTable : ITable;
GroupByTableCursorSelect<TTable> ToSelect<TTable>(this GroupByTableCursor<TTable> cursor)
        where TTable : ITable;
```

#### 4.1.6 导航到GroupByTableCursorSelect
>* 获取分组分页
```csharp
GroupByTableCursorSelect<TTable> ToSelect<TTable>(this GroupByTableCursor<TTable> cursor)
        where TTable : ITable;
```

#### 4.1.7 导航到GroupByMultiSelect
>* 获取联表分组查询
```csharp
GroupByMultiSelect ToSelect(this GroupByMultiTableSqlQuery source);
GroupByMultiSelect ToSelect(this GroupByMultiTableQuery source);
```
#### 4.1.8 导航到GroupByMultiCursorSelect
>* 获取联表分组分页
```csharp
GroupByMultiCursorSelect ToSelect(this GroupByMultiTableCursor cursor);
```

## 5. 分组筛选扩展方法
### 5.1 SelectGroupBy扩展方法
>* 筛选当前分组字段
```csharp
TSelect SelectGroupBy<TSelect>(this TSelect select)
        where TSelect : SelectFieldsBase, IGroupBySelect;
```

### 5.2 SelectCount扩展方法
>* 筛选计数
```csharp
TSelect SelectCount<TSelect>(this TSelect select, string alias = "Count")
        where TSelect : SelectFieldsBase, IGroupBySelect;
```

### 5.3 SelectAggregate扩展方法
>* 聚合筛选
```csharp
TSelect SelectCount<TSelect>(this TSelect select, string alias = "Count")
        where TSelect : SelectFieldsBase, IGroupBySelect;
```

## 6. 其他通用功能
>* [参看ShadowSqlCore相关文档](../../shadowcore/select/index.md)
