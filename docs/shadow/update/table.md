# 单表更新
>更新单表中数据

## 1. 接口
>[IUpdate](/api/shadowcore/update/IUpdate.html)

## 2. 类
>[TableUpdate](/api/shadowcore/update/TableUpdate.html)

## 3. 方法
### 3.1 Set方法
```csharp
var table = new UserTable();
var update = new TableUpdate(table, table.Id.Equal())
    .Set(table.Status.EqualToValue(false));
// UPDATE [Users] SET [Status]=0 WHERE [Id]=@Id
```

### 3.2 Create静态方法
```csharp
var id = Column.Use("Id");
var status = Column.Use("Status");
var update = TableUpdate.Create("Users", id.EqualValue(1))
     .Set(status.EqualToValue(false));
// UPDATE [Users] SET [Status]=0 WHERE [Id]=1
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableUpdate](/api/shadowcore/update/TableUpdate.html)的方法和扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/update/table.md)