# ShadowSql和SqlKata对比

## 1、WhereBench
>* 简单查询对比
>* SqlKata耗时是SqlQuery的8倍多
>* SqlKata耗时是Query的10倍左右
>* SqlKata耗时是ByParametricLogic的14倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的25倍
>* 内存消耗也是差不多比例

| Method                     | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        |   557.9 ns |  12.16 ns |  19.98 ns |  0.12 |    0.01 |      - |    1680 B |        0.13 |
| ShadowSqlByQuery           |   434.7 ns |   5.88 ns |   9.99 ns |  0.10 |    0.01 |      - |    1488 B |        0.12 |
| ShadowSqlByParametricLogic |   299.1 ns |   6.10 ns |  10.52 ns |  0.07 |    0.00 |      - |    1080 B |        0.08 |
| ShadowSqlByLogic           |   167.4 ns |   2.05 ns |   3.37 ns |  0.04 |    0.00 |      - |     576 B |        0.05 |
| SqlKataBench               | 4,480.0 ns | 162.01 ns | 283.74 ns |  1.00 |    0.09 | 0.7000 |   12712 B |        1.00 |

## 2、CursorBench
>* 单表排序筛选游标对比
>* SqlKata耗时是SqlQuery的4倍多
>* SqlKata耗时是Query的8倍多
>* SqlKata耗时是ByParametricLogic的9倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的12倍
>* 内存消耗也是差不多比例

| Method                     | Mean       | Error    | StdDev    | Ratio | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |-----------:|---------:|----------:|------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        | 1,803.9 ns | 67.71 ns | 111.25 ns |  0.21 | 0.1000 |   2.45 KB |        0.13 |
| ShadowSqlByQuery           | 1,007.3 ns | 12.03 ns |  20.10 ns |  0.12 | 0.1000 |   2.28 KB |        0.12 |
| ShadowSqlByParametricLogic |   833.5 ns |  9.53 ns |  15.65 ns |  0.10 | 0.1000 |   1.96 KB |        0.10 |
| ShadowSqlByLogic           |   708.5 ns | 10.08 ns |  17.91 ns |  0.08 |      - |   1.54 KB |        0.08 |
| SqlKataBench               | 8,549.1 ns | 36.10 ns |  64.16 ns |  1.00 | 1.1000 |  19.08 KB |        1.00 |

## 3、GroupByBench
>* 分组查询对比
>* SqlKata耗时是SqlQuery的12倍多
>* SqlKata耗时是Query的12倍多
>* SqlKata耗时是ByParametricLogic的20倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的25倍
>* 内存消耗也是差不多比例

| Method                     | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        |  1.978 us | 0.4583 us | 0.8027 us |  2.451 us |  0.09 |    0.03 | 0.2000 |   4.04 KB |        0.06 |
| ShadowSqlByQuery           |  2.023 us | 0.0646 us | 0.1044 us |  1.998 us |  0.09 |    0.00 | 0.2000 |   3.63 KB |        0.06 |
| ShadowSqlByParametricLogic |  1.558 us | 0.0887 us | 0.1457 us |  1.620 us |  0.07 |    0.01 | 0.1000 |   3.13 KB |        0.05 |
| ShadowSqlByLogic           |  1.060 us | 0.0123 us | 0.0208 us |  1.055 us |  0.05 |    0.00 | 0.1000 |   2.39 KB |        0.04 |
| SqlKataBench               | 23.174 us | 0.1320 us | 0.2346 us | 23.140 us |  1.00 |    0.01 | 3.7000 |  63.46 KB |        1.00 |

## 4、JoinBench
>* 联表查询对比
>* SqlKata耗时是SqlQuery的4倍多
>* SqlKata耗时是Query的4倍多
>* SqlKata耗时是ByParametricLogic的5倍
>* ByLogic性能还是最好,但与ByParametricLogic相差不大

| Method                     | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        |  2.377 us | 0.0193 us | 0.0317 us |  2.387 us |  0.20 |    0.00 | 0.3000 |   5.35 KB |        0.21 |
| ShadowSqlByQuery           |  2.346 us | 0.0198 us | 0.0325 us |  2.355 us |  0.20 |    0.00 | 0.3000 |   5.35 KB |        0.21 |
| ShadowSqlByParametricLogic |  1.901 us | 0.4974 us | 0.8842 us |  2.413 us |  0.16 |    0.07 | 0.1000 |   3.34 KB |        0.13 |
| ShadowSqlByLogic           |  1.874 us | 0.3258 us | 0.5443 us |  2.112 us |  0.16 |    0.05 | 0.1000 |   2.71 KB |        0.10 |
| SqlKataBench               | 11.733 us | 0.0606 us | 0.1061 us | 11.750 us |  1.00 |    0.01 | 1.5000 |  25.89 KB |        1.00 |

