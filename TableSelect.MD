# 单表查询

## 1、原生sql查询
~~~csharp
var students = SqliteExecutor.From("Students")
    .ToQuery()
    .Where("Age=10")
    .ToDapperSelect()
    .Get<Student>();
~~~

## 2、按列名查询
~~~csharp
var students = SqliteExecutor.From("Students")
    .ToQuery()
    .ColumnValue("Age", 10)
    .ToDapperSelect()
    .Get<Student>();
~~~

## 3、自定义列查询
~~~csharp
var age = Column.Use("Age");
var students = SqliteExecutor.From("Students")
    .ToQuery()
    .Where(age.EqualValue(10))
    .ToDapperSelect()
    .Get<Student>();
~~~

## 4、自定义表查询
>自定义表方便调用自己的列
~~~csharp
var table = new StudentTable("Students");
var students = table.ToQuery()
    .Where(table.Age.EqualValue(10))
    .ToSelect()
    .Get<Student>(SqliteExecutor);
~~~

## 5、委托查询
~~~csharp
var students = new StudentTable("Students")
    .ToQuery()
    .Where(student => student.Age.EqualValue(10))
    .ToSelect()
    .Get<Student>(SqliteExecutor);
~~~

## 6、表定义列查询
>表中不存在的列,使用Column引用抛异常
~~~csharp
var students = new Table("Students")
    .DefineColums("Age")
    .ToQuery()
    .Where(student => student.Column("Age").EqualValue(10))
    .ToSelect()
    .Get<Student>(SqliteExecutor);
~~~

## 7、虚拟字段查询
>Field不检查表结构是否含该字段
~~~csharp
var students = SqliteExecutor.From("Students")
    .ToQuery()
    .Where(student => student.Field("Age").EqualValue(10))
    .ToDapperSelect()
    .Get<Student>();
~~~