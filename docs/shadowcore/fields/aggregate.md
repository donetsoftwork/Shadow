# 聚合字段
>* 用于聚合查询和排序

## 1. 接口
>* [IAggregateField](xref:ShadowSql.Aggregates.IAggregateField)

## 2. AggregateFieldInfo
>* 参看[AggregateFieldInfo](xref:ShadowSql.Aggregates.AggregateFieldInfo)
```csharp
class AggregateFieldInfo(ICompareField target, string aggregate) : IAggregateField;
```
### 2.1 按列聚合扩展方法
```csharp
IAggregateField Aggregate(this ICompareField field, string aggregate);
IAggregateField Sum(this ICompareField field);
IAggregateField Max(this ICompareField field);
IAggregateField Min(this ICompareField field);
IAggregateField Avg(this ICompareField field);
```

### 2.2 分组聚合扩展方法
```csharp
IAggregateField Aggregate(this IGroupByView view, string aggregate, string field);
IAggregateField Sum(this IGroupByView view, string field);
IAggregateField Max(this IGroupByView view, string field);
IAggregateField Min(this IGroupByView view, string field);
IAggregateField Avg(this IGroupByView view, string field);
```

## 3. DistinctCountFieldInfo
>* 参看[DistinctCountFieldInfo](xref:ShadowSql.Aggregates.DistinctCountFieldInfo)
>* 对列排重计数
>* sql示意为: COUNT(DISTINCT field)
```csharp
class DistinctCountFieldInfo(ICompareField field) : IAggregateField;
```

### 3.1 DistinctCount扩展方法
```csharp
DistinctCountFieldInfo DistinctCount(this ICompareField field);
DistinctCountFieldInfo DistinctCount(this IGroupByView view, string field);
```

## 4. CountFieldInfo
>* 参看[CountFieldInfo](xref:ShadowSql.FieldInfos.CountFieldInfo)
>* 分组计数
>* sql示意为: COUNT(*)
```csharp
class CountFieldInfo : IAggregateField;
```

### 4.1 Count扩展方法
```csharp
CountFieldInfo Count(this IGroupByView view);
```

### 4.2 分组计数排序扩展方法
>* 分组计数排序调用了CountFieldInfo
```csharp
TGroupByCursor CountAsc<TGroupByCursor>(this TGroupByCursor cursor)
    where TGroupByCursor : GroupByCursorBase;
TGroupByCursor CountDesc<TGroupByCursor>(this TGroupByCursor cursor)
     where TGroupByCursor : GroupByCursorBase;
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* [字段简介](./index.md)