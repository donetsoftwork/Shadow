# 赋值运算符
>* 赋值运算符用于实现赋值操作逻辑
>* 用于连接值和被赋予的字段

## 1. AssignSymbol
>* 参看[AssignSymbol](xref:ShadowSql.Assigns.AssignSymbol)
~~~csharp
class AssignSymbol : ISqlEntity{
/// <summary>
/// 操作符
/// </summary>
string Operation { get; }
}
~~~

## 2. 运算符
>* =是默认也是最常用的运算符

|名称|SQL|作用|
|:--|:--|:--|:--|:--|
|Assign|=|等于|
|AddAssign|+=|加上并赋值|
|SubAssign|-=|减去并赋值|
|MulAssign|*=|乘上并赋值|
|DivAssign|/=|除去并赋值|
|ModAssign|%=|取模并赋值|
|AndAssign|&=|“位与”并赋值|
|OrAssign|\|=|“位或”并赋值|
|XorAssign|^=|“异或”并赋值|

## 3. 其他相关功能
>* 参看[赋值简介](./index.md)
>* 参看[赋值操作](./operation.md)
