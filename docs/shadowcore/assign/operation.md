# 赋值逻辑
>* 由字段、赋值运算符和值组成
>* 值也可以是另一个字段

## 1. 接口
>* [IAssignInfo](xref:ShadowSql.Assigns.IAssignInfo)
>* [IAssignOperation](xref:ShadowSql.Assigns.IAssignOperation)

## 2. 类
>* [AssignOperation](xref:ShadowSql.Assigns.AssignOperation)
~~~csharp
class AssignOperation(IAssignView column, AssignSymbol assign, ISqlValue value)
    : IAssignOperation;
~~~

## 3. 赋参数
~~~csharp
AssignOperation EqualTo(this IAssignField column, string parameter = "");
AssignOperation Add(this IAssignField column, string parameter = "");
AssignOperation Sub(this IAssignField column, string parameter = "");
AssignOperation Mul(this IAssignField column, string parameter = "");
AssignOperation Div(this IAssignField column, string parameter = "");
AssignOperation Mod(this IAssignField column, string parameter = "");
AssignOperation And(this IAssignField column, string parameter = "");
AssignOperation Or(this IAssignField column, string parameter = "");
AssignOperation Xor(this IAssignField column, string parameter = "");
~~~

## 4. 赋值
~~~csharp
AssignOperation EqualToValue<TValue>(this IAssignView column, TValue value);
AssignOperation AddValue<TValue>(this IAssignView column, TValue value);
AssignOperation SubValue<TValue>(this IAssignView column, TValue value);
AssignOperation MulValue<TValue>(this IAssignView column, TValue value);
AssignOperation DivValue<TValue>(this IAssignView column, TValue value);
AssignOperation ModValue<TValue>(this IAssignView column, TValue value);
AssignOperation AndValue<TValue>(this IAssignView column, TValue value);
AssignOperation OrValue<TValue>(this IAssignView column, TValue value);
AssignOperation XorValue<TValue>(this IAssignView column, TValue value);
~~~

## 5. 数据库值
~~~csharp
AssignOperation EqualTo(this IAssignView column, ISqlValue right);
AssignOperation Add(this IAssignView column, ISqlValue right);
AssignOperation Sub(this IAssignView column, ISqlValue right);
AssignOperation Mul(this IAssignView column, ISqlValue right);
AssignOperation Div(this IAssignView column, ISqlValue right);
AssignOperation Mod(this IAssignView column, ISqlValue right);
AssignOperation And(this IAssignView column, ISqlValue right);
AssignOperation Or(this IAssignView column, ISqlValue right);
AssignOperation Xor(this IAssignView column, ISqlValue right);
~~~

## 6. 其他相关功能
>* 参看[赋值简介](../assign/index.md)
>* 参看[赋值运算符](./symbol.md)
>* 参看[赋值原生sql](../assign/raw.md)