using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.business;
using TaskManager.Context;
using TaskManager.Entities.EntitiesDTO;

namespace TaskManager.Controllers
{
   [Route ("api/user")]
    public class UserController : ControllerBase {
        private TaskManagerContext context = null;
        public UserController (TaskManagerContext ctx, ILogger<UserController> logger ) {
            logger.LogDebug("User Controller is Running");
            context = ctx;
        }

        /// <summary>
        /// Get users async 
        /// </summary>
        /// <returns>list of users</returns>
        [HttpGet]
        [Route ("users")]
        [ProducesResponseType( 200,Type=typeof(List<UserResponseDTO>))]        
        public async Task<IActionResult> Get () {
            try {
                return await Task.Run<IActionResult> (async () => {
                    using (IService business = new Service (this.context)) {
                        var respose=(await business.GetUsersAsync ());
                        if(respose.Count==0)
                            return NotFound();
                        return Ok(respose);
                    }
                });
            } catch (Exception ex) {
                return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get user async by sequence
        /// </summary>
        /// <param name="seq">sequence</param>
        /// <returns>users</returns>
        [HttpGet]
        [Route("users/{seq}")]
        [ProducesResponseType( 200,Type=typeof(UserResponseDTO))]        
        [ProducesResponseType( 404,Type=typeof(UserResponseDTO))]        

        public async Task<IActionResult> Get(long seq)
        {
            try {
                return await Task.Run<IActionResult> (async () => {
                    using (IService business = new Service (this.context)) {
                        var response= await business.GetUserBySequenceAsync(seq);                        
                        if(response== null )
                            return NotFound();                        
                        return Ok (response);                        
                    }
                });
            } catch (Exception ex) {
                return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get users async by name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>users</returns>
        [HttpGet]
        [Route("users/name")]
        [ProducesResponseType( 200,Type=typeof(List<UserResponseDTO>))]        

        public async Task<IActionResult> GetByName(string name)
        {
            try {
                return await Task.Run<IActionResult> (async () => {
                    using (IService business = new Service (this.context)) {
                        var response=(await business.GetUsersByNameAsync(name));
                         if(response.Count==0 )
                            return NotFound();                        
                        return Ok (response);
                    }
                });
            } catch (Exception ex) {
                return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="model">user to insert</param>
        /// <returns>user created</returns>
        [HttpPost]
        [Route ("Users")]
        [ProducesResponseType( 201,Type=typeof(UserResponseDTO))]        
        public async Task<IActionResult> Post ([FromBody] userRegisterDTO model) {
            try {
                return await Task.Run (async() => {
                    using(IService business =new Service(this.context)){                        
                        var response= await business.RegisterUserAsync(model);
                        string url=HttpContext.Request.Host.Value +$"/api/user/users/{response.SEQUSER}";                     
                        Uri urlResponse= new Uri(url );                        
                        return Created(urlResponse, response);
                    }
                });
            } catch (System.Exception ex) {
                return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Edit an existing user
        /// </summary>
        /// <param name="model">user to edit</param>
        /// <returns>user edited</returns>
        [HttpPut]
        [Route ("Users")]        
        [ProducesResponseType( 201,Type=typeof(UserResponseDTO))]        
        public async Task<IActionResult> Put ( [FromBody] userRegisterDTO model) {
            try {
                return await Task.Run (async() => {
                    using(IService business = new Service(this.context)){                        
                        var response= await business.UpdateRegisterUserAsync( model);
                        string url=HttpContext.Request.Host.Value + $"/api/user/users/{response.SEQUSER}";                     
                        Uri urlResponse= new Uri(url );                        
                        return Created(urlResponse, response);
                    }
                });
            } catch (System.Exception ex) {
                return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="seq">sequence of user to delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route ("Users/{seq}")]
        [ProducesResponseType( 204)]        
        public async Task<IActionResult> Delete ( long seq) {
            try {
                return await Task.Run (async() => {
                    using(IService business = new Service(this.context)){                        
                       await business.DeleteUserAsync(seq);                      
                        return NoContent();
                    }
                });
            } catch (System.Exception ex) {
                return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}