using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Entities.EntitiesDB;
using TaskManager.Entities.EntitiesDTO;
using TaskManager.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TaskManager.Repository
{
   /// <summary>
    /// Repository to manipulate the user entity in the database
    /// </summary>
    public class UserRepository : IUserRepository {

        #region Properties
        private TaskManagerContext context = null;
        #endregion

        #region Ctor
        public UserRepository (TaskManagerContext ctx) {
            this.context = ctx;
        }

        #endregion        

        #region Methods
        public async Task DeleteUserAsync (long seqUser) {
            try {
                await Task.Run(async () =>
                {
                    var entity = await this.context.USER.FirstOrDefaultAsync(s => s.SEQUSER == seqUser);
                    this.context.USER.Remove(entity);
                    await this.context.SaveChangesAsync();
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<List<UserResponseDTO>> GetUsersAsync (Expression<Func<USER,bool>> conditions= null) {
            try {
                if(conditions != null)
                    return await context.USER
                        .Where(conditions)
                        .OrderBy(x=> x.NAME)
                        .Select (user =>
                            new UserResponseDTO {
                                NAME = user.NAME,
                                EMAIL = user.EMAIL,
                                SEQUSER = user.SEQUSER,
                                DATEREGISTER = user.DATEREGISTER
                            })
                        .AsNoTracking().ToListAsync();
                else
                    return await context.USER
                        .OrderBy(x=> x.NAME)
                        .Select (user =>
                            new UserResponseDTO {
                                NAME = user.NAME,
                                EMAIL = user.EMAIL,
                                SEQUSER = user.SEQUSER,
                                DATEREGISTER = user.DATEREGISTER
                            })
                        .AsNoTracking().ToListAsync();

            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

         public async Task<List<UserResponseDTO>> GetUsersAsync (int pageNo, int pageSize, Expression<Func<USER,bool>> conditions= null) {
            try {
                return await Task.Run(async ()=>{

                    var skip = (pageNo - 1) * pageSize;

                    if(conditions != null){
                        return await context.USER
                            .Where(conditions)
                            .OrderBy(x=> x.NAME)
                            .Skip(skip).Take(pageSize)
                            .Select(user =>
                                new UserResponseDTO {
                                    NAME = user.NAME,
                                    EMAIL = user.EMAIL,
                                    SEQUSER = user.SEQUSER,
                                    DATEREGISTER = user.DATEREGISTER
                            }).ToListAsync();
                    }                  
                    else{
                        return await context.USER
                            .OrderBy(x=> x.NAME)
                            .Skip(skip).Take(pageSize)
                            .Select(user =>
                                new UserResponseDTO {
                                    NAME = user.NAME,
                                    EMAIL = user.EMAIL,
                                    SEQUSER = user.SEQUSER,
                                    DATEREGISTER = user.DATEREGISTER
                            }).ToListAsync();
                    }
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<UserResponseDTO> GetUsersBySeqUserAsync (long seqUser) {
            try {
                return await context.USER.Where(x=> x.SEQUSER== seqUser).OrderBy(x=> x.NAME).Select (user =>
                    new UserResponseDTO {
                        NAME = user.NAME,
                        EMAIL = user.EMAIL,
                        SEQUSER = user.SEQUSER,
                        DATEREGISTER = user.DATEREGISTER
                    }).AsNoTracking().FirstOrDefaultAsync();              
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<UserResponseDTO> SaveUserAsync (userRegisterDTO model) {
            try {
                return await Task.Run(async () =>
                {
                    USER user = await ConvertResquestDtoToEntityDataBase(model);
                    await this.context.USER.AddAsync(user);
                    return await this.context.SaveChangesAsync() > 0 ? await ConvertEntityDataBaseToResponseDTO(user) : null;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<UserResponseDTO> UpdateUserAsync (userRegisterDTO model) {
            try {
                return await Task.Run(async () =>
                {
                    USER user = await ConvertResquestDtoToEntityDataBase(model);
                    this.context.USER.Update(user);
                    return await this.context.SaveChangesAsync() > 0 ? await ConvertEntityDataBaseToResponseDTO(user) : null;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        /// <summary>
        /// Convert dto to entity user of database
        /// </summary>
        /// <param name="modelDTO">dto to convert</param>
        /// <returns>entity "TASk" of database</returns>
        private async Task<USER> ConvertResquestDtoToEntityDataBase (userRegisterDTO modelDTO) {
            try {
                return await Task.Run(() =>
                {
                    USER user = new USER
                    {
                        NAME = modelDTO.NAME,
                        EMAIL = modelDTO.EMAIL,
                        PASSWORD = modelDTO.PASSWORD,
                        DATEREGISTER = DateTime.Now                        
                    };
                    return user;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

       /// <summary>
       /// Convert entity of database response dto of user
       /// </summary>
       /// <param name="modelDB">entity of database to convert</param>
       /// <returns>response dto of user </returns>     
        private async Task<UserResponseDTO> ConvertEntityDataBaseToResponseDTO (USER modelDB) {
            try {
                return await Task.Run( ()=> {
                    UserResponseDTO modelResponse = null;
                    if (modelDB != null)
                        modelResponse = new UserResponseDTO {
                            NAME = modelDB.NAME,
                            EMAIL = modelDB.EMAIL,
                            SEQUSER = modelDB.SEQUSER,
                            DATEREGISTER = modelDB.DATEREGISTER
                        };
                return modelResponse;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }       
        #endregion

    }    
}