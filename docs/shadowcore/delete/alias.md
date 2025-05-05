# 别名表删除
>从别名表中删除数据

## 1. 接口
>[IDelete](xref:ShadowSql.Delete.IDelete)

## 2. 类
>[AliasTableDelete](xref:ShadowSql.Delete.AliasTableDelete)

## 3. 相关方法
### 3.1 ToDelete扩展方法
```csharp
AliasTableDelete ToDelete(this IAliasTable table, ISqlLogic where)
	=> new(table, where);
```
```csharp
PostAliasTable table = new("p");
var delete = new AliasTableDelete(table, table.Id.EqualValue(1));
// DELETE p FROM [Posts] AS p WHERE p.[Id]=10
```


## 4. 普通示例
```csharp
PostAliasTable table = new("p");
var delete = new AliasTableDelete(table, table.Id.EqualValue(1));
// DELETE p FROM [Posts] AS p WHERE p.[Id]=1
```