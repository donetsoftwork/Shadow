# 数据获取简介
>* 数据获取组件
>* 本组件用来组装sql的SELECT语句

## 1. 接口
>[ISelect](xref:ShadowSql.Select.ISelect)

## 2. 筛选列方法
### 2.1 Select
#### 2.1.1 Select扩展方法
>* 筛选列
```csharp
TSelectFields Select<TSelectFields>(this TSelectFields select, params IEnumerable<IFieldView> fields)
    where TSelectFields : SelectFieldsBase;
```

#### 2.1.2 Selectc重载扩展方法
>* 按列名筛选
```csharp
TSelectFields Select<TSelectFields>(this TSelectFields select, params IEnumerable<string> columns)
    where TSelectFields : SelectFieldsBase;
```

#### 2.1.3 Select重载扩展方法
>* 筛选子查询
```csharp
TSelectFields Select<TSelectFields>(this TSelectFields fields, ISingleSelect select)
    where TSelectFields : SelectFieldsBase;
```

### 2.2 Alias
#### 2.2.1 Alias扩展方法
>* 按原生sql筛选并设置别名
```csharp
TSelectFields Alias<TSelectFields>(this TSelectFields fields, string alias, string statement)
    where TSelectFields : SelectFieldsBase;
```

#### 2.2.1 Alias重载扩展方法
>* 按子查询筛选并设置别名
```csharp
TSelectFields Alias<TSelectFields>(this TSelectFields fields, ISingleSelect select, string alias)
    where TSelectFields : SelectFieldsBase;
```

### 2.3 SelectCount扩展方法
>* 筛选计算
```csharp
TSelectFields SelectCount<TSelectFields>(this TSelectFields select, string alias = "Count")
    where TSelectFields : SelectFieldsBase;
```

## 3. Apply扩展方法
>* 操作Select的高阶函数

```csharp
TSelect Apply<TSelect>(this TSelect select, Action<TSelect> action)
        where TSelect : ISelect;
```
