# 功能

|操作|ToQuery|Join|GroupBy|ToCursor|ToSelect|ToUpdate|ToDelete|ToInsert|
|:--|:--|:--|:--|:--|:--|:--|:--|:--|
|ITable|TableQuery|JoinTableQuery|GroupByTable|TableCursor|TableSelect|TableUpdate|TableDelete|SingleInsert|
|IAliasTable|AliasTableQuery|不支持|GroupByAliasTable|AliasTableCursor|AliasTableSelect|AliasTableUpdate|AliasTableDelete|不支持|
|TableQuery|不需要|JoinTableQuery|GroupByTable|TableCursor|TableSelect|TableUpdate|TableDelete|先ToSelect|
|MultiTableQuery|不需要|不支持|GroupByMultiQuery|GroupByMultiCursor|MultiTableSelect|MultiTableUpdate|MultiTableDelete|先ToSelect|
|JoinTableQuery|不需要|JoinTableQuery|GroupByMultiQuery|GroupByMultiCursor|MultiTableSelect|MultiTableUpdate|MultiTableDelete|先ToSelect|
|GroupByTable|不需要|不支持|不支持|GroupByTableCursor|GroupByTableSelect|不支持|不支持|先ToSelect|
|GroupByAliasTable|不需要|不支持|不支持|GroupByAliasTableCursor|GroupByAliasTableSelect|不支持|不支持|先ToSelect|
