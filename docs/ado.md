# .net core需要重学Ado.net
>* Ado.net是.net程序的数据访问接口
>* 这里只讨论数据库操作通用的部分

## 一、经典用法
```csharp
using var connection = new MySqlConnection(connString);
connection.Open();
using var command = new MySqlCommand(query, connection);
using var reader = command.ExecuteReader();
while (reader.Read())
{
    // 处理数据
}
```

## 二、新的用法
```csharp
await using var connection = new MySqlConnection(connString);
await connection.OpenAsync();
await using var command = new MySqlCommand(query, connection);
await using var reader = await command.ExecuteReaderAsync();
while (await reader.ReadAsync())
{
    // 处理数据
}
```

## 三、新旧对比
>* 新用法看着更复杂
>* 但新用法是异步的，更适合高并发场景, 提高程序整体的吞吐量

### 1. 同步方法一般是对异步方法的包装
>* 例如ExecuteReader()内部其实调用的是ExecuteReaderAsync().GetAwaiter().GetResult()
>* 所以同步方法会阻塞线程，降低并发能力
>* 涉及IO操作强烈推荐使用异步方法

## 四、新增DbDataSource
### 1. DbDataSource用法
>* MySqlDataSource是DbDataSource的实现类

```csharp
var dataSource = new MySqlDataSource(connString);
await using var connection = await dataSource.OpenConnectionAsync();
// 其他操作同上
```

### 2. 优点
>* connection对象是非线程安全的，在并发场景下会包异常
>* DbDataSource起到工厂作用，在需要的时候再创建connection对象，避免线程安全问题

### 3. 有些数据库提供商不支持DbDataSource
>* 例如Sqlite不支持

### 4. 破破烂烂的世界需要缝缝补补