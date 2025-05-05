# ShadowSql
>一个.net ORM工具

## 一、简介
>* 主要用来拼接sql
>* 通过扩展Dapper来增强ORM功能
>* 对标开源项目SqlKata(详情查看Benchmarks测试项目)

## 二、Quick Examples
### 1、配置Dapper执行器
>* 配置数据库方言
>* 配置数据库连接

~~~csharp
var engine = new SqliteEngine();
var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
var executor = new DapperExecutor(engine, connection);
~~~

### 2、读取整张表
~~~csharp
var students = SqliteExecutor.From("Students")
    .ToDapperSelect()
    .Get<Student>();
~~~

### 3、查询数据
#### 3.1 原生sql查询
~~~csharp
var students = SqliteExecutor.From("Students")
    .ToSqlQuery()
    .Where("Age=10")
    .ToDapperSelect()
    .Get<Student>();
~~~

#### 3.2 逻辑运算查询
~~~csharp
var ageFilter = Column.Use("Age").EqualValue(10);
var students = SqliteExecutor.From("Students")
    .ToQuery()
    .And(ageFilter)
    .ToDapperSelect()
    .Get<Student>();
~~~

#### 3.3 Or查询
~~~csharp
var students = SqliteExecutor.From("Students")
    .ToSqlQuery()
    .ColumnValue("Age", 10)
    .ToOr()
    .ColumnLikeValue("Name", "张%")
    .ToDapperSelect()
    .Get<Student>();
~~~

### 4、直接逻辑运算过滤
>这种方式性能最好,内存也少

~~~csharp
var table = new StudentTable("Students");
var students = table.ToSelect(table.Age.EqualValue(10))
    .Get<Student>(SqliteExecutor);
~~~

### 5、Cursor(分页及排序)
>* Skip和Take用于分页
>* Desc、Asc及OrderBy用来排序

~~~csharp
var table = new StudentTable("Students");
var students = table.ToQuery()
    .Where(table.Age.GreaterEqualValue(9))
    .ToCursor()
    .Desc(table.Id)
    .Skip(10)
    .Take(10)
    .ToSelect()
    .Get<Student>(SqliteExecutor);
~~~

### 6、联表
~~~csharp
var s = new StudentAliasTable("s");
var c = new SchoolClassAliaTable("c");
var select = s.Join(c)
    .On(s.ClassId.Equal(c.Id))
    .Root.ToSelect();
select.Fields.Select(s.Id, s.Name, s.Age, c.Name.As("ClassName"), c.Teacher);

var students = select.Get<StudentView>(SqliteExecutor);
~~~

### 7、添加数据
#### 7.1 按列参数添加
~~~csharp
var studentInsert = CreateStudentTable()
    .ToInsert()
    .InsertSelfColumns();
SqliteExecutor.Execute(studentInsert, new Student(1, "张三", 10, 1));
SqliteExecutor.Execute(studentInsert, new Student(2, "李四", 11, 1));
~~~

#### 7.2 按值添加
~~~csharp
var insert = SqliteExecutor.From("Students")
    .AddColums(_name, _age)
    .ToDapperInsert()
    .Insert(_name.InsertValue("张三"))
    .Insert(s => s.Column("Age").InsertValue(11));
var result = insert.Execute();
~~~

### 8、删除数据
~~~csharp
var table = new StudentTable();
var result = table.ToQuery()
    .Where(table.Age.LessValue(7))
    .ToDapperDelete(SqliteExecutor)
    .Execute();
~~~

### 9、修改数据
~~~csharp
var table = new StudentTable();
var result = table.ToQuery()
    .Where(table.Age.LessValue(7))
    .ToDapperUpdate(SqliteExecutor)
    .Set(table.ClassId.EqualToValue(1))
    .Execute();
~~~