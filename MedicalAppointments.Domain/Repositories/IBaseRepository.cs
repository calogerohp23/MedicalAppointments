﻿using MedicalAppointments.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicalAppointments.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<OperationResult> Save(TEntity entity);
        Task<OperationResult> Update(TEntity entity);
        Task<OperationResult> Remove(TEntity entity);
        Task<OperationResult> GetAll();
        Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult> GetEntityBy(int id);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);
    }

}
