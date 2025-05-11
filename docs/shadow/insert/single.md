# 插入单条
>* 本组件用来组装sql的INSERT语句
>* 本组件是对ShadowSql.Core同名组件的泛型扩展
>* 支持对表类型特殊处理,增强功能、增加易用性

## 1. 接口
>* [ISingleInsert](xref:ShadowSql.Insert.ISingleInsert)

## 2. 基类
>* [SingleInsertBase](xref:ShadowSql.Insert.SingleInsertBase)

## 3. 类型
>* [SingleInsert\<TTable\>](xref:ShadowSql.Insert.SingleInsert%601)

## 4. 相关方法
### 4.1 ToInsert扩展方法
>从表创建SingleInsert
```csharp
SingleInsert<TTable> ToInsert<TTable>(this TTable table)
    where TTable : IInsertTable;
```
```csharp
var table = new StudentTable();
var insert = table.ToInsert()
	.Insert(table.Name.Insert("StudentName"))
	.Insert(table.Score.InsertValue(90));
// INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,90)
```

### 4.2 Insert方法
```csharp
SingleInsert<TTable> Insert(Func<TTable, IInsertValue> select)
    where TTable : IInsertTable;
```
```csharp
var insert = new StudentTable()
    .ToInsert()
    .Insert(student => student.Name.Insert("StudentName"))
    .Insert(student => student.Score.InsertValue(90));;
// INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,90)
```

## 5. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[SingleInsert\<TTable\>](xref:ShadowSql.Insert.SingleInsert%601)的方法和扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/insert/single.md)