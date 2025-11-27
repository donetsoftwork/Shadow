# ShadowSql.Dapper.Core
>* ShadowSql.Core的Dapper扩展
>* 用于执行ShadowSql.Core拼接的sql

以下简单示例

## 1. 读取一张表
~~~csharp
    var table = EmptyTable.Use("Students");        
    var count = await table.CountAsync(Executor);
    var select = new TableSelect(table);
    var students = await select.GetAsync<Student>(Executor)；
~~~

### 2. 分页查询
~~~csharp
    var table = new StudentTable("Students");
    var query = new TableSqlQuery(table)
        .Where(table.Age.GreaterEqualValue(9));
    var count = await query.CountAsync(Executor);
    var cursor = new TableCursor(query)
        .Desc(table.Id)
        .Skip(1)
        .Take(10);
    var select = new TableSelect(cursor);
    var students = await select.GetAsync<Student>(Executor);
~~~

## 3. 删除
~~~csharp
    var table = new StudentTable();
    var query = new TableSqlQuery(table)
        .Where(table.Age.LessValue(7));
    var delete = new TableDelete(table, query.Filter);
    var result = await Executor.ExecuteAsync(delete);
~~~

## 4. 更新
~~~csharp
    var table = new StudentTable();
    var query = new TableSqlQuery(table)
        .Where(table.Age.LessValue(7));
    var update = new TableUpdate(table, query.Filter)
        .Set(table.ClassId.AssignValue(1));
    var result = await SqliteExecutor.ExecuteAsync(update);
~~~

## 5. 插入
~~~csharp
    var table = _db.From("Students")
        .AddColums(_name, _age);
    var insert = new SingleInsert(table)
        .Insert(_name.InsertValue("张三"))
        .Insert(_age.InsertValue(11));
    var result = await insert.ExecuteAsync(Executor);
~~~

