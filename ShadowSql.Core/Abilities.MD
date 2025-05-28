# 功能

## 一、数据库
### 1、接口
>IDB

### 2、类型
>* DB
>* SimpleDB
>* DBSchema

### 3、作用
>* 映射数据库
>* 管理/获取表(From(tableName))

### 二、表(视图)
### 1、原始表
### 1.1 接口
>* ITable
~~~csharp
interface ITable : IInsertTable, IUpdateTable, ITableView
~~~


### 1.2 类型
>* Table
>* EmptyTable
>* TableSchema

### 1.3 作用
>* 映射数据表
>* 管理/获取列(GetColumn(columName))
>* 映射字段(Field(fieldName))

>* 注:添、删、改、查可以基于Column也可以基于Field,两种不同模式
>* 注:基于Column就必须提前定义好,未定义的Column不能使用
>* 注:基于Field无需定义,直接使用

### 2、别名表
>用于多表(联表、子查询)以示区分

### 2.1 接口
>* IAliasTable

### 2.2 类型
>* TableAlias
~~~csharp
public class TableAlias<TTable>(TTable target, string tableAlias)
    : IAliasTable
    where TTable : ITableView
~~~

### 3、多(联)表
### 3.1 接口
>* IMultiTable
>* IMultiTableQuery : IMultiTable

### 3.2 类型
>* MultiTableQuery
>* JoinTableQuery
>* JoinOnQuery
>
>* 注: 其中JoinOnQuery为JoinTableQuery的组件,用于Join的俩俩关联

## 三、列(字段)
### 1、前缀字段
### 1.1 接口
>IPrefixField
~~~csharp
interface IPrefixField : IField;
~~~
>
>* 注:用于多表(联表、子查询),通过前缀增加表限定符区分同名列

### 1.2 类型
>* PrefixColumn
~~~csharp
class PrefixColumn(IColumn column, params string[] prefix)
    : IPrefixField
~~~

### 2、列(一般指原始列)
### 2.1 接口
>IColumn
~~~csharp
interface IColumn : IFieldView, IAssignView, ICompareField, IOrderField;
~~~

### 2.2 类型
>* Column
>* ColumnSchema
>* 也包含IPrefixField的实现类型

### 2.3 用途
>* 作为ITable成员
>* select(IFieldView)
>* update(IAssignView)
>* query/where(ICompareField)
>* orderby(IOrderField)

### 3、聚合字段
### 3.1 接口
>IAggregateField

### 3.2 类型
>* AggregateColumn
>* AggregateFieldInfo

### 4、聚合别名字段
### 4.1 接口
>IAggregateFieldAlias

### 4.2 类型
>* AggregateColumnAlias
>* AggregateAliasFieldInfo

### 5、别名字段
### 5.1 接口
>IAliasField

### 5.2 类型
>* AliasColumn
>* AliasFieldInfo
>* 及IAggregateFieldAlias的实现类

### 6、展示字段
### 6.1 接口
>IField

~~~csharp
interface IField : IFieldView, ICompareField, IAssignField;
~~~

### 6.2 类型
>* FieldInfo
>* TableFieldInfo
>* AliasTableFieldInfo
>* 及IColumn的实现类型

### 7、逻辑比较字段
### 6.1 接口
>ICompareField

### 6.2 类型
>* IColumn和IField的实现类型
