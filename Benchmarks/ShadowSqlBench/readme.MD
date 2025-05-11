# ShadowSql和SqlKata对比

## 1、WhereBench
>* 简单查询对比
>* SqlKata耗时是TableName的20倍多
>* SqlKata耗时是SqlQuery的33倍
>* Logic性能最好,SqlKata耗时是他的50倍
>* SqlKata内存消耗也是3种的10多倍以上

| Method               | Mean        | Error     | StdDev    | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|--------------------- |------------:|----------:|----------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName |   220.64 ns |  2.883 ns |  3.205 ns |  0.05 | 0.0955 |      - |    1656 B |        0.13 |
| ShadowSqlBySqlQuery  |   115.90 ns |  0.638 ns |  0.709 ns |  0.03 | 0.0530 |      - |     920 B |        0.07 |
| ShadowSqlByLogic     |    69.81 ns |  1.259 ns |  1.450 ns |  0.02 | 0.0330 |      - |     576 B |        0.05 |
| SqlKataBench         | 4,173.57 ns | 24.612 ns | 28.343 ns |  1.00 | 0.7365 | 0.0040 |   12712 B |        1.00 |

## 2、CursorBench
>* 单表排序筛选游标对比

| Method               | Mean       | Error    | StdDev   | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|--------------------- |-----------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName |   743.3 ns |  4.15 ns |  4.61 ns |  0.09 | 0.1460 |      - |   2.46 KB |        0.13 |
| ShadowSqlBySqlQuery  |   269.9 ns |  1.34 ns |  1.55 ns |  0.03 | 0.0960 |      - |   1.63 KB |        0.09 |
| ShadowSqlByLogic     |   205.5 ns |  1.81 ns |  2.01 ns |  0.02 | 0.0820 |      - |   1.38 KB |        0.07 |
| SqlKataBench         | 8,337.5 ns | 37.42 ns | 40.04 ns |  1.00 | 1.1320 | 0.0120 |  19.08 KB |        1.00 |

## 3、GroupByBench
>* 分组查询对比

| Method               | Mean        | Error    | StdDev   | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|--------------------- |------------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName |    645.8 ns |  4.28 ns |  4.93 ns |  0.03 | 0.2220 | 0.0010 |   3.74 KB |        0.06 |
| ShadowSqlBySqlQuery  |    397.6 ns |  2.83 ns |  3.25 ns |  0.02 | 0.1540 | 0.0010 |   2.61 KB |        0.04 |
| ShadowSqlByLogic     |    316.6 ns |  5.42 ns |  6.24 ns |  0.01 | 0.1310 |      - |   2.21 KB |        0.03 |
| SqlKataBench         | 22,773.3 ns | 57.59 ns | 64.01 ns |  1.00 | 3.7690 | 0.0600 |   63.5 KB |        1.00 |

## 4、JoinBench
>* 联表查询对比

| Method               | Mean        | Error    | StdDev   | Ratio | Gen0   | Gen1   | Allocated | Alloc Ratio |
|--------------------- |------------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
| ShadowSqlByTableName |    753.6 ns |  6.44 ns |  7.42 ns |  0.07 | 0.2970 | 0.0020 |   5.02 KB |        0.19 |
| ShadowSqlBySqlQuery  |    480.0 ns |  8.74 ns | 10.07 ns |  0.04 | 0.1830 |      - |   3.09 KB |        0.12 |
| ShadowSqlByLogic     |    437.2 ns |  4.01 ns |  4.62 ns |  0.04 | 0.1610 |      - |   2.72 KB |        0.11 |
| SqlKataBench         | 11,475.9 ns | 46.18 ns | 47.42 ns |  1.00 | 1.5360 | 0.0190 |  25.89 KB |        1.00 |
