# 严格查询
>* 按字段名严格查询扩展
>* 校验字段,如果字段不存在抛异常
>* 无需校验请使用[按字段查询](./fieldquery.md)
>* using ShadowSql.StrictQueries后即可使用

## 1. StrictParameter扩展方法
```csharp
TQuery StrictParameter<TQuery>(this TQuery query, string columnName, string op = "=", string parameter = "")
	where TQuery : IDataSqlQuery;
TQuery StrictEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery StrictNotEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery StrictGreater<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery StrictLess<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery StrictGreaterEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery StrictLessEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery StrictIn<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery StrictNotIn<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery StrictLike<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery StrictNotLike<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery StrictBetween<TQuery>(this TQuery query, string columnName, string begin = "", string end = "")
        where TQuery : IDataSqlQuery;
TQuery StrictNotBetween<TQuery>(this TQuery query, string columnName, string begin = "", string end = "")
        where TQuery : IDataSqlQuery;
```
```csharp
var userTable = new Table("Users")
    .DefineColumns("Id", "Status");
var query = userTable.ToSqlQuery()
    .StrictParameter("Id", "<", "LastId")
    .StrictParameter("Status", "=", "state");
```
>SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=@state

## 2. StrictValue扩展方法
```csharp
TQuery StrictValue<TQuery, TValue>(this TQuery query, string columnName, TValue value, string op = "=")
	where TQuery : IDataSqlQuery;
TQuery StrictEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery StrictNotEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery StrictGreaterValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery StrictLessValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery StrictGreaterEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery StrictLessEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery StrictInValue<TQuery, TValue>(this TQuery query, string columnName, params TValue[] values)
        where TQuery : IDataSqlQuery;
TQuery StrictNotInValue<TQuery, TValue>(this TQuery query, string columnName, params TValue[] values)
        where TQuery : IDataSqlQuery;
TQuery StrictLikeValue<TQuery>(this TQuery query, string columnName, string value)
        where TQuery : IDataSqlQuery;
TQuery StrictNotLikeValue<TQuery>(this TQuery query, string columnName, string value)
        where TQuery : IDataSqlQuery;
TQuery StrictBetweenValue<TQuery, TValue>(this TQuery query, string columnName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery;
TQuery StrictNotBetweenValue<TQuery, TValue>(this TQuery query, string columnName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery;
```
```csharp
var userTable = new Table("Users")
    .DefineColumns("Id", "Status");
var query = userTable.ToSqlOrQuery()
    .StrictValue("Id", 100, "<")
    .StrictValue("Status", true);
```
>SELECT * FROM [Users] WHERE [Id]<100 OR [Status]=1

## 3. StrictIsNull扩展方法
```csharp
TQuery StrictIsNull<TQuery>(this TQuery query, string columnName)
        where TQuery : IDataSqlQuery;
```

## 4. StrictIsNotNull扩展方法
```csharp
TQuery StrictNotNull<TQuery>(this TQuery query, string columnName)
        where TQuery : IDataSqlQuery;
```

## 5. TableStrictParameter扩展方法
>该方法适用MultiTableSqlQuery和JoinTableSqlQuery
```csharp
TQuery TableStrictParameter<TQuery>(this TQuery query, string tableName, string columnName, string op = "=", string parameter = "")
        where TQuery : IMultiView, IDataSqlQuery;
TQuery TableStrictEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictNotEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictGreater<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictLess<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictGreaterEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictLessEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictIn<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictNotIn<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictLike<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictNotLike<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictBetween<TQuery>(this TQuery query, string tableName, string columnName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictNotBetween<TQuery>(this TQuery query, string tableName, string columnName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
```
```csharp
var query = commentTable.SqlJoin(postTable)
    .OnColumn("PostId", "Id")
    .Root            
    .TableStrictParameter("Comments", "Pick", "=", "PickState")
    .TableStrictParameter("Posts", "Author");
```
>SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=@PickState AND t2.[Author]=@Author

## 6. TableStrictValue扩展方法
>该方法适用MultiTableSqlQuery和JoinTableSqlQuery
```csharp
TQuery TableStrictValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value, string op = "=")
        where TQuery : IMultiView, IDataSqlQuery;
TQuery TableStrictEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictNotEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictGreaterValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictLessValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictGreaterEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictLessEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictInValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, params TValue[] values)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictNotInValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, params TValue[] values)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictLikeValue<TQuery>(this TQuery query, string tableName, string columnName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictNotLikeValue<TQuery>(this TQuery query, string tableName, string columnName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableStrictNotBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery;
```
```csharp
var query = commentTable.SqlMulti(postTable)
    .Where("t1.PostId=t2.Id")
    .TableStrictValue("Comments", "Pick", false)
    .TableStrictValue("Posts", "Author", "张三");
```
>SELECT * FROM [Comments] AS t1,[Posts] AS t2 WHERE t1.[Pick]=0 AND t2.[Author]='张三' AND t1.PostId=t2.Id

## 7. TableStrictIsNull
```csharp
TQuery TableStrictIsNull<TQuery>(this TQuery query, string tableName, string columnName)
        where TQuery : MultiTableBase, IDataSqlQuery;
```

## 8. TableStrictIsNull
```csharp
TQuery TableStrictNotNull<TQuery>(this TQuery query, string tableName, string columnName)
        where TQuery : MultiTableBase, IDataSqlQuery;
```

## 9. 使用Strict扩展方法
```csharp
IField Strict(this ITableView view, string columnName);
```
```csharp
var u = new Table("Users")
    .DefineColumns("Id", "Status");
var query = u.ToSqlQuery()
    .Where(u.Strict("Id").GreaterEqual("LastId"))
    .Where(u => u.Strict("Status").EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]>=@LastId AND [Status]=1
```
```csharp
var commentTable = new Table("Comments")
    .DefineColumns("PostId", "Pick");
var postTable = new Table("Posts")
    .DefineColumns("Id", "Author");
var query = commentTable.SqlJoin(postTable)
    .OnColumn("PostId", "Id")
    .Root
    .Where(join => join.From("Comments").Strict("Pick").Equal())
    .Where("t2", p => p.Strict("Author").EqualValue("张三"));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=@Pick AND t2.[Author]='张三'
```
