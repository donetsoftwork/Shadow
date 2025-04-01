# ShadowSql和SqlKata对比

## 1、WhereBench
>* 简单查询对比
>* SqlKata耗时是SqlQuery的8倍多
>* SqlKata耗时是Query的10倍左右
>* SqlKata耗时是ByParametricLogic的14倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的25倍
>* 内存消耗也是差不多比例

| Method                     | Mean       | Error     | StdDev    | Median     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        |   557.7 ns |   5.70 ns |   9.36 ns |   556.3 ns |  0.12 |    0.01 |      - |    1680 B |        0.13 |
| ShadowSqlByQuery           |   482.7 ns |   7.44 ns |  12.83 ns |   481.5 ns |  0.10 |    0.01 |      - |    1512 B |        0.12 |
| ShadowSqlByParametricLogic |   322.5 ns |   5.30 ns |   9.15 ns |   322.6 ns |  0.07 |    0.01 |      - |    1112 B |        0.09 |
| ShadowSqlByLogic           |   189.2 ns |   3.42 ns |   5.62 ns |   187.5 ns |  0.04 |    0.00 |      - |     608 B |        0.05 |
| SqlKataBench               | 4,680.7 ns | 272.66 ns | 484.65 ns | 4,428.1 ns |  1.01 |    0.14 | 0.7000 |   12712 B |        1.00 |

## 2、FetchBench
>* 单表排序筛选对比
>* SqlKata耗时是ShadowSqlBySqlQuery的4倍多
>* SqlKata耗时是ShadowSqlByQuery的4倍多
>* SqlKata耗时是ByParametricLogic的9倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的12倍
>* 内存消耗也是差不多比例

| Method                     | Mean       | Error    | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |-----------:|---------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        | 1,847.8 ns | 74.34 ns | 122.14 ns |  0.22 |    0.01 | 0.1000 |   2.45 KB |        0.13 |
| ShadowSqlByQuery           | 1,867.2 ns | 37.41 ns |  62.50 ns |  0.22 |    0.01 | 0.1000 |   2.28 KB |        0.12 |
| ShadowSqlByParametricLogic |   894.7 ns | 20.15 ns |  33.11 ns |  0.11 |    0.00 | 0.1000 |   1.99 KB |        0.10 |
| ShadowSqlByLogic           |   671.3 ns |  7.96 ns |  14.15 ns |  0.08 |    0.00 |      - |   1.38 KB |        0.07 |
| SqlKataBench               | 8,504.4 ns | 52.65 ns |  93.58 ns |  1.00 |    0.02 | 1.1000 |  19.08 KB |        1.00 |

## 3、GroupByBench
>* 分组查询对比
>* SqlKata耗时是ByFieldName的12倍多
>* SqlKata耗时是ByParametricLogic的16倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的25倍
>* 内存消耗也是差不多比例

| Method                     | Mean       | Error    | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |-----------:|---------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        | 1,847.8 ns | 74.34 ns | 122.14 ns |  0.22 |    0.01 | 0.1000 |   2.45 KB |        0.13 |
| ShadowSqlByQuery           | 1,867.2 ns | 37.41 ns |  62.50 ns |  0.22 |    0.01 | 0.1000 |   2.28 KB |        0.12 |
| ShadowSqlByParametricLogic |   894.7 ns | 20.15 ns |  33.11 ns |  0.11 |    0.00 | 0.1000 |   1.99 KB |        0.10 |
| ShadowSqlByLogic           |   671.3 ns |  7.96 ns |  14.15 ns |  0.08 |    0.00 |      - |   1.38 KB |        0.07 |
| SqlKataBench               | 8,504.4 ns | 52.65 ns |  93.58 ns |  1.00 |    0.02 | 1.1000 |  19.08 KB |        1.00 |

## 4、JoinBench
>* 联表查询对比
>* SqlKata耗时是ByFieldName的5倍多
>* SqlKata耗时是ByParametricLogic的6倍
>* ByLogic性能还是最好,但与ByFieldName和ByParametricLogic相差不大

| Method                     | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        |  2.423 us | 0.0877 us | 0.1465 us |  2.366 us |  0.21 |    0.01 | 0.3000 |    5.3 KB |        0.20 |
| ShadowSqlByQuery           |  2.547 us | 0.1016 us | 0.1753 us |  2.507 us |  0.22 |    0.02 | 0.2000 |   4.98 KB |        0.19 |
| ShadowSqlByParametricLogic |  2.043 us | 0.3421 us | 0.6081 us |  2.428 us |  0.18 |    0.05 | 0.2000 |   3.38 KB |        0.13 |
| ShadowSqlByLogic           |  1.873 us | 0.2908 us | 0.5093 us |  2.107 us |  0.16 |    0.04 | 0.1000 |   2.75 KB |        0.11 |
| SqlKataBench               | 11.629 us | 0.0900 us | 0.1528 us | 11.590 us |  1.00 |    0.02 | 1.5000 |  25.89 KB |        1.00 |

