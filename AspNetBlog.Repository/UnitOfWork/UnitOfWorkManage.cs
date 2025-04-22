using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace AspNetBlog.Repository.UnitOfWork;

public class UnitOfWorkManage : IUnitOfWorkManage
{
    private readonly ILogger<UnitOfWorkManage> _logger;
    private readonly ISqlSugarClient _sqlSugarClient;
    public readonly ConcurrentStack<string> TranStack = new();

    public UnitOfWorkManage(ISqlSugarClient sqlSugarClient, ILogger<UnitOfWorkManage> logger)
    {
        _sqlSugarClient = sqlSugarClient;
        _logger = logger;
    }

    /// <summary>
    /// 获取 DB，保证唯一性
    /// </summary>
    /// <returns></returns>
    public SqlSugarScope GetDbClient()
    {
        // 必须要 as，后边会用到切换数据库操作
        return _sqlSugarClient as SqlSugarScope;
    }

    public void BeginTran()
    {
        lock (this)
        {
            // BeginTran() 是多库事务
            GetDbClient().BeginTran();
        }
    }

    public void CommitTran()
    {
        lock (this)
        {
            GetDbClient().CommitTran();
        }
    }

    public void RollbackTran()
    {
        lock (this)
        {
            GetDbClient().RollbackTran();
        }
    }

    public UnitOfWork CreateUnitOfWork()
    {
        UnitOfWork uow = new()
        {
            Logger = _logger,
            Db = _sqlSugarClient,
            Tenant = (ITenant)_sqlSugarClient,
            IsTran = true
        };

        uow.Db.Open();
        uow.Tenant.BeginTran();
        _logger.LogDebug("UnitOfWork Begin");
        return uow;
    }

}