# 赋值原生sql
>* 按原生sql更新

## 1. 接口
>* [IAssignInfo](xref:ShadowSql.Assigns.IAssignInfo)

## 2. 类
>* [RawAssignInfo](xref:ShadowSql.Assigns.RawAssignInfo)
~~~csharp
class RawAssignInfo(string assignInfo) : IAssignInfo;
~~~

## 3. 扩展方法
~~~csharp
TUpdate SetRaw<TUpdate>(this TUpdate update, string assignSql)
        where TUpdate : UpdateBase, IUpdate;
~~~

## 4. 其他相关功能
>* 参看[赋值简介](../assign/index.md)