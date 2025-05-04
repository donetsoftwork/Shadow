# 表删除
>* 从表中删除数据

## 1. 接口
>[IDelete](/api/ShadowSql.Delete.IDelete.html)

## 2. 类
>[TableDelete](/api/ShadowSql.Delete.TableDelete.html)

## 3. 相关方法
### 3.1 ToDelete扩展方法
```csharp
TableDelete ToDelete(this ITable table, ISqlLogic where);
```
```csharp
var table = new StudentTable();
var delete = table.ToDelete(table.Score.LessValue(60));
```
>DELETE FROM [Students] WHERE [Score]<60

### 3.2 ToDelete重载扩展方法
```csharp
TableDelete ToDelete<TTable>(this TTable table, Func<TTable, ISqlLogic> query)
    where TTable : ITable;
```
```csharp
var delete = new StudentTable()
    .ToDelete(table => table.Score.LessValue(60));
```
>DELETE FROM [Students] WHERE [Score]<60

## 4. 其他相关功能
>* 本组件并非只有以上功能,其他功能参看以下文档:
>* 参看[TableDelete](/api/ShadowSql.Delete.TableDelete.html)的扩展方法部分
>* 参看[ShadowSqlCore相关文档](../../shadowcore/delete/table.md)
