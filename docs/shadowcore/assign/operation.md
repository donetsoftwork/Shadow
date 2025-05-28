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
AssignOperation Assign(this IAssignField column, string parameter = "");
AssignOperation AddAssign(this IAssignField column, string parameter = "");
AssignOperation SubAssign(this IAssignField column, string parameter = "");
AssignOperation MulAssign(this IAssignField column, string parameter = "");
AssignOperation DivAssign(this IAssignField column, string parameter = "");
AssignOperation ModAssign(this IAssignField column, string parameter = "");
AssignOperation AndAssign(this IAssignField column, string parameter = "");
AssignOperation OrAssign(this IAssignField column, string parameter = "");
AssignOperation XorAssign(this IAssignField column, string parameter = "");
~~~

## 4. 赋值
~~~csharp
AssignOperation AssignValue<TValue>(this IAssignView column, TValue value);
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
AssignOperation Assign(this IAssignView column, ISqlValue right);
AssignOperation AddAssign(this IAssignView column, ISqlValue right);
AssignOperation SubAssign(this IAssignView column, ISqlValue right);
AssignOperation MulAssign(this IAssignView column, ISqlValue right);
AssignOperation DivAssign(this IAssignView column, ISqlValue right);
AssignOperation ModAssign(this IAssignView column, ISqlValue right);
AssignOperation AndAssign(this IAssignView column, ISqlValue right);
AssignOperation OrAssign(this IAssignView column, ISqlValue right);
AssignOperation XorAssign(this IAssignView column, ISqlValue right);
~~~

## 6. 其他相关功能
>* 参看[赋值简介](../assign/index.md)
>* 参看[赋值运算符](./symbol.md)
>* 参看[赋值原生sql](../assign/raw.md)