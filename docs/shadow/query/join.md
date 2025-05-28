# 联表逻辑查询
>* 联表查询数据
>* 需要[别名表关联](./aliasjoinon.md)或[表关联](./joinon.md)配合使用
>* 由于子表的数量不确定,把联表拆分为两两关联
>* 借助两两关联泛型扩展,联表的每个表查询都可以使用自定义类型

## 1. 接口
>* [IJoinTable](xref:ShadowSql.Identifiers.IJoinTable)
>* [IMultiView](xref:ShadowSql.Identifiers.IMultiView)
>* [IDataQuery](xref:ShadowSql.Queries.IDataQuery)

## 2. 基类
>[MultiTableBase](xref:ShadowSql.Join.MultiTableBase)

## 3. 类
>[JoinTableQuery](xref:ShadowSql.Join.JoinTableQuery)

## 4. Apply扩展方法
```csharp
JoinTableQuery Apply<TAliasTable>(this JoinTableQuery multiTable, string tableName, Func<Logic, TAliasTable, Logic> query)
        where TAliasTable : IAliasTable;
```
```csharp
var query = new CommentAliasTable("c")
    .Join(new PostAliasTable("p"))
    .Apply((q, c, p) => q.And(c.PostId.Equal(p.Id)))
    .Root
    .Apply<CommentAliasTable>("c", (q, c) => q.And(c.Pick.EqualValue(true)))
    .Apply<PostAliasTable>("p", (q, p) => q.And(p.Author.EqualValue("张三")));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'
```

## 5. 使用别名表关联
>* 参看[别名表关联](./aliasjoinon.md)
>* 使用别名表关联更有优势
### 5.1 ApplyLeft
```csharp
AliasJoinOnQuery<TLeft, TRight> ApplyLeft(Func<Logic, TLeft, Logic> query);
```
```csharp
var query = new CommentAliasTable("c")
    .Join(new PostAliasTable("p"))
    .Apply((q, c, p) => q.And(c.PostId.Equal(p.Id)))
    .ApplyLeft((q, c) => q.And(c.Pick.EqualValue(true)));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] Apply c.[Pick]=1
```

### 5.2 ApplyRight
```csharp
AliasJoinOnQuery<TLeft, TRight> ApplyRight(Func<TRight, AtomicLogic> query);
```
```csharp
var query = new PostAliasTable("p")
    .Join(new CommentAliasTable("c"))
    .Apply((q, p, c) => q.And(p.Id.Equal(c.PostId)))            
    .ApplyRight((q, c) => q.And(c.Pick.EqualValue(true)));
// SELECT * FROM [Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] WHERE c.[Pick]=1
```

## 6. 使用表关联
>* 参看[表关联](./joinon.md)
### 6.1 ApplyLeft
```csharp
JoinOnQuery<LTable, RTable> ApplyLeft(Func<LTable, IColumn> select, Func<IColumn, AtomicLogic> query);
```
```csharp
var query = new CommentTable()
    .Join(new PostTable())
    .Apply(
        c => c.PostId,
        p => p.Id,
        (q, PostId, Id) => q.And(PostId.Equal(Id))
    )
    .ApplyLeft(c => c.Pick, (q, Pick) => q.And(Pick.EqualValue(true)));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] Apply t1.[Pick]=1
```

### 6.2 ApplyRight
```csharp
JoinOnQuery<LTable, RTable> ApplyRight(Func<RTable, IColumn> select, Func<IColumn, AtomicLogic> query);
```
```csharp
var query = new PostTable()
    .Join(new CommentTable())
    .Apply(
        p => p.Id,
        c => c.PostId,
        (q, Id, PostId) => q.And(Id.Equal(PostId))
    )
    .ApplyRight(c => c.Pick, (q, Pick) => q.And(Pick.EqualValue(true)));
// SELECT * FROM [Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] WHERE t2.[Pick]=1
```

## 7. ApplyLeft扩展方法
>查询左表
```csharp
TJoinOn ApplyLeft<TJoinOn>(this TJoinOn joinOn, string columnName, Func<Logic, ICompareView, Logic> query)
        where TJoinOn : JoinOnBase, IJoinOn, IDataQuery;
```
```csharp
var query = EmptyTable.Use("Comments")
    .Join(EmptyTable.Use("Posts"))
    .Apply("PostId", "Id", (q, PostId, Id) => q.And(PostId.Equal(Id)))
    .ApplyLeft("Pick", (q, Pick) => q.And(Pick.EqualValue(true)));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] Apply t1.[Pick]=1
```

## 8. ApplyRight扩展方法
>查询右表
```csharp
TJoinOn ApplyRight<TJoinOn>(this TJoinOn joinOn, string columnName, Func<Logic, ICompareView, Logic> query)
        where TJoinOn : JoinOnBase, IJoinOn, IDataQuery;
```
```csharp
var query = EmptyTable.Use("Posts")
    .Join(EmptyTable.Use("Comments"))
    .Apply("Id", "PostId", (q, Id, PostId) => q.And(Id.Equal(PostId)))
    .ApplyRight("Pick", (q, Pick) => q.And(Pick.EqualValue(true)));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] Apply t1.[Pick]=1
```

## 9. 其他示例
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var query = c.Join(p)
    .And(c.PostId.Equal(p.Id))
    .Root
    .And(c.Pick.EqualValue(true))
    .And(p.Author.EqualValue("张三"));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'
```

## 10. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[JoinTableQuery](xref:ShadowSql.Join.JoinTableQuery)的方法和扩展方法部分
>* 参看[别名表关联](./aliasjoinon.md)
>* 参看[表关联](./joinon.md)
>* 参看[sql查询简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/query/join.md)