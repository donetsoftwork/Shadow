# Dapper.Shadow
>* ShadowSql的Dapper扩展
>* 用于执行ShadowSql拼接的sql
>* 支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等,可扩展其他数据库方言的支持
>* Nuget包名: ShadowSql.Dapper
>* 易用版

## 1. 读取一张表
~~~csharp
    var select = Executor.From("Students")
        .ToDapperSelect();
    var count = await select.CountAsync();
    var students = await select.GetAsync<Student>();
~~~

### 2. 分页查询
~~~csharp
    var query = new StudentTable("Students")
        .ToSqlQuery()
        .Where(table => table.Age.GreaterEqualValue(9));
    var count = await query.CountAsync(Executor);
    var students = await query.Take(10, 10)
        .Desc(table => table.Id)
        .ToSelect()
        .GetAsync<Student>(Executor);
~~~

## 3. 删除
~~~csharp
    var result = await new StudentTable()
        .ToSqlQuery()
        .Where(table => table.Age.LessValue(7))
        .ToDelete()
        .ExecuteAsync(Executor);
~~~

## 4. 更新
~~~csharp
    var result = await new StudentTable()
        .ToSqlQuery()
        .Where(table => table.Age.LessValue(7))
        .ToUpdate()
        .Set(table => table.ClassId.AssignValue(1))
        .ExecuteAsync(Executor);
~~~

## 5. 插入
~~~csharp
    var insert = new StudentTable()
        .ToInsert()
        .Insert(student => student.Name.Insert())
        .Insert(student => student.Age.InsertValue(11));
    var result = await insert.ExecuteAsync(Executor, new { Name = "张三" });
~~~