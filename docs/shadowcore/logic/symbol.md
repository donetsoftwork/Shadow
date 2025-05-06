# 比较运算符
>* 比较运算符用于实现基于字段的原子逻辑
>* 比较运算符分为单元、二元和三元
>* 比较运算符支持否定运算
>* 比较运算符支持前后交换运算

## 1. CompareSymbol
>* [比较运算符](xref:ShadowSql.Compares.CompareSymbol)
~~~csharp
class CompareSymbol : ISqlEntity{
/// <summary>
/// 操作符
/// </summary>
string Operation { get; }
/// <summary>
/// 否定运算
/// </summary>
CompareSymbol Not();
/// <summary>
/// 前后交换运算
/// </summary>
CompareSymbol Reverse();
}
~~~

## 2. 单元运算符
|名称|SQL|作用|否定运算|前后交换运算|
|:--|:--|:--|:--|:--|
|IsNull| IS NULL|是否为空|NotNull|IsNull|
|NotNull| IS NOT NULL|是否非空|IsNull|NotNull|

## 3. 二元运算符
### 3.1 判同逻辑
|名称|SQL|作用|否定运算|前后交换运算|
|:--|:--|:--|:--|:--|
|Equal|=|是否相同|NotEqual|Equal|
|NotEqual|<>|是否不同|Equal|NotEqual|

### 3.2 数值比较运算符
>* 只支持数值类型(整数和浮点数)

|名称|SQL|作用|否定运算|前后交换运算|
|:--|:--|:--|:--|:--|
|Greater|>|是否大于|LessEqual|LessEqual|
|LessEqual|<=|是否小于等于|Greater|Greater|
|Less|<|是否小于|GreaterEqual|GreaterEqual|
|GreaterEqual|>=|是否大于等于|Less|Less|

### 3.3 模式匹配运算符
>* 只支持字符类型

|名称|SQL|作用|否定运算|前后交换运算|
|:--|:--|:--|:--|:--|
|Like|LIKE|是否匹配|NotLike|Like|
|NotLike|NOT LIKE|是否不匹配|Like|NotLike|

### 3.3 集合运算符
|名称|SQL|作用|否定运算|
|:--|:--|:--|:--|
|In|IN|是否在...之内|NotIn|
|NotIn|NOT IN|是否不在...之内|In|

>集合逻辑不支持交换

## 4. 三元运算符
>* 只支持数值类型(整数和浮点数)

|名称|SQL|作用|否定运算|
|:--|:--|:--|:--|
|Between|BETWEEN|是否在...之间|NotBetween|
|NotBetween|NOT BETWEEN|是否不在...之间|Between|

>三元运算符不支持交换

## 5. 其他相关功能
>* 参看[逻辑简介](./index.md)
>* 参看[比较逻辑](./compare.md)
