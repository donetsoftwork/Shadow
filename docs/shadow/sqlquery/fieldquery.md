# 按字段查询
>* 按字段名查询扩展
>* 无需提前定义,随查随用
>* 需要校验请使用[按列查询](./fieldquery.md)
>* using ShadowSql.FieldQueries后即可使用

## 1. FieldParameter扩展方法
```csharp
TQuery FieldParameter<TQuery>(this TQuery query, string fieldName, string op = "=", string parameter = "")
	where TQuery : IDataSqlQuery;
TQuery FieldEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldNotEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldGreaterValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldLessValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldGreaterEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldLessEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldInValue<TQuery, TValue>(this TQuery query, string fieldName, params TValue[] values)
        where TQuery : IDataSqlQuery;
TQuery FieldNotInValue<TQuery, TValue>(this TQuery query, string fieldName, params TValue[] values)
        where TQuery : IDataSqlQuery;
TQuery FieldLikeValue<TQuery>(this TQuery query, string fieldName, string value)
        where TQuery : IDataSqlQuery;
TQuery FieldNotLikeValue<TQuery>(this TQuery query, string fieldName, string value)
        where TQuery : IDataSqlQuery;
TQuery FieldBetweenValue<TQuery, TValue>(this TQuery query, string fieldName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery;
TQuery FieldNotBetweenValue<TQuery, TValue>(this TQuery query, string fieldName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery;
```
```csharp
var query = new TableSqlQuery("Users")
    .FieldParameter("Id", "<", "LastId")
    .FieldEqual("Status", "state");
// SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=@state
```

## 2. FieldValue扩展方法
```csharp
TQuery FieldValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value, string op = "=")
	where TQuery : IDataSqlQuery;
TQuery FieldEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldNotEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldGreaterValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldLessValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldGreaterEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldLessEqualValue<TQuery, TValue>(this TQuery query, string fieldName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery FieldInValue<TQuery, TValue>(this TQuery query, string fieldName, params TValue[] values)
        where TQuery : IDataSqlQuery;
TQuery FieldNotInValue<TQuery, TValue>(this TQuery query, string fieldName, params TValue[] values)
        where TQuery : IDataSqlQuery;
TQuery FieldLikeValue<TQuery>(this TQuery query, string fieldName, string value)
        where TQuery : IDataSqlQuery;
TQuery FieldNotLikeValue<TQuery>(this TQuery query, string fieldName, string value)
        where TQuery : IDataSqlQuery;
TQuery FieldBetweenValue<TQuery, TValue>(this TQuery query, string fieldName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery;
TQuery FieldNotBetweenValue<TQuery, TValue>(this TQuery query, string fieldName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery;
```
```csharp
var query = new TableSqlQuery("Users")
    .ToOr()
    .FieldValue("Id", 100, "<")
    .FieldEqualValue("Status", true);
// SELECT * FROM [Users] WHERE [Id]<100 OR [Status]=1
```

## 3. FieldIsNull扩展方法
```csharp
TQuery FieldIsNull<TQuery>(this TQuery query, string FieldName)
        where TQuery : IDataSqlQuery;
```

## 4. FieldIsNotNull扩展方法
```csharp
TQuery FieldNotNull<TQuery>(this TQuery query, string FieldName)
        where TQuery : IDataSqlQuery;
```

## 5. TableFieldParameter扩展方法
>该方法适用MultiTableSqlQuery和JoinTableSqlQuery
```csharp
TQuery TableFieldParameter<TQuery>(this TQuery query, string tableName, string fieldName, string op = "=", string parameter = "")
        where TQuery : IMultiView, IDataSqlQuery;
TQuery TableFieldEqual<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldNotEqual<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldGreater<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldLess<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldGreaterEqual<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldLessEqual<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldIn<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldNotIn<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldLike<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldNotLike<TQuery>(this TQuery query, string tableName, string fieldName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldBetween<TQuery>(this TQuery query, string tableName, string fieldName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldNotBetween<TQuery>(this TQuery query, string tableName, string fieldName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
```
```csharp
var query = new MultiTableSqlQuery()
    .AddMembers("Comments", "Posts")
    .Where("t1.PostId=t2.Id")            
    .TableFieldParameter("Comments", "Pick", "=", "PickState")
    .TableFieldEqual("Posts", "Author");
// SELECT * FROM [Comments] AS t1,[Posts] AS t2 WHERE t1.[Pick]=@PickState AND t2.[Author]=@Author AND t1.PostId=t2.Id
```

## 6. TableFieldValue扩展方法
>该方法适用MultiTableSqlQuery和JoinTableSqlQuery
```csharp
TQuery TableFieldValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value, string op = "=")
        where TQuery : IMultiView, IDataSqlQuery;
TQuery TableFieldEqualValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldNotEqualValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldGreaterValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldLessValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldGreaterEqualValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldLessEqualValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldInValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, params TValue[] values)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldNotInValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, params TValue[] values)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldLikeValue<TQuery>(this TQuery query, string tableName, string fieldName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldNotLikeValue<TQuery>(this TQuery query, string tableName, string fieldName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableFieldNotBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string fieldName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery;
```
```csharp
var query = _db.From("Comments")
    .SqlJoin(_db.From("Posts"))
    .OnColumn("PostId", "Id")
    .Root
    .TableFieldValue("Comments", "Pick", false)
    .TableFieldEqualValue("Posts", "Author", "张三");
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=0 AND t2.[Author]='张三'
```

## 7. TableFieldIsNull
```csharp
TQuery TableFieldIsNull<TQuery>(this TQuery query, string tableName, string FieldName)
        where TQuery : MultiTableBase, IDataSqlQuery;
```

## 8. TableFieldIsNull
```csharp
TQuery TableFieldNotNull<TQuery>(this TQuery query, string tableName, string FieldName)
        where TQuery : MultiTableBase, IDataSqlQuery;
```

## 9. 使用Field方法
```csharp
var u = SimpleTable.Use("Users");
var query = u.ToSqlQuery()
    .Where(u.Field("Id").GreaterEqual("LastId"))
    .Where(u => u.Field("Status").EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]>=@LastId AND [Status]=1
```
```csharp
var query = SimpleTable.Use("Comments")
    .SqlJoin(SimpleTable.Use("Posts"))
    .OnColumn("PostId", "Id")
    .Root
    .Where(join => join.From("Comments").Field("Pick").Equal())
    .Where("t2", p => p.Field("Author").EqualValue("张三"));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=@Pick AND t2.[Author]='张三'
```
