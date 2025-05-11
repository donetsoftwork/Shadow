# PrefixField
>* 用于自定义别名表结构
>* 映射sql中带表前缀的列
>* 带表前缀的列影子(占位符)

## 1. 接口
>* [IPrefixField](xref:ShadowSql.Identifiers.IPrefixField)
>* [IField](xref:ShadowSql.Identifiers.IField)
>* [IFieldView](xref:ShadowSql.Identifiers.IFieldView)
>* [ICompareField](xref:ShadowSql.Identifiers.ICompareField)
>* [IOrderField](xref:ShadowSql.Identifiers.IOrderField)
>* [IOrderAsc](xref:ShadowSql.Identifiers.IOrderAsc)
>* [IOrderView](xref:ShadowSql.Identifiers.IOrderView)
>* [IAssignView](xref:ShadowSql.Identifiers.IAssignView)

## 2. PrefixField类型
>* 参看[PrefixField](xref:ShadowSql.Variants.PrefixField)
```csharp
class PrefixField(IColumn column, params string[] prefix) : IPrefixField;
```

## 3. Prefix扩展方法
>* 构造及获取[IPrefixField](xref:ShadowSql.Identifiers.IPrefixField)
```csharp
PrefixField Prefix(this IColumn column, string tableName);
IPrefixField Prefix(this IColumn column, IAliasTable table);
IPrefixField Prefix(this IAliasTable table, string columnName);
IPrefixField Prefix(this IAliasTable table, IColumn column);
IPrefixField Prefix(this IJoinOn join, IColumn column);
IPrefixField Prefix(this IJoinOn join, string columnName);
IPrefixField Prefix<TTable>(this IAliasTable<TTable> table, Func<TTable, IColumn> query);
```

## 4. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* [字段简介](./index.md)