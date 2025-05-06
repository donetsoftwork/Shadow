# sql逻辑
>* 用来实现[sql查询](../sqlquery/index.md)

## 1. 接口
>* [ISqlLogic](xref:ShadowSql.Logics.ISqlLogic)

## 2. SqlQuery基类
>* SqlQuery用来定义sql逻辑的规范
>* 参看[SqlQuery](xref:ShadowSql.Queries.SqlQuery)
>* 实现类是[SqlAndQuery](xref:ShadowSql.Queries.SqlAndQuery)和[SqlAndQuery](xref:ShadowSql.Queries.SqlOrQuery)
~~~csharp
class SqlQuery : ISqlLogic;
~~~

### 2.1 原生sql查询方法
~~~csharp
SqlAndQuery And(params IEnumerable<string> conditions);
SqlOrQuery Or(params IEnumerable<string> conditions);
~~~

### 2.2 原子逻辑查询方法
~~~csharp
SqlAndQuery And(AtomicLogic atomic);
SqlOrQuery Or(AtomicLogic atomic);
~~~

### 2.3 AND和OR切换方法
>* 用于切换为AND或OR
>* 也能用来实现AND和OR的嵌套查询
~~~csharp
SqlAndQuery ToAnd();
SqlOrQuery ToOr();
~~~
### 2.4 静态工厂方法
~~~csharp
static SqlAndQuery CreateAndQuery();
static SqlOrQuery CreateOrQuery();
~~~

## 3. SqlAndQuery
>* 参看[SqlAndQuery](xref:ShadowSql.Queries.SqlAndQuery)
## 3.1 SqlAndQuery
>* 实现SqlQuery
>* 用来实现AND查询
>* 包含[ComplexAndLogic](xref:ShadowSql.Logics.ComplexAndLogic)和[SqlConditionLogic](xref:ShadowSql.Logics.SqlConditionLogic)
>* 参看[AND逻辑](./and.md)
>* 参看[原生sql条件](./condition.md)

~~~csharp
class SqlAndQuery(ComplexAndLogic complex, SqlConditionLogic conditions) : SqlQuery, ISqlLogic;
~~~

## 3.2 And扩展方法
>* 嵌套OR逻辑
~~~csharp
SqlAndQuery And(this SqlAndQuery query, OrLogic logic);
SqlAndQuery And(this SqlAndQuery query, ComplexOrLogic logic);
~~~
~~~csharp
var score = Column.Use("Score");
var age = Column.Use("Age");
var query = SqlQuery.CreateAndQuery()
    .And(score.LessValue(60))
    .And(age.LessValue(10) | age.GreaterValue(15));
// [Score]<60 AND ([Age]<10 OR [Age]>15)
~~~

## 4. SqlOrQuery
>* 参看[SqlOrQuery](xref:ShadowSql.Queries.SqlOrQuery)
## 4.1 SqlOrQuery
>* 实现SqlQuery
>* 用来实现OR查询
>* 包含[ComplexOrLogic](xref:ShadowSql.Logics.ComplexOrLogic)和[SqlConditionLogic](xref:ShadowSql.Logics.SqlConditionLogic)
>* 参看[OR逻辑](./or.md)
>* 参看[原生sql条件](./condition.md)

~~~csharp
class SqlOrQuery(ComplexOrLogic complex, SqlConditionLogic conditions) : SqlQuery, ISqlLogic;
~~~

## 4.2 Or扩展方法
>* 嵌套AND逻辑
~~~csharp
SqlOrQuery Or(this SqlOrQuery query, AndLogic logic);
SqlOrQuery Or(this SqlOrQuery query, ComplexAndLogic logic);
~~~
~~~csharp
var score = Column.Use("Score");
var age = Column.Use("Age");
var query = SqlQuery.CreateOrQuery()
    .Or(score.GreaterValue(90))
    .Or(age.GreaterValue(10) & age.LessValue(13));
// [Score]>90 OR ([Age]>10 AND [Age]<13)
~~~

## 5. 其他相关功能
>* 参看[逻辑简介](./index.md)
>* 参看[原子逻辑](./atomic.md)
>* 参看[AND逻辑](./and.md)
>* 参看[OR逻辑](./or.md)
>* 参看[原生sql条件](./condition.md)