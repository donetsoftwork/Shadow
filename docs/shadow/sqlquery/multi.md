# sql多表查询
>* 从多张表中按sql查询数据
>* 是一种简化的联表查询,没有JOIN关键字
>* 多表由多个别名表组成
>* 通过扩展对本组件增强功能、增加易用性

## 1. 接口
>* [IMultiView](/api/ShadowSql.Identifiers.IMultiView.html)
>* [IDataSqlQuery](/api/ShadowSql.Queries.IDataSqlQuery.html)

## 2. 基类
>[MultiTableBase](/api/ShadowSql.Join.MultiTableBase.html)

## 3. 类
>[MultiTableSqlQuery](/api/ShadowSql.Join.MultiTableSqlQuery.html)


## 4. SqlMulti
### 4.1 SqlMulti扩展方法
>* 从表创建MultiTableSqlQuery
```csharp
MultiTableSqlQuery SqlMulti(this ITable table, ITable other);
```
```csharp
var multiTable = _db.From("Employees")
    .SqlMulti(_db.From("Departments"));
// SELECT * FROM [Employees] AS t1,[Departments] AS t2
```

### 4.2 SqlMulti重载扩展方法
>* 从别名表创建MultiTableSqlQuery
```csharp
MultiTableSqlQuery SqlMulti(this IAliasTable table, IAliasTable other);
```
```csharp
var multiTable = _db.From("Employees")
    .As("e")
    .SqlMulti(_db.From("Departments").As("d"))
    .Where("e.DepartmentId=d.Id");
// SELECT * FROM [Employees] AS e,[Departments] AS d WHERE e.DepartmentId=d.Id
```

## 5. Apply扩展方法
```csharp
MultiTableSqlQuery Apply<TAliasTable>(this MultiTableSqlQuery multiTable, string tableName, Func<SqlQuery, TAliasTable, SqlQuery> query)
        where TAliasTable : IAliasTable;
```
```csharp
var query = new CommentAliasTable("c")
    .SqlMulti(new PostAliasTable("p"))
    .Where("c.PostId=p.Id")
    .Apply<CommentAliasTable>("c", (q, c) => q.And(c.Pick.EqualValue(true)))
    .Apply<PostAliasTable>("p", (q, p) => q.And(p.Author.EqualValue("张三")));
// SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[Pick]=1 AND p.[Author]='张三' AND c.PostId=p.Id
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[MultiTableSqlQuery](/api/ShadowSql.Join.MultiTableSqlQuery.html)的方法和扩展方法部分
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/multi.md)