using SqlSugar;

namespace AspNetBlog.Model;

/// <summary>
/// 按钮跟权限关联表
/// </summary>
public class RoleModulePermission : RootEntityTKey<long>
{
    public long RoleId { get; set; }
    /// <summary>
    /// 接口 ID
    /// </summary>
    public long ModuleId { get; set; }
    /// <summary>
    /// 菜单前端 ID
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long PermissionId { get; set; }

    /// <summary>
    /// 获取或设置是否禁用，逻辑上的删除，非物理删除
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public bool? IsDeleted { get; set; }
       
    /// <summary>
    /// 创建ID
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
    /// 修改ID
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public long? ModifyId { get; set; }
    /// <summary>
    /// 修改者
    /// </summary>
    [SugarColumn(Length = 50, IsNullable = true)]
    public string ModifyBy { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(IsNullable = true)]
    public DateTime? ModifyTime { get; set; } = DateTime.Now;

    // 下边三个实体参数，只是做传参作用，所以忽略下
    [SugarColumn(IsIgnore = true)]
    public Role Role { get; set; }
    [SugarColumn(IsIgnore = true)]
    public Modules Module { get; set; }
      
}