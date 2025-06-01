# 联表分页
>* 从联表分页获取数据组件
>* 本组件用来处理sql的SELECT分页语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性
>* 本组件通过[联表游标](../cursor/join.md)来分页

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)
>* [IMultiSelect](xref:ShadowSql.Expressions.Select.IMultiSelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>* [MultiTableCursorSelect](xref:ShadowSql.Expressions.CursorSelect.MultiTableCursorSelect)

## 4. ToSelect扩展方法
>* 从联表游标创建[MultiTableCursorSelect](xref:ShadowSql.Expressions.CursorSelect.MultiTableCursorSelect)
~~~csharp
MultiTableCursorSelect ToSelect(this MultiTableCursor cursor);
~~~
~~~csharp
var select = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On(u => u.Id, r => r.UserId)
    .Root
    .ToCursor(10, 20)
    .Desc("t1", (User t1) => t1.Id)
    .ToSelect();
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] ORDER BY t1.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 5. Select
~~~csharp
MultiTableCursorSelect Select<TEntity, TProperty>(string tableName, Expression<Func<TEntity, TProperty>> select);
~~~
~~~csharp
var select = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .Root
    .ToCursor(10, 20)
    .Desc("t1", (User t1) => t1.Id)
    .ToSelect()
    .Select("Users", (User t1) => t1.Id);
// SELECT t1.[Id] FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] ORDER BY t1.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~
~~~csharp
var select = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .And((u,r) => u.Id == r.UserId)
    .Root
    .ToCursor(10, 20)
    .Desc("t1", (User t1) => t1.Id)
    .ToSelect()
    .Select("Users", (User t1) => new { t1.Id, t1.Name });
// SELECT t1.[Id],t1.[Name] FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] ORDER BY t1.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[联表游标](../cursor/join.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)
