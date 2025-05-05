# 原生sql条件
>* 支持原生sql条件

## 1. 接口
>* [ISqlLogic](xref:ShadowSql.Logics.ISqlLogic)

## 2. 基类
>* [AtomicLogic](xref:ShadowSql.Logics.AtomicLogic)

## 3. SqlConditionLogic
>* 用来支持原生sql条件
>* 包含一个原始sql列表
>* 包含一个sql分割符(AND或OR)
>* 实现了AtomicLogic,可以作为原子逻辑对象使用
>* 参看[SqlConditionLogic](xref:ShadowSql.Logics.SqlConditionLogic)
~~~csharp
class SqlConditionLogic(List<string> items, LogicSeparator separator) : AtomicLogic, ISqlLogic;
~~~

## 4. 其他相关功能
>* 参看[逻辑简介](./index.md)
>* 参看[原子逻辑](./atomic.md)
>* 参看[sql逻辑](./sqlquery.md)