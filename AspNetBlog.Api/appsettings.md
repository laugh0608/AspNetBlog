```json5
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Redis": {
    "Enable": true,
    "ConnectionString": "127.0.0.1:6379",
    "InstanceName": "" // 前缀
  },
  // 优化 DB 配置、不会再区分单库多库
  // MainDb：标识当前项目的主库，所对应的连接字符串的 Enabled 必须为 true
  // Log：标识日志库，所对应的连接字符串的 Enabled 必须为 true
  "MainDB": "Main", // 当前项目的主库，所对应的连接字符串的 Enabled 必须为 true
  "DBS": [
    /*
      对应下边的 DbType
      MySql = 0,
      SqlServer = 1,
      Sqlite = 2,
      Oracle = 3,
      PostgreSQL = 4,
      Dm = 5, // 达梦
      Kdbndp = 6, // 人大金仓
    */
    {
      "ConnId": "Main",
      "DbType": 2,
      "Enabled": true,
      "Connection": "WMBlog.db" // sqlite 只写数据库名就行
    },
    {
      "ConnId": "Log", // 日志库连接固定名称，不要改，其他的可以改
      "DbType": 2,
      "Enabled": true,
      "HitRate": 50,
      "Connection": "WMBlogLog.db" // sqlite 只写数据库名就行
    }
  ]
}
```
