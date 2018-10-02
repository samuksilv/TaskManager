using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Entities.EntitiesDTO;

namespace TaskManager.business
{

    /// <summary>
    /// Business methods
    /// </summary>
    public interface IService : IDisposable {
        #region user methods

        /// <summary>   
        /// save new user on database
        /// </summary>
        /// <param name="model">user to save</param>
        /// <returns>user saved on database</returns>
        Task<UserResponseDTO> RegisterUserAsync (userRegisterDTO model);

        /// <summary>
        /// delete user from database
        /// </summary>
        /// <param name="seq">sequence of user</param>
        /// <returns>none</returns>
        Task DeleteUserAsync (long seq);

        /// <summary>
        /// update user on database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>user updated on databse</returns>
        Task<UserResponseDTO> UpdateRegisterUserAsync (userRegisterDTO model);

        /// <summary>
        /// query to get all users
        /// </summary>
        /// <returns>list od users</returns>
        Task<List<UserResponseDTO>> GetUsersAsync ();

        /// <summary>
        /// query to get one user by sequence value
        /// </summary>
        /// <param name="seq">sequence of user</param>
        /// <returns>user with specific sequence</returns>
        Task<UserResponseDTO> GetUserBySequenceAsync (long seq);

        #endregion
        #region task methods

        /// <summary>
        /// save task on database
        /// </summary>
        /// <param name="model">task to insert</param>
        /// <returns>task saved on database</returns>
        Task<TaskResponseDTO> SaveTaskAsync (TaskDTO model);

        /// <summary>
        /// delete task from database
        /// </summary>
        /// <param name="seq">sequence of task</param>
        /// <returns>none</returns>
        Task DeleteTaskAsync (long seq);

        /// <summary>
        /// update task on database
        /// </summary>
        /// <param name="model">task to update</param>
        /// <returns>task updated on database</returns>
        Task<TaskResponseDTO> UpdateTaskAsync (TaskDTO model);

        /// <summary>
        /// query to get task 
        /// </summary>
        /// <returns>list of tasks</returns>
        Task<List<TaskResponseDTO>> GetTasksAsync ();

        /// <summary>
        /// query to get one task by sequence value
        /// </summary>
        /// <param name="seq">sequence of task</param>
        /// <returns>task with specific sequence</returns>
        Task<TaskResponseDTO> GetTaskBySequenceAsync (long seq);

        /// <summary>
        /// quey to get task with specific interval of time
        /// </summary>
        /// <param name="initialDate"> date initial of filter</param>
        /// <param name="lastDate">last date of filter</param>
        /// <param name="seqUser">sequence of user</param>
        /// <returns>list of tasks with specific period</returns>
        Task<List<TaskResponseDTO>> GetTaskByPeriodAsync (DateTime initialDate, DateTime lastDate, long seqUser);
            
        #endregion
    }
}