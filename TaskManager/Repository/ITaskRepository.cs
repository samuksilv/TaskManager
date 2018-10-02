using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Entities.EntitiesDB;
using TaskManager.Entities.EntitiesDTO;

namespace TaskManager.Repository
{
    /// <summary>
    /// Repository to manipulate the task entities in the database
    /// </summary>
    public interface ITaskRepository 
     {
        /// <summary>
        /// Save new task on database 
        /// </summary>
        /// <param name="model">model to insert in database</param>
        /// <returns>entity saved in the database</returns>  
        Task<TaskResponseDTO> SaveTaskAsync(TaskDTO model);

        /// <summary>
        /// Delete task on database
        /// </summary>
        /// <param name="seqTask">sequence of task to delete of database</param>
        /// <returns>none</returns>
        Task DeleteTaskAsync (long seqTask);

        /// <summary>
        /// Update task on database
        /// </summary>
        /// <param name="model">model to Update in database</param>
        /// <returns>entity updated in the database</returns>
        Task<TaskResponseDTO> UpdateTaskAsync (TaskDTO model);


        /// <summary>
        /// get list of task from database
        /// </summary>    
        /// <param>conditions for clause "Where"</param>   
        /// <returns>list of tasks in database</returns>
        Task<List<TaskResponseDTO>> GetTasksAsync(Expression<Func<TASK,bool>> conditions= null);

        /// <summary>
        /// get task by sequence of user on database
        /// </summary>
        /// <param name="seqUser">sequence of user</param>
        /// <returns>list of tasks in database</returns>
        Task<List<TaskResponseDTO>> GetTasksBySeqUserAsync (long seqUser);

        /// <summary>
        /// get task by sequence of task on database
        /// </summary>
        /// <param name="seqTask">sequence of task</param>
        /// <returns>task with sequence is equals at parameter seqTask </returns>
        Task<TaskResponseDTO> GetTasksBySeqTaskAsync (long seqTask);

        /// <summary>
        /// get task by sequence and specific period 
        /// </summary>
        /// <param name="initialDate"> initial date of tasks</param>
        /// <param name="lastDate">last date of tasks</param>
        /// <param name="seqUser">sequence of user</param>
        /// <returns>tasks of user with specific period</returns>
        Task<List<TaskResponseDTO>> GetTasksByPeriodAsync (DateTime initialDate, DateTime lastDate, long seqUser);
    }
}