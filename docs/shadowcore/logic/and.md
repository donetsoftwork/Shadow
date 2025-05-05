# AND逻辑
>* 用AND拼接逻辑

## 1. 接口
>* [IAndLogic](xref:ShadowSql.Logics.IAndLogic)
>* [ISqlLogic](xref:ShadowSql.Logics.ISqlLogic)

## 2. 基类
>* [AtomicLogic](xref:ShadowSql.Logics.AtomicLogic)
>* [Logic](xref:ShadowSql.Logics.Logic)
>* [CompareLogicBase](xref:ShadowSql.CompareLogics.CompareLogicBase)

## 3. AndLogic
>* 只包含原子逻辑的AND逻辑
>* 参看[AndLogic](xref:ShadowSql.Logics.AndLogic)

### 3.1 AndLogic类
~~~csharp
class AndLogic : Logic, IAndLogic, ISqlLogic;
~~~

### 3.2 逻辑运算符重载
>* &是与逻辑
>* |是或逻辑
>* !是非逻辑
~~~csharp
AndLogic operator &(AndLogic logic, AtomicLogic other);
AndLogic operator &(AndLogic logic, AndLogic other);
ComplexAndLogic operator &(AndLogic logic, ComplexAndLogic other);
Logic operator &(AndLogic logic, OrLogic other);
Logic operator &(AndLogic logic, ComplexOrLogic other);
Logic operator &(AndLogic logic, Logic other);
Logic operator |(AndLogic logic, AtomicLogic other);
Logic operator |(AndLogic logic, OrLogic other);
ComplexOrLogic operator |(AndLogic logic, ComplexOrLogic other);
Logic operator |(AndLogic logic, AndLogic other);
Logic operator |(AndLogic logic, ComplexAndLogic other);
Logic operator |(AndLogic logic, Logic other);
OrLogic operator !(AndLogic logic);
~~~

## 4. ComplexAndLogic
>* 可以包含原子逻辑
>* 也可以嵌套OR逻辑
>* 参看[ComplexAndLogic](xref:ShadowSql.Logics.ComplexAndLogic)

### 4.1 ComplexAndLogic类
~~~csharp
class ComplexAndLogic : ComplexLogicBase, IAndLogic, ISqlLogic;
~~~

### 4.2 逻辑运算符重载
>* &是与逻辑
>* |是或逻辑
>* !是非逻辑
~~~csharp
ComplexAndLogic operator &(ComplexAndLogic logic, AtomicLogic other);
ComplexAndLogic operator &(ComplexAndLogic logic, AndLogic other);
ComplexAndLogic operator &(ComplexAndLogic logic, ComplexAndLogic other);
ComplexAndLogic operator &(ComplexAndLogic logic, OrLogic other);
ComplexAndLogic operator &(ComplexAndLogic logic, ComplexOrLogic other);
Logic operator &(ComplexAndLogic logic, Logic other);
Logic operator |(ComplexAndLogic logic, AtomicLogic other);
Logic operator |(ComplexAndLogic logic, OrLogic other);
ComplexOrLogic operator |(ComplexAndLogic logic, ComplexOrLogic other);
ComplexOrLogic operator |(ComplexAndLogic logic, AndLogic other);
ComplexOrLogic operator |(ComplexAndLogic logic, ComplexAndLogic other);
Logic operator |(ComplexAndLogic logic, Logic other);
ComplexOrLogic operator !(ComplexAndLogic logic);
~~~

## 5. 其他相关功能
>* 参看[逻辑简介](./index.md)
>* 参看[逻辑基类](./logic.md)
>* 参看[原子逻辑](./atomic.md)
>* 参看[OR逻辑](./or.md)