using SqlSugar;

namespace AspNetBlog.Model;

public class RootEntityTKey<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// ID
    /// 泛型主键 TKey
    /// </summary>
    [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
    public TKey Id { get; set; }
}