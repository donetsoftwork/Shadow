# TableSchema
>* 作为表的影子(占位符)
>* 主要用来支持createtable
>* 与Table一样也支持select、update、insert和delete等
>* TableSchema[按字段查询](../shadow/sqlquery/fieldquery.md)也是会校验的,不存在的字段会抛异常
>* 本文sql示例按Sqlite数据库

## 1. 接口
>* [ITable](xref:ShadowSql.Identifiers.ITable)
>* [ITableView](xref:ShadowSql.Identifiers.ITableView)
>* [IInsertTable](xref:ShadowSql.Identifiers.IInsertTable)
>* [IUpdateTable](xref:ShadowSql.Identifiers.IUpdateTable)

## 2. TableSchema类
>* 参看[TableSchema](xref:Shadow.DDL.Schemas.TableSchema)
```csharp
class TableSchema
    : ITable, IInsertTable, IUpdateTable, ITableView {
    string Schema { get; }
    string Name { get; }
    ColumnSchema[] Columns { get; }
    ColumnSchema[] Keys { get; }
    ColumnSchema? GetColumn(string columName);
}
```

## 3. CreateTable功能
~~~csharp
    ColumnSchema id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
    ColumnSchema name = new("Name", "TEXT");
    TableSchema table = new("Students", [id, name]);
    CreateTable create = new(table);
// CREATE TABLE "Students"("Id" INTEGER PRIMARY KEY AUTOINCREMENT,"Name" TEXT)
~~~

## 4. DropTable功能
~~~csharp
var drop = new DropTable("Students");
// DROP TABLE "Students"
~~~

## 5. 其他功能与Table类似
>* 参看[Table](../shadowcore/tables/table.md)
