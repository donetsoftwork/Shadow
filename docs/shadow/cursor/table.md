# 表游标
>* 对表进行截取,处理分页和排序
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[ICursor](xref:ShadowSql.Cursors.ICursor)

## 2. 基类
>[CursorBase](xref:ShadowSql.Cursors.CursorBase)

## 3. 类
>[TableCursor\<TTable\>](xref:ShadowSql.Cursors.TableCursor%601)
~~~csharp
class TableCursor<TTable>
    where TTable : ITable;
~~~

## 4. ToCursor
### 4.1 ToCursor扩展方法
~~~csharp
TableCursor<TTable> ToCursor<TTable>(this TTable source, int limit = 0, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var cursor = _db.From("Users")
    .ToCursor(10, 20);
~~~

### 4.2 ToCursor重载扩展方法
~~~csharp
TableCursor<TTable> ToCursor<TTable>(this TTable source, ISqlLogic where, int limit = 0, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var age = Column.Use("Age");
var cursor = _db.From("Users")
    .ToCursor(age.GreaterValue(30), 10, 20);
~~~

### 4.3 ToCursor重载扩展方法
~~~csharp
TableCursor<TTable> ToCursor<TTable>(this TableSqlQuery<TTable> query, int limit = 0, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var cursor = _db.From("Users")
    .ToSqlQuery()
    .Where("Age>30")
    .ToCursor(10, 20);
~~~

### 4.4 ToCursor重载扩展方法
~~~csharp
TableCursor<TTable> ToCursor<TTable>(this TableQuery<TTable> query, int limit = 0, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var age = Column.Use("Age");
var cursor = _db.From("Users")
    .ToQuery()
    .And(age.GreaterValue(30))
    .ToCursor(10, 20);
~~~

### 4.5 ToCursor重载扩展方法
~~~csharp
TableCursor<ITable> ToCursor(this TableSqlQuery query, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = new TableSqlQuery("Users")
    .Where("Age>30")
    .ToCursor(10, 20);
~~~

### 4.6 ToCursor重载扩展方法
~~~csharp
TableCursor<ITable> ToCursor(this TableQuery query, int limit = 0, int offset = 0);
~~~
~~~csharp
var age = Column.Use("Age");
var cursor = new TableQuery("Users")
    .And(age.GreaterValue(30))
    .ToCursor(10, 20);
~~~

## 5. Take
>* Take方法是ToCursor的平替
### 5.1 Take扩展方法
~~~csharp
TableCursor<TTable> Take<TTable>(this TTable source, int limit, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var cursor = _db.From("Users")
    .Take(10, 20);
~~~

### 5.2 Take重载扩展方法
~~~csharp
TableCursor<TTable> Take<TTable>(this TTable source, ISqlLogic where, int limit, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var age = Column.Use("Age");
var cursor = _db.From("Users")
    .Take(age.GreaterValue(30), 10, 20);
~~~

### 5.3 Take重载扩展方法
~~~csharp
TableCursor<TTable> Take<TTable>(this TableSqlQuery<TTable> query, int limit, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var cursor = _db.From("Users")
    .ToSqlQuery()
    .Where("Age>30")
    .Take(10, 20);
~~~

### 5.4 Take重载扩展方法
~~~csharp
TableCursor<TTable> ToCursor<TTable>(this TableQuery<TTable> query, int limit, int offset = 0)
        where TTable : ITable;
~~~
~~~csharp
var age = Column.Use("Age");
var cursor = _db.From("Users")
    .ToQuery()
    .And(age.GreaterValue(30))
    .Take(10, 20);
~~~

### 5.5 Take重载扩展方法
~~~csharp
TableCursor<ITable> Take(this TableSqlQuery query, int limit, int offset = 0);
~~~
~~~csharp
var cursor = new TableSqlQuery("Users")
    .Where("Age>30")
    .Take(10, 20);
~~~

### 5.6 Take重载扩展方法
~~~csharp
TableCursor<ITable> Take(this TableQuery query, int limit, int offset = 0);
~~~
~~~csharp
var age = Column.Use("Age");
var cursor = new TableQuery("Users")
    .And(age.GreaterValue(30))
    .Take(10, 20);
~~~

## 6. Asc方法
~~~csharp
TableCursor<TTable> Asc(Func<TTable, IOrderView> select);
~~~
~~~csharp
var select = new UserTable()
    .ToCursor(10, 20)
    .Asc(table => table.Id);
~~~

## 7. Desc方法
~~~csharp
TableCursor<TTable> Desc(Func<TTable, IOrderAsc> select);
~~~
~~~csharp
var select = new UserTable()
    .ToCursor(10, 20)
    .Desc(table => table.Id);
~~~

## 8. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableCursor\<TTable\>](xref:ShadowSql.Cursors.TableCursor%601)的方法和扩展方法部分
>* 参看[游标简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/cursor/index.md)
