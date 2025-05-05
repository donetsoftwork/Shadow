# 简介
>* Shadow这里是影子的意思
>* 通过表和字段的影子来编程,来拼接sql
>* sql是基于逻辑运算的,而非简单字符串拼接

## 1. 查询条件的逻辑
### 1.1 广义逻辑
>* 广义逻辑是所有继承ISqlLogic接口的对象
~~~csharp
/// <summary>
/// 逻辑条件
/// </summary>
public interface ISqlLogic : ISqlFragment {
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <returns></returns>
    ISqlLogic Not();
}
~~~

### 1.2 狭义逻辑
>* 狭义逻辑由原子逻辑、AND逻辑、OR逻辑构成
>* 狭义逻辑是广义逻辑的子集,继承ISqlLogic接口
#### 1.2.1 原子逻辑
>* AtomicLogic
~~~csharp
public abstract class AtomicLogic : ISqlLogic;
~~~

#### 1.2.2 AND逻辑
~~~csharp
class AndLogic : Logic, IAndLogic, ISqlLogic;
class ComplexAndLogic : ComplexLogicBase, IAndLogic, ISqlLogic;
~~~

#### 1.2.3 OR逻辑
~~~csharp
class OrLogic : Logic, IOrLogic, ISqlLogic;
class ComplexOrLogic : ComplexLogicBase, IOrLogic, ISqlLogic;
~~~

>* 注:其中ComplexAndLogic是复合逻辑用于嵌套OR查询
>* 注:其中ComplexOrLogic是复合逻辑用于嵌套AND查询

### 1.3 比较运算逻辑
#### 1.3.1 由字段和比较运算符构成
>* 比较逻辑逻辑基类
~~~csharp
class CompareLogicBase(ICompareView field, CompareSymbol op);
~~~

#### 1.3.2 普通比较逻辑
>* 由字段、比较运算符及数据库值构成
>* 数据库值也可以
~~~csharp
CompareLogic(ICompareView field, CompareSymbol op, ISqlValue value);
~~~


## 1. 影子编程
>* 通过自定义表(及其列)作为实际数据表(或模型类)的影子
>* 自定义表(及其列)可以实现绝大多数sql的拼写
>* 通过查看引用可以定位到使用到该表或该字段的所有sql
>* 重构时查看字段引用可以准确评估影响范围
>* 删除字段相应sql拼写方法编译失败,按图索骥可以快速完成重构
>* 修改字段名相应sql可以同步重构完成
>* 也就是可实现编译检测的sql拼写

## 2. 逻辑查询
>* 基于列进行AND和OR逻辑运算来查询
>* 所有的查询条件都由原子逻辑(AtomicLogic)构成
>* 通过AND、OR、NOT及其嵌套可以实现复杂逻辑

## 3. Sql查询
>* 再逻辑运算的基础上增加原生sql的支持
