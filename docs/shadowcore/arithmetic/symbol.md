# 算术运算符
>* 算术运算符用于计算
>* 用于连接值两个[ICompareView](xref:ShadowSql.Identifiers.ICompareView)

## 1. ArithmeticSymbol
>* 参看[ArithmeticSymbol](xref:ShadowSql.Arithmetic.ArithmeticSymbol)
~~~csharp
class ArithmeticSymbol : ISqlEntity{
/// <summary>
/// 操作符
/// </summary>
char Operation { get; }
}
~~~

## 2. 运算符
>* =是默认也是最常用的运算符

|名称|SQL|作用|
|:--|:--|:--|:--|:--|
|Add|+|加|
|Sub|-|减|
|Mul|*|乘|
|Div|/|除|
|Mod|%|取模|
|And|&|位与|
|Or|\||位或|
|Xor|^|异或|

## 3. 其他相关功能
>* 参看[算术简介](./index.md)
>* 参看[算术操作](./operation.md)
