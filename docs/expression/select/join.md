# 联表
>* 从联表获取数据组件
>* 本组件用来处理sql的SELECT语句
>* 本组件是对ShadowSql.Core同名组件的扩展

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)
>* [IMultiSelect](xref:ShadowSql.Expressions.Select.IMultiSelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>* [MultiTableSelect](xref:ShadowSql.Expressions.Select.MultiTableSelect)

## 4. ToSelect
>* 创建[MultiTableSelect](xref:ShadowSql.Expressions.Select.MultiTableSelect)
~~~csharp
MultiTableSelect ToSelect(this IMultiView table);
MultiTableSelect ToSelect(this IJoinOn table);
~~~
>注:不能从IJoin对象获取数据,它的ToSelect实际是操作他的Root联表对象

### 4.1 从sql联表获取
~~~csharp
var select = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On(u => u.Id, r => r.UserId)
    .Root
    .ToSelect();
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]
~~~

### 4.2 从逻辑联表获取
~~~csharp
var select = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .And((u, r) => u.Id == r.UserId)
    .ToSelect();
// SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]
~~~

## 5. Select方法
~~~csharp
MultiTableSelect Select<TEntity, TProperty>(string tableName, Expression<Func<TEntity, TProperty>> select);
~~~
~~~csharp
var select = EmptyTable.Use("Users")
    .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
    .On((u, r) => u.Id == r.UserId)
    .ToSelect()
    .Select("Users", (User t1) => t1.Id);
// SELECT t1.[Id] FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]
~~~
~~~csharp
var select = EmptyTable.Use("Users")
    .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
    .And((u, r) => u.Id == r.UserId)
    .ToSelect()
    .Select("Users", (User t1) => new { t1.Id, t1.Name });
// SELECT t1.[Id],t1.[Name] FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)
