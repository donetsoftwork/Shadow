# 使用方式
>* ShadowSql不是全家桶
>* 不把所有功能都做一个项目里面就是为了大家不一次引用所有的nuget包
>* 大家可以先判断需要哪些功能,再引用对应的nuget包
>* 当然同时引用这6个nuget包也是可以的

## 一、相关项目简介
|名称|项目|Nuget包|依赖|大小|主要用途|
|:---|:---|:---|:---|:---|:---|
|精简版|ShadowSql.Core|ShadowSql.Core|无|147K|简洁高效拼接sql|
|易用版|ShadowSql|ShadowSql|ShadowSql.Core|96K(+147K)|泛型表操作、链式拼接sql|
|表达式版|ShadowSql.Expressions|ShadowSql.Expressions|ShadowSql.Core|75K(+147K)|表达式树操作、链式拼接sql|
|DDL|Shadow.DDL|Shadow.DDL|ShadowSql.Core|15K(+147K)|拼接CreateTable、表Schema支持|
|Dapper|Dapper|Dapper|无|240K|执行sql、类型Mapping|
|精简版扩展|Dapper.Shadow.Core|ShadowSql.Dapper.Core|ShadowSql.Core和Dapper|17K(+387K)|执行ShadowSql.Core拼接的sql|
|易用版扩展|Dapper.Shadow|ShadowSql.Dapper|ShadowSql.Dapper.Core、ShadowSql.Core、ShadowSql和Dapper|35K(+500K)|执行ShadowSql拼接的sql|

## 二、如何选型
>* 主要看个人或团队偏好

### 1. 偏好Lambda选表达式版
>* 表达式树是Lambda的影子,可以生成委托,但这里只解析表达式树
>* 表达式版提供类EF的查询体验
>* 表达式树会用到反射,性能上会有点损耗,对比带来的便利性完全是可以接受的
>* 如果使用参数查询并缓存sql复用,完全可以忽略这个性能损耗,甚至可以忽略整个拼写sql的性能消耗
>* 表达式版搭配Dapper.Shadow.Core或者直接搭配Dapper甚至ADO.NET使用


#### 1.1 对比EFCore
#### [EFCore](#tab/ef)
> ShadowSql不含EFCore的功能,这个示例是为了和表达式版对比
~~~csharp
var query = context.Set<User>("Users")
    .Where(u => u.Status)
    .Take(10)
    .Skip(20)
    .OrderByDescending(u => u.Id)
    .Select(u => new { u.Id, u.Name });
~~~

#### [表达式版](#tab/expression)
> 表达式版可以写出与EFCore类似的代码
[!code-csharp[](../Tests/ShadowSql.ExpressionsTests/CursorSelect/TableCursorSelectTests.cs#Filter)]

---

#### 1.2 表达式版参数化查询示例
> 表达式版可以写出与EFCore类似的代码
>
[!code-csharp[](../Tests/ShadowSql.ExpressionsTests/CursorSelect/TableCursorSelectTests.cs#Parameter)]
>
> [!tip]
> MsSql: SELECT [Id],[Name] FROM [Users] WHERE [Id]=@Id2 AND [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY

#### 1.3 表达式版更多功能
>* 参看[表达式版简介](./expression/index.md)

### 2. 偏好链式查询选易用版
>* 易用版支持泛型表操作几乎无性能损耗(对比精简版)
>* 每个模型类最好定义数据表影子类(联表定义别名表),以便做到sql编译检测
>* 不定义数据表影子类可以用[按字段查询](./shadow/sqlquery/fieldquery.md)或[严格查询](./shadow/sqlquery/fieldquery.md)
>* ShadowSql搭配Dapper.Shadow或Dapper.Shadow.Core或直接搭配Dapper甚至ADO.NET使用

#### 2.1 易用版示例
~~~csharp
var select = new UserTable()
    .ToSqlQuery()
    .Where(table => table.Status.EqualValue(true))
    .Take(10, 20)
    .Asc(table => table.Id)
    .ToSelect()
    .Select(table => [table.Id, table.Name]);
~~~
> 
> [!tip]
> MsSql: SELECT [Id],[Name] FROM [Users] WHERE [Id]=@Id2 AND [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY

#### 2.2 易用版更多功能
>* 参看[易用版版简介](./shadow/index.md)

### 3. 有“洁癖”选精简版
>* 使用精简版也可以定义数据表影子类(联表定义别名表)
>* 查询时使用列或者数据表的列,这样代码可读性高,也可以做到sql编译检测
>* ShadowSql.Core搭配Dapper.Shadow.Core或者直接搭配Dapper甚至ADO.NET使用

#### 3.1 精简版示例
~~~csharp
var table = new UserTable();
var query = new TableSqlQuery(table)
    .Where(table.Status.EqualValue(true));
var cursor = new TableCursor(query)
    .Take(10)
    .Skip(20)
    .Asc(table.Id);
var select = new CursorSelect(cursor)
    .Select(table.Id, table.Name);
~~~

#### 3.2 易用版更多功能
>* 参看[精简版简介](./shadowcore/index.md)

### 4. 需要CreateTable或表Schema的选DDL
>* DDL可以搭配易用版或表达式版
>* 参看[DDL简介](./ddl/index.md)

#### 4.1 CreateTable
~~~csharp
    ColumnSchema id = new("Id", "INTEGER") { ColumnType = ColumnType.Identity | ColumnType.Key };
    ColumnSchema name = new("Name", "TEXT");
    TableSchema table = new("Students", [id, name]);
    CreateTable create = new(table);
~~~
> 
> [!tip]
> Sqlite: CREATE TABLE "Students"("Id" INTEGER PRIMARY KEY AUTOINCREMENT,"Name" TEXT)

#### 4.2 表Schema查询示例
~~~csharp
UserTable table = new("Users", "tenant1");
var query = new TableQuery(table)
    .And(table.Status.EqualValue(true));
var cursor = new TableCursor(query, 10, 20)
    .Desc(table.Id);
var select = new CursorSelect(cursor)
    .Select(table.Id, table.Name);
~~~
> 
> [!tip]
> MsSql: SELECT [Id],[Name] FROM [tenant1].[Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY

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

#### 4.3 DDL+表达式版查询示例
~~~csharp
var select = new TableSchema("Users", [], "tenant1")
    .ToSqlQuery<User>()
    .Where(u => u.Status)
    .Take(10, 20)
    .Desc(u => u.Id)
    .ToSelect();
~~~
> 
> [!tip]
> MsSql: SELECT * FROM [tenant1].[Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY

### 5. 易用版和表达式版一般不一起使用
>* 易用版和表达式版有同名类和方法,在不同命名空间
>* 同一个类引用命名空间ShadowSql和ShadowSql.Expressions调用某些方法可能会报不明确引用错误
>* 同一项目一个类用易用版,另一个类用表达式版是可行的
