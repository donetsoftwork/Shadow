---
_layout: landing
---

# ShadowSql
>* 一个.net sql拼写工具
>* 通过扩展Dapper实现ORM
>* 对标开源项目SqlKata

## 一、六个子项目
### 1、[精简版](./shadowcore/index.md)
>* .net拼接sql工具
>* 支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成
>* Nuget包名: ShadowSql.Core
>* 精简版

### 2、[易用版](./shadow/index.md)
>* .net拼接sql工具
>* 支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成
>* 支持自定义表和自定义别名表拼接sql
>* 易用版

### 3、[表达式版](./expression/index.md)
>* .net拼接sql工具
>* 支持多种数据库,包括MsSql,MySql,Oracle,Sqlite,Postgres等
>* 整个sql拼写只使用1个StringBuilder,减少字符串碎片生成
>* 支持表达式树拼接sql
>* 表达式版

### 4、[Dapper.Shadow.Core](./dappercore/index.md)
>* ShadowSql.Core的Dapper扩展
>* 用于执行ShadowSql.Core拼接的sql
>* Nuget包名: ShadowSql.Dapper.Core
>* 精简版

### 5、[Dapper.Shadow](./dapper/index.md)
>* ShadowSql的Dapper扩展
>* 用于执行ShadowSql拼接的sql
>* Nuget包名: ShadowSql.Dapper
>* 易用版

### 6、[DDL](./ddl/index.md)
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
>* 简单查询对比
>* SqlKata耗时是TableName的20倍多
>* SqlKata耗时是SqlQuery的30倍多
>* SqlKata耗时是Expression的9倍
>* Logic性能最好,SqlKata耗时是他的50倍
>* SqlKata内存消耗也远远大于以上4种

| Method                | Mean        | Error     | StdDev    | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |------------:|----------:|----------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |   194.10 ns |  2.178 ns |  2.508 ns |  0.05 | 0.0795 |      - |    1376 B |        0.11 |
| ShadowSqlBySqlQuery   |   125.34 ns |  1.132 ns |  1.211 ns |  0.03 | 0.0530 |      - |     920 B |        0.07 |
| ShadowSqlByExpression |   471.93 ns |  3.632 ns |  4.183 ns |  0.11 | 0.0915 |      - |    1584 B |        0.12 |
| ShadowSqlByLogic      |    73.90 ns |  0.472 ns |  0.544 ns |  0.02 | 0.0330 |      - |     576 B |        0.05 |
| SqlKataBench          | 4,163.90 ns | 18.189 ns | 20.946 ns |  1.00 | 0.7365 | 0.0040 |   12712 B |        1.00 |

>* 更多对比信息查阅测试项目ShadowSqlBench
>[ShadowSqlBench](https://github.com/donetsoftwork/Shadow/tree/master/Benchmarks/ShadowSqlBench)

## 三、本项目的特点
### 1、支持影子编程
### 1.1 使用表达式树作为影子
>* 参看[表达式版](./expression/index.md)

### 1.2 使用自定义表作为影子
>* 参看[易用版](./shadow/index.md)

### 1.3 使用影子编程
>* 通过查看引用可以定位到使用到该表或该字段的所有sql
>* 重构时查看字段引用可以准确评估影响范围
>* 删除字段相应sql拼写方法编译失败,按图索骥可以快速完成重构
>* 修改字段名相应sql可以同步重构完成
>* 实现编译检测的sql拼写

### 2、通用轻量级
>* 跨平台,支持net7.0;net8.0;net9.0;netstandard2.0;netstandard2.1
>* 支持多种数据库(MsSql,MySql,Oracle,Sqlite,Postgres等),可扩展其他数据库方言的支持
>* 本工具很小、且不依赖第三方包

### 3、高性能
>* 拼写sql耗时更少
>* 拼写sql内存消耗更少
>* 拼写sql方便直接对接Dapper,使用Dapper高效执行sql

### 4、简便易用
>* 能快速[上手](./quick.md)
>* 大多数API和sql一致,代码可读性强,使用简单
>* 支持链式语法,大多数功能可以直接一行代码搞定
