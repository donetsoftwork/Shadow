# 功能

|操作|ToQuery|Join|GroupBy|ToFetch|ToSelect|ToUpdate|ToDelete|ToInsert|
|:--|:--|:--|:--|:--|:--|:--|:--|:--|
|ITable|TableQuery|JoinTableQuery|GroupByTable|TableFetch|TableSelect|TableUpdate|TableDelete|SingleInsert|
|IAliasTable|AliasTableQuery|不支持|GroupByAliasTable|AliasTableFetch|AliasTableSelect|AliasTableUpdate|AliasTableDelete|不支持|
|TableQuery|不需要|JoinTableQuery|GroupByTable|TableFetch|TableSelect|TableUpdate|TableDelete|先ToSelect|
|MultiTableQuery|不需要|不支持|GroupByMultiQuery|GroupByMultiFetch|MultiTableSelect|MultiTableUpdate|MultiTableDelete|先ToSelect|
|JoinTableQuery|不需要|JoinTableQuery|GroupByMultiQuery|GroupByMultiFetch|MultiTableSelect|MultiTableUpdate|MultiTableDelete|先ToSelect|
|GroupByTable|不需要|不支持|不支持|GroupByTableFetch|GroupByTableSelect|不支持|不支持|先ToSelect|
|GroupByAliasTable|不需要|不支持|不支持|GroupByAliasTableFetch|GroupByAliasTableSelect|不支持|不支持|先ToSelect|
