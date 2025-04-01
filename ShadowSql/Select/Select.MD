# 筛选

## 一、ISelectFech
>* 用于分页获取数据
>* 需要orderby

## 二、ISingleSelect
>* 用于获取单行单条数据
>* 用于SubInLogic子查询
>* 需要orderby

## 三、ISelect
>* 用于ExistsLogic子查询
>* 用于groupby

## 四、变化
>* 单列(ISingleSelect)还是多列(ISelect)
>* 是否需要orderby和分页(ISelectFech)
>* 从单表、多(联)表还是groupby中筛选列
>* 从单表、多(联)表还是groupby中排序

## 五、拆分
### 1.Select
>* ISelect
>* ISingleSelect
>* Select<TSelectSource, TSelectFields>
>* SingleSelect<TSelectSource, TSelectFields>

### 2.SelectSource
>* IDataView
>* TableSelectSource
>* MultiSelectSource
>* GroupBySelectSource

### 3.ISelectFech
>* OrderBy、Limit、Offset
>* TableSelectFech
>* MultiSelectFech
>* GroupBySelectFech

### 4.ISelectFields
>* TableSelectFields
>* MultiSelectFields
>* GroupBySelectFields

