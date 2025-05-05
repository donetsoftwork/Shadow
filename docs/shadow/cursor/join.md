# 联表游标
>* 对联表进行截取,处理分页和排序
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[ICursor](xref:ShadowSql.Cursors.ICursor)

## 2. 基类
>[CursorBase](xref:ShadowSql.Cursors.CursorBase)

## 3. 类
>[MultiTableCursor](xref:ShadowSql.Cursors.MultiTableCursor)

## 4. ToCursor扩展方法
>* 把[IMultiView](xref:ShadowSql.Identifiers.IMultiView)转换为联表游标,IMultiView的4个实现类可用在这里
>* [JoinTableSqlQuery](xref:ShadowSql.Join.JoinTableSqlQuery)
>* [JoinTableQuery](xref:ShadowSql.Join.JoinTableQuery)
>* [MultiTableSqlQuery](xref:ShadowSql.Join.MultiTableSqlQuery)
>* [MultiTableQuery](xref:ShadowSql.Join.MultiTableQuery)
~~~csharp
MultiTableCursor ToCursor(this MultiTableBase query, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = _db.From("Employees")
    .SqlJoin(_db.From("Departments"))
    .On("DepartmentId", "Id")
    .Root
    .ToCursor(10, 20);
~~~
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var cursor = c.Join(p)
    .And(c.PostId.Equal(p.Id))
    .Root
    .ToCursor(10, 20);
~~~
~~~csharp
var cursor = _db.From("Employees")
    .SqlMulti(_db.From("Departments"))
    .Where("t1.DepartmentId=t2.Id")
    .ToCursor(10, 20);
~~~
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var cursor = c.Multi(p)
    .And(c.PostId.Equal(p.Id))
    .ToCursor(10, 20);
~~~

## 5. 按表排序
### 5.1 Asc方法
>正序
~~~csharp
MultiTableCursor Asc<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable;
~~~
~~~csharp
var cursor = new CommentTable()
    .SqlJoin(new PostTable())
    .On(c => c.PostId, p => p.Id)
    .Root
    .ToCursor()
    .Asc<PostTable>("t2", t2 => t2.Id);
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t2.[Id]
~~~

### 5.2 Desc方法
>倒序
~~~csharp
MultiTableCursor Desc<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable;
~~~
~~~csharp
CommentTable c = new();
PostTable p = new();
var cursor = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .Root
    .ToCursor()
    .Desc<PostTable>("t2", t2 => t2.Id);
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t2.[Id] DESC
~~~

## 6. 按别名表排序
### 6.1 Asc方法
>正序
~~~csharp
MultiTableCursor Asc<TAliasTable>(string tableName, Func<TAliasTable, IOrderView> select)
        where TAliasTable : IAliasTable;
~~~
~~~csharp
var cursor = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .Root
    .ToCursor()
    .Asc<CommentAliasTable>("c", c => c.Id);
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Id]
~~~

### 6.2 Desc方法
>倒序
~~~csharp
MultiTableCursor Desc<TAliasTable>(string tableName, Func<TAliasTable, IOrderAsc> select)
        where TAliasTable : IAliasTable;
~~~
~~~csharp
var cursor = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .Root
    .ToCursor()
    .Desc<CommentAliasTable>("c", c => c.Pick);
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Pick] DESC
~~~

## 7. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[MultiTableCursor](xref:ShadowSql.Cursors.MultiTableCursor)的方法和扩展方法部分
>* 参看[游标简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/cursor/index.md)