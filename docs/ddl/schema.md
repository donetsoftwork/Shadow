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

### 5.1 表Schema查询示例
~~~csharp
UserTable table = new("Users", "tenant1");
var query = new TableQuery(table)
    .And(table.Status.EqualValue(true));
var cursor = new TableCursor(query, 10, 20)
    .Desc(table.Id);
var select = new CursorSelect(cursor)
    .Select(table.Id, table.Name);
// MsSql生成sql: SELECT [Id],[Name] FROM [tenant1].[Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~
~~~csharp
public class UserTable(string tableName = "Users", string schema = "")
    : TableSchema(tableName, [Defines.Id, Defines.Name, Defines.Status], schema)
{
    #region Columns
    public readonly ColumnSchema Id = Defines.Id;
    new public readonly ColumnSchema Name  = Defines.Name;
    public readonly ColumnSchema Status = Defines.Status;
    #endregion

    class Defines
    {
        public static readonly ColumnSchema Id = new("Id") { ColumnType = ColumnType.Key };
        public static readonly ColumnSchema Name = new("Name");
        public static readonly ColumnSchema Status = new("Status");
    }
}
~~~

### 5.2 DDL+表达式版查询示例
~~~csharp
var select = new TableSchema("Users", [], "tenant1")
    .ToSqlQuery<User>()
    .Where(u => u.Status)
    .Take(10, 20)
    .Desc(u => u.Id)
    .ToSelect();
// MsSql生成sql: SELECT * FROM [tenant1].[Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~