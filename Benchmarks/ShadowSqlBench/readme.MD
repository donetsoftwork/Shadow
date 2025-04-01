# ShadowSql和SqlKata对比

## 1、WhereBench
>* 简单查询对比
>* SqlKata耗时是ByFieldName的8倍多
>* SqlKata耗时是ByParametricLogic的14倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的25倍
>* 内存消耗也是差不多比例

| Method                     | Mean       | Error     | StdDev    | Median     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlByFieldName       |   555.3 ns |  11.04 ns |  19.34 ns |   551.9 ns |  0.12 |    0.01 |      - |    1664 B |        0.13 |
| ShadowSqlByParametricLogic |   320.1 ns |   6.74 ns |  11.81 ns |   319.1 ns |  0.07 |    0.01 |      - |    1112 B |        0.09 |
| ShadowSqlByLogic           |   187.0 ns |   4.49 ns |   7.50 ns |   185.8 ns |  0.04 |    0.00 |      - |     608 B |        0.05 |
| SqlKataBench               | 4,555.9 ns | 217.01 ns | 385.74 ns | 4,322.3 ns |  1.01 |    0.12 | 0.7000 |   12712 B |        1.00 |

## 2、FetchBench
>* 单表排序筛选对比
>* SqlKata耗时是ByFieldName的4倍多
>* SqlKata耗时是ByParametricLogic的9倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的12倍
>* 内存消耗也是差不多比例

| Method                     | Mean       | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlByFieldName       | 1,811.6 ns | 173.88 ns | 285.68 ns |  0.21 |    0.03 | 0.1000 |   2.47 KB |        0.13 |
| ShadowSqlByParametricLogic |   896.6 ns |  13.32 ns |  22.25 ns |  0.11 |    0.00 | 0.1000 |   2.03 KB |        0.11 |
| ShadowSqlByLogic           |   692.6 ns |  10.52 ns |  18.42 ns |  0.08 |    0.00 |      - |   1.41 KB |        0.07 |
| SqlKataBench               | 8,514.1 ns |  75.19 ns | 133.66 ns |  1.00 |    0.02 | 1.1000 |  19.08 KB |        1.00 |

## 3、GroupByBench
>* 分组查询对比
>* SqlKata耗时是ByFieldName的12倍多
>* SqlKata耗时是ByParametricLogic的16倍
>* ByLogic性能最好,直接拼接参数,SqlKata耗时是他的25倍
>* 内存消耗也是差不多比例

| Method                     | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlByFieldName       |  1.873 us | 0.4660 us | 0.8039 us |  0.08 |    0.03 | 0.2000 |    4.1 KB |        0.06 |
| ShadowSqlByParametricLogic |  1.450 us | 0.0567 us | 0.0948 us |  0.06 |    0.00 | 0.1000 |    3.2 KB |        0.05 |
| ShadowSqlByLogic           |  1.007 us | 0.0156 us | 0.0260 us |  0.04 |    0.00 | 0.1000 |   2.41 KB |        0.04 |
| SqlKataBench               | 23.029 us | 0.0982 us | 0.1745 us |  1.00 |    0.01 | 3.7000 |  63.46 KB |        1.00 |

## 4、JoinBench
>* 联表查询对比
>* SqlKata耗时是ByFieldName的5倍多
>* SqlKata耗时是ByParametricLogic的6倍
>* ByLogic性能还是最好,但与ByFieldName和ByParametricLogic相差不大

| Method                     | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|--------------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| ShadowSqlByFieldName       |  2.087 us | 0.2753 us | 0.4750 us |  2.240 us |  0.18 |    0.04 | 0.2000 |   4.95 KB |        0.19 |
| ShadowSqlByParametricLogic |  1.768 us | 0.4712 us | 0.8376 us |  2.300 us |  0.15 |    0.07 | 0.2000 |   3.52 KB |        0.14 |
| ShadowSqlByLogic           |  1.796 us | 0.3658 us | 0.6502 us |  2.127 us |  0.15 |    0.05 | 0.1000 |   2.89 KB |        0.11 |
| SqlKataBench               | 11.719 us | 0.1046 us | 0.1859 us | 11.666 us |  1.00 |    0.02 | 1.5000 |  25.89 KB |        1.00 |

