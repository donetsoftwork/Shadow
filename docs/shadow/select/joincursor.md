# 联表分页
>* 从联表分页获取数据组件
>* 本组件用来处理sql的SELECT分页语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性
>* 本组件通过[联表游标](../cursor/join.md)来分页

## 1. 接口
>[ISelect](/api/ShadowSql.Select.ISelect.html)

## 2. 基类
>* [SelectFieldsBase](/api/ShadowSql.Select.SelectFieldsBase.html)

## 3. 类
>[MultiTableCursorSelect](/api/ShadowSql.CursorSelect.MultiTableCursorSelect.html)

## 4. ToSelect扩展方法
>* 从联表游标创建[MultiTableCursorSelect](/api/ShadowSql.CursorSelect.MultiTableCursorSelect.html)
~~~csharp
MultiTableSelect ToSelect(this IMultiView table);
~~~
~~~csharp
var select = _db.From("Employees")
    .SqlJoin(_db.From("Departments"))
    .OnColumn("DepartmentId", "Id")
    .Root
    .ToCursor(10, 20)
    .OrderBy("t1.Id DESC")
    .ToSelect();
// SELECT * FROM [Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] ORDER BY t1.Id DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 5. Select
### 5.1 Select方法
>* 从表筛选字段
~~~csharp
MultiTableCursorSelect Select<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable;
MultiTableCursorSelect Select<TTable>(string tableName, Func<TTable, IEnumerable<IColumn>> select)
        where TTable : ITable;
~~~
~~~csharp
var select = new CommentTable()
    .SqlJoin(new PostTable())
    .On(t1 => t1.PostId, t2 => t2.Id)
    .Root
    .ToCursor(10, 20)
    .Desc<CommentTable>("Comments", c => c.Id)
    .ToSelect()
    .Select<CommentTable>("Comments", t1 => t1.Content);
// SELECT t1.[Content] FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t1.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~
~~~csharp
var select = new CommentTable()
    .SqlJoin(new PostTable())
    .On(t1 => t1.PostId, t2 => t2.Id)
    .Root
    .ToCursor(10, 20)
    .Desc<CommentTable>("Comments", c => c.Id)
    .ToSelect()
    .Select<CommentTable>("Comments", t1 => [t1.Id, t1.Content]);
// SELECT t1.[Id],t1.[Content] FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t1.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

### 5.2 Select重载方法
>* 从别名表筛选字段
~~~csharp
MultiTableCursorSelect Select<TAliasTable>(string tableName, Func<TAliasTable, IFieldView> select)
        where TAliasTable : IAliasTable;
MultiTableCursorSelect Select<TAliasTable>(string tableName, Func<TAliasTable, IEnumerable<IFieldView>> select)
        where TAliasTable : IAliasTable;
~~~
~~~csharp
var select = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .Root
    .ToCursor(10, 20)
    .Desc<CommentAliasTable>("c", c => c.Id)
    .ToSelect()
    .Select<CommentAliasTable>("Comments", c => c.Content);
// SELECT c.[Content] FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var select = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .Root
    .ToCursor(10, 20)
    .Desc<CommentAliasTable>("c", c => c.Id)
    .ToSelect()
    .Select<CommentAliasTable>("Comments", c => [c.Id, c.Content]);
// SELECT c.[Id],c.[Content] FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 6. SelectTable扩展方法
>筛选别名表
~~~csharp
TMultiTableSelect SelectTable<TMultiTableSelect>(this TMultiTableSelect multiSelect, IAliasTable aliasTable)
        where TMultiTableSelect : MultiSelectBase;
TMultiTableSelect SelectTable<TMultiTableSelect>(this TMultiTableSelect multiSelect, string tableName)
        where TMultiTableSelect : MultiSelectBase;
~~~
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var select = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .Root
    .ToCursor(10, 20)
    .Desc<CommentAliasTable>("c", c => c.Id)
    .ToSelect()
    .SelectTable(c);
// SELECT c.* FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~
~~~csharp
var select = new Table("Comments")
    .SqlJoin(new Table("Posts"))
    .OnColumn("PostId", "Id")
    .Root
    .ToCursor(10, 20)
    .OrderBy("t1.Id DESC")
    .ToSelect()
    .SelectTable("Comments");
// SELECT t1.* FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t1.Id DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 7. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[联表游标](../cursor/join.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)
