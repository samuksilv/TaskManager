using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities.EntitiesDB;
using TaskManager.Entities.EntitiesDTO;
using TaskManager.Context;
using System.Linq;

namespace TaskManager.Repository {
    /// <summary>
    /// Repository to manipulate the task entities in the database
    /// </summary>
    public class TaskRepository : ITaskRepository {
        #region Properties        
        private TaskManagerContext context = null;
        #endregion
        #region Ctor
        public TaskRepository (TaskManagerContext ctx) {
            this.context = ctx;
        }        

        #endregion        
                
        #region Methods  

        public async Task<TaskResponseDTO> SaveTaskAsync (TaskDTO model) {
            try {
                return await Task.Run (async () => {
                    TASK task = await ConvertResquestDtoToEntityDataBase (model);
                    await this.context.TASK.AddAsync (task);
                    return await this.context.SaveChangesAsync () > 0 ? await ConvertEntityDataBaseToResponseDTO (task) : null;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task DeleteTaskAsync (long seqTask) {
            try {
                await Task.Run (async () => {
                    var entity = await this.context.TASK.FirstOrDefaultAsync (s => s.SEQTASK == seqTask);
                    this.context.Entry (entity).State = EntityState.Deleted;
                    await this.context.SaveChangesAsync ();
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<TaskResponseDTO> UpdateTaskAsync (TaskDTO model) {
            try {
                return await Task.Run (async () => {
                    TASK task = await ConvertResquestDtoToEntityDataBase (model);
                    this.context.TASK.Update (task);
                    return await this.context.SaveChangesAsync () > 0 ? await ConvertEntityDataBaseToResponseDTO (task) : null;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<List<TaskResponseDTO>> GetTasksAsync (Expression<Func<TASK, bool>> conditions = null) {
            try {

                if (conditions != null)
                    return await this.context.TASK
                        .Where (conditions)
                        .Select (modelDb =>
                            new TaskResponseDTO {
                                DESCRIPTION = modelDb.DESCRIPTION,
                                    LOCAL = modelDb.LOCAL,
                                    DATEINITIAL = modelDb.DATEINITIAL,
                                    WARNINGTIME = modelDb.WARNINGTIME,
                                    NAME = modelDb.USER.NAME,
                                    SEQTASK = modelDb.SEQTASK,
                                    SEQUSER = modelDb.SEQUSER
                            }).OrderBy (x => x.DATEINITIAL).AsNoTracking ().ToListAsync ();
                else
                    return await this.context.TASK
                        .Select (modelDb =>
                            new TaskResponseDTO {
                                DESCRIPTION = modelDb.DESCRIPTION,
                                    LOCAL = modelDb.LOCAL,
                                    DATEINITIAL = modelDb.DATEINITIAL,
                                    WARNINGTIME = modelDb.WARNINGTIME,
                                    NAME = modelDb.USER.NAME,
                                    SEQTASK = modelDb.SEQTASK,
                                    SEQUSER = modelDb.SEQUSER
                            }).OrderBy (x => x.DATEINITIAL).AsNoTracking ().ToListAsync ();

            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<List<TaskResponseDTO>> GetTasksBySeqUserAsync (long seqUser) {
            try {
                return await GetTasksAsync (Task => Task.SEQUSER == seqUser);
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<TaskResponseDTO> GetTasksBySeqTaskAsync (long seqTask) {
            try {
                return await context.TASK.Select (modelDb =>
                        new TaskResponseDTO {
                            DESCRIPTION = modelDb.DESCRIPTION,
                                LOCAL = modelDb.LOCAL,
                                DATEINITIAL = modelDb.DATEINITIAL,
                                WARNINGTIME = modelDb.WARNINGTIME,
                                NAME = modelDb.USER.NAME,
                                SEQTASK = modelDb.SEQTASK,
                                SEQUSER = modelDb.SEQUSER
                        }).OrderBy (x => x.DATEINITIAL)
                    .AsNoTracking ().FirstOrDefaultAsync (x => x.SEQTASK == seqTask);
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<List<TaskResponseDTO>> GetTasksByPeriodAsync (DateTime initialDate, DateTime lastDate, long seqUser) {
            try {
                return await GetTasksAsync (task => task.DATEINITIAL >= initialDate && task.DATEINITIAL <= lastDate);
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        /// <summary>
        /// Convert dto to entity task of database
        /// </summary>
        /// <param name="modelDTO">dto to convert</param>
        /// <returns>entity "TASk" of database</returns>
        private async Task<TASK> ConvertResquestDtoToEntityDataBase (TaskDTO modelDTO) {
            try {
                return await Task.Run (() => {
                    TASK task = new TASK {
                    DESCRIPTION = modelDTO.DESCRIPTION,
                    LOCAL = modelDTO.LOCAL,
                    DATEINITIAL = modelDTO.DATEINITIAL,
                    WARNINGTIME = modelDTO.WARNINGTIME,
                    SEQUSER = modelDTO.SEQUSER
                    };
                    return task;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        /// <summary>
        /// Convert entity of database response dto of task
        /// </summary>
        /// <param name="modelDB">entity of database to convert</param>
        /// <returns>response dto of task </returns>    
        private async Task<TaskResponseDTO> ConvertEntityDataBaseToResponseDTO (TASK modelDB) {

            try {
                return await Task.Run (() => {
                    TaskResponseDTO modelResponse = null;
                    if (modelDB != null)
                        modelResponse = new TaskResponseDTO {
                            DESCRIPTION = modelDB.DESCRIPTION,
                            LOCAL = modelDB.LOCAL,
                            DATEINITIAL = modelDB.DATEINITIAL,
                            WARNINGTIME = modelDB.WARNINGTIME,
                            NAME = modelDB.USER.NAME,
                            SEQTASK = modelDB.SEQTASK,
                            SEQUSER = modelDB.SEQUSER
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