# 逻辑简介
>* 查询条件基于逻辑运算,而非简单字符串拼接
>* 逻辑运算又可以分为狭义逻辑和广义逻辑

## 一、狭义逻辑
>* 狭义逻辑由原子逻辑、逻辑
>* 这里的原子逻辑是指继承抽象类AtomicLogic的对象
>* 这里的逻辑是指继承抽象类Logic的对象
>* 逻辑分为AND逻辑和OR逻辑
>* 狭义逻辑用于实现[逻辑查询](../query/index.md)

### 1. 原子逻辑
>* 原子逻辑是继承抽象类AtomicLogic的对象
>* 原子逻辑作为AND和OR运算的基本组成元素
>* 参看[原子逻辑](./atomic.md)
~~~csharp
class AtomicLogic : ISqlLogic;
~~~

#### 1.1 比较逻辑
>* 比较逻辑是最常用的原子逻辑
>* 参看[比较逻辑](./compare.md)

#### 1.2 比较运算符
>* 比较逻辑由字段和比较运算符组成
>* 参看[比较运算符](./symbol.md)

### 2. 逻辑基类
>* 逻辑基类用来定义逻辑运算的规范
>* 用来实现[AND逻辑](./and.md)和[OR逻辑](./or.md)
>* 参看[逻辑基类](./logic.md)
~~~csharp
class Logic : ISqlLogic;
~~~

### 3. AND逻辑
>* AND逻辑可以由AND连接原子逻辑组成
>* AND复合逻辑可以包含嵌套的OR逻辑
>* 参看[AND逻辑](./and.md)

### 4. OR逻辑
>* OR逻辑可以由OR连接原子逻辑组成
>* OR复合逻辑可以包含嵌套的AND逻辑
>* 参看[OR逻辑](./or.md)

## 二、广义逻辑
>* 广义逻辑是继承接口ISqlLogic的对象
>* 狭义逻辑的对象都符合广义逻辑

### 1.原生sql条件
>* [SqlConditionLogic](xref:ShadowSql.Logics.SqlConditionLogic)用来支持原生sql条件
>* 参看[原生sql条件](./condition.md)

### 2. sql逻辑
>* sql逻辑是复合逻辑+SqlConditionLogic
>* 参看[sql逻辑](./sqlquery.md)

## 三. 其他相关功能
>* 参看[原子逻辑](./atomic.md)
>* 参看[AND逻辑](./and.md)
>* 参看[OR逻辑](./or.md)
>* 参看[原生sql条件](./condition.md)
>* 参看[sql逻辑](./sqlquery.md)
>* 参看[逻辑查询](../query/index.md)
>* 参看[sql查询](../sqlquery/index.md)