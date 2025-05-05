# 多表逻辑查询
>* 从多张表中按逻辑查询数据
>* 是一种简化的联表查询,没有JOIN关键字
>* 多表由多个别名表组成

## 1. 接口
>* [IMultiView](xref:ShadowSql.Identifiers.IMultiView)
>* [IDataQuery](xref:ShadowSql.Queries.IDataQuery)

## 2. 基类
>[MultiTableBase](xref:ShadowSql.Join.MultiTableBase)

## 3. 类
>[MultiTableQuery](xref:ShadowSql.Join.MultiTableQuery)

## 4. Multi
### 4.1 Multi扩展方法
>* 从表创建MultiTableQuery
```csharp
MultiTableQuery Multi(this ITable table, ITable other);
```
```csharp
var multiTable = _db.From("Employees")
    .Multi(_db.From("Departments"));
// SELECT * FROM [Employees] AS t1,[Departments] AS t2
```

### 4.2 Multi重载扩展方法
>* 从别名表创建MultiTableQuery
```csharp
MultiTableQuery Multi(this IAliasTable table, IAliasTable other);
```
```csharp
var multiTable = _db.From("Employees")
    .As("e")
    .SqlMulti(_db.From("Departments").As("d"));
// SELECT * FROM [Employees] AS e,[Departments] AS d
```

## 5. Apply扩展方法
```csharp
MultiTableQuery Apply<TAliasTable>(this MultiTableQuery multiTable, string tableName, Func<Logic, TAliasTable, Logic> query)
        where TAliasTable : IAliasTable;
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var query = c.Multi(p)
    .And(c.PostId.Equal(p.Id))
    .Apply<CommentAliasTable>("c", (q, c) => q.And(c.Pick.EqualValue(true)))
    .Apply<PostAliasTable>("p", (q, p) => q.And(p.Author.EqualValue("张三")));
// SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id] AND c.[Pick]=1 AND p.[Author]='张三'
```

## 6. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[MultiTableQuery](xref:ShadowSql.Join.MultiTableQuery)的方法和扩展方法部分
>* 参看[查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/Query/multi.md)