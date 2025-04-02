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
| ShadowSqlBySqlQuery        |   561.5 ns |  10.31 ns |  16.64 ns |   560.8 ns |  0.13 |    0.01 |      - |    1680 B |        0.13 |
| ShadowSqlByQuery           |   445.1 ns |   6.88 ns |  12.04 ns |   443.1 ns |  0.10 |    0.01 |      - |    1488 B |        0.12 |
| ShadowSqlByParametricLogic |   330.2 ns |   6.25 ns |  11.10 ns |   328.6 ns |  0.07 |    0.01 |      - |    1112 B |        0.09 |
| ShadowSqlByLogic           |   190.1 ns |   3.54 ns |   5.92 ns |   188.1 ns |  0.04 |    0.00 |      - |     608 B |        0.05 |
| SqlKataBench               | 4,519.9 ns | 216.02 ns | 383.98 ns | 4,347.3 ns |  1.01 |    0.11 | 0.7000 |   12712 B |        1.00 |

## 2、CursorBench
>* 单表排序筛选游标对比
>* SqlKata耗时是SqlQuery的4倍多
>* SqlKata耗时是Query的8倍多
>* SqlKata耗时是ByParametricLogic的9倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的12倍
>* 内存消耗也是差不多比例

| Method                     | Mean       | Error    | StdDev   | Ratio | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |-----------:|---------:|---------:|------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        | 1,811.3 ns | 18.38 ns | 30.20 ns |  0.21 | 0.1000 |   2.45 KB |        0.13 |
| ShadowSqlByQuery           | 1,019.3 ns | 15.56 ns | 26.42 ns |  0.12 | 0.1000 |   2.28 KB |        0.12 |
| ShadowSqlByParametricLogic |   898.6 ns | 10.40 ns | 17.38 ns |  0.11 | 0.1000 |   1.99 KB |        0.10 |
| ShadowSqlByLogic           |   678.9 ns |  9.44 ns | 16.29 ns |  0.08 |      - |   1.38 KB |        0.07 |
| SqlKataBench               | 8,518.7 ns | 47.80 ns | 84.97 ns |  1.00 | 1.1000 |  19.08 KB |        1.00 |

## 3、GroupByBench
>* 分组查询对比
>* SqlKata耗时是SqlQuery的12倍多
>* SqlKata耗时是Query的12倍多
>* SqlKata耗时是ByParametricLogic的20倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的25倍
>* 内存消耗也是差不多比例

| Method                     | Mean        | Error     | StdDev    | Median      | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |------------:|----------:|----------:|------------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        |  2,120.3 ns | 400.23 ns | 700.96 ns |  2,461.4 ns |  0.09 |    0.03 | 0.2000 |   4.05 KB |        0.06 |
| ShadowSqlByQuery           |  2,023.2 ns |  79.49 ns | 128.36 ns |  2,005.2 ns |  0.09 |    0.01 | 0.2000 |   3.63 KB |        0.06 |
| ShadowSqlByParametricLogic |  1,180.7 ns |  16.53 ns |  26.69 ns |  1,176.2 ns |  0.05 |    0.00 | 0.1000 |   2.96 KB |        0.05 |
| ShadowSqlByLogic           |    895.2 ns |   9.10 ns |  14.95 ns |    895.8 ns |  0.04 |    0.00 | 0.1000 |   2.18 KB |        0.03 |
| SqlKataBench               | 23,059.5 ns | 151.11 ns | 264.66 ns | 23,003.6 ns |  1.00 |    0.02 | 3.7000 |  63.46 KB |        1.00 |

## 4、JoinBench
>* 联表查询对比
>* SqlKata耗时是SqlQuery的4倍多
>* SqlKata耗时是Query的4倍多
>* SqlKata耗时是ByParametricLogic的5倍
>* ByLogic性能还是最好,但与ByParametricLogic相差不大

| Method                     | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlBySqlQuery        |  2.652 us | 0.3705 us | 0.6391 us |  2.380 us |  0.23 |    0.05 | 0.3000 |    5.3 KB |        0.20 |
| ShadowSqlByQuery           |  2.509 us | 0.0911 us | 0.1522 us |  2.430 us |  0.21 |    0.01 | 0.3000 |    5.3 KB |        0.20 |
| ShadowSqlByParametricLogic |  1.996 us | 0.3583 us | 0.6369 us |  2.417 us |  0.17 |    0.05 | 0.2000 |   3.38 KB |        0.13 |
| ShadowSqlByLogic           |  1.896 us | 0.2773 us | 0.4634 us |  2.116 us |  0.16 |    0.04 | 0.1000 |   2.75 KB |        0.11 |
| SqlKataBench               | 11.701 us | 0.0515 us | 0.0902 us | 11.704 us |  1.00 |    0.01 | 1.5000 |  25.89 KB |        1.00 |

