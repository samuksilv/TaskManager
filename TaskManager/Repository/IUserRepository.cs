using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Entities.EntitiesDB;
using TaskManager.Entities.EntitiesDTO;

namespace TaskManager.Repository
{
     /// <summary>
    /// Repository to manipulate the user entity in the database
    /// </summary>
    public interface IUserRepository 
    {   
        /// <summary>
        /// Save new user on database
        /// </summary>
        /// <param name="model">model to insert in database</param>
        /// <returns>entity saved in the database</returns>     
        Task<UserResponseDTO> SaveUserAsync (userRegisterDTO model);

        /// <summary>
        /// Delete user on database
        /// </summary>
        /// <param name="seqUser">sequence of user to delete</param>
        /// <returns>none</returns>
        Task DeleteUserAsync (long seqUser);

        /// <summary>
        /// Update user on database
        /// </summary>
        /// <param name="model">model to update in database</param>
        /// <returns>entity updated in the database</returns>
        Task<UserResponseDTO> UpdateUserAsync (userRegisterDTO model);

        /// <summary>
        /// get list of user on database
        /// </summary>   
        /// <param>conditions for clause "Where"</param>    
        /// <returns>list of users</returns>
        Task<List<UserResponseDTO>> GetUsersAsync (Expression<Func<USER,bool>> conditions= null);

        /// <summary>
        /// get list of user on database, with pagination
        /// </summary>
        /// <param name="pageNo">page number </param>
        /// <param name="pageSize">size of page </param>
        /// <param name="conditions">conditions for clause "Where"</param>
        /// <returns>list of users</returns>
        Task<List<UserResponseDTO>> GetUsersAsync (int pageNo, int pageSize,Expression<Func<USER,bool>> conditions= null);
        /// <summary>
        /// get user by sequence
        /// </summary>
        /// <param name="seqUser">sequence of user</param>
        /// <returns>user with sequence equals parameter seqUser</returns>
        Task<UserResponseDTO> GetUsersBySeqUserAsync (long seqUser);
    }
}