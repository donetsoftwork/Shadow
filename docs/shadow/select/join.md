# 联表
>* 从联表获取数据组件
>* 本组件用来处理sql的SELECT语句
>* 本组件是对ShadowSql.Core同名组件的扩展

## 1. 接口
>[ISelect](/api/ShadowSql.Select.ISelect.html)

## 2. 基类
>* [SelectFieldsBase](/api/ShadowSql.Select.SelectFieldsBase.html)

## 3. 类
>[MultiTableSelect](/api/ShadowSql.Select.MultiTableSelect.html)

## 4. ToSelect
>创建[MultiTableSelect](/api/ShadowSql.Select.MultiTableSelect.html)
~~~csharp
MultiTableSelect ToSelect(this IMultiView table);
MultiTableSelect ToSelect(this IJoinOn table);
~~~
>注:不能从IJoin对象获取数据,它的ToSelect实际是操作他的Root联表对象

### 4.1 从sql联表获取
>* 依赖[sql联表](../sqlquery/join.md)
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var select = c.SqlJoin(p)
    .And(c.PostId, p.Id)
    .Root
    .ToSelect();
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]
~~~

### 4.2 从sql多表获取
>* 依赖[sql多表](../sqlquery/multi.md)
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var select = c.SqlMulti(p)
    .Where(c.PostId.Equal(p.Id))
    .ToSelect();
// SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id]
~~~

### 4.3 从逻辑联表获取
>* 依赖[逻辑联表](../query/join.md)
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var select = c.Join(p)
    .And(c.PostId.Equal(p.Id))
    .Root
    .ToSelect();
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]
~~~

### 4.4 从逻辑多表获取
>* 依赖[逻辑多表](../query/multi.md)
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var select = c.Multi(p)
    .And(c.PostId.Equal(p.Id))
    .ToSelect();
// SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id]
~~~

## 5. Select
### 5.1 Select方法
>* 从表筛选字段
~~~csharp
MultiTableSelect Select<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable;
MultiTableSelect Select<TTable>(string tableName, Func<TTable, IEnumerable<IColumn>> select)
        where TTable : ITable;
~~~
~~~csharp
var select = new CommentTable()
    .SqlJoin(new PostTable())
    .On(t1 => t1.PostId, t2 => t2.Id)
    .ToSelect()
    .Select<CommentTable>("Comments", t1 => t1.Content);
// SELECT t1.[Content] FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]
~~~
~~~csharp
var select = new CommentTable()
    .SqlJoin(new PostTable())
    .On(t1 => t1.PostId, t2 => t2.Id)
    .ToSelect()
    .Select<CommentTable>("Comments", t1 => [t1.Id, t1.Content]);
// SELECT t1.[Id],t1.[Content] FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]
~~~

### 5.2 Select重载方法
>* 从别名表筛选字段
~~~csharp
MultiTableSelect Select<TAliasTable>(string tableName, Func<TAliasTable, IFieldView> select)
        where TAliasTable : IAliasTable;
MultiTableSelect Select<TAliasTable>(string tableName, Func<TAliasTable, IEnumerable<IFieldView>> select)
        where TAliasTable : IAliasTable;
~~~
~~~csharp
var select = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .ToSelect()
    .Select<CommentAliasTable>("Comments", c => c.Content);
// SELECT c.[Content] FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]
~~~
~~~csharp
var select = new CommentAliasTable("c")
    .SqlJoin(new PostAliasTable("p"))
    .On(c => c.PostId, p => p.Id)
    .ToSelect()
    .Select<CommentAliasTable>("Comments", c => [c.Id, c.Content]);
// SELECT c.[Id],c.[Content] FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]
~~~

## 6. SelectTable扩展方法
>筛选别名表
~~~csharp
TMultiTableSelect SelectTable<TMultiTableSelect>(this TMultiTableSelect multiSelect, IAliasTable aliasTable)
        where TMultiTableSelect : MultiSelectBase;
TMultiTableSelect SelectTable<TMultiTableSelect>(this TMultiTableSelect multiSelect, string tableName)
        where TMultiTableSelect : MultiSelectBase;
~~~
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var select = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .ToSelect()
    .SelectTable(c);
// SELECT c.* FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]
~~~
~~~csharp
var select = new Table("Comments")
    .SqlJoin(new Table("Posts"))
    .OnColumn("PostId", "Id")
    .ToSelect()
    .SelectTable("Comments");
// SELECT t1.* FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]
~~~

## 7. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[sql联表](../sqlquery/join.md)
>* 参看[sql多表](../sqlquery/multi.md)
>* 参看[逻辑联表](../query/join.md)
>* 参看[逻辑多表](../query/multi.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)
