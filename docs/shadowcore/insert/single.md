# 插入单条
>* 本组件用来组装sql的INSERT语句

## 1. 接口
>* [ISingleInsert](xref:ShadowSql.Insert.ISingleInsert)

## 2. 基类
>* [SingleInsertBase](xref:ShadowSql.Insert.SingleInsertBase)

## 3. 类型
>* [SingleInsert](xref:Dapper.Shadow.Insert)

## 4. 方法
### 4.1 Insert方法
```csharp
TInsert Insert<TInsert>(this TInsert insert, IInsertValue value)
        where TInsert : SingleInsertBase, ISingleInsert;
```
```csharp
var table = new StudentTable();
var insert = new SingleInsert(table)
	.Insert(table.Name.Insert("StudentName"))
	.Insert(table.Score.InsertValue(90));
// INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,90)
```

### 4.2 InsertColumn方法
```csharp
TInsert InsertColumn<TInsert>(this TInsert insert, IColumn column)
        where TInsert : SingleInsertBase, ISingleInsert;
```
```csharp
var table = new StudentTable();
var insert = new SingleInsert(table)
    .InsertColumn(table.Name)
    .InsertColumn(table.Score);
// INSERT INTO [Students]([Name],[Score])VALUES(@Name,@Score)
```

### 4.3 InsertColumns方法
```csharp
TInsert InsertColumns<TInsert>(this TInsert insert, params IEnumerable<IColumn> columns)
        where TInsert : SingleInsertBase, ISingleInsert;
```
```csharp
var table = new StudentTable();
var insert = new SingleInsert(table)
    .InsertColumns(table.Name, table.Score);
// INSERT INTO [Students]([Name],[Score])VALUES(@Name,@Score)
```

### 4.4 InsertSelfColumns方法
```csharp
TInsert InsertSelfColumns<TInsert>(this TInsert insert)
        where TInsert : SingleInsertBase, ISingleInsert;
```
```csharp
var table = new StudentTable();
var insert = new SingleInsert(table)
    .InsertSelfColumns();
// INSERT INTO [Students]([Name],[Score])VALUES(@Name,@Score)
```