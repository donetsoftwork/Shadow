# 按列查询
>* 按列名查询扩展
>* 校验列,如果列不存在抛异常
>* 无需校验请使用[按字段查询](./fieldquery.md)
>* using ShadowSql.ColumnQueries后即可使用

## 1. ColumnParameter扩展方法
```csharp
TQuery ColumnParameter<TQuery>(this TQuery query, string columnName, string op = "=", string parameter = "")
	where TQuery : IDataSqlQuery;
TQuery ColumnEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnNotEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnGreater<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnLess<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnGreaterEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnLessEqual<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnIn<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnNotIn<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnLike<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnNotLike<TQuery>(this TQuery query, string columnName, string parameter = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnBetween<TQuery>(this TQuery query, string columnName, string begin = "", string end = "")
        where TQuery : IDataSqlQuery;
TQuery ColumnNotBetween<TQuery>(this TQuery query, string columnName, string begin = "", string end = "")
        where TQuery : IDataSqlQuery;
```
```csharp
var userTable = new Table("Users")
    .DefineColums("Id", "Status");
var query = userTable.ToSqlQuery()
    .ColumnParameter("Id", "<", "LastId")
    .ColumnParameter("Status", "=", "state");
```
>SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=@state

## 2. ColumnValue扩展方法
```csharp
TQuery ColumnValue<TQuery, TValue>(this TQuery query, string columnName, TValue value, string op = "=")
	where TQuery : IDataSqlQuery;
TQuery ColumnEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery ColumnNotEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery ColumnGreaterValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery ColumnLessValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery ColumnGreaterEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery ColumnLessEqualValue<TQuery, TValue>(this TQuery query, string columnName, TValue value)
        where TQuery : IDataSqlQuery;
TQuery ColumnInValue<TQuery, TValue>(this TQuery query, string columnName, params TValue[] values)
        where TQuery : IDataSqlQuery;
TQuery ColumnNotInValue<TQuery, TValue>(this TQuery query, string columnName, params TValue[] values)
        where TQuery : IDataSqlQuery;
TQuery ColumnLikeValue<TQuery>(this TQuery query, string columnName, string value)
        where TQuery : IDataSqlQuery;
TQuery ColumnNotLikeValue<TQuery>(this TQuery query, string columnName, string value)
        where TQuery : IDataSqlQuery;
TQuery ColumnBetweenValue<TQuery, TValue>(this TQuery query, string columnName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery;
TQuery ColumnNotBetweenValue<TQuery, TValue>(this TQuery query, string columnName, TValue begin, TValue end)
        where TQuery : IDataSqlQuery;
```
```csharp
var userTable = new Table("Users")
    .DefineColums("Id", "Status");
var query = userTable.ToSqlOrQuery()
    .ColumnValue("Id", 100, "<")
    .ColumnValue("Status", true);
```
>SELECT * FROM [Users] WHERE [Id]<100 OR [Status]=1

## 3. ColumnIsNull扩展方法
```csharp
TQuery ColumnIsNull<TQuery>(this TQuery query, string columnName)
        where TQuery : IDataSqlQuery;
```

## 4. ColumnIsNotNull扩展方法
```csharp
TQuery ColumnNotNull<TQuery>(this TQuery query, string columnName)
        where TQuery : IDataSqlQuery;
```

## 5. TableColumnParameter扩展方法
>该方法适用MultiTableSqlQuery和JoinTableSqlQuery
```csharp
TQuery TableColumnParameter<TQuery>(this TQuery query, string tableName, string columnName, string op = "=", string parameter = "")
        where TQuery : IMultiView, IDataSqlQuery;
TQuery TableColumnEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnNotEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnGreater<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnLess<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnGreaterEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnLessEqual<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnIn<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnNotIn<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnLike<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnNotLike<TQuery>(this TQuery query, string tableName, string columnName, string parameter = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnBetween<TQuery>(this TQuery query, string tableName, string columnName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnNotBetween<TQuery>(this TQuery query, string tableName, string columnName, string begin = "", string end = "")
        where TQuery : MultiTableBase, IDataSqlQuery;
```
```csharp
var query = commentTable.SqlJoin(postTable)
    .OnColumn("PostId", "Id")
    .Root            
    .TableColumnParameter("Comments", "Pick", "=", "PickState")
    .TableColumnParameter("Posts", "Author");
```
>SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=@PickState AND t2.[Author]=@Author

## 6. TableColumnValue扩展方法
>该方法适用MultiTableSqlQuery和JoinTableSqlQuery
```csharp
TQuery TableColumnValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value, string op = "=")
        where TQuery : IMultiView, IDataSqlQuery;
TQuery TableColumnEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnNotEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnGreaterValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnLessValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnGreaterEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnLessEqualValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnInValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, params TValue[] values)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnNotInValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, params TValue[] values)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnLikeValue<TQuery>(this TQuery query, string tableName, string columnName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnNotLikeValue<TQuery>(this TQuery query, string tableName, string columnName, string value)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery;
TQuery TableColumnNotBetweenValue<TQuery, TValue>(this TQuery query, string tableName, string columnName, TValue begin, TValue end)
        where TQuery : MultiTableBase, IDataSqlQuery;
```
```csharp
var query = commentTable.SqlMulti(postTable)
    .Where("t1.PostId=t2.Id")
    .TableColumnValue("Comments", "Pick", false)
    .TableColumnValue("Posts", "Author", "张三");
```
>SELECT * FROM [Comments] AS t1,[Posts] AS t2 WHERE t1.[Pick]=0 AND t2.[Author]='张三' AND t1.PostId=t2.Id

## 7. TableColumnIsNull
```csharp
TQuery TableColumnIsNull<TQuery>(this TQuery query, string tableName, string columnName)
        where TQuery : MultiTableBase, IDataSqlQuery;
```

## 8. TableColumnIsNull
```csharp
TQuery TableColumnNotNull<TQuery>(this TQuery query, string tableName, string columnName)
        where TQuery : MultiTableBase, IDataSqlQuery;
```

## 9. 使用Column扩展方法
```csharp
IColumn Column(this ITableView view, string columnName);
```
```csharp
var u = new Table("Users")
    .DefineColums("Id", "Status");
var query = u.ToSqlQuery()
    .Where(u.Column("Id").GreaterEqual("LastId"))
    .Where(u => u.Column("Status").EqualValue(true));
// SELECT * FROM [Users] WHERE [Id]>=@LastId AND [Status]=1
```
```csharp
var commentTable = new Table("Comments")
    .DefineColums("PostId", "Pick");
var postTable = new Table("Posts")
    .DefineColums("Id", "Author");
var query = commentTable.SqlJoin(postTable)
    .OnColumn("PostId", "Id")
    .Root
    .Where(join => join.From("Comments").Column("Pick").Equal())
    .Where("t2", p => p.Column("Author").EqualValue("张三"));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=@Pick AND t2.[Author]='张三'
```
