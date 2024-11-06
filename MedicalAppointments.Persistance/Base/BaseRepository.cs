using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Repositories;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MedicalAppointments.Persistance.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly DbSet<TEntity> _entities;
        public BaseRepository(MedicalAppointmentContext medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            _entities = _medicalAppointmentContext.Set<TEntity>();
        }
        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.AnyAsync(filter);
        }

        public virtual async Task<OperationResult> GetAll()
        {
            OperationResult result = new();
            try
            {
                var data = await _entities.ToListAsync();
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error has ocurred {ex.Message} when obtaining the registry.";
            }
            return result;
        }

        public virtual async Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            OperationResult operationResult = new();
            try
            {
                var data = await _entities.Where(filter).ToListAsync();
                operationResult.Data = data;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = $"An error has ocurred {ex.Message} when obtaining the registry.";
            }
            return operationResult;
        }

        public virtual async Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult result = new();
            try
            {
                var entity = await _entities.FindAsync(id);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error has ocurred {ex.Message} obtaining the entity.";
            }
            return result;
        }

        public virtual async Task<OperationResult> Remove(int id, TEntity entity)
        {
            OperationResult result = new();
            try
            {
                _entities.Remove(entity);
                await _medicalAppointmentContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error has ocurred {ex.Message} removing the entity.";
            }
            return result;
        }

        public virtual async Task<OperationResult> Save(TEntity entity)
        {
            OperationResult result = new();
            try
            {
                _entities.Add(entity);
                await _medicalAppointmentContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                result.Success = false;
                result.Message = $"An error has ocurred {ex.Message} saving the entity.";
            }
            return result;
        }

        public virtual async Task<OperationResult> Update(int id, TEntity entity)
        {
            OperationResult result = new();
            try
            {
                _entities.Update(entity);
                await _medicalAppointmentContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error has ocurred {ex.Message} updating the entity.";
            }
            return result;
        }
    }
}
