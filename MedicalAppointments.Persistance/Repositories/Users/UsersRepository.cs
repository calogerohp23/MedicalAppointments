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
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;

                await base.Save(entity);

                operationResult.Success = true;
                operationResult.Message = "The user was saved successfully";
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
            if (await base.Exists(users => users.UserID == id))
            {
                operationResult.Success = false;
                operationResult.Message = "The selected ID does not exist";
                return operationResult;
            }
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
                usersToUpdate.UpdatedAt = DateTime.Now;
                usersToUpdate.UpdatedBy = entity.UpdatedBy;
                usersToUpdate.IsActive = entity.IsActive;

                await _medicalAppointmentContext.SaveChangesAsync();
                operationResult.Success = true;
                operationResult.Message = "The user was updated succesfuly";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error updating the user";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> Remove(int id, Domain.Entities.Users.Users entity)
        {
            OperationResult operationResult = new();
            if (await base.Exists(users => users.UserID == id))
            {
                operationResult.Success = false;
                operationResult.Message = "The selected ID does not exist";
                return operationResult;
            }
            try
            {
                Domain.Entities.Users.Users? usersToRemove = await _medicalAppointmentContext.Users.FindAsync(id);
                usersToRemove.IsActive = false;
                usersToRemove.UpdatedAt = entity.UpdatedAt;
                usersToRemove.UpdatedBy = entity.UpdatedBy;
                
                await _medicalAppointmentContext.SaveChangesAsync();
                
                operationResult.Success = true;
                operationResult.Message = "The user was updated succesfuly";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error removing the user";
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
                operationResult.Success = true;
                operationResult.Message = "The users were found!";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error finding all the users";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async override Task<OperationResult> GetEntityBy(int id)
        {            
            OperationResult operationResult = new();
            if (await base.Exists(users => users.UserID == id)){
                operationResult.Success = false;
                operationResult.Message = "The selected ID does not exist";
                return operationResult;
            }
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
                operationResult.Success = true;
                operationResult.Message = "The query was succesful";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "There was an error finding the user";
                logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}