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
    public class UnitController : ControllerBase
    {
        [HttpPost]
        [Route("SaveUnit")]
        public async Task<IActionResult> SaveUnit([FromBody] Unit request)
        {
            try
            {
                var result = await Task.Run(() => new UnitProcessor().SaveUnit(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateUnit")]
        public async Task<IActionResult> UpdateUnit([FromBody] Unit request)
        {
            try
            {
                var result = await Task.Run(() => new UnitProcessor().UpdateUnit(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("GetUnit")]
        public async Task<IActionResult> GetUnit(UnitListRequest request)
        {
            try
            {
                var result = await Task.Run(() => new UnitProcessor().GetUnit(request));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("DeleteUnit")]
        public async Task<IActionResult> DeleteUnit([FromQuery] int id)
        {
            try
            {
                var result = await Task.Run(() => new UnitProcessor().DeleteUnit(id));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
