# 聚合

## 一、按列聚合扩展方法
## 1. IAggregateField
>* 可用于Having和排序
```csharp
IAggregateField Aggregate(this ICompareField field, string aggregate);
IAggregateField Sum(this ICompareField field);
IAggregateField Max(this ICompareField field);
IAggregateField Min(this ICompareField field);
IAggregateField Avg(this ICompareField field);
IAggregateField DistinctCount(this ICompareField field);
```

## 2. IAggregateFieldAlias
>* 可用于Select
```csharp
IAggregateFieldAlias AggregateAs(this ICompareField field, string alias = "");
IAggregateFieldAlias SumAs(this ICompareField field, string alias = "");
IAggregateFieldAlias MaxAs(this ICompareField field, string alias = "");
IAggregateFieldAlias MinAs(this ICompareField field, string alias = "");
IAggregateFieldAlias AvgAs(this ICompareField field, string alias = "");
IAggregateFieldAlias DistinctCountAs(this ICompareField field, string alias = "");
```


## 二、分组聚合扩展方法
## 1. IAggregateField
>* 可用于Having和排序
```csharp
IAggregateField Aggregate(this IGroupByView view, string aggregate, string field);
IAggregateField Sum(this IGroupByView view, string field);
IAggregateField Max(this IGroupByView view, string field);
IAggregateField Min(this IGroupByView view, string field);
IAggregateField Avg(this IGroupByView view, string field);
IAggregateField DistinctCount(this IGroupByView view, string field);
```

## 2. CountFieldInfo
>* 继承接口IAggregateField
>* 不基于列的聚合(COUNT(*))
>* 可用于Having和排序
```csharp
CountFieldInfo Count(this IGroupByView view);
```

## 3. IAggregateFieldAlias
>* 可用于Select
```csharp
IAggregateFieldAlias AggregateAs(this IGroupByView view, string aggregate, string field, string alias = "");
IAggregateFieldAlias SumAs(this IGroupByView view, string field, string alias = "");
IAggregateFieldAlias MaxAs(this IGroupByView view, string field, string alias = "");
IAggregateFieldAlias MinAs(this IGroupByView view, string field, string alias = "");
IAggregateFieldAlias AvgAs(this IGroupByView view, string field, string alias = "");
IAggregateFieldAlias DistinctCountAs(this IGroupByView view, string field, string alias = "");
```

## 4. CountAliasFieldInfo
>* 继承接口IAggregateFieldAlias
>* 不基于列的聚合(COUNT(*) AS alias)
>* 可用于Select
```csharp
CountAliasFieldInfo CountAs(this ITableView view, string alias = "Count");
```
