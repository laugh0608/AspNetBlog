using SqlSugar;

namespace AspNetBlog.Model;

public abstract class BaseLog : RootEntityTKey<long>
{
    [SplitField]
    public DateTime? DateTime { get; set; }

    [SugarColumn(IsNullable = true)]
    public string Level { get; set; }

    [SugarColumn(IsNullable = true, ColumnDataType = "longtext,text,clob")]
    public string Message { get; set; }

    [SugarColumn(IsNullable = true, ColumnDataType = "longtext,text,clob")]
    public string MessageTemplate { get; set; }

    [SugarColumn(IsNullable = true, ColumnDataType = "longtext,text,clob")]
    public string Properties { get; set; }
}