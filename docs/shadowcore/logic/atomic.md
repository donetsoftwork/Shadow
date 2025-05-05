# 原子逻辑

## 1. 原子逻辑
>* 原子逻辑是继承抽象类AtomicLogic的对象
~~~csharp
class AtomicLogic : ISqlLogic;
~~~

## 2. 查询的基石
>* 原子逻辑作为AND和OR运算的基本组成元素
>* 原子逻辑可用于[逻辑查询](../query/index.md)
>* 原子逻辑也可以用于[sql查询](../sqlquery/index.md)

## 3. 比较逻辑
>* 比较逻辑是最常用的原子逻辑
>* 参看[比较逻辑](./compare.md)

## 4. SqlConditionLogic
>* SqlConditionLogic也是原子逻辑
>* SqlConditionLogic支持原生sql条件
>* [SqlConditionLogic](xref:ShadowSql.Logics.SqlConditionLogic)
>* 用于实现[sql逻辑](./sqlquery.md)

## 5. 支持逻辑运算
>* 支持逻辑运算符重载
>* &是与逻辑
>* |是或逻辑
>* !是非逻辑
~~~csharp
AndLogic operator &(AtomicLogic logic, AtomicLogic other);
AndLogic operator &(AtomicLogic logic, AndLogic other);
ComplexAndLogic operator &(AtomicLogic logic, ComplexAndLogic other);
Logic operator &(AtomicLogic logic, OrLogic other);
Logic operator &(AtomicLogic logic, ComplexOrLogic other);
OrLogic operator |(AtomicLogic logic, AtomicLogic other);
Logic operator |(AtomicLogic logic, AndLogic other);
Logic operator |(AtomicLogic logic, ComplexAndLogic other);
OrLogic operator |(AtomicLogic logic, OrLogic other);
ComplexOrLogic operator |(AtomicLogic logic, ComplexOrLogic other);
AtomicLogic operator !(AtomicLogic logic);
~~~

## 6. 其他相关功能
>* 参看[逻辑简介](./index.md)
>* 参看[AND逻辑](./and.md)
>* 参看[OR逻辑](./or.md)
>* 参看[sql逻辑](./sqlquery.md)