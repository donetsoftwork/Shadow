# 单表更新
>更新单表中数据

## 1. 接口
>* [IUpdate](xref:ShadowSql.Update.IUpdate)

## 2. 类
>* [TableUpdate](xref:ShadowSql.Update.TableUpdate)

## 3. Create静态方法
```csharp
TableUpdate Create(string tableName, ISqlLogic filter);
```
```csharp
var id = Column.Use("Id");
var status = Column.Use("Status");
var update = TableUpdate.Create("Users", id.EqualValue(1))
     .Set(status.AssignValue(false));
// UPDATE [Users] SET [Status]=0 WHERE [Id]=1
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableUpdate](xref:ShadowSql.Update.TableUpdate)的方法和扩展方法部分
>* 参看[更新简介](./index.md)
