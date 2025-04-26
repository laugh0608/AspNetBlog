namespace AspNetBlog.Model.Vo;

public class AuditSqlLogVo
{
    public DateTime? DateTime { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
    public string MessageTemplate { get; set; }
    public string Properties { get; set; }
}