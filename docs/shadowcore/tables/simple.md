# EmptyTable
>* 简单表(列为空)
>* 作为表的影子(占位符)
>* 主要用于[按字段查询](../../shadow/sqlquery/fieldquery.md)
>* 支持select和delete等
>* 不支持update和insert,进行联表后也不支持

## 1. 接口
>* [ITable](xref:ShadowSql.Identifiers.ITable)
>* [ITableView](xref:ShadowSql.Identifiers.ITableView)

## 2. EmptyTable类
>* 参看[EmptyTable](xref:ShadowSql.Tables.EmptyTable)
```csharp
class EmptyTable(string name) : ITable, ITableView {
    string Name { get; }
}
```

## 3. 静态方法Use
>* 缓存EmptyTable对象
>* 避免重复创建及垃圾回收
```csharp
static EmptyTable Use(string tableName);
```

## 4. Select功能
>* 参看[获取表精简版](../select/table.md)
>* 参看[表分页精简版](../select/cursor.md)
>* 参看[获取表易用版](../../shadow/select/table.md)
>* 参看[表分页易用版](../../shadow/select/tablecursor.md)
```csharp
var query = EmptyTable.Use("Users")
    .ToSqlQuery()
    .FieldEqualValue("Id", 1)
    .ToSelect()
    .Select("Id", "Name");
// SELECT [Id],[Name] FROM [Users] WHERE [Id]=1
```

## 5. Delete功能
>* 参看[精简版](../delete/table.md)
>* 参看[易用版](../../shadow/delete/table.md)
```csharp
var query = EmptyTable.Use("Users")
    .ToSqlQuery()
    .FieldEqualValue("Id", 1)
    .ToDelete();
// DELETE FROM [Users] WHERE [Id]=1
```

## 6. 其他相关功能
>* 本组件并非只支持以上功能,其他功能参看以下文档:
>* 参看[EmptyTable](xref:ShadowSql.Tables.EmptyTable)
>* 参看[按字段查询](../../shadow/sqlquery/fieldquery.md)
>* 参看[获取简介](../../shadow/select/index.md)
>* 参看[获取表](../../shadow/select/table.md)
>* 参看[分页获取表](../../shadow/select/tablecursor.md)
