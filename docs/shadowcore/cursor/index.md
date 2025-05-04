# 游标简介
>* 本组件与数据库游标的作用稍有不同,是对数据进行截取,用于处理分页和排序
>* 其一是sql的ORDER BY部分
>* 其二是分页获取数据
>* 按where查询好的数据,本组件再进行排序和分页筛选
>* 好比用游标卡尺精准截取
>* 本组件是[游标获取](../select/cursor.md)的依赖组件,不单独使用

## 1. 接口
>[ICursor](/api/ShadowSql.Cursors.ICursor.html)

## 2. 基类
>[CursorBase](/api/ShadowSql.Cursors.CursorBase.html)

## 3. 排序方法
>* 多次调用排序方法会叠加,都会拼接到ORDER BY子句中
>* 调用所有排序方法也是有顺序的,先被调用的拼写sql在前
### 3.1 Asc扩展方法
>正序
```csharp
TCursor Asc<TCursor>(this TCursor cursor, IOrderView field)
    where TCursor : CursorBase;
TCursor Asc<TCursor>(this TCursor cursor, params IEnumerable<string> fieldNames)
    where TCursor : CursorBase;
TCursor Asc<TCursor>(this TCursor cursor, Func<ITableView, IOrderView> select)
    where TCursor : CursorBase, ICursor;
```

### 3.2 Desc扩展方法
>倒序
```csharp
TCursor Desc<TCursor>(this TCursor cursor, IOrderAsc field)
    where TCursor : CursorBase;
TCursor Desc<TCursor>(this TCursor cursor, params IEnumerable<string> fieldNames)
    where TCursor : CursorBase;
TCursor Desc<TCursor>(this TCursor cursor, Func<ITableView, IOrderAsc> select)
     where TCursor : CursorBase, ICursor;
```

### 3.3 OrderBy扩展方法
>使用原始SQL语句进行排序
```csharp
TCursor OrderBy<TCursor>(this TCursor cursor, string orderBy)
    where TCursor : CursorBase;
```

## 4. 分页方法
>LINQ一致,通过Skip和Take方法进行分页
### 4.1 Skip扩展方法
>跳过前n条数据
```csharp
TCursor Skip<TCursor>(this TCursor cursor, int offset)
    where TCursor : CursorBase;
```

### 4.2 Take扩展方法
>获取前n条数据
```csharp
TCursor Take<TCursor>(this TCursor cursor, int limit)
    where TCursor : CursorBase;
```
