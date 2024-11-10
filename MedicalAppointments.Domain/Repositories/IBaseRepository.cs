using MedicalAppointments.Domain.Result;
using System.Linq.Expressions;

namespace MedicalAppointments.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<OperationResult> Save(TEntity entity);
        Task<OperationResult> Update(int id, TEntity entity);
        Task<OperationResult> Remove(int id, TEntity entity);
        Task<OperationResult> GetAll();
        Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult> GetEntityBy(int id);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);
    }

}
