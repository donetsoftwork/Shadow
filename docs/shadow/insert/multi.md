# 插入多条
>* 本组件用来组装sql的INSERT语句,支持一次插入多条数据
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [IMultiInsert](/api/ShadowSql.Insert.IMultiInsert.html)

## 2. 基类
>* [MultiInsertBase](/api/ShadowSql.Insert.MultiInsertBase.html)

## 3. 类型
>* [MultiInsert\<TTable\>](/api/ShadowSql.Insert.MultiInsert-1.html)

## 4 方法
### 4.1 ToMultiInsert扩展方法
>从表创建MultiInsert
```csharp
MultiInsert<TTable> ToMultiInsert<TTable>(this TTable table)
        where TTable : ITable;
```
```csharp
var table = new StudentTable();
var insert = table.ToMultiInsert()
    .Insert(table.Name.InsertValues("张三", "李四", "王二"))
    .Insert(table.Score.InsertValues(90, 85, 87));
// INSERT INTO [Students]([Name],[Score])VALUES('张三',90),('李四',85),('王二',87)
```

### 4.2 Insert方法
```csharp
MultiInsert<TTable> Insert(Func<TTable, InsertValues> select);
```
```csharp
var insert = new StudentTable()
    .ToMultiInsert()
    .Insert(student => student.Name.InsertValues("张三", "李四", "王二"))
    .Insert(student => student.Score.InsertValues(90, 85, 87));
// INSERT INTO [Students]([Name],[Score])VALUES('张三',90),('李四',85),('王二',87)
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[MultiInsert\<TTable\>](/api/ShadowSql.Insert.MultiInsert-1.html)的方法和扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/insert/multi.md)