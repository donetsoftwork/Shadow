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

## 3. 删除
~~~csharp
    var result = new StudentTable()
        .ToSqlQuery()
        .Where(table => table.Age.LessValue(7))
        .ToDelete()
        .Execute(Executor);
~~~

## 4. 更新
~~~csharp
    var result = new StudentTable()
        .ToSqlQuery()
        .Where(table => table.Age.LessValue(7))
        .ToUpdate()
        .Set(table => table.ClassId.EqualToValue(1))
        .Execute(Executor);
~~~

## 5. 插入
~~~csharp
    var insert = new StudentTable()
        .ToInsert()
        .Insert(student => student.Name.Insert())
        .Insert(student => student.Age.InsertValue(11));
    var result = insert.Execute(Executor, new { Name = "张三" });
~~~
>