# Table
>* 定义表结构,包含列
>* 作为表的影子(占位符)
>* 支持select、update、insert和delete
>* 可以支持[严格查询](../../shadow/sqlquery/columnquery.md)
>* 可作为自定义表的基类

## 1. 接口
>* [ITable](xref:ShadowSql.Identifiers.ITable)
>* [ITableView](xref:ShadowSql.Identifiers.ITableView)
>* [IInsertTable](xref:ShadowSql.Identifiers.IInsertTable)
>* [IUpdateTable](xref:ShadowSql.Identifiers.IUpdateTable)

## 2. Table类
>* 参看[Table](xref:ShadowSql.Identifiers.Table)
```csharp
class Table(string name) : ITable, IInsertTable, IUpdateTable, ITableView {
    string Name { get; }
    IEnumerable<IColumn> Columns { get; }
}
```

## 3. 方法
### 3.1 定义列方法
```csharp
void AddColumn(IColumn column);
IColumn DefineColumn(string columnName);
TTable AddColums<TTable>(this TTable table, params IEnumerable<IColumn> columns)
    where TTable : Table;
TTable DefineColums<TTable>(this TTable table, params IEnumerable<string> columns)
    where TTable : Table;
```
```csharp
var name = Column.Use("Name");
var age = Column.Use("Age");
var table = new Table("Students")
    .AddColums(name, age);
```
```csharp
var table = new Table("Students")
    .DefineColums("Name", "Age");
```

### 3.2 IgnoreInsert扩展方法
>* 插入忽略配置
>* 该扩展方法适用继承Table的自定义表
```csharp
TTable IgnoreInsert<TTable>(this TTable table, params IEnumerable<IColumn> columns)
    where TTable : Table;
TTable IgnoreInsert<TTable>(this TTable table, params IEnumerable<string> columns)
    where TTable : Table;
```
```csharp
var table = new Table("Users")
    .DefineColums("Id","Name", "Age")
    .IgnoreInsert("Id");
var insert = new SingleInsert(table)
    .InsertSelfColumns();
// 预设Id是自增列,可查询不可插入
// INSERT INTO [Users]([Name],[Age])VALUES(@Name,@Age)
```

### 3.3 IgnoreUpdate扩展方法
>* 更新忽略配置
>* 该扩展方法适用继承Table的自定义表
```csharp
TTable IgnoreUpdate<TTable>(this TTable table, params IEnumerable<IColumn> columns)
    where TTable : Table;
TTable IgnoreUpdate<TTable>(this TTable table, params IEnumerable<string> columns)
    where TTable : Table;
```
```csharp
var id = Column.Use("Id");
var name = Column.Use("Name");
var age = Column.Use("Age");
var table = new Table("Users")
    .AddColums(id, name, age)
    .IgnoreUpdate(id);
var update = new TableUpdate(table, id.Equal())
    .SetSelfFields();
// 预设Id是主键,可查询不可更新
// UPDATE [Users] SET [Name]=@Name,[Age]=@Age WHERE [Id]=@Id
```

### 3.4 Copy扩展方法
```csharp
Table Copy(this Table source, string newName = "");
```
```csharp
var table = new Table("Users")
    .DefineColums("Id", "Name", "Age")
    .IgnoreInsert("Id");
var backup = table.Copy("Users2025");
var insert = new SingleInsert(backup)
    .InsertSelfColumns();
// 预设Id是自增列,可查询不可插入
// INSERT INTO [Users2025]([Name],[Age])VALUES(@Name,@Age)
```

## 4. Insert功能
### 4.1 插入单条
>* 参看[插入单条精简版](../insert/single.md)
>* 参看[插入单条易用版](../../shadow/insert/single.md)
```csharp
var name = Column.Use("Name");
var score = Column.Use("Score");
var insert = new SingleInsert("Students")
    .InsertColumns(name, score);
// INSERT INTO [Students]([Name],[Score])VALUES(@Name,@Score)
```

### 4.2 插入多条
>* 参看[插入多条精简版](../insert/multi.md)
>* 参看[插入多条易用版](../../shadow/insert/multi.md)
```csharp
var name = Column.Use("Name");
var score = Column.Use("Score");
var insert = new MultiInsert("Students")
    .Insert(name.InsertValues("张三", "李四"))
    .Insert(score.InsertValues(90, 85));
// INSERT INTO [Students]([Name],[Score])VALUES('张三',90),('李四',85)
```

### 4.3 插入Select子查询
>* 参看[插入Select精简版](../insert/select.md)
>* 参看[插入Select易用版](../../shadow/insert/select.md)
```csharp
var name = Column.Use("Name");
var age = Column.Use("Age");
var query = new TableSqlQuery("Students")
    .Where("AddTime between '2024-01-01' and '2025-01-01'");
var select = new TableSelect(query)
    .Select(name, age);
var insert = new SelectInsert("Backup2024", select);
// INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'
```

## 5. Delete功能
>* 参看[表删除精简版](../delete/table.md)
>* 参看[表删除易用版](../../shadow/delete/table.md)
```csharp
var id = Column.Use("Id");
var delete = new TableDelete("Users", id.EqualValue(1));
// DELETE FROM [Users] WHERE [Id]=1
```

## 6. Update功能
>* 参看[单表更新精简版](../update/table.md)
>* 参看[单表更新易用版](../../shadow/update/table.md)
```csharp
var id = Column.Use("Id");
var age = Column.Use("Age");
var update = new TableUpdate("Users", id.EqualValue(1))
    .Set(age.AddValue(1));
// UPDATE [Users] SET [Age]+=1 WHERE [Id]=1
```

## 7. Select功能
>* 参看[获取表精简版](../select/table.md)
>* 参看[表分页精简版](../select/cursor.md)
>* 参看[获取表易用版](../../shadow/select/table.md)
>* 参看[表分页易用版](../../shadow/select/tablecursor.md)
```csharp
var select = new TableSelect("Users")
    .Select("Id", "Name");
// SELECT [Id],[Name] FROM [Users]
```

## 8. 自定义表
>* 继承Table的自定义表也继承了Table的所有功能
>* select、update、insert和deleted等都可以支持
```csharp
public class CommentTable : Table {
    public CommentTable(string tableName = "Comments") : base(tableName) {
        Id = DefineColumn(nameof(Id));
        UserId = DefineColumn(nameof(UserId));
        PostId = DefineColumn(nameof(PostId));
        Content = DefineColumn(nameof(Content));
        Pick = DefineColumn(nameof(Pick));
    }
    public readonly IColumn Id;
    public readonly IColumn UserId;
    public readonly IColumn PostId;
    public readonly IColumn Content;
    public readonly IColumn Pick;
}
```
```csharp
var table = new CommentTable();
var query = new TableSqlQuery(table)
    .Where(table.Pick.EqualValue(true));
var select = new TableSelect(query)
    .Select(table.Id, table.Content);
// SELECT [Id],[Content] FROM [Comments] WHERE [Pick]=1
```

## 9. 其他相关功能
>* 本组件并非只支持以上功能,其他功能参看以下文档:
>* 参看[Table](xref:ShadowSql.Identifiers.Table)
>* 参看[插入单条精简版](../insert/single.md)
>* 参看[插入单条易用版](../../shadow/insert/single.md)
>* 参看[插入多条精简版](../insert/multi.md)
>* 参看[插入多条易用版](../../shadow/insert/multi.md)
>* 参看[插入Select精简版](../insert/select.md)
>* 参看[插入Select易用版](../../shadow/insert/select.md)
>* 参看[表删除精简版](../delete/table.md)
>* 参看[表删除易用版](../../shadow/delete/table.md)
>* 参看[单表更新精简版](../update/table.md)
>* 参看[单表更新易用版](../../shadow/update/table.md)
>* 参看[按字段查询](../../shadow/sqlquery/fieldquery.md)
>* 参看[严格查询](../../shadow/sqlquery/columnquery.md)
>* 参看[获取表精简版](../select/table.md)
>* 参看[表分页精简版](../select/cursor.md)
>* 参看[获取表易用版](../../shadow/select/table.md)
>* 参看[表分页易用版](../../shadow/select/tablecursor.md)
