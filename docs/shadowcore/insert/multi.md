# 插入多条
>* 本组件用来组装sql的INSERT语句,支持一次插入多条数据

## 1. 接口
>* [IMultiInsert](xref:ShadowSql.Insert.IMultiInsert)

## 2. 基类
>* [MultiInsertBase](xref:ShadowSql.Insert.MultiInsertBase)

## 3. 类型
>* [MultiInsert](xref:ShadowSql.Insert.MultiInsert)

## 4 方法
### 4.1 Insert方法
>多次InsertValues的值数量要一致,否则可能会导致越界异常

```csharp
TInsert Insert<TInsert>(this TInsert insert, IInsertValues value)
        where TInsert : MultiInsertBase, IMultiInsert;
```
```csharp
var table = new StudentTable();
var insert = new MultiInsert(table)
    .Insert(table.Name.InsertValues("张三", "李四", "王二"))
    .Insert(table.Score.InsertValues(90, 85, 87));
// INSERT INTO [Students]([Name],[Score])VALUES('张三',90),('李四',85),('王二',87)
```