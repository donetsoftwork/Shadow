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

## 2、CursorBench
>* 单表排序筛选游标对比

| Method                | Mean       | Error    | StdDev   | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |-----------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |   386.4 ns |  2.40 ns |  2.77 ns |  0.05 | 0.1280 |      - |   2.19 KB |        0.11 |
| ShadowSqlBySqlQuery   |   261.7 ns |  2.42 ns |  2.78 ns |  0.03 | 0.0960 |      - |   1.63 KB |        0.09 |
| ShadowSqlByExpression | 2,068.7 ns | 34.86 ns | 38.75 ns |  0.25 | 0.2840 |      - |    4.8 KB |        0.25 |
| ShadowSqlByLogic      |   204.4 ns |  1.95 ns |  2.25 ns |  0.02 | 0.0820 |      - |   1.38 KB |        0.07 |
| SqlKataBench          | 8,342.6 ns | 80.23 ns | 89.17 ns |  1.00 | 1.1320 | 0.0120 |  19.08 KB |        1.00 |

## 3、GroupByBench
>* 分组查询对比

| Method                | Mean        | Error    | StdDev   | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |------------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |    613.4 ns |  6.19 ns |  7.13 ns |  0.03 | 0.2050 | 0.0010 |   3.47 KB |        0.05 |
| ShadowSqlBySqlQuery   |    391.3 ns |  5.34 ns |  6.15 ns |  0.02 | 0.1540 | 0.0010 |   2.61 KB |        0.04 |
| ShadowSqlByExpression |  2,078.1 ns | 14.93 ns | 15.98 ns |  0.09 | 0.3610 | 0.0010 |   6.09 KB |        0.10 |
| ShadowSqlByLogic      |    312.9 ns |  3.11 ns |  3.58 ns |  0.01 | 0.1310 |      - |   2.21 KB |        0.03 |
| SqlKataBench          | 22,624.0 ns | 51.76 ns | 57.53 ns |  1.00 | 3.7690 | 0.0600 |   63.5 KB |        1.00 |

## 4、JoinBench
>* 联表查询对比

| Method                | Mean        | Error    | StdDev   | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|---------------------- |------------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName  |    833.3 ns |  4.62 ns |  5.32 ns |  0.07 | 0.2780 |      - |   4.69 KB |        0.18 |
| ShadowSqlBySqlQuery   |    474.6 ns |  5.87 ns |  6.76 ns |  0.04 | 0.1830 |      - |   3.09 KB |        0.12 |
| ShadowSqlByExpression |  3,665.8 ns | 15.65 ns | 18.03 ns |  0.32 | 0.5710 | 0.0040 |   9.63 KB |        0.37 |
| ShadowSqlByLogic      |    431.4 ns |  4.74 ns |  5.26 ns |  0.04 | 0.1610 |      - |   2.72 KB |        0.11 |
| SqlKataBench          | 11,525.6 ns | 35.26 ns | 40.61 ns |  1.00 | 1.5360 | 0.0190 |  25.89 KB |        1.00 |
