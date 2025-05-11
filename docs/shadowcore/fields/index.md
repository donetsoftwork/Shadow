# 字段简介
>* 列及字段

## 1. 接口
|interface|功能|作用|
|:--|:--|:--|
|[IColumn](xref:ShadowSql.Identifiers.IColumn)|原始列|定义表、继承IField|
|[IField](xref:ShadowSql.Identifiers.IField)|字段|继承IFieldView、ICompareField、IOrderField和IAssignView等|
|[IPrefixField](xref:ShadowSql.Identifiers.IPrefixField)|前缀字段|别名表字段|
|[IFieldView](xref:ShadowSql.Identifiers.IFieldView)|展示字段|用于select|
|[ICompareView](xref:ShadowSql.Identifiers.ICompareView)|比较字段|用于查询|
|[ICompareField](xref:ShadowSql.Identifiers.ICompareField)|比较字段|用于查询和聚合|
|[IOrderField](xref:ShadowSql.Identifiers.IOrderField)|排序字段|用于排序|
|[IOrderAsc](xref:ShadowSql.Identifiers.IOrderAsc)|排序字段|用于正序|
|[IOrderDesc](xref:ShadowSql.Identifiers.IOrderDesc)|排序字段|用于倒序|
|[IOrderView](xref:ShadowSql.Identifiers.IOrderView)|排序字段|一般表示原生orderby子句|
|[IAssignView](xref:ShadowSql.Identifiers.IAssignView)|赋值字段|用于更新|
|[IFieldAlias](xref:ShadowSql.Identifiers.IFieldAlias)|别名字段|用于select|
|[IAggregateField](xref:ShadowSql.Aggregates.IAggregateField)|聚合字段|用于聚合查询和排序|
|[IAggregateFieldAlias](xref:ShadowSql.Aggregates.IAggregateFieldAlias)|聚合别名|用于select|
## 2. 功能类
### 2.1 Column
>* 表的原始列
>* 作为数据表列的影子(占位符)
>* 参看[Column](./column.md)

### 2.2 PrefixField
>* 用于自定义别名表结构
>* 映射sql中带表前缀的列
>* 带表前缀的列影子(占位符)
>* 参看[PrefixField](./prefix.md)

### 2.3 ColumnSchema
>* 包含数据库字段类型的原始列
>* 主要用于组成TableSchema,用来支持CreateTable
>* 参看[ColumnSchema](./schema.md)

### 2.4 AliasFieldInfo
>* 参看[别名字段](./alias.md)
>* 参看[IFieldAlias](xref:ShadowSql.Identifiers.IFieldAlias)
>* 参看[AliasFieldInfo](xref:ShadowSql.FieldInfos.AliasFieldInfo)

### 2.5 AggregateFieldInfo
>* 参看[聚合字段](./aggregate.md)
>* 参看[IAggregateField](xref:ShadowSql.Aggregates.IAggregateField)
>* 参看[AggregateFieldInfo](xref:ShadowSql.Aggregates.AggregateFieldInfo)

### 2.6 AggregateAliasFieldInfo
>* 参看[聚合别名](./aggregatealias.md)
>* 参看[IAggregateFieldAlias](xref:ShadowSql.Aggregates.IAggregateFieldAlias)
>* 参看[AggregateAliasFieldInfo](xref:ShadowSql.Aggregates.AggregateAliasFieldInfo)

## 3. 功能
### 3.1 用于select
### 3.1.1 IFieldView
>* 继承接口[IFieldView](xref:ShadowSql.Identifiers.IFieldView)

### 3.2 比较逻辑
>* 比较逻辑用于查询
>* 参看[获取表精简版](../select/table.md)
>* 参看[获取表易用版](../../shadow/select/table.md)

#### 3.2.1 无参逻辑
```csharp
AtomicLogic IsNull(this ICompareView field);
AtomicLogic NotNull(this ICompareView field);
```
#### 3.2.2 与值比较逻辑
```csharp
AtomicLogic EqualValue<TValue>(this ICompareView field, TValue value);
AtomicLogic NotEqualValue<TValue>(this ICompareView field, TValue value);
AtomicLogic GreaterValue<TValue>(this ICompareView field, TValue value);
AtomicLogic LessValue<TValue>(this ICompareView field, TValue value);
AtomicLogic GreaterEqualValue<TValue>(this ICompareView field, TValue value);
AtomicLogic LessEqualValue<TValue>(this ICompareView field, TValue value);
AtomicLogic LikeValue(this ICompareView field, string value);
AtomicLogic NotLikeValue(this ICompareView field, string value);
AtomicLogic InValue<TValue>(this ICompareView field,  params IEnumerable<TValue> values);
AtomicLogic NotInValue<TValue>(this ICompareView field, params IEnumerable<TValue> values);
AtomicLogic BetweenValue<TValue>(this ICompareView field, TValue begin, TValue end);
AtomicLogic NotBetweenValue<TValue>(this ICompareView field, TValue begin, TValue end);
```

#### 3.2.3 与参数比较逻辑
```csharp
AtomicLogic Compare(this ICompareField field, string op, string parameter = "");
AtomicLogic Equal(this ICompareField field, string parameter = "");
AtomicLogic NotEqual(this ICompareField field, string parameter = "");
AtomicLogic Greater(this ICompareField field, string parameter = "");
AtomicLogic Less(this ICompareField field, string parameter = "");
AtomicLogic GreaterEqual(this ICompareField field, string parameter = "");
AtomicLogic LessEqual(this ICompareField field, string parameter = "");
AtomicLogic Like(this ICompareField field, string parameter = "");
AtomicLogic NotLike(this ICompareField field, string parameter = "");
AtomicLogic In(this ICompareField field, string parameter = "");
AtomicLogic NotIn(this ICompareField field, string parameter = "");
AtomicLogic Between(this ICompareField field, string begin = "", string end = "");
AtomicLogic NotBetween(this ICompareField field, string begin = "", string end = "");
```

### 3.3 赋值逻辑
>* 赋值逻辑用于更新
>* 参看[赋值逻辑](../assign/operation.md)
>* 参看[单表更新精简版](../update/table.md)
>* 参看[单表更新易用版](../../shadow/update/table.md)


### 3.4 插值逻辑
>* 插值逻辑用于Insert
#### 3.4.1 插入参数
>* 用于插入单条
>* 参看[插入单条精简版](../insert/single.md)
>* 参看[插入单条易用版](../../shadow/insert/single.md)
```csharp
InsertValue Insert(this IColumn column, string parameterName = "");
```

#### 3.4.2 插入值
>* 用于插入单条
>* 参看[插入单条精简版](../insert/single.md)
>* 参看[插入单条易用版](../../shadow/insert/single.md)
```csharp
InsertValue InsertValue<TValue>(this IColumn column, TValue value);
```

#### 3.4.3 插入多值
>* 用于插入多条
>* 参看[插入多条精简版](../insert/multi.md)
>* 参看[插入多条易用版](../../shadow/insert/multi.md)
```csharp
InsertValues InsertValues<TValue>(this IColumn column, params IEnumerable<TValue> values);
```

### 3.4 排序逻辑
>* 排序逻辑用于游标筛选
>* 参看[表分页精简版](../select/cursor.md)
>* 参看[表分页易用版](../../shadow/select/tablecursor.md)
```csharp
IOrderDesc Desc(this IOrderAsc field);
```
