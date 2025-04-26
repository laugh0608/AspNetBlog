using System.Linq.Expressions;
using SqlSugar;

namespace AspNetBlog.Repository.Base;

// 仓储基类接口
// 添加一个 TEntity 泛型，添加到仓储接口里
public interface IBaseRepository<TEntity> where TEntity : class
{
    // 添加暴露数据库拿到的数据，仅对外部访问
    ISqlSugarClient Db { get; }
    
    Task<List<long>> AddSplit(TEntity entity);

    Task<long> Add(TEntity entity);

    // 然后给 TEntity 返回一个 List
    // Task<List<TEntity>> Query();
    Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
    Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression,
        Expression<Func<T, T2, T3, TResult>> selectExpression,
        Expression<Func<T, T2, T3, bool>> whereLambda = null)
        where T : class, new();
    
    Task<List<TEntity>> QuerySplit(Expression<Func<TEntity, bool>> whereExpression, string orderByFields = null);
}