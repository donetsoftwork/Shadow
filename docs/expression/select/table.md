# 获取表
>* 从表获取数据组件
>* 本组件用来处理sql的SELECT语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对数据表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [ISelect](xref:ShadowSql.Select.ISelect)

## 2. 基类
>* [SelectFieldsBase](xref:ShadowSql.SelectFields.SelectFieldsBase)

## 3. 类
>* [TableSelect\<TEntity\>](xref:ShadowSql.Expressions.Select.TableSelect%601)


## 4. ToSelect
>* 创建[TableSelect\<TEntity\>](xref:ShadowSql.Expressions.Select.TableSelect%601)

### 4.1 从表获取
~~~csharp
TableSelect<TEntity> ToSelect<TEntity>(this ITable table);
~~~
~~~csharp
var select = _db.From("Users")
    .ToSelect<User>();
// SELECT [Id],[Name] FROM [Users]
~~~

### 4.2 从表和查询条件获取
~~~csharp
TableSelect<TEntity> ToSelect<TEntity>(this ITable table, Expression<Func<TEntity, bool>> query);
TableSelect<TEntity> ToSelect<TEntity, TParameter>(this ITable table, Expression<Func<TEntity, TParameter, bool>> query);
~~~
~~~csharp
var select = _db.From("Users")
    .ToSelect<User>(u => u.Status);
// SELECT *FROM [Users] WHERE [Status]=1
~~~
~~~csharp
var select = _db.From("Users")
    .ToSelect<User, User>((u, p) => u.Id == p.Id);
// SELECT * FROM [Users] WHERE [Id]=@Id
~~~

### 4.3 从sql表查询获取
>* 依赖[sql表查询](../sqlquery/table.md)
~~~csharp
TableSelect<TEntity> ToSelect<TEntity>(this TableSqlQuery<TEntity> query);
~~~
~~~csharp
var select = _db.From("Users")
    .ToSqlQuery<User>()
    .Where(u => u.Status)
    .ToSelect();
// SELECT * FROM [Users] WHERE Status=1
~~~

### 4.4 从逻辑表查询获取
>* 依赖[逻辑表查询](../query/table.md)
~~~csharp
TableSelect<TEntity> ToSelect<TEntity>(this TableQuery<TEntity> query);
~~~
~~~csharp
var select = _db.From("Users")
    .ToQuery<User>()
    .And(u => u.Status)
    .ToSelect();
// SELECT * FROM [Users] WHERE Status=1
~~~

## 5. Select方法
~~~csharp
TableSelect<TEntity> Select<TProperty>(Expression<Func<TEntity, TProperty>> select);
~~~
~~~csharp
var select = _db.From("Users")
    .ToSelect<User>()
    .Select(u => new { u.Id, u.Name });
// SELECT [Id],[Name] FROM [Users]
~~~
~~~csharp
var select = _db.From("Users")
    .ToSelect<User>(u => u.Status)
    .Select(u => u.Id);
// SELECT [Id] FROM [Users] WHERE [Status]=1
~~~

## 6. 其他相关功能
>* 参看[TableSelect\<TEntity\>](xref:ShadowSql.Expressions.Select.TableSelect%601)的方法和扩展方法部分
>* 参看[获取简介](./index.md)
>* 参看[sql表查询](../sqlquery/table.md)
>* 参看[逻辑表查询](../query/table.md)
>* 参看[ShadowSqlCore相关文档](../../shadowcore/select/index.md)
