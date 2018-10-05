using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Context;
using TaskManager.Entities.EntitiesDTO;
using TaskManager.UnitOfWork;

namespace TaskManager.business
{
        public class Service : IService {
        #region Properties  
        private IUnitOfWork _unitOfWork = null;
        #endregion
        #region Ctor

        public Service (IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }

        #endregion
        #region Methods
        #region Methods for user

        public async Task<UserResponseDTO> RegisterUserAsync (userRegisterDTO model) {
            try {
                return await Task.Run (async () => {
                    var response = await this._unitOfWork.UserRepository.SaveUserAsync (model);
                    this._unitOfWork.Commit ();
                    return response;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task DeleteUserAsync (long seq) {
            try {
                await Task.Run (async () => {
                    await this._unitOfWork.UserRepository.DeleteUserAsync (seq);
                    this._unitOfWork.Commit ();
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<UserResponseDTO> UpdateRegisterUserAsync (userRegisterDTO model) {
            try {
                return await Task.Run (async () => {
                    var response = await this._unitOfWork.UserRepository.UpdateUserAsync (model);
                    this._unitOfWork.Commit ();
                    return response;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<List<UserResponseDTO>> GetUsersAsync () {
            try {
                return await this._unitOfWork.UserRepository.GetUsersAsync ();
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

         public async Task<List<UserResponseDTO>> GetUsersByNameAsync (string name) {
            try {
                return  await this._unitOfWork.UserRepository
                        .GetUsersAsync (x=> x.NAME.ToLower().Contains(name.ToLower()));
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }


        public async Task<UserResponseDTO> GetUserBySequenceAsync (long seq) {
            try {
                return await this._unitOfWork.UserRepository.GetUsersBySeqUserAsync (seq);
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        #endregion
        #region Methods for task

        public async Task<TaskResponseDTO> SaveTaskAsync (TaskDTO model) {
            try {
                return await Task.Run (async () => {
                    var response = await this._unitOfWork.TaskRepository.SaveTaskAsync (model);
                    this._unitOfWork.Commit ();
                    return response;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task DeleteTaskAsync (long seq) {
            try {
                await Task.Run (async () => {
                    await this._unitOfWork.TaskRepository.DeleteTaskAsync (seq);
                    this._unitOfWork.Commit ();
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<TaskResponseDTO> UpdateTaskAsync (TaskDTO model) {
            try {
                return await Task.Run (async () => {
                    TaskResponseDTO response = await this._unitOfWork.TaskRepository.UpdateTaskAsync (model);
                    this._unitOfWork.Commit ();
                    return response;
                });
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<List<TaskResponseDTO>> GetTasksAsync () {
            try {
                return await this._unitOfWork.TaskRepository.GetTasksAsync ();
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<TaskResponseDTO> GetTaskBySequenceAsync (long seq) {
            try {
                return await this._unitOfWork.TaskRepository.GetTasksBySeqTaskAsync (seq);
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public async Task<List<TaskResponseDTO>> GetTaskByPeriodAsync (DateTime initialDate, DateTime lastDate, long seqUser) {
            try {
                return await this._unitOfWork.TaskRepository.GetTasksByPeriodAsync(initialDate, lastDate, seqUser);
            } catch (System.Exception ex) {
                throw new ApplicationException (ex.Message);
            }
        }

        public void Dispose () {
            this._unitOfWork.Dispose ();            
        }
        #endregion
        #endregion
    }

}