# sql联表查询
>* 联表查询数据
>* 需要[别名表关联](./aliasjoinon.md)或[表关联](./joinon.md)配合使用
>* 由于子表的数量不确定,把联表拆分为两两关联
>* 借助两两关联泛型扩展,联表的每个表查询都可以使用自定义类型

## 1. 接口
>* [IJoinTable](/api/ShadowSql.Identifiers.IJoinTable.html)
>* [IMultiView](/api/ShadowSql.Identifiers.IMultiView.html)
>* [IDataSqlQuery](/api/ShadowSql.Queries.IDataSqlQuery.html)

## 2. 基类
>[MultiTableBase](/api/ShadowSql.Join.MultiTableBase.html)

## 3. 类
>[JoinTableSqlQuery](/api/ShadowSql.Join.JoinTableSqlQuery.html)

## 4. Apply扩展方法
```csharp
MultiTableSqlQuery Apply<TAliasTable>(this MultiTableSqlQuery multiTable, string tableName, Func<SqlQuery, TAliasTable, SqlQuery> query)
        where TAliasTable : IAliasTable;
```
```csharp
var query = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .Root
    .Apply<CommentAliasTable>("c", (q, c) => q.And(c.Pick.EqualValue(true)))
    .Apply<PostAliasTable>("p", (q, p) => q.And(p.Author.EqualValue("张三")));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'
```

## 5. 使用别名表关联
>* 参看[别名表关联](./aliasjoinon.md)
>* 使用别名表关联更有优势
### 5.1 WhereLeft
```csharp
AliasJoinOnSqlQuery<TLeft, TRight> WhereLeft(Func<TLeft, AtomicLogic> query);
```
```csharp
var joinOn = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .WhereLeft(c => c.Pick.EqualValue(true));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1
```

### 5.2 WhereRight
```csharp
AliasJoinOnSqlQuery<TLeft, TRight> WhereRight(Func<TRight, AtomicLogic> query);
```
```csharp
var joinOn = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .WhereRight(p => p.Author.NotEqualValue("张三"));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]<>'张三'
```

## 6. 使用表关联
>* 参看[表关联](./joinon.md)
### 6.1 WhereLeft
```csharp
JoinOnSqlQuery<LTable, RTable> WhereLeft(Func<LTable, IColumn> select, Func<IColumn, AtomicLogic> query);
```
```csharp
var joinOn = new CommentTable()
    .SqlJoin(new PostTable())
    .On(c => c.PostId, p => p.Id)
    .WhereLeft(c => c.Pick, Pick => Pick.EqualValue(true));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1
```

### 6.2 WhereRight
```csharp
JoinOnSqlQuery<LTable, RTable> WhereRight(Func<RTable, IColumn> select, Func<IColumn, AtomicLogic> query);
```
```csharp
var joinOn = new CommentTable()
    .SqlJoin(new PostTable())
    .On(c => c.PostId, p => p.Id)
    .WhereRight(p => p.Author, Author => Author.NotEqualValue("张三"));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t2.[Author]<>'张三'
```

## 7. WhereLeft扩展方法
>查询左表
```csharp
TJoinOn WhereLeft<TJoinOn>(this TJoinOn joinOn, string columnName, Func<ICompareView, AtomicLogic> query)
        where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery;
```
```csharp
 var joinOn = SimpleDB.From("Comments")
     .SqlJoin(SimpleDB.From("Posts"))
     .OnColumn("PostId", "Id")
     .WhereLeft("Pick", Pick => Pick.EqualValue(true));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1
```

## 8. WhereRight扩展方法
>查询右表
```csharp
TJoinOn WhereRight<TJoinOn>(this TJoinOn joinOn, string columnName, Func<ICompareView, AtomicLogic> query)
        where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery;
```
```csharp
var joinOn = SimpleDB.From("Comments")
    .SqlJoin(SimpleDB.From("Posts"))
    .OnColumn("PostId", "Id")
    .WhereRight("Author", Author => Author.NotEqualValue("张三"));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1
```

## 9. 其他示例
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var query = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .Root
    .Where(c.Pick.EqualValue(true))
    .Where(p.Author.EqualValue("张三"));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'
```

## 10. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[JoinTableSqlQuery](/api/ShadowSql.Join.JoinTableSqlQuery.html)的方法和扩展方法部分
>* 参看[别名表关联](./aliasjoinon.md)
>* 参看[表关联](./joinon.md)
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/sqlquery/join.md)