# sql表查询
>从表中按sql查询数据

## 1. 接口
>* [IDataSqlQuery](xref:ShadowSql.Queries.IDataSqlQuery)
>* [IWhere](xref:ShadowSql.Filters.IWhere)

## 2. 类
>[TableSqlQuery](xref:ShadowSql.Tables.TableSqlQuery)

## 3. 构造函数
### 3.1 使用table
>query默认SqlAndQuery
```csharp
public TableSqlQuery(ITable table, SqlQuery query);
public TableSqlQuery(ITable table)
```
```csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
     .Where(table.Status.EqualValue(true));
```
>SELECT * FROM [Users] WHERE [Status]=1

### 3.2 使用表名
>query默认SqlAndQuery
```csharp
public TableSqlQuery(string tableName, SqlQuery query);
public TableSqlQuery(string tableName);
```
```csharp
var query = new TableSqlQuery("Users")
    .Where(u => u.Field("Id").Less("LastId"));
```
>SELECT * FROM [Users] WHERE [Id]<@LastId

## 4. 其他相关功能
>* 参看[sql查询简介](./index.md)
