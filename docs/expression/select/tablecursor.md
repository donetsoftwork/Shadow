# 表分页
>* 从表分页获取数据组件
>* 本组件用来处理sql的SELECT分页语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性
>* 本组件通过[表游标](../cursor/table.md)来分页

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>* [TableCursorSelect\<TEntity\>](xref:ShadowSql.Expressions.CursorSelect.TableCursorSelect%601)


## 4. ToSelect扩展方法
>* 从表游标创建TableCursorSelect
~~~csharp
TableCursorSelect<TEntity> ToSelect<TEntity>(this TableCursor<TEntity> cursor);
~~~
~~~csharp
var select = _db.From("Users")
    .ToCursor<User>(10, 20)
    .Desc(u => u.Id)
    .ToSelect();
// SELECT * FROM [Users] ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 5. Select方法
>* 该方法利用泛型优势,更方便使用表字段来筛选
~~~csharp
TableCursorSelect<TEntity> Select<TProperty>(Expression<Func<TEntity, TProperty>> select);
~~~
~~~csharp
var select = _db.From("Users")
    .Take<User>(10, 20)
    .Asc(u => u.Id)
    .ToSelect()
    .Select(u => new { u.Id, u.Name });
// SELECT [Id],[Name] FROM [Users] ORDER BY [Id] OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~
~~~csharp
var select = _db.From("Users")
    .Take<User>(10, 20)
    .Asc(u => u.Id)
    .ToSelect()
    .Select(u => u.Id);
// SELECT [Id] FROM [Users] ORDER BY [Id] OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~

## 6. 其他相关功能
>* 参看[TableCursorSelect\<TEntity\>](xref:ShadowSql.Expressions.CursorSelect.TableCursorSelect%601)的方法和扩展方法部分
>* 参看[获取简介](./index.md)
>* 参看[表游标](../cursor/table.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)
