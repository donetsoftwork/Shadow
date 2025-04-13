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

## 3. 删除
~~~csharp
    var table = new StudentTable();
    var query = new TableSqlQuery(table)
        .Where(table.Age.LessValue(7));
    var delete = new TableDelete(table, query.Filter);
    var result = Executor.Execute(delete);
~~~

## 4. 更新
~~~csharp
    var table = new StudentTable();
    var query = new TableSqlQuery(table)
        .Where(table.Age.LessValue(7));
    var update = new TableUpdate(table, query.Filter)
        .Set(table.ClassId.EqualToValue(1));
    var result = SqliteExecutor.Execute(update);
~~~

## 5. 插入
~~~csharp
    var table = _db.From("Students")
        .AddColums(_name, _age);
    var insert = new SingleInsert(table)
        .Insert(_name.InsertValue("张三"))
        .Insert(_age.InsertValue(11));
    var result = insert.Execute(Executor);
~~~
>
