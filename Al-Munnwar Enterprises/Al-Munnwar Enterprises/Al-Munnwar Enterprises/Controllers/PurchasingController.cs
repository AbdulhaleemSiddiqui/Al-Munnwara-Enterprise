using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Processor;
using Microsoft.AspNetCore.Mvc;
using System;

using System.Threading.Tasks;

namespace Al_Munnwar_Enterprises.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PurchasingController : ControllerBase
    {
        [HttpPost]
        [Route("SavePurchasing")]
        public async Task<IActionResult> SavePurchasing([FromBody] PurchaseRequest request)
        {
            try
            {
                var result = await Task.Run(() => new PurchaseProcessor().SavePurchase(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdatePurchase")]
        public async Task<IActionResult> UpdatePurchase([FromBody] PurchaseRequest request)
        {
            try
            {
                var result = await Task.Run(() => new PurchaseProcessor().UpdatePurchase(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("GetPurchase")]
        public async Task<IActionResult> GetPurchase(PurchaseListRequest request)
        {
            try
            {
                var result = await Task.Run(() => new PurchaseProcessor().GetPurchase(request));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("DeletePurchasing")]
        public async Task<IActionResult> DeletePurchasing([FromQuery] int id)
        {
            try
            {
                var result = await Task.Run(() => new PurchaseProcessor().DeletePurchase(id));
                return new JsonResult(result);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("GetItem")]
        public async Task<IActionResult> GetItem(PurchaseListRequest request)
        {
            try
            {
                var result = await Task.Run(() => new PurchaseProcessor().GetItem());
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
