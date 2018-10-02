using System;
using Microsoft.EntityFrameworkCore.Storage;
using TaskManager.Repository;

namespace TaskManager.UnitOfWork
{
     public interface IUnitOfWork: IDisposable
    {
        #region Properties                                
        
        IDbContextTransaction Transaction{get;}

        #region Repositories
        ITaskRepository TaskRepository{get;}
        IUserRepository UserRepository{get;}            
        #endregion

        #endregion       

        #region Methods
        void Commit();
        void RollBack();                   
        #endregion
    }
}