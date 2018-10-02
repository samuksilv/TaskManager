using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TaskManager.Context;
using TaskManager.Repository;

namespace TaskManager.UnitOfWork
{
     public class UnitOfWork : IUnitOfWork
    {
        #region Properties        
        private TaskManagerContext _context= null;
        private IDbContextTransaction _transaction ;
        private bool _disposed;

        public IDbContextTransaction Transaction 
        {
            get
            {                
                DbContext dbContext = _context as DbContext;
                if (dbContext.Database.CurrentTransaction == null)                
                    _transaction = dbContext.Database.BeginTransaction();
                return _transaction;                
            }
        }
        #endregion

        #region Ctor
        public UnitOfWork(TaskManagerContext context)
        {
            this._disposed= false;
            this._context= context;
            DbContext dbContext = _context as DbContext;
            _transaction = dbContext.Database.CurrentTransaction;
            if (_transaction == null)
                _transaction = dbContext.Database.BeginTransaction();
        }
            
        #endregion
        
        #region Repositories        
        private ITaskRepository _taskRepository;
        public ITaskRepository TaskRepository 
        {
            get
            {
                if (_taskRepository == null)
                    _taskRepository = new TaskRepository(this._context);

                return _taskRepository;
            }
        }
        private IUserRepository _userRepository;
        public IUserRepository UserRepository 
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(this._context);

                return _userRepository;
            }
        }
        #endregion

        #region Methods        

        public void Commit()
        {
            Transaction?.Commit();
        }                

        public void RollBack()
        {
            Transaction?.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this._disposed)
            {               
                DbContext dbContext = _context as DbContext;
                Transaction?.Dispose();                    
                dbContext.Dispose();                

                this._disposed = true;
            }
        }

        #endregion

    }
}