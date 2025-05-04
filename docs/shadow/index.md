# ShadowSql
>* .net拼接sql工具
>* 支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等
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
>* 接口[ITable](/api/ShadowSql.Identifiers.ITable.html)
>* 类型为[Table](/api/ShadowSql.Identifiers.Table.html)、[SimpleTable](/api/ShadowSql.Simples.SimpleTable.html)、[TableSchema](/api/Shadow.DDL.Schemas.TableSchema.html)或自定义等

#### 1.1.2 TableAlias 
>* 表别名对象
>* 接口[IAliasTable](/api/ShadowSql.Identifiers.IAliasTable.html)
>* 类型为[TableAlias\<TTable\>](/api/ShadowSql.Variants.TableAlias-1.html)或自定义等

#### 1.1.3 TableQuery
>* 表查询对象
>* 接口[IDataQuery](/api/ShadowSql.Queries.IDataQuery.html)
>* 类型为[TableSqlQuery\<TTable\>](/api/ShadowSql.Tables.TableSqlQuery-1.html)或[TableQuery\<TTable\>](/api/ShadowSql.Tables.TableQuery-1.html)等

#### 1.1.4 JoinQuery
>* 多、联表查询对象
>* 接口[IMultiView](/api/ShadowSql.Identifiers.IMultiView.html)
>* 类型为[JoinTableSqlQuery](/api/ShadowSql.Join.JoinTableSqlQuery.html)、[JoinTableQuery](/api/ShadowSql.Join.JoinTableQuery.html)、[MultiTableSqlQuery](/api/ShadowSql.Join.MultiTableSqlQuery.html)或[MultiTableQuery](/api/ShadowSql.Join.MultiTableQuery.html)等

#### 1.1.5 GroupByQuery
>* 分组查询对象
>* 接口[IGroupByView](/api/ShadowSql.Identifiers.IGroupByView.html)
>* 类型为[GroupByTableSqlQuery](/api/ShadowSql.GroupBy.GroupByTableSqlQuery-1.html)、[GroupByTableQuery](/api/ShadowSql.GroupBy.GroupByTableQuery-1.html)、[GroupByMultiSqlQuery](/api/ShadowSql.GroupBy.GroupByMultiSqlQuery.html)或[GroupByMultiQuery](/api/ShadowSql.GroupBy.GroupByMultiQuery.html)等

#### 1.1.6 Cursor
>* 游标对象,封装ORDER BY和分页参数
>* 接口[ICursor](/api/ShadowSql.Cursors.ICursor.html)
>* 类型为[TableCursor\<TTable\>](/api/ShadowSql.Cursors.TableCursor-1.html)、[MultiTableCursor](/api/ShadowSql.Cursors.MultiTableCursor.html)、[GroupByTableCursor\<TTable\>](/api/ShadowSql.Cursors.GroupByTableCursor-1.html)或[GroupByMultiCursor](/api/ShadowSql.Cursors.GroupByMultiCursor.html)等

### 1.2 功能说明
#### 1.2.1 Select
>* 筛选获取对象
>* 接口[ISelect](/api/ShadowSql.Select.ISelect.html)
>* 类型为[TableSelect\<TTable\>](/api/ShadowSql.Select.TableSelect-1.html)、[MultiTableSelect](/api/ShadowSql.Select.MultiTableSelect.html)、[GroupByTableSelect\<TTable\>](/api/ShadowSql.Select.GroupByTableSelect-1.html)、[GroupByMultiSelect](/api/ShadowSql.Select.GroupByMultiSelect.html)、[TableCursorSelect\<TTable\>](/api/ShadowSql.Select.TableCursorSelect-1.html)、[MultiTableCursorSelect](/api/ShadowSql.Select.MultiTableCursorSelect.html)、[GroupByTableCursorSelect\<TTable\>](/api/ShadowSql.Select.GroupByTableCursorSelect-1.html)或[GroupByMultiCursorSelect](/api/ShadowSql.Select.GroupByMultiCursorSelect.html)等

#### 1.2.2 Insert
>* 插入对象
>* 接口[IInsert](/api/ShadowSql.Insert.IInsert.html)
>* 类型为[SingleInsert\<TTable\>](/api/ShadowSql.Insert.SingleInsert-1.html)、[MultiInsert\<TTable\>](/api/ShadowSql.Insert.MultiInsert-1.html)或[SelectInsert\<TTable\>](/api/ShadowSql.Insert.SelectInsert-1.html)等

#### 1.2.3 Update
>* 更新对象
>* 接口[IUpdate](/api/ShadowSql.Update.IUpdate.html)
>* 类型为[TableUpdate\<TTable\>](/api/ShadowSql.Update.TableUpdate-1.html)或[MultiTableUpdate](/api/ShadowSql.Update.MultiTableUpdate.html)等

#### 1.2.4 Delete
>* 删除对象
>* 接口[IDelete](/api/ShadowSql.Delete.IDelete.html)
>* 类型为[TableDelete](/api/ShadowSql.Delete.TableDelete.html)或[MultiTableDelete](/api/ShadowSql.Delete.MultiTableDelete.html)等

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
