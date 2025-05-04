---
_layout: landing
---

# ShadowSql
>* 一个.net sql拼写工具
>* 通过扩展Dapper实现ORM
>* 对标开源项目SqlKata

## 一、五个子项目
### 1、[ShadowSql.Core](./shadowcore/index.md)
>* .net拼接sql工具
>* 支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成
>* Nuget包名: ShadowSql.Core
>* 精简版

### 2、[ShadowSql](./shadow/index.md)
>* .net拼接sql工具
>* 支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成
>* 易用版

### 3、[Dapper.Shadow.Core](./dappercore/index.md)
>* ShadowSql.Core的Dapper扩展
>* 用于执行ShadowSql.Core拼接的sql
>* Nuget包名: ShadowSql.Dapper.Core
>* 精简版

### 4、[Dapper.Shadow](./dapper/index.md)
>* ShadowSql的Dapper扩展
>* 用于执行ShadowSql拼接的sql
>* Nuget包名: ShadowSql.Dapper
>* 易用版

### 3、[Shadow.DDL](./ddl/index.md)
>* 用于拼写DDL的sql语句,主要是CreateTable
>* 搭配Dapper.Shadow可以实现执行DDL操作


## 二、对标开源项目SqlKata
### 1、拼接sql从头到尾只使用一个StringBuilder
#### 1.1 ShadowSql通过StringBuilder拼接
>* 拼接sql继承接口ISqlFragment和ISqlEntity
>* 对同一个StringBuilder对象进行操作

~~~csharp
public interface ISqlFragment {
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine">数据库</param>
    /// <param name="sql">sql</param>
    /// <returns></returns>
    bool TryWrite(ISqlEngine engine, StringBuilder sql);
}
public interface ISqlEntity {
    /// <summary>
    /// sql拼接
    /// </summary>
    /// <param name="engine">数据库</param>
    /// <param name="sql">sql</param>
    void Write(ISqlEngine engine, StringBuilder sql);
}
~~~

#### 1.2 SqlKata通过string拼接
>* SqlKatat通过Replace、Concat和Join等进行sql拼接
~~~csharp
        protected virtual string CompileConditions(SqlResult ctx, List<AbstractCondition> conditions) {
            var result = new List<string>();

            for (var i = 0; i < conditions.Count; i++) {
                var compiled = CompileCondition(ctx, conditions[i]);
                if (string.IsNullOrEmpty(compiled))
                    continue;
                var boolOperator = i == 0 ? "" : (conditions[i].IsOr ? "OR " : "AND ");
                result.Add(boolOperator + compiled);
            }
            return string.Join(" ", result);
        }
        public static string ReplaceIdentifierUnlessEscaped(this string input, string escapeCharacter, string identifier, string newIdentifier) {
            var nonEscapedRegex = new Regex($@"(?<!{Regex.Escape(escapeCharacter)}){Regex.Escape(identifier)}");
            var nonEscapedReplace = nonEscapedRegex.Replace(input, newIdentifier);
            var escapedRegex = new Regex($@"{Regex.Escape(escapeCharacter)}{Regex.Escape(identifier)}");
            return escapedRegex.Replace(nonEscapedReplace, identifier);
        }
~~~

### 2、执行更快、内存消耗更少
#### 2.1 简单查询对比
>* SqlKata耗时是SqlQuery的8倍多
>* SqlKata耗时是Query的10倍左右
>* SqlKata耗时是ByParametricLogic的14倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的25倍
>* 内存消耗也是差不多比例

| Method                     | Mean       | Error     | StdDev    | Median     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        |   561.5 ns |  10.31 ns |  16.64 ns |   560.8 ns |  0.13 |    0.01 |      - |    1680 B |        0.13 |
| ShadowSqlByQuery           |   445.1 ns |   6.88 ns |  12.04 ns |   443.1 ns |  0.10 |    0.01 |      - |    1488 B |        0.12 |
| ShadowSqlByParametricLogic |   330.2 ns |   6.25 ns |  11.10 ns |   328.6 ns |  0.07 |    0.01 |      - |    1112 B |        0.09 |
| ShadowSqlByLogic           |   190.1 ns |   3.54 ns |   5.92 ns |   188.1 ns |  0.04 |    0.00 |      - |     608 B |        0.05 |
| SqlKataBench               | 4,519.9 ns | 216.02 ns | 383.98 ns | 4,347.3 ns |  1.01 |    0.11 | 0.7000 |   12712 B |        1.00 |

>* 更多对比信息查阅测试项目ShadowSqlBench
>[ShadowSqlBench](https://github.com/donetsoftwork/Shadow/tree/master/Benchmarks/ShadowSqlBench)

## 三、本项目的特点
### 1、支持影子编程
>* 通过自定义表(及其列)作为实际数据表(或模型类)的影子
>* 自定义表(及其列)可以实现绝大多数sql的拼写
>* 通过查看引用可以定位到使用到该表或该字段的所有sql
>* 重构时查看字段引用可以准确评估影响范围
>* 删除字段相应sql拼写方法编译失败,按图索骥可以快速完成重构
>* 修改字段名相应sql可以同步重构完成
>* 也就是可实现编译检测的sql拼写

### 2、高性能
>* 拼写sql耗时更少
>* 拼写sql内存消耗更少
>* 拼写sql方便直接对接Dapper,使用Dapper高效执行sql

### 3、简便易用
>* 能[快速上手](./getting-started.md)
>* 大多数API和sql一致,代码可读性强,使用简单
>* 支持链式语法,大多数功能可以直接一行代码搞定
