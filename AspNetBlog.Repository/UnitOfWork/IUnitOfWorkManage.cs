using SqlSugar;

namespace AspNetBlog.Repository.UnitOfWork;

public interface IUnitOfWorkManage
{
    SqlSugarScope GetDbClient();
    void BeginTran();
    void CommitTran();
    void RollbackTran();
    UnitOfWork CreateUnitOfWork();
}