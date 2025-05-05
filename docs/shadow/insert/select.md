# 插入select
>* 本组件用来拼接含SELECT子句的INSERT语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [ISelectInsert](xref:ShadowSql.Insert.ISelectInsert)

## 2. 基类
>* [SelectInsertBase](xref:ShadowSql.Insert.SelectInsertBase)

## 3. 类型
>* [SelectInsert<TTable>](xref:ShadowSql.Insert.SelectInsert%601)

## 4 方法
### 4.1 ToInsert扩展方法
```csharp
SelectInsert<TTable> ToInsert<TTable>(this TTable table, ISelect select)
        where TTable : ITable;
```
```csharp
var select = _db.From("Students")
    .ToSqlQuery()
    .Where("AddTime between '2024-01-01' and '2025-01-01'")
    .ToSelect()
    .Select("Name", "Age");
var insert = _db.From("Backup2024")
    .ToInsert(select);
// INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'
```

### 4.2 InsertTo
#### 4.2.1 InsertTo扩展方法
```csharp
SelectInsert<TTable> InsertTo<TTable>(this ISelect select, TTable table)
      where TTable : ITable;
```
```csharp
var insert = _db.From("Students")
    .ToSqlQuery()
    .Where("AddTime between '2024-01-01' and '2025-01-01'")
    .ToSelect()
    .Select("Name", "Age")
    .InsertTo(_db.From("Backup2024"));
// INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'
```

#### 4.2.2 InsertTo重载扩展方法
```csharp
SelectInsert<TTable> InsertTo<TTable>(this ISelect select, TTable table)
      where TTable : ITable;
```
```csharp
var insert = _db.From("Students")
    .ToSqlQuery()
    .Where("AddTime between '2024-01-01' and '2025-01-01'")
    .ToSelect()
    .Select("Name", "Age")
    .InsertTo(_db.From("Backup2024"));
// INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[SelectInsert<TTable>](xref:ShadowSql.Insert.SelectInsert%601)的方法和扩展方法部分
>* 参看[select](../select/index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/insert/select.md)
