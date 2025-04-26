using SqlSugar;

namespace AspNetBlog.Model;

// 指定要使用的数据库
[Tenant(configId: "log")] // 这一行就是表明要使用的数据库是来自于 appsetting 中的 ConnId（不分大小写）
// 指定要使用的表名
// [SugarTable("AuditSqlLog_20231201", "Sql 审计日志")] // ('数据库表名'，'数据库表备注')
// 更改为自动分表
[SplitTable(SplitType.Month)] // 按月分表（自带分表支持 年、季、月、周、日）
[SugarTable($@"{nameof(AuditSqlLog)}_{{year}}{{month}}{{day}}")]
public class AuditSqlLog : BaseLog
{

}