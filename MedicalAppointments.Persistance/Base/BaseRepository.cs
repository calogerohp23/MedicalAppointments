﻿using MedicalAppointments.Domain.Repositories;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MedicalAppointments.Persistance.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private DbSet<TEntity> entities;
        public BaseRepository(MedicalAppointmentContext medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.entities = _medicalAppointmentContext.Set<TEntity>();
        }
        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await this.entities.AnyAsync(filter);
        }

        public virtual async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var data = await this.entities.ToListAsync();
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
            OperationResult operationResult = new OperationResult();
            try
            {
                var data = await this.entities.Where(filter).ToListAsync();
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
            OperationResult result = new OperationResult();
            try
            {
                var entity = await this.entities.FindAsync(id);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error has ocurred {ex.Message} obtaining the entity.";
            }
            return result;
        }

        public virtual async Task<OperationResult> Remove(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                entities.Remove(entity);
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
            OperationResult result = new OperationResult();
            try
            {
                entities.Add(entity);
                await _medicalAppointmentContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error has ocurred {ex.Message} saving the entity.";
            }
            return result;
        }

        public virtual async Task<OperationResult> Update(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                entities.Update(entity);
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
