namespace AspNetBlog.Common;

// 公共类
public class AopLogInfo
{
    // 请求时间
    public string RequestTime { get; set; } = string.Empty;
    // 操作人员
    public string OpUserName { get; set; } = string.Empty;
    // 请求方法名
    public string RequestMethodName { get; set; } = string.Empty;
    // 请求参数名
    public string RequestParamsName { get; set; } = string.Empty;
    // 请求参数数据JSON
    public string RequestParamsData { get; set; } = string.Empty;
    // 请求响应间隔时间
    public string ResponseIntervalTime { get; set; } = string.Empty;
    // 响应时间
    public string ResponseTime { get; set; } = string.Empty;
    // 响应结果
    public string ResponseJsonData { get; set; } = string.Empty;
}