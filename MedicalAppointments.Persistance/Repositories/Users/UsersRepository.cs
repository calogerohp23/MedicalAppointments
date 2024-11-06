using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Users;
using MedicalAppointments.Persistance.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace MedicalAppointments.Persistance.Repositories.Users
{
    public class UsersRepository : BaseRepository<Domain.Entities.Users.Users>, IUsersRepository
    {
        private readonly MedicalAppointmentContext _medicalAppointmentContext;
        private readonly ILogger<UsersRepository> logger;

        public UsersRepository(MedicalAppointmentContext medicalAppointmentContext, ILogger<UsersRepository> logger) : base(medicalAppointmentContext)
        {
            _medicalAppointmentContext = medicalAppointmentContext;
            this.logger = logger;
        }

        public async override Task<OperationResult> Save(Domain.Entities.Users.Users entity)
        {
            OperationResult operationResult = new();
            try
            {
                await base.Save(entity);
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the user";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Update(int id,Domain.Entities.Users.Users entity)
        {
            OperationResult operationResult = new();
            try
            {
                Domain.Entities.Users.Users? usersToUpdate = await _medicalAppointmentContext.Users.FindAsync(id);
                usersToUpdate.FirstName = entity.FirstName;
                usersToUpdate.LastName = entity.LastName;
                usersToUpdate.Email = entity.Email;
                usersToUpdate.Password = entity.Password;
                usersToUpdate.RoleId = entity.RoleId;
                usersToUpdate.CreatedAt = entity.CreatedAt;
                usersToUpdate.CreatedBy = entity.CreatedBy;
                usersToUpdate.UpdatedAt = entity.UpdatedAt;
                usersToUpdate.UpdatedBy = entity.UpdatedBy;
                usersToUpdate.IsActive = entity.IsActive;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(int id, Domain.Entities.Users.Users entity)
        {
            OperationResult operationResult = new();
            try
            {
                Domain.Entities.Users.Users? usersToRemove = await _medicalAppointmentContext.Users.FindAsync(id);
                usersToRemove.IsActive = false;
                usersToRemove.UpdatedAt = entity.UpdatedAt;
                usersToRemove.UpdatedBy = entity.UpdatedBy;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from users in _medicalAppointmentContext.Users
                                              join roles in _medicalAppointmentContext.Roles on users.RoleId equals roles.RoleID
                                              where users.IsActive == true
                                              select new UserRoleModel()
                                              {
                                                  UserID = users.UserID,
                                                  FirstName = users.FirstName,
                                                  LastName = users.LastName,
                                                  Email = users.Email,
                                                  Password = users.Password,
                                                  RoleType = roles.RoleName,
                                                  CreatedAt = users.CreatedAt,
                                                  CreatedBy = users.CreatedBy,
                                                  UpdatedAt = users.UpdatedAt,
                                                  UpdatedBy = users.UpdatedBy
                                              }).AsNoTracking()
                                              .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {
            OperationResult operationResult = new();
            try
            {
                operationResult.Data = await (from users in _medicalAppointmentContext.Users
                                              join roles in _medicalAppointmentContext.Roles on users.RoleId equals roles.RoleID
                                              where users.IsActive == true && users.UserID == id
                                              select new UserRoleModel()
                                              {
                                                  UserID = users.UserID,
                                                  FirstName = users.FirstName,
                                                  LastName = users.LastName,
                                                  Email = users.Email,
                                                  Password = users.Password,
                                                  RoleType = roles.RoleName,
                                                  CreatedAt = users.CreatedAt,
                                                  CreatedBy = users.CreatedBy,
                                                  UpdatedAt = users.UpdatedAt,
                                                  UpdatedBy = users.UpdatedBy
                                              }).AsNoTracking()
                              .ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error saving the doctor";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}