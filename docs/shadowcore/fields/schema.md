# ColumnSchema
>* 包含数据库字段类型的原始列
>* 主要用于组成TableSchema,用来支持CreateTable

## 1. 接口
>* [IColumn](xref:ShadowSql.Identifiers.IColumn)
>* [IField](xref:ShadowSql.Identifiers.IField)
>* [IFieldView](xref:ShadowSql.Identifiers.IFieldView)
>* [ICompareField](xref:ShadowSql.Identifiers.ICompareField)
>* [IOrderField](xref:ShadowSql.Identifiers.IOrderField)
>* [IOrderAsc](xref:ShadowSql.Identifiers.IOrderAsc)
>* [IOrderView](xref:ShadowSql.Identifiers.IOrderView)
>* [IAssignView](xref:ShadowSql.Identifiers.IAssignView)

## 2. ColumnSchema类型
>* 参看[ColumnSchema](xref:Shadow.DDL.Schemas.ColumnSchema)
```csharp
class ColumnSchema(string name, string sqlType = "INT") : IColumn {
	string SqlType { get; }
	string Default { get; set; }
	bool NotNull { get; set; }
	ColumnType ColumnType { get; set; }
}
```

## 3. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* [TableSchema](../tables/schema.md)
>* [字段简介](./index.md)
