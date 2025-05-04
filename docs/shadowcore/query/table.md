# 表逻辑查询
>从表中按逻辑查询数据

## 1. 接口
>* [IDataQuery](/api/ShadowSql.Queries.IDataQuery.html)

## 2. 类
>[TableQuery](/api/ShadowSql.Tables.TableQuery.html)

## 3. 构造函数
### 3.1 使用table
>filter默认AndLogic
```csharp
public TableQuery(ITable table, Logic filter);
public TableQuery(ITable table)
```
```csharp
var table = new UserTable();
var filter = new TableQuery(table)
     .And(table.Status.EqualValue(true));
```
>SELECT * FROM [Users] WHERE [Status]=1

### 3.2 使用表名
>filter默认AndLogic
```csharp
public TableQuery(string tableName, Logic filter);
public TableQuery(string tableName);
```
```csharp
var filter = new Tablefilter("Users")
    .And(u => u.Field("Id").Less("LastId"));
```
>SELECT * FROM [Users] WHERE [Id]<@LastId

## 5. 其他相关功能
>* 参看[逻辑查询简介](./index.md)