using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.business;
using TaskManager.Context;
using TaskManager.Entities.EntitiesDTO;

namespace TaskManager.Controllers
{
   [Route ("api/user")]
    public class UserController : ControllerBase {
        private TaskManagerContext context = null;
        public UserController (TaskManagerContext ctx) {
            context = ctx;
        }

        [HttpGet]
        [Route ("users")]
        public async Task<IActionResult> Get () {
            try {
                return await Task.Run (async () => {
                    using (IService business = new Service (this.context)) {
                        return Ok (await business.GetUsersAsync ());
                    }
                });
            } catch (Exception ex) {
                return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try {
                return await Task.Run (async () => {
                    using (IService business = new Service (this.context)) {
                        return Ok (await business.GetUserBySequenceAsync(id));
                    }
                });
            } catch (Exception ex) {
                return StatusCode ((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route ("Users")]
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

    }
}