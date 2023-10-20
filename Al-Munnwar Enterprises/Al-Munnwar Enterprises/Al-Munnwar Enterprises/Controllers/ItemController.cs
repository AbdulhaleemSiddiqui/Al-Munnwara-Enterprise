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
    public class ItemController : ControllerBase
    {
        [HttpPost]
        [Route("SaveItem")]
        public async Task<IActionResult> SaveItem([FromBody] ItemRequest request)
        {
            try
            {
                var result=await Task.Run(() => new ItemProcessor().SaveItem(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemRequest request)
        {
            try
            {
                var result=await Task.Run(() => new ItemProcessor().UpdateItem(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("GetItem")]
        public async Task<IActionResult> GetItem(ItemListRequest request)
        {
            try
            {
                var result=await Task.Run(() => new ItemProcessor().GetItem(request));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetUnit")]
        public async Task<IActionResult> GetUnit()
        {
            try
            {
                var result = await Task.Run(() => new ItemProcessor().GetUnit());
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetVendor")]
        public async Task<IActionResult> GetVendor()
        {
            try
            {
                var result = await Task.Run(() => new ItemProcessor().GetVendor());
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("DeleteItem")]
        public async Task<IActionResult> DeleteItem([FromQuery] int id)
        {
            try
            {
                var result = await Task.Run(() => new ItemProcessor().DeleteItem(id));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
