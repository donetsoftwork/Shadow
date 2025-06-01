# 别名表
>* 作为别名表的影子(占位符)
>* 主要用来联表或子查询

## 1. 接口
>* [IAliasTable](xref:ShadowSql.Identifiers.IAliasTable)
>* [IAliasTable\<TTable\>](xref:ShadowSql.Identifiers.IAliasTable%601)
>* [ITableView](xref:ShadowSql.Identifiers.ITableView)

## 2. TableAlias类
>* 参看[TableAlias\<TTable\>](xref:ShadowSql.Variants.TableAlias%601)
```csharp
class TableAlias<TTable> : IAliasTable<TTable>, IAliasTable{
	TTable Target { get; }
	IEnumerable<IPrefixField> PrefixFields { get; }
	IPrefixField AddColumn(IColumn column);
}
```

## 3. As扩展方法
>* 从Table创建[TableAlias\<TTable\>](xref:ShadowSql.Variants.TableAlias%601)
```csharp
TableAlias<TTable> As<TTable>(this TTable table, string alias)
	where TTable : ITable
```

## 4. 联表获取
>* 参看[联表获取](../../shadow/select/join.md)
>* 参看[联表分页](../../shadow/select/joincursor.md)
```csharp
var c = new Table("Comments")
    .As("c");
var p = new Table("Posts")
    .As("p");
var select = c.SqlJoin(p)
    .OnColumn("PostId", "Id")
    .ToSelect()
    .SelectTable(c);
// SELECT c.* FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]
```

## 5. 联表更新
>* 参看[联表更新精简版](../../shadow/update/multi.md)
>* 参看[联表更新易用版](../../shadow/update/multi.md)
```csharp
var c = new Table("Comments")
    .As("c");
var p = new Table("Posts")
    .As("p");
var update = c.SqlJoin(p)
    .OnColumn("PostId", "Id")
    .Root
    .Where(p.Field("Author").LikeValue("%专家"))
    .ToUpdate()
    .Update(c)
    .SetValue("Pick", true);
// UPDATE c SET c.[Pick]=1 FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author] LIKE '%专家'
```

## 6. 联表删除
>* 参看[联表删除精简版](../../shadow/delete/multi.md)
>* 参看[联表删除易用版](../../shadow/delete/multi.md)
```csharp
var c = new Table("Comments")
    .As("c");
var p = new Table("Posts")
    .As("p");
var update = c.SqlJoin(p)
    .OnColumn("PostId", "Id")
    .Root
    .TableFieldEqualValue("p", "Author", "王二")
    .ToDelete()
    .Delete(c);
// DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='王二'
```

## 7. 自定义别名表
>* 继承TableAlias的自定义别名表也继承了TableAlias的所有功能
>* 更方便联表操作
```csharp
public class CommentAliasTable : TableAlias<CommentTable> {
    public CommentAliasTable(string tableAlias)
        : this(new CommentTable("Comments"), tableAlias) { }
    public CommentAliasTable(CommentTable table, string tableAlias)
        : base(table, tableAlias) {
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
public class PostAliasTable : TableAlias<PostTable> {
    public PostAliasTable(string tableAlias)
        : this(new PostTable("Posts"), tableAlias) { }
    public PostAliasTable(PostTable table, string tableAlias)
        : base(table, tableAlias) {
        Id = AddColumn(table.Id);
        Title = AddColumn(table.Title);
        Author = AddColumn(table.Author);
        AuthorId = AddColumn(table.AuthorId);
    }
    public readonly IPrefixField Id;
    public readonly IPrefixField Title;
    public readonly IPrefixField Author;
    public readonly IPrefixField AuthorId;
}
```
```csharp
CommentAliasTable c = new("c");
PostAliasTable p = new("p");
var delete = c.SqlJoin(p)
    .On(c.PostId, p.Id)
    .Root
    .Where(p.Author.EqualValue("王二"))
    .ToDelete()
    .Delete(c);
// DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='王二'
```

## 9. 其他相关功能
>* 本组件并非只支持以上功能,其他功能参看以下文档:
>* 参看[TableAlias\<TTable\>](xref:ShadowSql.Variants.TableAlias%601)
>* 参看[联表获取](../../shadow/select/join.md)
>* 参看[联表分页](../../shadow/select/joincursor.md)
>* 参看[联表更新精简版](../../shadow/update/multi.md)
>* 参看[联表更新易用版](../../shadow/update/multi.md)
>* 参看[联表删除精简版](../../shadow/delete/multi.md)
>* 参看[联表删除易用版](../../shadow/delete/multi.md)
