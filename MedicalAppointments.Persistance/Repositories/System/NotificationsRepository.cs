using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.System;
using MedicalAppointments.Persistance.Models.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedicalAppointments.Persistance.Repositories.Insurance
{
    public class NotificationsRepository : BaseRepository<Notifications>, INotificationsRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<NotificationsRepository> logger;

        public NotificationsRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<NotificationsRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Notifications entity)
        {
            OperationResult operationResult = new();
            if(entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "The entity is null";
                return operationResult;
            }
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex) 
            {
                operationResult.Success = false;
                operationResult.Message = "The Notification couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(Notifications entity)
        {
            OperationResult operationResult = new();
            try
            {
                Notifications? notificationsToUpdate = await _medicalAppointmentContext.Notifications.FindAsync(entity.NotificationId);
                notificationsToUpdate.UserId = entity.UserId;
                notificationsToUpdate.Message = entity.Message;
                notificationsToUpdate.SentAt = entity.SentAt;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Notification couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(Notifications entity)
        {
            OperationResult operationResult = new();
            try
            {
                Notifications? notificationsToRemove = await _medicalAppointmentContext.Notifications.FindAsync(entity.NotificationId);
                notificationsToRemove.UserId = entity.UserId;
                notificationsToRemove.Message = entity.Message;
                notificationsToRemove.SentAt = entity.SentAt;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Notification couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from notifications in _medicalAppointmentContext.Notifications
                                              join users in _medicalAppointmentContext.Users on notifications.UserId equals users.UserID
                                              select new NotificationsModel()
                                              {
                                                  NotificationsID = notifications.NotificationId,
                                                  UserName = users.FirstName + " " + users.LastName,
                                                  Message = notifications.Message,
                                                  SentAt = notifications.SentAt,
                                              }).AsNoTracking()
                                              .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Notification couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new(); try
            {
                operationResult.Data = await (from notifications in _medicalAppointmentContext.Notifications
                                              join users in _medicalAppointmentContext.Users on notifications.UserId equals users.UserID
                                              where notifications.NotificationId == id
                                              select new NotificationsModel()
                                              {
                                                  NotificationsID = notifications.NotificationId,
                                                  UserName = users.FirstName + " " + users.LastName,
                                                  Message = notifications.Message,
                                                  SentAt = notifications.SentAt,
                                              }).AsNoTracking()
                              .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "The Notification couldn't be saved.";
                this.logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }
    }
}