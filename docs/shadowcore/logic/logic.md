# 逻辑基类
>* 用来定义逻辑运算的规范
>* 用来实现[AND逻辑](./and.md)和[OR逻辑](./or.md)

## 1. 接口
>* [ISqlLogic](xref:ShadowSql.Logics.ISqlLogic)

## 2. 逻辑基类
~~~csharp
class Logic : ISqlLogic;
~~~

## 3. 与逻辑
~~~csharp
Logic And(AtomicLogic atomic);
Logic And(AndLogic and);
Logic And(ComplexAndLogic and);
Logic And(OrLogic or);
Logic And(ComplexOrLogic or);
~~~

## 4. 或逻辑
~~~csharp
Logic Or(AtomicLogic atomic);
Logic Or(OrLogic or);
Logic Or(ComplexOrLogic or);
Logic Or(AndLogic and);
Logic Or(ComplexAndLogic and);
~~~

## 5. 逻辑运算符重载
>* &是与逻辑
>* |是或逻辑
>* !是非逻辑
~~~csharp
Logic operator &(Logic logic, AtomicLogic other);
Logic operator &(Logic logic, AndLogic other);
Logic operator &(Logic logic, ComplexAndLogic other);
Logic operator &(Logic logic, OrLogic other);
Logic operator &(Logic logic, ComplexOrLogic other);
Logic operator &(Logic logic, Logic other);
Logic operator |(Logic logic, AtomicLogic other);
Logic operator |(Logic logic, OrLogic other);
Logic operator |(Logic logic, ComplexOrLogic other);
Logic operator |(Logic logic, AndLogic other);
Logic operator |(Logic logic, ComplexAndLogic other);
Logic operator |(Logic logic, Logic other);
Logic operator !(Logic logic);
~~~

## 6. 其他相关功能
>* 参看[逻辑简介](./index.md)
>* 参看[原子逻辑](./atomic.md)
>* 参看[AND逻辑](./and.md)
>* 参看[OR逻辑](./or.md)