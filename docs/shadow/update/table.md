# 单表更新
>更新单表中数据

## 1. 接口
>* [IUpdate](xref:ShadowSql.Update.IUpdate)

## 2. 类
>* [TableUpdate\<TTable\>](xref:ShadowSql.Update.TableUpdate%601)

## 3. ToUpdate
### 3.1 ToUpdate扩展方法
```csharp
TableUpdate<TTable> ToUpdate<TTable>(this TTable table, Func<TTable, ISqlLogic> query)
        where TTable : IUpdateTable;
```
```csharp
var update = new UserTable()
    .ToUpdate(table => table.Id.Equal())
    .Set(table => table.Status.AssignValue(false));
// UPDATE [Users] SET [Status]=0 WHERE [Id]=@Id
```

### 3.2 ToUpdate重载扩展方法
```csharp
TableUpdate<TTable> ToUpdate<TTable>(this TableSqlQuery<TTable> tableQuery)
        where TTable : ITable, IUpdateTable;
```
```csharp
var update = new StudentTable()
    .ToSqlQuery()
    .Where(table => table.Score.LessValue(60))
    .ToUpdate()
    .Set(table => table.Score.AddValue(10));
// UPDATE [Students] SET [Score]+=10 WHERE [Score]<60
```

### 3.3 ToUpdate重载扩展方法
```csharp
TableUpdate<TTable> ToUpdate<TTable>(this TableQuery<TTable> tableQuery)
        where TTable : ITable, IUpdateTable;
```
```csharp
var update = new StudentTable()
    .ToQuery()
    .And(table => table.Score.LessValue(60))
    .ToUpdate()
    .Set(table => table.Score.AddValue(10));
// UPDATE [Students] SET [Score]+=10 WHERE [Score]<60
```

## 4. Set方法
```csharp
TableUpdate<TTable> Set(Func<TTable, IAssignInfo> operation)
```
```csharp
var update = new UserTable()
    .ToUpdate(table => table.Id.Equal())
    .Set(table => table.Status.AssignValue(false));
// UPDATE [Users] SET [Status]=0 WHERE [Id]=@Id
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableUpdate\<TTable\>](xref:ShadowSql.Update.TableUpdate%601)的方法和扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/update/table.md)