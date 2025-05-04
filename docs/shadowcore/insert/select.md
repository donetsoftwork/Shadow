# 插入select
>* 本组件用来拼接含SELECT子句的INSERT语句

## 1. 接口
>* [ISelectInsert](/api/ShadowSql.Insert.ISelectInsert.html)

## 2. 基类
>* [SelectInsertBase](/api/ShadowSql.Insert.SelectInsertBase.html)

## 3. 类型
>* [SelectInsert](/api/ShadowSql.Insert.SelectInsert.html)

## 4 方法
### 4.1 Insert方法
>该方法并非必须,适用于插入和查询的列不一致的情况
```csharp
TInsert Insert<TInsert>(this TInsert insert, params IEnumerable<IColumn> columns)
        where TInsert : SelectInsertBase, ISelectInsert;
```
```csharp
IColumn name = Column.Use("Name");
IColumn age = Column.Use("Age");
IColumn name2 = Column.Use("Name2");
IColumn age2 = Column.Use("Age2");
var query = new TableSqlQuery("Students")
    .Where("AddTime between '2024-01-01' and '2025-01-01'");
var select = new TableSelect(query)
    .Select(name, age);
var insert = new SelectInsert("Backup2024", select)
    .Insert(name2, age2);
// INSERT INTO [Backup2024]([Name2],[Age2])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'
```

### 4.2 Insert重载方法
>该方法并非必须,适用于插入和查询的列不一致的情况
```csharp
TInsert Insert<TInsert>(this TInsert insert, params IEnumerable<string> columnNames)
        where TInsert : SelectInsertBase, ISelectInsert;
```
```csharp
IColumn name = Column.Use("Name");
IColumn age = Column.Use("Age");
var query = new TableSqlQuery("Students")
    .Where("AddTime between '2024-01-01' and '2025-01-01'");
var select = new TableSelect(query)
    .Select(name, age);
var insert = new SelectInsert("Backup2024", select)
    .Insert("Name2", "Age2");
// INSERT INTO [Backup2024]([Name2],[Age2])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'
```

## 4.3 普通示例
```csharp
IColumn name = Column.Use("Name");
IColumn age = Column.Use("Age");
var query = new TableSqlQuery("Students")
    .Where("AddTime between '2024-01-01' and '2025-01-01'");
var select = new TableSelect(query)
    .Select(name, age);
var insert = new SelectInsert("Backup2024", select);
// INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'
```