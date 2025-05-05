# OR逻辑
>* 用AND拼接逻辑

## 1. 接口
>* [IOrLogic](xref:ShadowSql.Logics.IOrLogic)
>* [ISqlLogic](xref:ShadowSql.Logics.ISqlLogic)

## 2. 基类
>* [AtomicLogic](xref:ShadowSql.Logics.AtomicLogic)
>* [Logic](xref:ShadowSql.Logics.Logic)
>* [CompareLogicBase](xref:ShadowSql.CompareLogics.CompareLogicBase)

## 3. OrLogic
>* 只包含原子逻辑的OR逻辑
>* 参看[OrLogic](xref:ShadowSql.Logics.OrLogic)

### 3.1 OrLogic类
~~~csharp
class OrLogic : Logic, IOrLogic, ISqlLogic;
~~~

### 3.2 逻辑运算符重载
>* &是与逻辑
>* |是或逻辑
>* !是非逻辑
~~~csharp
Logic operator &(OrLogic logic, AtomicLogic other);
Logic operator &(OrLogic logic, AndLogic other);
IAndLogic operator &(OrLogic logic, ComplexAndLogic other);
Logic operator &(OrLogic logic, OrLogic other);
Logic operator &(OrLogic logic, ComplexOrLogic other);
Logic operator &(OrLogic logic, Logic other);
OrLogic operator |(OrLogic logic, AtomicLogic other);
OrLogic operator |(OrLogic logic, OrLogic other);
ComplexOrLogic operator |(OrLogic logic, ComplexOrLogic other);
Logic operator |(OrLogic logic, AndLogic other);
Logic operator |(OrLogic logic, ComplexAndLogic other);
Logic operator |(OrLogic logic, Logic other);
AndLogic operator !(OrLogic logic);
~~~

## 4. ComplexOrLogic
>* 可以包含原子逻辑
>* 也可以嵌套AND逻辑
>* 参看[ComplexOrLogic](xref:ShadowSql.Logics.ComplexOrLogic)

### 4.1 ComplexOrLogic类
~~~csharp
class ComplexOrLogic : ComplexLogicBase, IOrLogic, ISqlLogic;
~~~

### 4.2 逻辑运算符重载
>* &是与逻辑
>* |是或逻辑
>* !是非逻辑
~~~csharp
ComplexOrLogic operator |(ComplexOrLogic logic, AtomicLogic other);
ComplexOrLogic operator |(ComplexOrLogic logic, AndLogic other);
ComplexOrLogic operator |(ComplexOrLogic logic, ComplexAndLogic other);
ComplexOrLogic operator |(ComplexOrLogic logic, OrLogic other);
ComplexOrLogic operator |(ComplexOrLogic logic, ComplexOrLogic other);
Logic operator &(ComplexOrLogic logic, Logic other);
Logic operator &(ComplexOrLogic logic, AtomicLogic other);
Logic operator &(ComplexOrLogic logic, AndLogic other);
ComplexAndLogic operator &(ComplexOrLogic logic, ComplexAndLogic other);
ComplexAndLogic operator &(ComplexOrLogic logic, OrLogic other);
ComplexAndLogic operator &(ComplexOrLogic logic, ComplexOrLogic other);
Logic operator |(ComplexOrLogic logic, Logic other);
ComplexAndLogic operator !(ComplexOrLogic logic);
~~~

## 5. 其他相关功能
>* 参看[逻辑简介](./index.md)
>* 参看[逻辑基类](./logic.md)
>* 参看[原子逻辑](./atomic.md)
>* 参看[AND逻辑](./and.md)