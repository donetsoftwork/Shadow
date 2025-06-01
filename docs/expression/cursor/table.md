# 表游标
>* 对表进行截取,处理分页和排序
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>[ICursor](xref:ShadowSql.Cursors.ICursor)

## 2. 基类
>[CursorBase](xref:ShadowSql.Cursors.CursorBase)

## 3. 类
>[TableCursor\<TEntity\>](xref:ShadowSql.Expressions.Cursors.TableCursor%601)
~~~csharp
class TableCursor<TEntity> : ICursor;
~~~

## 4. ToCursor
### 4.1 ToCursor扩展方法
~~~csharp
TableCursor<TEntity> ToCursor<TEntity>(this ITable source, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = _db.From("Users")
    .ToCursor<User>(10, 20);
~~~

### 4.2 ToCursor重载扩展方法
~~~csharp
TableCursor<TEntity> ToCursor<TEntity>(this TableSqlQuery<TEntity> query, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = _db.From("Users")
    .ToSqlQuery<User>()
    .Where(u => u.Name == "张三")
    .ToCursor(10, 20);
~~~

### 4.3 ToCursor重载扩展方法
~~~csharp
TableCursor<TEntity> ToCursor<TEntity>(this TableQuery<TEntity> query, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = _db.From("Users")
    .ToQuery<User>()
    .And(u => u.Name == "张三")
    .ToCursor(10, 20);
~~~

### 4.4 ToCursor重载扩展方法
~~~csharp
TableCursor<TEntity> ToCursor<TEntity>(this TableSqlQuery query, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = new TableSqlQuery<User>("Users")
    .Where(u => u.Name == "张三")
    .ToCursor(10, 20);
~~~

### 4.5 ToCursor重载扩展方法
~~~csharp
TableCursor<TEntity> ToCursor<TEntity>(this TableQuery query, int limit = 0, int offset = 0);
~~~
~~~csharp
var cursor = new TableQuery<User>("Users")
    .And(u => u.Name == "张三")
    .ToCursor(10, 20);
~~~

## 5. Take
>* Take方法是ToCursor的平替
### 5.1 Take扩展方法
~~~csharp
TableCursor<TEntity> Take<TEntity>(this ITable source, int limit, int offset = 0);
~~~
~~~csharp
var cursor = _db.From("Users")
    .Take<User>(10, 20);
~~~

### 5.2 Take重载扩展方法
~~~csharp
TableCursor<TEntity> Take<TEntity>(this TableSqlQuery<TEntity> query, int limit, int offset = 0);
~~~
~~~csharp
var cursor = _db.From("Users")
    .ToSqlQuery<User>()
    .Where(u => u.Name == "张三")
    .Take(10, 20);
~~~

### 5.3 Take重载扩展方法
~~~csharp
TableCursor<TEntity> Take<TEntity>(this TableQuery<TEntity> query, int limit, int offset = 0);
~~~
~~~csharp
var cursor = _db.From("Users")
    .ToQuery<User>()
    .And(u => u.Name == "张三")
    .Take(10, 20);
~~~

### 5.4 Take重载扩展方法
~~~csharp
TableCursor<TEntity> Take<TEntity>(this TableSqlQuery query, int limit, int offset = 0);
~~~
~~~csharp
var cursor = new TableSqlQuery<User>("Users")
    .Where(u => u.Name == "张三")
    .Take(10, 20);
~~~

### 5.5 Take重载扩展方法
~~~csharp
TableCursor<TEntity> Take<TEntity>(this TableQuery query, int limit, int offset = 0);
~~~
~~~csharp
var cursor = new TableQuery<User>("Users")
    .And(u => u.Name == "张三")
    .Take(10, 20);
~~~

## 6. Asc方法
~~~csharp
TableCursor<TEntity> Asc<TProperty>(Expression<Func<TEntity, TProperty>> select);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .ToCursor<User>()
    .Asc(u => u.Id);
~~~

## 7. Desc方法
~~~csharp
TableCursor<TEntity> Desc<TProperty>(Expression<Func<TEntity, TProperty>> select);
~~~
~~~csharp
var cursor = EmptyTable.Use("Users")
    .ToCursor<User>()
    .Desc(u => new { u.Age, u.Id });
~~~

## 8. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableCursor\<TTable\>](xref:ShadowSql.Expressions.Cursors.TableCursor%601)的方法和扩展方法部分
>* 参看[游标简介](./index.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/cursor/index.md)
