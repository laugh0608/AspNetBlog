using SqlSugar;

namespace AspNetBlog.Model;

/// <summary>
/// 接口 API 地址信息表
/// </summary>
public class Modules : RootEntityTKey<long>
{
    public long ParentId { get; set; }

    /// <summary>
    /// 获取或设置是否禁用，逻辑上的删除，非物理删除
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public bool? IsDeleted { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string Name { get; set; }
    /// <summary>
    /// 菜单链接地址
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string LinkUrl { get; set; }
    /// <summary>
    /// 区域名称
    /// </summary>
    [SugarColumn(Length = 2000, IsNullable = true)]
    public string Area { get; set; }
    /// <summary>
    /// 控制器名称
    /// </summary>
    [SugarColumn(Length = 2000, IsNullable = true)]
    public string Controller { get; set; }
    /// <summary>
    /// Action 名称
    /// </summary>
    [SugarColumn(Length = 2000, IsNullable = true)]
    public string Action { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string Icon { get; set; }
    /// <summary>
    /// 菜单编号
    /// </summary>
    [SugarColumn(Length = 10, IsNullable = true)]
    public string Code { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    public int OrderSort { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string Description { get; set; }
    /// <summary>
    /// 是否是右侧菜单
    /// </summary>
    public bool IsMenu { get; set; }
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
    public DateTime CreateTime { get; set; } = DateTime.Now;
    /// <summary>
    /// 修改 ID
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ModifyId { get; set; }
    /// <summary>
    /// 修改者
    /// </summary>
    [SugarColumn(Length = 100, IsNullable = true)]
    public string ModifyBy { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? ModifyTime { get; set; } = DateTime.Now;

    //public virtual Module ParentModule { get; set; }
    //public virtual ICollection<Module> ChildModule { get; set; }
    //public virtual ICollection<ModulePermission> ModulePermission { get; set; }
    //public virtual ICollection<RoleModulePermission> RoleModulePermission { get; set; }
}