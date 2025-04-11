# ShadowSql.Dapper.Core
>* ShadowSql.Core的Dapper扩展
>* 用于执行ShadowSql.Core拼接的sql

以下简单示例

## 1. 读取一张表
~~~csharp
    var table = SimpleDB.From("Students");        
    var count = table.Count(Executor);
    var select = new TableSelect(table);
    var students = select.Get<Student>(Executor)；
~~~

### 2. 分页查询
~~~csharp
    var table = new StudentTable("Students");
    var query = new TableSqlQuery(table)
        .Where(table.Age.GreaterEqualValue(9));
    var count = query.Count(Executor);
    var cursor = new TableCursor(query)
        .Desc(table.Id)
        .Skip(1)
        .Take(10);
    var select = new TableSelect(cursor);
    var students = select.Get<Student>(Executor);
~~~

