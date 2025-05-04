# 快速上手

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
    public readonly IPrefixColumn Id;
    public readonly IPrefixColumn UserId;
    public readonly IPrefixColumn PostId;
    public readonly IPrefixColumn Content;
    public readonly IPrefixColumn Pick;
}
~~~

### 3. 用DB管理表
~~~csharp
var table = _db.From("Users")
~~~

## 二、查询表
>* 参看[表查询](/shadow/sqlquery/table.html)
### 1. 自定义表查询
~~~csharp
var table = new UserTable();
var query = table.ToSqlQuery()
    .Where(table.Id.EqualValue(100));
// SELECT * FROM [Users] WHERE [Id]=100
~~~

### 2. 其他表查询
~~~csharp
var query =_db.From("Users")
    .ToSqlQuery()
    .FieldEqualValue("Id", 100);
// SELECT * FROM [Users] WHERE [Id]=100
~~~
~~~csharp
var query =_db.From("Users")
    .ToSqlQuery()
    .Where("Id=100");
// SELECT * FROM [Users] WHERE Id=100
~~~

## 三、联表查询
>* 参看[联表查询](/shadow/sqlquery/join.md)
### 1. 自定义别名表联表
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

### 2. 其他表联表
~~~csharp
var joinOn = _db.From("Comments")
    .SqlJoin(_db.From("Posts"))
    .OnColumn("PostId", "Id")
    .WhereLeft("Pick", Pick => Pick.EqualValue(true))
    .WhereRight("Author", Author => Author.NotEqualValue("张三"));
// SELECT * FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1 AND t2.[Author]<>'张三'
~~~

## 四、分组查询
>* 参看[分组查询](./shadow/sqlquery/groupby.md)
### 1. 可以直接对表分组
>* 调用SqlGroupBy
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

### 2. 对表查询分组
>* 调用路径:ToSqlQuery().SqlGroupBy()
>* [表查询](./shadow/sqlquery/table.md)
>* [分组查询](./shadow/sqlquery/groupby.md)
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

## 五、插入
>* 调用ToInsert
>* 参看[Insert](./shadow/insert/single.md)
~~~csharp
var table = new StudentTable();
var insert = table.ToInsert()
    .Insert(insert.Name.Insert("StudentName"))
    .Insert(insert.Score.InsertValue(90));
// INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,90)
~~~

## 六、删除
>* 调用ToDelete
>* 参看[delete](./shadow/delete/table.md)
~~~csharp
var table = new StudentTable();
var delete = table.ToSqlQuery()
    .Where(table.Score.LessValue(60))
    .ToDelete();
// DELETE FROM [Students] WHERE [Score]<60
~~~

## 七、更新
>* 调用ToUpdate
>* 参看[update](./shadow/update/table.md)
~~~csharp
var table = new StudentTable();
var update = table.ToSqlQuery()
    .Where(table.Score.LessValue(60))
    .ToUpdate()
    .Set(table => table.Score.AddValue(10));
// UPDATE [Students] SET [Score]+=10 WHERE [Score]<60
~~~

## 八、获取数据
>* 参看[select](./shadow/select/table.md)

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
