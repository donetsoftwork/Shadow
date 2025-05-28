# 算术逻辑
>* 由字段与字段、常量及参数合并为新字段的操作

## 1. 接口
>* [ICompareView](xref:ShadowSql.Identifiers.ICompareView)

## 2. 类
>* [ArithmeticView](xref:ShadowSql.Arithmetic.ArithmeticView)
~~~csharp
class ArithmeticView(ICompareView left, ArithmeticSymbol op, ICompareView right)
    : ICompareView;
~~~

## 3. 算术运算
~~~csharp
ArithmeticView Add(this ICompareView left, ICompareView right);
ArithmeticView Sub(this ICompareView left, ICompareView right);
ArithmeticView Mul(this ICompareView left, ICompareView right);
ArithmeticView Div(this ICompareView left, ICompareView right);
ArithmeticView Mod(this ICompareView left, ICompareView right);
ArithmeticView And(this ICompareView left, ICompareView right);
ArithmeticView Or(this ICompareView left, ICompareView right);
ArithmeticView Xor(this ICompareView left, ICompareView right);
~~~

## 4. 其他相关功能
>* 参看[算术简介](./index.md)
>* 参看[ICompareView](xref:ShadowSql.Identifiers.ICompareView)
>* 参看[算术运算符](./symbol.md)