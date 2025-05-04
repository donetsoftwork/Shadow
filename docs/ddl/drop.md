# 删表

## 1. 接口
>[IExecuteSql](/api/ShadowSql.Fragments.IExecuteSql.html)

## 2. 类
>[DropTable](/api/Shadow.DDL.DropTable.html)


## 3. DropTable
~~~csharp
var drop = new DropTable("Students");
~~~
>DROP TABLE "Students"

## 4. 搭配Dapper.Shadow删表
~~~csharp
    var result = new StudentTable()
    .ToDrop()
    .Execute(Executor);
~~~