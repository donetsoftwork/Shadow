# Column
>* 表的原始列
>* 作为数据表列的影子(占位符)

## 1. 接口
>* [IColumn](xref:ShadowSql.Identifiers.IColumn)
>* [IField](xref:ShadowSql.Identifiers.IField)
>* [IFieldView](xref:ShadowSql.Identifiers.IFieldView)
>* [ICompareField](xref:ShadowSql.Identifiers.ICompareField)
>* [IOrderField](xref:ShadowSql.Identifiers.IOrderField)
>* [IOrderAsc](xref:ShadowSql.Identifiers.IOrderAsc)
>* [IOrderView](xref:ShadowSql.Identifiers.IOrderView)
>* [IAssignView](xref:ShadowSql.Identifiers.IAssignView)

## 2. Column类型
>* 参看[Column](xref:ShadowSql.Identifiers.Column)
```csharp
class Column(string name) : IColumn;
```

## 3. Use静态方法
```csharp
static Column Use(string name);
```

## 4. Column扩展方法
>* 获取列,并校验
>* 如果列不存在触发异常
```csharp
IColumn Column(this ITable table, string columnName)
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* [字段简介](./index.md)
