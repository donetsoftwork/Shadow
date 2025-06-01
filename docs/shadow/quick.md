# 快速上手
>* 易用版快速上手

## 一、从表开始
### 1. 自定义表
>* 继承Table
>* 通过DefineColumn定义列
>* 建议定义为单例或用IOC管理
~~~csharp
public class CommentTable : Table
{
    public CommentTable(string tableName = "Comments")
        : base(tableName)
    {
        Id = DefineColumn(nameof(Id));
        UserId = DefineColumn(nameof(UserId));
        PostId = DefineColumn(nameof(PostId));
        Content = DefineColumn(nameof(Content));
        Pick = DefineColumn(nameof(Pick));
    }
    public readonly IColumn Id;
    public readonly IColumn UserId;
    public readonly IColumn PostId;
    public readonly IColumn Content;
    public readonly IColumn Pick;
}
~~~

### 2. 自定义别名表
>* 如果要联表,同步自定义别名表是有必要的
>* 继承TableAlias
>* 通过AddColumn定义前缀列
>* 建议定义为单例或用IOC管理
~~~csharp
public class CommentAliasTable : TableAlias<CommentTable>
{
    public CommentAliasTable(string tableAlias)
        : this(new CommentTable("Comments"), tableAlias){}
    public CommentAliasTable(CommentTable table, string tableAlias)
        : base(table, tableAlias)
    {
        Id = AddColumn(table.Id);
        UserId = AddColumn(table.UserId);
        PostId = AddColumn(table.PostId);
        Content = AddColumn(table.Content);
        Pick = AddColumn(table.Pick);
    }
    public readonly IPrefixField Id;
    public readonly IPrefixField UserId;
    public readonly IPrefixField PostId;
    public readonly IPrefixField Content;
    public readonly IPrefixField Pick;
}
~~~

### 3. 用DB管理表
~~~csharp
var table = _db.From("Users")
~~~

## 二、查询表
>* 参看[表查询](./sqlquery/table.md)
### 1. 自定义表查询
#### 1.1 常量查询
~~~csharp
var table = new UserTable();
var query = table.ToSqlQuery()
    .Where(table.Id.EqualValue(100));
// SELECT * FROM [Users] WHERE [Id]=100
~~~

#### 1.2 参数查询
~~~csharp
var table = new UserTable();
var query = table.ToSqlQuery()
    .Where(table.Id.Equal("LastId"));
// SELECT * FROM [Users] WHERE [Id]=@LastId
~~~

### 2. 其他表查询
#### 2.1 常量查询
~~~csharp
var query =_db.From("Users")
    .ToSqlQuery()
    .FieldEqualValue("Id", 100);
// SELECT * FROM [Users] WHERE [Id]=100
~~~

#### 2.2 参数查询
~~~csharp
var query =_db.From("Users")
    .ToSqlQuery()
    .FieldEqual("Id");
// SELECT * FROM [Users] WHERE [Id]=@Id
~~~


## 三、联表查询
>* 参看[联表查询](./sqlquery/join.md)
### 1. 自定义别名表联表
#### 1.1 常量查询
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var query = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .Root
    .Where(c.Pick.EqualValue(true))
    .Where(p.Author.EqualValue("张三"));
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'
~~~

#### 1.2 参数查询
~~~csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var query = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .Root
    .Where(c.Pick.Equal())
    .Where(p.Author.Equal());
// SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=@Pick AND p.[Author]=@Author
~~~

### 2. 其他表联表
#### 2.1 常量查询
~~~csharp
var joinOn = _db.From("Comments")
    .SqlJoin(_db.From("Posts"))
    .OnColumn("PostId", "Id")
    .WhereLeft("Pick", Pick => Pick.EqualValue(true))
    .WhereRight("Author", Author => Author.NotEqualValue("张三"));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1 AND t2.[Author]<>'张三'
~~~

#### 2.2 参数查询
~~~csharp
var joinOn = _db.From("Comments")
    .SqlJoin(_db.From("Posts"))
    .OnColumn("PostId", "Id")
    .WhereLeft("Pick", Pick => Pick.Equal())
    .WhereRight("Author", Author => Author.NotEqual());
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=@Pick AND t2.[Author]<>@Author
~~~

## 四、分组查询
>* 参看[分组查询](./sqlquery/groupby.md)
### 1. 可以直接对表分组
>* 调用SqlGroupBy

#### 1.1 常量查询
~~~csharp
var groupBy = _db.From("Comments")
    .SqlGroupBy("PostId")
    .Having(g => g.Sum("Pick").GreaterValue(100));
// SELECT * FROM [Comments] GROUP BY [PostId] HAVING SUM([Pick])>100
~~~
~~~csharp
var table = new CommentTable();
var groupBy = table.SqlGroupBy(table.PostId)
    .Having(table.Pick.Sum().GreaterValue(100));
// SELECT * FROM [Comments] GROUP BY [PostId] HAVING SUM([Pick])>100
~~~

#### 1.2 参数查询
~~~csharp
var groupBy = _db.From("Comments")
    .SqlGroupBy("PostId")
    .Having(g => g.Sum("Pick").Greater("PostPick"));
// SELECT * FROM [Comments] GROUP BY [PostId] HAVING SUM([Pick])>@PostPick
~~~
~~~csharp
var table = new CommentTable();
var groupBy = table.SqlGroupBy(table.PostId)
    .Having(table.Pick.Sum().Greater("PostPick"));
// SELECT * FROM [Comments] GROUP BY [PostId] HAVING SUM([Pick])>@PostPick
~~~

### 2. 对表查询分组
>* 调用路径:ToSqlQuery().SqlGroupBy()
>* [表查询](./sqlquery/table.md)
>* [分组查询](./sqlquery/groupby.md)

#### 2.1 常量查询
~~~csharp
var groupBy = _db.From("Comments")
    .ToSqlQuery()
    .FieldGreaterEqualValue("Pick", 10)
    .SqlGroupBy("PostId")
    .Having(g => g.Sum("Pick").GreaterValue(100));
// SELECT * FROM [Comments] WHERE [Pick]>=10 GROUP BY [PostId] HAVING SUM([Pick])>100
~~~
~~~csharp
var table = new CommentTable();
var groupBy = table.ToSqlQuery()
    .Where(table.Pick.GreaterEqualValue(10))
    .SqlGroupBy(table.PostId)
    .Having(table.Pick.Sum().GreaterValue(100));
// SELECT * FROM [Comments] WHERE [Pick]>=10 GROUP BY [PostId] HAVING SUM([Pick])>100
~~~

#### 2.2 参数查询
~~~csharp
var groupBy = _db.From("Comments")
    .ToSqlQuery()
    .FieldGreaterEqual("Pick")
    .SqlGroupBy("PostId")
    .Having(g => g.Sum("Pick").Greater("PostPick"));
// SELECT * FROM [Comments] WHERE [Pick]>=@Pick GROUP BY [PostId] HAVING SUM([Pick])>@PostPick
~~~
~~~csharp
var table = new CommentTable();
var groupBy = table.ToSqlQuery()
    .Where(table.Pick.GreaterEqual())
    .SqlGroupBy(table.PostId)
    .Having(table.Pick.Sum().Greater("PostPick"));
// SELECT * FROM [Comments] WHERE [Pick]>=@Pick GROUP BY [PostId] HAVING SUM([Pick])>@PostPick
~~~

## 五、插入
>* 参看[Insert](./insert/single.md)
### 1. 按列插入
```csharp
var name = Column.Use("Name");
var score = Column.Use("Score");
var insert = new SingleInsert("Students")
    .InsertColumns(name, score);
// INSERT INTO [Students]([Name],[Score])VALUES(@Name,@Score)
```

### 2. 插入所有字段
>* 支持忽略部分字段
```csharp
var table = new Table("Users")
    .DefineColums("Id","Name", "Age")
    .IgnoreInsert("Id");
var insert = table.ToInsert()
    .InsertSelfColumns();
// 预设Id是自增列,可查询不可插入
// INSERT INTO [Users]([Name],[Age])VALUES(@Name,@Age)
```

## 六、删除
>* 调用ToDelete
>* 参看[delete](./delete/table.md)
~~~csharp
var table = new StudentTable();
var delete = table.ToSqlQuery()
    .Where(table.Score.LessValue(60))
    .ToDelete();
// DELETE FROM [Students] WHERE [Score]<60
~~~

## 七、更新
>* 参看[update](./update/table.md)

### 1. 按列更新
#### 1.1 常量更新
~~~csharp
var table = new StudentTable();
var update = table.ToSqlQuery()
    .Where(table.Score.LessValue(60))
    .ToUpdate()
    .Set(table => table.Score.AddValue(10));
// UPDATE [Students] SET [Score]+=10 WHERE [Score]<60
~~~

#### 1.2 参数更新
~~~csharp
var table = new StudentTable();
var update = table.ToSqlQuery()
    .Where(table.Score.Less())
    .ToUpdate()
    .Set(table => table.Score.AddAssign());
// UPDATE [Students] SET [Score]+=@Score WHERE [Score]<@Score
~~~

### 2. 更新所有字段
>* 支持忽略部分字段
~~~csharp
var id = Column.Use("Id");
var name = Column.Use("Name");
var age = Column.Use("Age");
var table = new Table("Users")
    .AddColums(id, name, age)
    .IgnoreUpdate(id);
var update = table.ToUpdate(id.Equal())
    .SetSelfFields();
// 预设Id是主键不更新
// UPDATE [Users] SET [Name]=@Name,[Age]=@Age WHERE [Id]=@Id
~~~

## 八、获取数据
>* 参看[select](./select/table.md)

### 1. 直接获取全表
>* 调用ToSelect
~~~csharp
UserTable table = new();
var select = table.ToSelect()
    .Select(table.Id, table.Name);
// SELECT [Id],[Name] FROM [Users]
~~~

### 2. 从表查询获取
>* 调用路径:ToSqlQuery().ToSelect()
~~~csharp
var table = new UserTable();
var select = table.ToSqlQuery()
    .Where(table.Status.EqualValue(true))
    .ToSelect()
    .Select(table.Id, table.Name);;
// SELECT [Id],[Name] FROM [Users]
~~~

### 3. 分页获取
>* 调用路径:ToSqlQuery().ToCursor().ToSelect()
~~~csharp
var table = new UserTable();
var select = table.ToSqlQuery()
    .Where(table.Status.EqualValue(true))
    .ToCursor(10, 20)
    .Asc(table.Id)
    .ToSelect()
    .Select(table.Id, table.Name);
// SELECT [Id],[Name] FROM [Users] WHERE [Status]=1 ORDER BY [Id] OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY
~~~
