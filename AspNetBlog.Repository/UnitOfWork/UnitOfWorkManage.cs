using System.Collections.Concurrent;
using System.Reflection;
using AspNetBlog.Common.Extensions;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace AspNetBlog.Repository.UnitOfWork;

public class UnitOfWorkManage : IUnitOfWorkManage
{
    private readonly ILogger<UnitOfWorkManage> _logger;
    private readonly ISqlSugarClient _sqlSugarClient;
    
    private int _tranCount { get; set; }
    public int TranCount => _tranCount;
    
    // ConcurrentStack<> 线程安全，允许多个线程去读取和修改栈的数据
    public readonly ConcurrentStack<string> TranStack = new();

    public UnitOfWorkManage(ISqlSugarClient sqlSugarClient, ILogger<UnitOfWorkManage> logger)
    {
        _sqlSugarClient = sqlSugarClient;
        _logger = logger;
        _tranCount = 0;
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

    // 创建工作单元
    public UnitOfWork CreateUnitOfWork()
    {
        UnitOfWork uow = new UnitOfWork();
        uow.Logger = _logger;
        uow.Db = _sqlSugarClient;
        uow.Tenant = (ITenant)_sqlSugarClient;
        uow.IsTran = true;

        uow.Db.Open();
        uow.Tenant.BeginTran();
        _logger.LogDebug("UnitOfWork Begin");
        return uow;
    }

    // 开启事务
    public void BeginTran()
    {
        lock (this)
        {
            _tranCount++;
            GetDbClient().BeginTran();
        }
    }
    public void BeginTran(MethodInfo method)
    {
        lock (this)
        {
            GetDbClient().BeginTran();
            TranStack.Push(method.GetFullName());
            _tranCount = TranStack.Count;
        }
    }

    // 提交事务
    public void CommitTran()
    {
        lock (this)
        {
            _tranCount--;
            if (_tranCount == 0)
            {
                try
                {
                    GetDbClient().CommitTran();
                }
                catch (Exception ex)
                {
                    // 打印异常信息
                    Console.WriteLine(ex.Message);
                    // 触发事务回滚
                    GetDbClient().RollbackTran();
                }
            }
        }
    }
    public void CommitTran(MethodInfo method)
    {
        lock (this)
        {
            string result = "";
            while (!TranStack.IsEmpty && !TranStack.TryPeek(out result))
            {
                Thread.Sleep(1);
            }


            if (result == method.GetFullName())
            {
                try
                {
                    GetDbClient().CommitTran();

                    _logger.LogDebug($"Commit Transaction");
                    Console.WriteLine($"Commit Transaction");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    GetDbClient().RollbackTran();
                    _logger.LogDebug($"Commit Error , Rollback Transaction");
                }
                finally
                {
                    while (!TranStack.TryPop(out _))
                    {
                        Thread.Sleep(1);
                    }

                    _tranCount = TranStack.Count;
                }
            }
        }
    }

    // 事务回滚
    public void RollbackTran()
    {
        lock (this)
        {
            _tranCount--;
            GetDbClient().RollbackTran();
        }
    }
    public void RollbackTran(MethodInfo method)
    {
        lock (this)
        {
            string result = "";
            while (!TranStack.IsEmpty && !TranStack.TryPeek(out result))
            {
                Thread.Sleep(1);
            }

            if (result == method.GetFullName())
            {
                GetDbClient().RollbackTran();
                _logger.LogDebug($"Rollback Transaction");
                Console.WriteLine($"Rollback Transaction");
                while (!TranStack.TryPop(out _))
                {
                    Thread.Sleep(1);
                }

                _tranCount = TranStack.Count;
            }
        }
    }
}