using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Processor;
using Microsoft.AspNetCore.Mvc;

using System;

using System.Threading.Tasks;

namespace Al_Munnwar_Enterprises.Controllers
{
    [ApiController]
    [Route("api/")]
    public class QuotationController : ControllerBase
    {
        [HttpPost]
        [Route("SaveQuotation")]
        public async Task<IActionResult> SaveQuotation([FromBody] QuotationRequest request)
        {
            try
            {
                var result = await Task.Run(() => new QuotationProcessor().SaveQuotation(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateQuotation")]
        public async Task<IActionResult> UpdateQuotation([FromBody] QuotationRequest request)
        {
            try
            {
                var result = await Task.Run(() => new QuotationProcessor().UpdateQuotation(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] QuotationRequest request)
        {
            try
            {
                var result = await Task.Run(() => new QuotationProcessor().UpdateStatus(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("GetQuotation")]
        public async Task<IActionResult> GetQuotation(QuotationListRequest request)
        {
            try
            {
                var result = await Task.Run(() => new QuotationProcessor().GetQuotation(request));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }
        
        [HttpGet]
        [Route("GetItem")]
        public async Task<IActionResult> GetItem()
        {
            try
            {
                var result = await Task.Run(() => new QuotationProcessor().GetItem());
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetTax")]
        public async Task<IActionResult> GetTax()
        {
            try
            {
                var result = await Task.Run(() => new QuotationProcessor().GetTax());
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("DeleteQuotation")]
        public async Task<IActionResult> DeleteQuotation([FromQuery] int id)
        {
            try
            {
                var result = await Task.Run(() => new QuotationProcessor().DeleteQuotation(id));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
