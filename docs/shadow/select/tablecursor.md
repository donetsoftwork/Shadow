# 表分页
>* 从表分页获取数据组件
>* 本组件用来处理sql的SELECT分页语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性
>* 本组件通过[表游标](../cursor/table.md)来分页

## 1. 接口
>[ISelect](/api/ShadowSql.Select.ISelect.html)

## 2. 基类
>* [SelectFieldsBase](/api/ShadowSql.Select.SelectFieldsBase.html)

## 3. 类
>[TableCursorSelect\<TTable\>](/api/ShadowSql.CursorSelect.TableCursorSelect-1.html)
~~~csharp
class TableCursorSelect<TTable>(TableCursor<TTable> cursor)
    where TTable : ITable;
~~~

## 4. ToSelect扩展方法
>* 从表游标创建TableCursorSelect
~~~csharp
TableCursorSelect<TTable> ToSelect<TTable>(this TableCursor<TTable> cursor)
        where TTable : ITable;
~~~
~~~csharp
var select = _db.From("Users")
    .ToCursor(10, 20)
    .Desc("Id")
    .ToSelect()
    .Select("Id", "Name");
// SELECT [Id],[Name] FROM [Users] ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 5. Select方法
>* 该方法利用泛型优势,更方便使用表字段来筛选
~~~csharp
TableCursorSelect<TTable> Select(Func<TTable, IFieldView> select);
TableCursorSelect<TTable> Select(Func<TTable, IEnumerable<IFieldView>> select);
~~~
~~~csharp
var select = new UserTable()
    .ToSqlQuery()
    .Where(table => table.Status.EqualValue(true))
    .ToCursor(10, 20)
    .Asc(table => table.Id)
    .ToSelect()
    .Select(table => [table.Id, table.Name]);
// SELECT [Id],[Name] FROM [Users] WHERE [Status]=1 ORDER BY [Id] OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 6. 其他相关功能
>* 参看[获取简介](./index.md)
>* 参看[表游标](../cursor/table.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)
