# 别名表删除
>* 从别名表中删除数据

## 1. 接口
>[IDelete](/api/ShadowSql.Delete.IDelete.html)

## 2. 类
>[AliasTableDelete](/api/ShadowSql.Delete.AliasTableDelete.html)

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

## 4. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[AliasTableDelete](/api/ShadowSql.Delete.AliasTableDelete.html)的扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/delete/alias.md)
