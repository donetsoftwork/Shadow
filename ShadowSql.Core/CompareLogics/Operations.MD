# 数据库操作

## 一、比较逻辑操作
>* ICompareLogic
>* CompareOperate

```csharp
/// <summary>
/// 比较逻辑
/// </summary>
public interface ICompareLogic : ILogic
{
    /// <summary>
    /// 左边字段
    /// </summary>
    ICompareField Field { get; }
    /// <summary>
    /// 运算符
    /// </summary>
    Compare Operation { get; }
    ///// <summary>
    ///// 右边值(也可以是字段)
    ///// </summary>
    //ISqlValue Right { get; }
    ///// <summary>
    ///// 第二个值(也可以是字段)
    ///// </summary>
    //ISqlValue End { get; }
}
```


### 1、Field
>ICompareField

#### 1.1 可使用类型
>* Column(IColumn)
>* 

### 2、Operation
>Compare

### 3、Right
>* ISqlValue
>* 特殊情况(IsNull和NotNull操作)可以缺失

#### 3.1 可使用类型

### 4、End
>* BetweenOperate
>* ISqlValue
>* 只有特定特殊情况(Between和NotBetween操作)才有

## 二、赋值操作
>* IAssignStatement

```csharp
/// <summary>
/// 赋值操作
/// </summary>
public interface IAssignStatement : ISqlEntity
{
    /// <summary>
    /// 左边列
    /// </summary>
    IColumn Field { get; }
    /// <summary>
    /// 右边值(也可以是字段)
    /// </summary>
    ISqlValue Right { get; }
}
```

### 1、Field
>* IColumn

### 2、Right
>* ISqlValue

### 3、隐藏操作符
>* 使用Compare.Equal

