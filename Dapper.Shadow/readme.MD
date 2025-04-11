# ShadowSql.Dapper
>* ShadowSql的Dapper扩展
>* 用于执行ShadowSql拼接的sql

以下简单示例

## 1. 读取一张表
~~~csharp
    var select = Executor.From("Students")
        .ToDapperSelect();
    var count = select.Count();
    var students = select.Get<Student>();
~~~

### 2. 分页查询
~~~csharp
    var query = new StudentTable("Students")
        .ToSqlQuery()
        .Where(table => table.Age.GreaterEqualValue(9));
    var count = query.Count(Executor);
    var students = query.ToCursor()
        .Desc(table => table.Id)
        .Skip(1)
        .Take(10)
        .ToSelect()
        .Get<Student>(Executor);
~~~
