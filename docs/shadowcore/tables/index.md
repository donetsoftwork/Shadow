# 表
>* 作为数据表的影子(占位符)
>* 用来辅助拼接sql

## 1. 接口
|interface|功能|作用|
|:--|:--|:--|
|[ITable](xref:ShadowSql.Identifiers.ITable)|原始表|映射数据表、继承ITableView|
|[ITableView](xref:ShadowSql.Identifiers.ITableView)|查询视图|用于查询|
|[IInsertTable](xref:ShadowSql.Identifiers.IInsertTable)|可插入的表|用于插入|
|[IUpdateTable](xref:ShadowSql.Identifiers.IUpdateTable)|可更新的表|用于更新|
|[IAliasTable](xref:ShadowSql.Identifiers.IAliasTable)|别名表|主要用于联表或子查询|
|[IAliasTable\<TTable\>](xref:ShadowSql.Identifiers.IAliasTable%601)|泛型别名表|主要用于联表或子查询|

## 2. Table
>* 作为表的影子(占位符)
>* 支持select、update、insert和delete
>* 参看[Table](./table.md)


## 3. SimpleTable
>* 作为表的影子(占位符)
>* 支持select和delete等
>* 参看[SimpleTable](./simple.md)

## 4. 别名表
>* 作为别名表的影子(占位符)
>* 主要用来联表或子查询
>* 参看[别名表](./alias.md)

## 5. TableSchema
>* 作为表的影子(占位符)
>* 主要用来支持createtable
>* 与Table一样也支持select、update、insert和delete等
>* 参看[TableSchema](./schema.md)

