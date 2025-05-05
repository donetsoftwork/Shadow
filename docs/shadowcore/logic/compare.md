# 比较逻辑
>* 比较逻辑由字段和比较运算符组成
>* 依赖[比较运算符](./symbol.md)
>* 依赖[ICompareView](xref:ShadowSql.Identifiers.ICompareView)

## 1. 接口
>* [ISqlLogic](xref:ShadowSql.Logics.ISqlLogic)

## 2. 基类
>* [AtomicLogic](xref:ShadowSql.Logics.AtomicLogic)
>* [CompareLogicBase](xref:ShadowSql.CompareLogics.CompareLogicBase)

## 3. CompareLogic
>* 普通比较逻辑
>* 参看[CompareLogic](xref:ShadowSql.CompareLogics.CompareLogic)

### 3.1 CompareLogic类
>* 由字段、比较运算符及数据库值构成
>* 数据库值也可以是字段
>* 参看[ICompareView](xref:ShadowSql.Identifiers.ICompareView)
~~~csharp
class CompareLogic(ICompareView field, CompareSymbol op, ISqlValue value);
~~~

### 3.2 字段参数化比较
~~~csharp
AtomicLogic Compare(this ICompareField column, string op, string parameter = "");
AtomicLogic Equal(this ICompareField column, string parameter = "")；
AtomicLogic NotEqual(this ICompareField column, string parameter = "");
AtomicLogic Greater(this ICompareField column, string parameter = "");
AtomicLogic Less(this ICompareField column, string parameter = "");
AtomicLogic GreaterEqual(this ICompareField column, string parameter = "");
AtomicLogic LessEqual(this ICompareField column, string parameter = "");
AtomicLogic Like(this ICompareField column, string parameter = "");
AtomicLogic NotLike(this ICompareField column, string parameter = "");
AtomicLogic In(this ICompareField column, string parameter = "");
AtomicLogic NotIn(this ICompareField column, string parameter = "");
AtomicLogic Between(this ICompareField column, string begin = "", string end = "");
AtomicLogic NotBetween(this ICompareField column, string begin = "", string end = "");
~~~
>注:以上返回类型为AtomicLogic,也是CompareLogic的实例

### 3.3 字段值比较
~~~csharp
AtomicLogic CompareValue<TValue>(this ICompareView column, string op, TValue value);
AtomicLogic EqualValue<TValue>(this ICompareView column, TValue value);
AtomicLogic NotEqualValue<TValue>(this ICompareView column, TValue value);
AtomicLogic GreaterValue<TValue>(this ICompareView column, TValue value);
AtomicLogic LessValue<TValue>(this ICompareView column, TValue value)；
AtomicLogic GreaterEqualValue<TValue>(this ICompareView column, TValue value);
AtomicLogic LessEqualValue<TValue>(this ICompareView column, TValue value);
AtomicLogic LikeValue(this ICompareView column, string value);
AtomicLogic NotLikeValue(this ICompareView column, string value);
AtomicLogic InValue<TValue>(this ICompareView column, params TValue[] values);
AtomicLogic NotInValue<TValue>(this ICompareView column, params TValue[] values);
AtomicLogic BetweenValue<TValue>(this ICompareView column, TValue begin, TValue end);
AtomicLogic NotBetweenValue<TValue>(this ICompareView column, TValue begin, TValue end);
~~~
>注:以上返回类型为AtomicLogic,也是CompareLogic的实例

### 3.4 字段数据库值比较
>* 常用于与字段进行比较
>* 参看[ISqlValue](xheef:ShadowSql.SqlVales.ISqlValue)
~~~csharp
AtomicLogic Equal(this ICompareView column, ISqlValue right);
AtomicLogic NotEqual(this ICompareView column, ISqlValue right);
AtomicLogic Greater(this ICompareView column, ISqlValue right);
AtomicLogic Less(this ICompareView column, ISqlValue right);
AtomicLogic GreaterEqual(this ICompareView column, ISqlValue right);
AtomicLogic LessEqual(this ICompareView column, ISqlValue right);
AtomicLogic BetweenValue(this ICompareView column, ISqlValue begin, ISqlValue end);
AtomicLogic NotBetweenValue(this ICompareView column, ISqlValue begin, ISqlValue end);
~~~
>注:以上返回类型为AtomicLogic,也是CompareLogic的实例

### 3.5 聚合参数化比较
~~~csharp
AtomicLogic Compare(this IAggregateField column, string op, string parameter = "");
AtomicLogic Equal(this IAggregateField column, string parameter = "")；
AtomicLogic NotEqual(this IAggregateField column, string parameter = "");
AtomicLogic Greater(this IAggregateField column, string parameter = "");
AtomicLogic Less(this IAggregateField column, string parameter = "");
AtomicLogic GreaterEqual(this IAggregateField column, string parameter = "");
AtomicLogic LessEqual(this IAggregateField column, string parameter = "");
AtomicLogic Like(this IAggregateField column, string parameter = "");
AtomicLogic NotLike(this IAggregateField column, string parameter = "");
AtomicLogic In(this IAggregateField column, string parameter = "");
AtomicLogic NotIn(this IAggregateField column, string parameter = "");
AtomicLogic Between(this IAggregateField column, string begin = "", string end = "");
AtomicLogic NotBetween(this IAggregateField column, string begin = "", string end = "");
~~~
>* 注:以上返回类型为AtomicLogic,也是CompareLogic的实例
>* 注:IAggregateField继承接口ICompareView,没必要单独定义与值和数据库值比较的方法

## 4. BetweenLogic
>* 三元运算,在...之间
>* 是CompareLogic的特殊情况之一
>* 参看[BetweenLogic](xref:ShadowSql.CompareLogics.BetweenLogic)

### 4.1 BetweenLogic类
>* 由字段、两个数据库值构成
>* 数据库值也可以是字段
~~~csharp
class BetweenLogic : CompareLogic;
~~~

### 4.2 BetweenLogic扩展方法
~~~csharp
AtomicLogic Between(this ICompareField column, string begin = "", string end = "");
AtomicLogic Between(this IAggregateField column, string begin = "", string end = "");
AtomicLogic BetweenValue<TValue>(this ICompareView column, TValue begin, TValue end);
AtomicLogic BetweenValue(this ICompareView column, ISqlValue begin, ISqlValue end);
~~~
>注:以上返回类型为AtomicLogic,也是BetweenLogic的实例

## 5. NotBetweenLogic
>* 三元运算,不在...之间
>* 是CompareLogic的特殊情况之一
>* 参看[NotBetweenLogic](xref:ShadowSql.CompareLogics.NotBetweenLogic)

### 5.1 NotBetweenLogic类
>* 由字段、两个数据库值构成
>* 数据库值也可以是字段
~~~csharp
class NotBetweenLogic : CompareLogic;
~~~

### 5.2 NotBetweenLogic扩展方法
~~~csharp
AtomicLogic NotBetween(this ICompareField column, string begin = "", string end = "");
AtomicLogic NotBetween(this IAggregateField column, string begin = "", string end = "");
AtomicLogic NotBetweenValue<TValue>(this ICompareView column, TValue begin, TValue end);
AtomicLogic NotBetweenValue(this ICompareView column, ISqlValue begin, ISqlValue end);
~~~
>注:以上返回类型为AtomicLogic,也是NotBetweenLogic的实例

## 6. IsNullLogic
>* 一元运算,是否为NULL
>* 参看[IsNullLogic](xref:ShadowSql.CompareLogics.IsNullLogic)

### 6.1 IsNullLogic类
>* 只含字段
~~~csharp
class IsNullLogic(ICompareView field)
    : CompareLogicBase;
~~~

### 6.2 IsNullLogic扩展方法
~~~csharp
AtomicLogic IsNull(this ICompareView column);
~~~
>注:以上返回类型为AtomicLogic,也是IsNullLogic的实例

## 7. NotNullLogic
>* 一元运算,是否为NULL
>* 参看[IsNullLogic](xref:ShadowSql.CompareLogics.IsNullLogic)

### 7.1 NotNullLogic类
>* 只含字段
~~~csharp
NotNullLogic(ICompareView field)
    : CompareLogicBase;
~~~

### 7.2 NotNullLogic扩展方法
~~~csharp
AtomicLogic NotNull(this ICompareView column);
~~~
>注:以上返回类型为AtomicLogic,也是NotNullLogic的实例

## 8. 其他相关功能
>* 参看[逻辑简介](./index.md)
>* 参看[比较运算符](./symbol.md)
