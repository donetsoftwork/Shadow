# 联表游标
>* 对联表进行截取,处理分页和排序
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[ICursor](xref:ShadowSql.Cursors.ICursor)

## 2. 基类
>[CursorBase](xref:ShadowSql.Cursors.CursorBase)

## 3. 类
>[MultiTableCursor](xref:ShadowSql.Expressions.Cursors.MultiTableCursor)

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
var cursor = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .Root
    .ToCursor(10, 20);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .And((u, r) => u.Id == r.UserId)
    .Root
    .ToCursor(10, 20);
~~~
~~~csharp
var cursor = new MultiTableSqlQuery()
    .AddMembers(_db.From("Employees"), _db.From("Departments"))
    .ToCursor(10, 20);
~~~
~~~csharp
var cursor = new MultiTableQuery()
    .AddMembers("Employees", "Departments")
    .ToCursor(10, 20);
~~~

## 5. Take扩展方法
>* Take方法是ToCursor的平替
>* 把[IMultiView](xref:ShadowSql.Identifiers.IMultiView)转换为联表游标,IMultiView的4个实现类可用在这里
>* [JoinTableSqlQuery](xref:ShadowSql.Join.JoinTableSqlQuery)
>* [JoinTableQuery](xref:ShadowSql.Join.JoinTableQuery)
>* [MultiTableSqlQuery](xref:ShadowSql.Join.MultiTableSqlQuery)
>* [MultiTableQuery](xref:ShadowSql.Join.MultiTableQuery)
~~~csharp
MultiTableCursor Take(this MultiTableBase query, int limit, int offset = 0);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .Root
    .Take(10, 20);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .And((u, r) => u.Id == r.UserId)
    .Root
    .Take(10, 20);
~~~
~~~csharp
var cursor = new MultiTableSqlQuery()
    .AddMembers(_db.From("Employees"), _db.From("Departments"))
    .Take(10, 20);
~~~
~~~csharp
var cursor = new MultiTableQuery()
    .AddMembers("Employees", "Departments")
    .Take(10, 20);
~~~

## 6. Asc方法
>* 正序
~~~csharp
MultiTableCursor Asc<TEntity, TProperty>(string tableName, Expression<Func<TEntity, TProperty>> select);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .And((u, r) => u.Id == r.UserId)
    .Root
    .ToCursor()
    .Asc("t2", (UserRole t2) => t2.Id);
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] ORDER BY t2.[Id]
~~~

## 7. Desc方法
>* 倒序
~~~csharp
MultiTableCursor Desc<TEntity, TProperty>(string tableName, Expression<Func<TEntity, TProperty>> select);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .As("u")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles").As("r"))
    .On((u, r) => u.Id == r.UserId)
    .Root
    .ToCursor()
    .Desc("r", (UserRole t2) => t2.Id);
// SELECT * FROM [Users] AS u INNER JOIN [UserRoles] AS r ON u.[Id]=r.[UserId] ORDER BY r.[Id] DESC
~~~

## 8. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[MultiTableCursor](xref:ShadowSql.Expressions.Cursors.MultiTableCursor)的方法和扩展方法部分
>* 参看[游标简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/cursor/index.md)