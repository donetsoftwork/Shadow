# ShadowSql
>* .net拼接sql工具
>* 支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等,可扩展其他数据库方言的支持
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成
>* 在ShadowSql.Core项目基础上,进行泛型扩展和增强
>* Nuget包名: ShadowSql
>* 易用版

>注:本项目文档未特殊说明的示例sql,默认使用MsSql,并非只支持MsSql

## 1. 以下是各功能的序列总图
>* 示意组件和功能之间的调用关系
>* 除了Cursor,其他都是标准sql的概念,明显易懂
>* Cursor是ORDER BY和分页参数的封装
~~~mermaid
sequenceDiagram
	participant Table
	participant TableAlias
	participant TableQuery
	participant JoinQuery
	participant GroupByQuery
	participant Cursor
	participant Select
	participant Insert
	participant Update
	participant Delete

	Table->>TableAlias: As()
	Table->>TableQuery: ToQuery()

	par Join
		Table->>JoinQuery: Join()
		TableAlias->>JoinQuery: Join()
	end	
	par GroupBy
		Table->>GroupByQuery: GroupBy()
		TableQuery->>GroupByQuery: GroupBy()
		JoinQuery->>GroupByQuery: GroupBy()
	end
	par ToCursor
		Table->>Cursor: ToCursor()
		TableQuery->>Cursor: ToCursor()
		JoinQuery->>Cursor: ToCursor()
		JoinQuery->>Cursor: ToCursor()
		GroupByQuery->>Cursor: ToCursor()
	end
	par ToSelect
		Table->>Select: ToSelect()
		TableQuery->>Select: ToSelect()
		JoinQuery->>Select: ToSelect()
		GroupByQuery->>Select: ToSelect()
		Cursor->>Select: ToSelect()
	end
	par ToInsert
		Table->>Insert: ToInsert()
		Select->>Insert: InsertTo()
	end
	par ToUpdate
		Table->>Update: ToUpdate()
		TableQuery->>Update: ToUpdate()
		JoinQuery->>Update: ToUpdate()
	end
	par ToDelete
		Table->>Delete: ToDelete()
		TableQuery->>Delete: ToDelete()
		JoinQuery->>Delete: ToDelete()
	end
~~~
### 1.1 组件说明
#### 1.1.1 Table
>* 原始表对象
>* 接口[ITable](xref:ShadowSql.Identifiers.ITable)
>* 类型为[Table](xref:ShadowSql.Identifiers.Table)、[EmptyTable](xref:ShadowSql.Tables.EmptyTable)、[TableSchema](xref:Shadow.DDL.Schemas.TableSchema)或自定义等

#### 1.1.2 TableAlias 
>* 表别名对象
>* 接口[IAliasTable](xref:ShadowSql.Identifiers.IAliasTable)
>* 接口[IAliasTable\<TTable\>](xref:ShadowSql.Identifiers.IAliasTable%601)
>* 类型为[TableAlias\<TTable\>](xref:ShadowSql.Variants.TableAlias%601)或自定义等

#### 1.1.3 TableQuery
>* 表查询对象
>* 接口[IDataQuery](xref:ShadowSql.Queries.IDataQuery)
>* 类型为[TableSqlQuery\<TTable\>](xref:ShadowSql.Tables.TableSqlQuery%601)或[TableQuery\<TTable\>](xref:ShadowSql.Tables.TableQuery%601)等

#### 1.1.4 JoinQuery
>* 多、联表查询对象
>* 接口[IMultiView](xref:ShadowSql.Identifiers.IMultiView)
>* 类型为[JoinTableSqlQuery](xref:ShadowSql.Join.JoinTableSqlQuery)、[JoinTableQuery](xref:ShadowSql.Join.JoinTableQuery)、[MultiTableSqlQuery](xref:ShadowSql.Join.MultiTableSqlQuery)或[MultiTableQuery](xref:ShadowSql.Join.MultiTableQuery)等

#### 1.1.5 GroupByQuery
>* 分组查询对象
>* 接口[IGroupByView](xref:ShadowSql.Identifiers.IGroupByView)
>* 类型为[GroupByTableSqlQuery](xref:ShadowSql.GroupBy.GroupByTableSqlQuery%601)、[GroupByTableQuery](xref:ShadowSql.GroupBy.GroupByTableQuery%601)、[GroupByMultiSqlQuery](xref:ShadowSql.GroupBy.GroupByMultiSqlQuery)或[GroupByMultiQuery](xref:ShadowSql.GroupBy.GroupByMultiQuery)等

#### 1.1.6 Cursor
>* 游标对象,封装ORDER BY和分页参数
>* 接口[ICursor](xref:ShadowSql.Cursors.ICursor)
>* 类型为[TableCursor\<TTable\>](xref:ShadowSql.Cursors.TableCursor%601)、[MultiTableCursor](xref:ShadowSql.Cursors.MultiTableCursor)、[GroupByTableCursor\<TTable\>](xref:ShadowSql.Cursors.GroupByTableCursor%601)或[GroupByMultiCursor](xref:ShadowSql.Cursors.GroupByMultiCursor)等

### 1.2 功能说明
#### 1.2.1 Select
>* 筛选获取对象
>* 接口[ISelect](xref:ShadowSql.Select.ISelect)
>* 类型为[TableSelect\<TTable\>](xref:ShadowSql.Select.TableSelect%601)、[MultiTableSelect](xref:ShadowSql.Select.MultiTableSelect)、[GroupByTableSelect\<TTable\>](xref:ShadowSql.Select.GroupByTableSelect%601)、[GroupByMultiSelect](xref:ShadowSql.Select.GroupByMultiSelect)、[TableCursorSelect\<TTable\>](xref:ShadowSql.CursorSelect.TableCursorSelect%601)、[MultiTableCursorSelect](xref:ShadowSql.CursorSelect.MultiTableCursorSelect)、[GroupByTableCursorSelect\<TTable\>](xref:ShadowSql.CursorSelect.GroupByTableCursorSelect%601)或[GroupByMultiCursorSelect](xref:ShadowSql.CursorSelect.GroupByMultiCursorSelect)等

#### 1.2.2 Insert
>* 插入对象
>* 接口[IInsert](xref:ShadowSql.Insert.IInsert)
>* 类型为[SingleInsert\<TTable\>](xref:ShadowSql.Insert.SingleInsert%601)、[MultiInsert\<TTable\>](xref:ShadowSql.Insert.MultiInsert%601)或[SelectInsert\<TTable\>](xref:ShadowSql.Insert.SelectInsert%601)等

#### 1.2.3 Update
>* 更新对象
>* 接口[IUpdate](xref:ShadowSql.Update.IUpdate)
>* 类型为[TableUpdate\<TTable\>](xref:ShadowSql.Update.TableUpdate%601)或[MultiTableUpdate](xref:ShadowSql.Update.MultiTableUpdate)等

#### 1.2.4 Delete
>* 删除对象
>* 接口[IDelete](xref:ShadowSql.Delete.IDelete)
>* 类型为[TableDelete](xref:ShadowSql.Delete.TableDelete)或[MultiTableDelete](xref:ShadowSql.Delete.MultiTableDelete)等

### 1.3 方法说明
#### 1.3.1 As
>* 转化为别名对象

#### 1.3.2 ToQuery
>* 转化为查询对象
>* 含ToSqlQuery、ToQuery、ToSqlOrQuery或ToOrQuery等

#### 1.3.3 Join
>* 多、联表查询
>* 含SqlJoin、Join、SqlMulti或SqlMulti等

#### 1.3.4 GroupBy
>* 分组查询
>* 含SqlGroupBy或GroupBy等

#### 1.3.5 ToCursor
>* 转化为游标对象

#### 1.3.6 ToSelect
>* 转化为筛选获取对象

#### 1.3.7 ToInsert
>* 转化为插入对象
>* 含ToInsert、ToMultiInsert或InsertTo等

#### 1.3.8 ToUpdate
>* 转化为更新对象

#### 1.3.9 ToDelete
>* 转化为删除对象

## 2. 单表功能图
>* 示意单表的功能调用关系

~~~mermaid
sequenceDiagram
	participant Table
	participant TableQuery
	participant GroupByQuery
	participant Cursor
	participant Select
	participant Insert
	participant Update
	participant Delete

	Table->>TableQuery: ToQuery()
	par GroupBy
		Table->>GroupByQuery: GroupBy()
		TableQuery->>GroupByQuery: GroupBy()
	end	
	par ToCursor
		Table->>Cursor: ToCursor()
		TableQuery->>Cursor: ToCursor()
		GroupByQuery->>Cursor: ToCursor()
	end
	par ToSelect
		Table->>Select: ToSelect()
		TableQuery->>Select: ToSelect()
		GroupByQuery->>Select: ToSelect()
		Cursor->>Select: ToSelect()
	end
	par ToInsert
		Table->>Insert: ToInsert()
		Select->>Insert: InsertTo()
	end
	par ToUpdate
		Table->>Update: ToUpdate()
		TableQuery->>Update: ToUpdate()
	end
	par ToDelete
		Table->>Delete: ToDelete()
		TableQuery->>Delete: ToDelete()
	end
~~~
