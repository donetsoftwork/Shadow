# ShadowSql和SqlKata对比

## 1、WhereBench
>* 简单查询对比
>* SqlKata耗时是TableName的20倍多
>* SqlKata耗时是SqlQuery的30倍多
>* SqlKata耗时是Expression的9倍多
>* Logic性能最好,SqlKata耗时是他的50倍
>* SqlKata内存消耗也远远大于以上4种

| Method                | Mean        | Error     | StdDev    | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |------------:|----------:|----------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |   194.10 ns |  2.178 ns |  2.508 ns |  0.05 | 0.0795 |      - |    1376 B |        0.11 |
| ShadowSqlBySqlQuery   |   125.34 ns |  1.132 ns |  1.211 ns |  0.03 | 0.0530 |      - |     920 B |        0.07 |
| ShadowSqlByExpression |   471.93 ns |  3.632 ns |  4.183 ns |  0.11 | 0.0915 |      - |    1584 B |        0.12 |
| ShadowSqlByLogic      |    73.90 ns |  0.472 ns |  0.544 ns |  0.02 | 0.0330 |      - |     576 B |        0.05 |
| SqlKataBench          | 4,163.90 ns | 18.189 ns | 20.946 ns |  1.00 | 0.7365 | 0.0040 |   12712 B |        1.00 |

| Method                | Mean         | Error      | StdDev     | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |-------------:|-----------:|-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |    199.63 ns |   3.178 ns |   3.263 ns |  0.05 |    0.00 | 0.0800 |      - |    1384 B |        0.11 |
| ShadowSqlBySqlQuery   |    122.36 ns |   2.011 ns |   2.065 ns |  0.03 |    0.00 | 0.0535 |      - |     928 B |        0.07 |
| ShadowSqlByExpression | 25,326.83 ns | 118.126 ns | 136.034 ns |  6.10 |    0.05 | 0.3420 | 0.3415 |    5904 B |        0.46 |
| ShadowSqlByLogic      |     79.02 ns |   0.247 ns |   0.253 ns |  0.02 |    0.00 | 0.0330 |      - |     576 B |        0.05 |
| SqlKataBench          |  4,153.33 ns |  26.661 ns |  30.703 ns |  1.00 |    0.01 | 0.7365 | 0.0040 |   12712 B |        1.00 |

| Method                | Mean         | Error     | StdDev     | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |-------------:|----------:|-----------:|------:|--------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |    190.64 ns |  2.307 ns |   2.564 ns |  0.05 |    0.00 | 0.0750 |      - |    1376 B |        0.11 |
| ShadowSqlBySqlQuery   |    122.85 ns |  1.101 ns |   1.178 ns |  0.03 |    0.00 | 0.0500 |      - |     920 B |        0.07 |
| ShadowSqlByExpression | 25,158.46 ns | 91.863 ns | 105.790 ns |  6.02 |    0.05 | 0.3400 | 0.3350 |    5904 B |        0.46 |
| ShadowSqlByLogic      |     74.46 ns |  1.364 ns |   1.459 ns |  0.02 |    0.00 | 0.0300 |      - |     576 B |        0.05 |
| SqlKataBench          |  4,178.19 ns | 26.447 ns |  30.457 ns |  1.00 |    0.01 | 0.7350 |      - |   12712 B |        1.00 |

## 2、CursorBench
>* 单表排序筛选游标对比

| Method                | Mean       | Error    | StdDev   | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |-----------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |   386.4 ns |  2.40 ns |  2.77 ns |  0.05 | 0.1280 |      - |   2.19 KB |        0.11 |
| ShadowSqlBySqlQuery   |   261.7 ns |  2.42 ns |  2.78 ns |  0.03 | 0.0960 |      - |   1.63 KB |        0.09 |
| ShadowSqlByExpression | 2,068.7 ns | 34.86 ns | 38.75 ns |  0.25 | 0.2840 |      - |    4.8 KB |        0.25 |
| ShadowSqlByLogic      |   204.4 ns |  1.95 ns |  2.25 ns |  0.02 | 0.0820 |      - |   1.38 KB |        0.07 |
| SqlKataBench          | 8,342.6 ns | 80.23 ns | 89.17 ns |  1.00 | 1.1320 | 0.0120 |  19.08 KB |        1.00 |

| Method                | Mean        | Error    | StdDev   | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |------------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |    410.0 ns |  7.26 ns |  7.46 ns |  0.05 | 0.1200 |      - |    2.2 KB |        0.12 |
| ShadowSqlBySqlQuery   |    258.6 ns |  6.16 ns |  6.32 ns |  0.03 | 0.0800 |      - |   1.64 KB |        0.09 |
| ShadowSqlByExpression | 28,749.8 ns | 52.54 ns | 58.40 ns |  3.44 | 0.5200 | 0.5000 |   8.81 KB |        0.46 |
| ShadowSqlByLogic      |    211.0 ns |  2.05 ns |  2.19 ns |  0.03 | 0.0800 |      - |   1.38 KB |        0.07 |
| SqlKataBench          |  8,369.4 ns | 10.47 ns | 10.75 ns |  1.00 | 1.1200 |      - |  19.08 KB |        1.00 |

## 3、GroupByBench
>* 分组查询对比

| Method                | Mean        | Error    | StdDev   | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |------------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |    613.4 ns |  6.19 ns |  7.13 ns |  0.03 | 0.2050 | 0.0010 |   3.47 KB |        0.05 |
| ShadowSqlBySqlQuery   |    391.3 ns |  5.34 ns |  6.15 ns |  0.02 | 0.1540 | 0.0010 |   2.61 KB |        0.04 |
| ShadowSqlByExpression |  2,078.1 ns | 14.93 ns | 15.98 ns |  0.09 | 0.3610 | 0.0010 |   6.09 KB |        0.10 |
| ShadowSqlByLogic      |    312.9 ns |  3.11 ns |  3.58 ns |  0.01 | 0.1310 |      - |   2.21 KB |        0.03 |
| SqlKataBench          | 22,624.0 ns | 51.76 ns | 57.53 ns |  1.00 | 3.7690 | 0.0600 |   63.5 KB |        1.00 |

| Method                | Mean        | Error     | StdDev    | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |------------:|----------:|----------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |    644.7 ns |   6.19 ns |   6.62 ns | 0.014 | 0.2000 |      - |   3.48 KB |        0.05 |
| ShadowSqlBySqlQuery   |    393.8 ns |   4.24 ns |   4.54 ns | 0.008 | 0.1500 |      - |   2.63 KB |        0.04 |
| ShadowSqlByExpression |  2,061.9 ns |   8.37 ns |   9.30 ns | 0.044 | 0.3500 |      - |   5.95 KB |        0.09 |
| ShadowSqlByLogic      |    319.0 ns |   5.03 ns |   5.16 ns | 0.007 | 0.1300 |      - |   2.21 KB |        0.03 |
| SqlKataBench          | 47,203.8 ns | 470.87 ns | 503.83 ns | 1.000 | 3.7600 | 0.0600 |   63.5 KB |        1.00 |

## 4、JoinBench
>* 联表查询对比

| Method                | Mean        | Error    | StdDev   | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |------------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |    833.3 ns |  4.62 ns |  5.32 ns |  0.07 | 0.2780 |      - |   4.69 KB |        0.18 |
| ShadowSqlBySqlQuery   |    474.6 ns |  5.87 ns |  6.76 ns |  0.04 | 0.1830 |      - |   3.09 KB |        0.12 |
| ShadowSqlByExpression |  3,665.8 ns | 15.65 ns | 18.03 ns |  0.32 | 0.5710 | 0.0040 |   9.63 KB |        0.37 |
| ShadowSqlByLogic      |    431.4 ns |  4.74 ns |  5.26 ns |  0.04 | 0.1610 |      - |   2.72 KB |        0.11 |
| SqlKataBench          | 11,525.6 ns | 35.26 ns | 40.61 ns |  1.00 | 1.5360 | 0.0190 |  25.89 KB |        1.00 |

| Method                | Mean        | Error     | StdDev    | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |------------:|----------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |    862.3 ns |   9.86 ns |  10.96 ns |  0.07 |    0.00 | 0.2790 | 0.0010 |    4.7 KB |        0.18 |
| ShadowSqlBySqlQuery   |    479.0 ns |   4.06 ns |   4.68 ns |  0.04 |    0.00 | 0.1840 |      - |   3.11 KB |        0.12 |
| ShadowSqlByLogic      |    441.0 ns |   4.51 ns |   4.82 ns |  0.04 |    0.00 | 0.1610 |      - |   2.72 KB |        0.11 |
| ShadowSqlByExpression | 60,223.2 ns | 678.42 ns | 781.27 ns |  5.22 |    0.07 | 1.0750 | 1.0740 |  18.11 KB |        0.70 |
| SqlKataBench          | 11,534.4 ns |  27.60 ns |  31.79 ns |  1.00 |    0.00 | 1.5360 | 0.0190 |  25.89 KB |        1.00 |
