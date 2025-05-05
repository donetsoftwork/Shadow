# 获取表
>* 从表获取数据组件
>* 本组件用来处理sql的SELECT语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[ISelect](xref:ShadowSql.Select.ISelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>[TableSelect\<TTable\>](xref:ShadowSql.Select.TableSelect%601)
~~~csharp
class TableSelect<TTable>
    where TTable : ITable;
~~~

## 4. ToSelect
>创建[TableSelect\<TTable\>](xref:ShadowSql.Select.TableSelect%601)
### 4.1 从表获取
~~~csharp
TableSelect<TTable> ToSelect<TTable>(this TTable table)
        where TTable : ITable;
~~~
~~~csharp
var select = _db.From("Users")
    .ToSelect()
    .Select("Id", "Name");
// SELECT [Id],[Name] FROM [Users]
~~~

### 4.2 从表和查询条件获取
~~~csharp
TableSelect<TTable> ToSelect<TTable>(this TTable table, ISqlLogic filter)
        where TTable : ITable;
TableSelect<TTable> ToSelect<TTable>(this TTable table, Func<TTable, ISqlLogic> filter)
        where TTable : ITable;
~~~
~~~csharp
UserTable table = new();
var select = table.ToSelect(table.Status.EqualValue(true))
    .Select(table.Id, table.Name);
// SELECT [Id],[Name] FROM [Users] WHERE [Status]=1
~~~
~~~csharp
var select = new UserTable()
    .ToSelect(table => table.Status.EqualValue(true));
// SELECT * FROM [Users] WHERE [Status]=1
~~~

### 4.3 从sql表查询获取
>* 依赖[sql表查询](../sqlquery/table.md)
~~~csharp
TableSelect<TTable> ToSelect<TTable>(this TableSqlQuery<TTable> query)
        where TTable : ITable;
~~~
~~~csharp
var select = _db.From("Users")
    .ToSqlQuery()
    .Where("Status=1")
    .ToSelect();
// SELECT * FROM [Users] WHERE Status=1
~~~

### 4.4 从逻辑表查询获取
>* 依赖[逻辑表查询](../query/table.md)
~~~csharp
TableSelect<TTable> ToSelect<TTable>(this TableQuery<TTable> query)
        where TTable : ITable;
~~~
~~~csharp
var select = new UserTable()
    .ToQuery()
    .And(table => table.Status.EqualValue(true))
    .ToSelect();
// SELECT * FROM [Users] WHERE Status=1
~~~

## 5. Select方法
>* 该方法利用泛型优势,更方便使用表字段来筛选
~~~csharp
TableSelect<TTable> Select(Func<TTable, IFieldView> select);
TableSelect<TTable> Select(Func<TTable, IEnumerable<IFieldView>> select);
~~~
~~~csharp
var select = new UserTable()
    .ToSelect(table => table.Status.EqualValue(true))
    .Select(table => [table.Id, table.Name]);
// SELECT [Id],[Name] FROM [Users] WHERE [Status]=1
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[sql表查询](../sqlquery/table.md)
>* 参看[逻辑表查询](../query/table.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)
