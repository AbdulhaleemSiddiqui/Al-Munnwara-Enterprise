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
    public class TaxController : ControllerBase
    {
        [HttpPost]
        [Route("SaveTax")]
        public async Task<IActionResult> SaveTax([FromBody] Tax request)
        {
            try
            {
                var result = await Task.Run(() => new TaxProcessor().SaveTax(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateTax")]
        public async Task<IActionResult> UpdateTax([FromBody] Tax request)
        {
            try
            {
                var result = await Task.Run(() => new TaxProcessor().UpdateTax(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("GetTax")]
        public async Task<IActionResult> GetTax(TaxListRequest request)
        {
            try
            {
                var result = await Task.Run(() => new TaxProcessor().GetTax(request));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("DeleteTax")]
        public async Task<IActionResult> DeleteTax([FromQuery] int id)
        {
            try
            {
                var result = await Task.Run(() => new TaxProcessor().DeleteTax(id));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
