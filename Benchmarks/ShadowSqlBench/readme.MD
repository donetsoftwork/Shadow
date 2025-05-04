# ShadowSql和SqlKata对比

## 1、WhereBench
>* 简单查询对比
>* SqlKata耗时是TableName的8倍多
>* SqlKata耗时是SqlQuery的14倍
>* Logic性能最好,SqlKata耗时是他的25倍
>* 内存消耗也是差不多比例

| Method               | Mean       | Error     | StdDev    | Median     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlByTableName |   567.6 ns |  10.29 ns |  17.74 ns |   565.0 ns |  0.12 |    0.01 |      - |    1656 B |        0.13 |
| ShadowSqlBySqlQuery  |   341.5 ns |   4.70 ns |   7.98 ns |   342.0 ns |  0.07 |    0.01 |      - |     920 B |        0.07 |
| ShadowSqlByLogic     |   195.1 ns |   4.86 ns |   7.84 ns |   192.3 ns |  0.04 |    0.00 |      - |     576 B |        0.05 |
| SqlKataBench         | 4,629.3 ns | 233.05 ns | 414.25 ns | 4,371.9 ns |  1.01 |    0.12 | 0.7000 |   12712 B |        1.00 |

## 2、CursorBench
>* 单表排序筛选游标对比

| Method               | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlByTableName | 1,895.8 ns | 161.10 ns | 255.51 ns |  0.22 |    0.03 | 0.1000 |   2.46 KB |        0.13 |
| ShadowSqlBySqlQuery  |   857.7 ns |  10.71 ns |  17.59 ns |  0.10 |    0.00 |      - |   1.63 KB |        0.09 |
| ShadowSqlByLogic     |   713.0 ns |   6.48 ns |  11.36 ns |  0.08 |    0.00 |      - |   1.38 KB |        0.07 |
| SqlKataBench         | 8,513.5 ns |  43.38 ns |  75.97 ns |  1.00 |    0.01 | 1.1000 |  19.08 KB |        1.00 |

## 3、GroupByBench
>* 分组查询对比


| Method               | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlByTableName |  2.006 us | 0.3432 us | 0.5734 us |  0.09 |    0.02 | 0.2000 |   3.83 KB |        0.06 |
| ShadowSqlBySqlQuery  |  1.256 us | 0.0268 us | 0.0433 us |  0.05 |    0.00 | 0.1000 |   2.61 KB |        0.04 |
| ShadowSqlByLogic     |  1.033 us | 0.0112 us | 0.0188 us |  0.04 |    0.00 | 0.1000 |   2.21 KB |        0.03 |
| SqlKataBench         | 22.983 us | 0.0884 us | 0.1525 us |  1.00 |    0.01 | 3.7000 |   63.5 KB |        1.00 |

## 4、JoinBench
>* 联表查询对比


| Method               | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlByTableName |  2.362 us | 0.0263 us | 0.0447 us |  2.361 us |  0.20 |    0.00 | 0.3000 |   5.15 KB |        0.20 |
| ShadowSqlBySqlQuery  |  1.660 us | 0.4810 us | 0.8549 us |  2.288 us |  0.14 |    0.07 | 0.1000 |   3.09 KB |        0.12 |
| ShadowSqlByLogic     |  1.786 us | 0.4144 us | 0.7367 us |  2.217 us |  0.15 |    0.06 | 0.1000 |   2.72 KB |        0.11 |
| SqlKataBench         | 11.704 us | 0.0561 us | 0.0998 us | 11.709 us |  1.00 |    0.01 | 1.5000 |  25.89 KB |        1.00 |

