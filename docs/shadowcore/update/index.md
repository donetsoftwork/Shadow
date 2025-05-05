# 更新简介
>* UPDATE由SET、WHERE、FROM及UPDATE等部分组成
>* 其中FROM及UPDATE较为简单
>* WHERE可以由[sqlquery](../sqlquery/index.md)和[query](../query/index.md)实现
>* 该功能主要处理SET子句
>* 实现了[单表更新](./table.md)和[联表更新](./multi.md)

## 1. 接口
>[IUpdate](xref:ShadowSql.Update.IUpdate)

## 2. 基类
>[UpdateBase](xref:ShadowSql.Update.UpdateBase)

## 3. 方法
### 3.1 Set扩展方法
>IAssignInfo一般通过列(字段)来运算
```csharp
TUpdate Set<TUpdate>(this TUpdate update, IAssignInfo operation)
	where TUpdate : UpdateBase, IUpdate;
```
```csharp
var table = new UserTable();
var update = new TableUpdate(table, table.Id.Equal())
    .Set(table.Status.EqualToValue(false));
// UPDATE [Users] SET [Status]=0 WHERE [Id]=@Id
```

### 3.2 SetParameter扩展方法
>按参数修改
```csharp
TUpdate SetParameter<TUpdate>(this TUpdate update, string columnName, string op = "=", string parameter = "")
        where TUpdate : UpdateBase, IUpdate;
```
```csharp
var id = Column.Use("Id");
var update = TableUpdate.Create("Users", id.EqualValue(1))
    .SetParameter("Status", "=", "DenyStatus");
// UPDATE [Users] SET [Status]=@DenyStatus WHERE [Id]=1
```

### 3.3 SetEqualTo扩展方法
>* SetEqualTo是SetParameter的简化
>* op固定为=
```csharp
TUpdate SetEqualTo<TUpdate>(this TUpdate update, string columnName, string parameter = "")
        where TUpdate : UpdateBase, IUpdate;
```
```csharp
var id = Column.Use("Id");
var update = TableUpdate.Create("Users", id.EqualValue(1))
    .SetEqualTo("Status", "DenyStatus");
// UPDATE [Users] SET [Status]=@DenyStatus WHERE [Id]=1
```

### 3.4 SetValue扩展方法
>按值修改
```csharp
TUpdate SetValue<TUpdate, TValue>(this TUpdate update, string columnName, TValue value, string op = "=")
    where TUpdate : UpdateBase, IUpdate;
```
```csharp
var id = Column.Use("Id");
var update = TableUpdate.Create("Students", id.EqualValue(1))
    .SetValue("Score", 8, "+=");
// UPDATE [Students] SET [Score]+=8 WHERE [Id]=1
```


### 3.5 SetEqualToValue扩展方法
>* SetEqualToValue是SetValue的简化
>* op固定为=
```csharp
TUpdate SetEqualToValue<TUpdate, TValue>(this TUpdate update, string columnName, TValue value)
    where TUpdate : UpdateBase, IUpdate;
```
```csharp
var id = Column.Use("Id");
var update = TableUpdate.Create("Students", id.EqualValue(1))
    .SetEqualToValue("Score", 60);
// UPDATE [Students] SET [Score]=60 WHERE [Id]=1
```

### 3.6 SetRaw扩展方法
>按原生sql修改
```csharp
TUpdate SetRaw<TUpdate>(this TUpdate update, string assignSql)
        where TUpdate : UpdateBase, IUpdate;
```
```csharp
var id = Column.Use("Id");
var update = TableUpdate.Create("Students", id.EqualValue(1))
    .SetRaw("Score=60");
// UPDATE [Students] SET Score=60 WHERE [Id]=1
```
