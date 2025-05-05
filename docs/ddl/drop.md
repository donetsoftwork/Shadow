# 删表

## 1. 接口
>[IExecuteSql](xref:ShadowSql.Fragments.IExecuteSql)

## 2. 类
>[DropTable](xref:Shadow.DDL.DropTable)


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