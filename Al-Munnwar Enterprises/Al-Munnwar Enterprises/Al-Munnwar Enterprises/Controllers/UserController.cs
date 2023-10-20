using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Processor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Munnwar_Enterprises.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser(User request)
        {
            try
            {
                var result = await Task.Run(() => new UserProcessor().LoginUser(request));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("SaveUser")]
        public async Task<IActionResult> SaveUser([FromBody] User request)
        {
            try
            {
                var result = await Task.Run(() => new UserProcessor().SaveUser(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User request)
        {
            try
            {
                var result = await Task.Run(() => new UserProcessor().UpdateUser(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(UserListRequest request)
        {
            try
            {
                var result = await Task.Run(() => new UserProcessor().GetUser(request));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            try
            {
                var result = await Task.Run(() => new UserProcessor().DeleteUser(id));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
