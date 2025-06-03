# TableSchemaBuilder
>* 构造TableSchema对象的工具
>* 建造者模式

## 1. TableSchemaBuilder类
>* 参看[TableSchemaBuilder](xref:Shadow.DDL.TableSchemaBuilder)
```csharp
class TableSchemaBuilder {
    string Schema { get; }
    string Name { get; }
    IEnumerable<ColumnSchema> Columns { get; }
    // ...其他方法...
    TableSchema Build();
}
```

## 2. DefineColumn方法
>* 定义并放回一个列(以便修改完善)
```csharp
ColumnSchema DefineColumn(string columnName, string sqlType = "INT");
```
```csharp
var builder = new TableSchemaBuilder("Students", "tenant1");
builder.DefineColumn("Id", "INTEGER")
    .ColumnType = ColumnType.Identity | ColumnType.Key;
```

## 3. DefinColumns方法
>* 定义多个相同类型的列
>* 只需要列名类型无所谓也可以使用该方法
```csharp
TableSchemaBuilder DefinColumns(string sqlType, params IEnumerable<string> columnName);
```
```csharp
var builder = new TableSchemaBuilder("Users", "tenant1")
    .DefinColumns("TEXT", "Id", "Name");
```

## 4. DefineKeys方法
>* 定义多个相同类型的主键
>* 只需要主键列名类型无所谓也可以使用该方法
```csharp
TableSchemaBuilder DefineKeys(string sqlType, params IEnumerable<string> columnName);
```
```csharp
var builder = new TableSchemaBuilder("user_role", "tenant1")
    .DefineKeys("INT", "UserId", "RoleId")
    .DefinColumns("DATETIME", "CreateTime");
```

## 5. DefineIdentity方法
>* 定义自增列
```csharp
TableSchemaBuilder DefineIdentity(string columnName, string sqlType = "INT");
```
```csharp
var builder = new TableSchemaBuilder("Students", "tenant1")
    .DefineIdentity("Id", "INTEGER")
    .DefinColumns("TEXT", "Name");
```

## 6. Build方法
>* 构造TableSchema对象
```csharp
TableSchema Build();
```
```csharp
var builder = new TableSchemaBuilder("Students", "tenant1")
    .DefineIdentity("Id", "INTEGER")
    .DefinColumns("TEXT", "Name");
TableSchema table = builder.Build();
```
