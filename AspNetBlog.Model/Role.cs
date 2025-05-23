using SqlSugar;

namespace AspNetBlog.Model;

// 实体层角色模型

/// <summary>
/// 角色表
/// </summary>
public class Role : RootEntityTKey<long>
{

    /// <summary>
    /// 获取或设置是否禁用，逻辑上的删除，非物理删除
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public bool? IsDeleted { get; set; }
    /// <summary>
    /// 角色名
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string Name { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string Description { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    public int OrderSort { get; set; }
    /// <summary>
    /// 自定义权限的部门 ids
    /// </summary>
    [SugarColumn(Length = 500, IsNullable = true)]
    public string Dids { get; set; }
    /// <summary>
    /// 权限范围
    /// -1 无任何权限；1 自定义权限；2 本部门；3 本部门及以下；4 仅自己；9 全部；
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public int AuthorityScope { get; set; } = -1;
    /// <summary>
    /// 是否激活
    /// </summary>
    public bool Enabled { get; set; }
    /// <summary>
    /// 创建 ID
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? CreateId { get; set; }
    /// <summary>
    /// 创建者
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string CreateBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? CreateTime { get; set; } = DateTime.Now;
    /// <summary>
    /// 修改 ID
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ModifyId { get; set; }
    /// <summary>
    /// 修改者
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public string ModifyBy { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? ModifyTime { get; set; } = DateTime.Now;
}