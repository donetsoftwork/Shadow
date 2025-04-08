# ShadowSql.Dapper
>* ShadowSql的Dapper扩展

## 一、StringBuilder拼接所有对象
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成

### 1. ISqlFragment
~~~csharp
public interface ISqlFragment
{
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <returns></returns>
    bool Write(ISqlEngine engine, StringBuilder sql);
}
~~~
>
>注: engine参数提供数据库方言差异的处理

### 2. ISqlEntity
~~~csharp
public interface ISqlEntity
{
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    void Write(ISqlEngine engine, StringBuilder sql);
}
~~~

## 二、支持不同数据库的差异
>* 支持主流数据库Mssql,mysql等
>* 通过实现接口ISqlEngine支持不同数据库方言

## 三、sql支持逻辑运算
### 1. 查询基于逻辑接口ILogic
~~~csharp
/// <summary>
/// 逻辑条件
/// </summary>
public interface ILogic : ISqlFragment
{
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <returns></returns>
    ILogic Not();
}
~~~

>注: 所有逻辑实现了反逻辑,以便执行逻辑非操作

### 2. 支持与或非逻辑运算
#### 2.1 逻辑与或操作符为LogicSeparator
>LogicSeparator只有两个实例对象(And和Or)

#### 2.2 与逻辑
>IAndLogic

~~~csharp
public interface IAndLogic : ISqlLogic
{
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="atomic"></param>
    /// <returns></returns>
    IAndLogic And(AtomicLogic atomic);
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    IAndLogic And(IAndLogic and);
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    IAndLogic And(IOrLogic or);
}
~~~

#### 2.3 或逻辑IOrLogic
>IOrLogic

~~~csharp
public interface IOrLogic : ISqlLogic
{
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="atomic"></param>
    /// <returns></returns>
    IOrLogic Or(AtomicLogic atomic);
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="or"></param>
    /// <returns></returns>
    IOrLogic Or(IOrLogic or);
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="and"></param>
    /// <returns></returns>
    IOrLogic Or(IAndLogic and);
}
~~~

#### 2.4 原子逻辑(AtomicLogic)
>构成And和或逻辑的最小单元逻辑

~~~csharp
/// <summary>
/// 原子逻辑基类
/// </summary>
public abstract class AtomicLogic : ISqlLogic
{
}
~~~

### 3. 比较逻辑运算符
~~~csharp
/// <summary>
/// 比较逻辑运算符
/// </summary>
class CompareSymbol {
    /// <summary>
    /// 否定运算
    /// </summary>
    public CompareSymbol Not();
    /// <summary>
    /// 前后交换运算
    /// </summary>
    /// <returns></returns>
    public CompareSymbol Reverse();
}
~~~

>* 运算符支持16种,分为8组
>* 每个运算符都有唯一的否定运算,支持非逻辑
>* 每个运算符都有唯一的交换运算,支持左右字段交换
>* 支持多种数据库运算符的别名
>* IsNull和NotNull特殊,使用是会忽略参数

### 4. 逻辑非
#### 4.1 支持负负得正
>两次逻辑非结果为原逻辑对象

#### 4.2 逻辑运算符支持逻辑非
>* 每个逻辑运算符都有唯一的反运算,支持逻辑非

## 四、sql查询
### 1. SqlQuery用于sql条件拼接
#### 1.1 SqlQuery支持与或非逻辑运算
>* SqlQuery支持sql和逻辑查询
>* 子类SqlAndQuery支持与逻辑运算
>* 子类SqlOrQuery支持或逻辑运算

#### 1.2 SqlAndQuery
>* And(ILogic)支持与逻辑运算
>* And(IEnumerable<string>)直接与sql查询条件字符串
>* ToOr()方法转化为SqlOrQuery用于支持或逻辑运算

#### 1.3 SqlOrQuery
>* Or(ILogic)支持或逻辑运算
>* Or(IEnumerable<string>)直接或sql查询条件字符串
>* ToAnd()方法转化为SqlAndQuery用于支持与逻辑运算

### 2. 支持字段参数化查询
#### 2.1 Column查询
~~~csharp
AtomicLogic Compare(ICompareField column, string op, string parameter = "");
~~~
##### 2.1.1 column参数
>* column参数用于适配表中字段
>* 拼接sql时按不同数据库进行格式化(eg:[column])
>* column支持表前缀(联表时)
>* 联表时多个字段符合返回第一个(拼接sql时增加前缀)

##### 2.1.2 op参数
>* op参数用于适配运算符
>* IsNull和NotNull会忽略参数

##### 2.1.2 parameter参数
>* parameter作为sql查询参数名
>* parameter为空按字段名生成

#### 2.2 Between和NotBetween
~~~csharp
AtomicLogic Between(ICompareField column, string begin = "", string end = "");
AtomicLogic NotBetween(ICompareField column, string begin = "", string end = "");
~~~

>* 注: column参数用于适配表中字段
>* 注: begin参数同Column查询的parameter参数
>* 注: end参数为空按begin+2

#### 2.3 12个简化方法

~~~csharp
AtomicLogic Equal(ICompareField column, string parameter = "");
AtomicLogic NotEqual(ICompareField column, string parameter = "");
AtomicLogic Greater(ICompareField column, string parameter = "");
AtomicLogic Less(ICompareField column, string parameter = "");
AtomicLogic GreaterEqual(ICompareField column, string parameter = "");
AtomicLogic LessEqual(ICompareField column, string parameter = "");
AtomicLogic In(ICompareField column, string parameter = "");
AtomicLogic NotIn(ICompareField column, string parameter = "");
AtomicLogic IsNull(ICompareField column);
AtomicLogic NotNull(ICompareField column);
AtomicLogic Like(ICompareField column, string parameter = "");
AtomicLogic NotLike(ICompareField column, string parameter = "");
~~~

>注: 其中In和NotIn生成的sql非daper慎用(可能不支持)

### 3. Query支持字段值化查询
#### 3.1 ColumnValue查询
~~~csharp
AtomicLogic ColumnValue<TValue>(ICompareView column, TValue value, string op = "=");
~~~
##### 3.1.1 column参数
>同Column查询的同名参数

##### 3.1.2 op参数
>同Column查询的同名参数

##### 3.1.2 value参数
>* 通过SqlValue.From包装为ISqlValue对象
>* 拼接sql可以通过ISqlEngine控制

#### 3.2 BetweenValue和NotBetweenValue
~~~csharp
AtomicLogic BetweenValue<TValue>(ICompareView column, TValue begin, TValue end);
AtomicLogic NotBetweenValue<TValue>(ICompareView column, TValue begin, TValue end);
~~~

>* 注: column参数同Column查询的同名参数
>* 注: begin参数同ColumnValue查询的value参数
>* 注: end参数同ColumnValue查询的value参数

#### 3.3 12个简化方法
~~~csharp
AtomicLogic EqualValue<TValue>(ICompareView column, TValue value);
AtomicLogic NotEqualValue<TValue>(ICompareView column, TValue value);
AtomicLogic GreaterValue<TValue>(ICompareView column, TValue value);
AtomicLogic LessValue<TValue>(ICompareView column, TValue value);
AtomicLogic GreaterEqualValue<TValue>(ICompareView column, TValue value);
AtomicLogic LessEqualValue<TValue>(ICompareView column, TValue value);
AtomicLogic InValue<TValue>(ICompareView column, params TValue[] values);
AtomicLogic NotInValue<TValue>(ICompareView column, params TValue[] values);
AtomicLogic IsNullValue<TValue>(ICompareView column);
AtomicLogic NotNullValue<TValue>(ICompareView column);
AtomicLogic LikeValue(ICompareView column, string value = "");
AtomicLogic NotLikeValue(ICompareView column, string value = "");
~~~
