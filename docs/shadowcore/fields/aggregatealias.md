# 聚合别名
>* 用于展示聚合结果

## 1. 接口
>* [IAggregateFieldAlias](xref:ShadowSql.Aggregates.IAggregateFieldAlias)

## 2. AggregateFieldInfo
>* 参看[AggregateFieldInfo](xref:ShadowSql.Aggregates.AggregateFieldInfo)
```csharp
class AggregateAliasFieldInfo(ICompareField field, string aggregate, string alias = "") : IAggregateFieldAlias;
```
### 2.1 按列聚合扩展方法
```csharp
IAggregateFieldAlias AggregateAs(this ICompareField field, string alias = "");
IAggregateFieldAlias SumAs(this ICompareField field, string alias = "");
IAggregateFieldAlias MaxAs(this ICompareField field, string alias = "");
IAggregateFieldAlias MinAs(this ICompareField field, string alias = "");
IAggregateFieldAlias AvgAs(this ICompareField field, string alias = "");
```

### 2.2 分组聚合扩展方法
```csharp
IAggregateFieldAlias AggregateAs(this IGroupByView view, string aggregate, string field, string alias = "");
IAggregateFieldAlias SumAs(this IGroupByView view, string field, string alias = "");
IAggregateFieldAlias MaxAs(this IGroupByView view, string field, string alias = "");
IAggregateFieldAlias MinAs(this IGroupByView view, string field, string alias = "");
IAggregateFieldAlias AvgAs(this IGroupByView view, string field, string alias = "");
```

## 3. DistinctCountAliasFieldInfo
>* 参看[DistinctCountAliasFieldInfo](xref:ShadowSql.Aggregates.DistinctCountAliasFieldInfo)
>* 对列排重计数
>* sql示意为: COUNT(DISTINCT field)
```csharp
class DistinctCountAliasFieldInfo(ICompareField field, string alias) : IAggregateFieldAlias;
```

### 3.1 DistinctCountAs扩展方法
```csharp
DistinctCountAliasFieldInfo DistinctCountAs(this ICompareField field, string alias = "Count");
DistinctCountAliasFieldInfo DistinctCountAs(this IGroupByView view, string field, string alias = "Count");
```

## 4. CountAliasFieldInfo
>* 参看[CountAliasFieldInfo](xref:ShadowSql.FieldInfos.CountAliasFieldInfo)
>* 分组计数
>* sql示意为: COUNT(*) AS alias
```csharp
class CountAliasFieldInfo(string alias) : IAggregateFieldAlias;
```

### 4.1 CountAs扩展方法
```csharp
CountAliasFieldInfo CountAs(this IGroupByView view, string alias = "Count");
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* [字段简介](./index.md)