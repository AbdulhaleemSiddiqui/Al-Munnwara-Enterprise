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
    public class VendorController : ControllerBase
    {
        [HttpPost]
        [Route("SaveVendor")]
        public async Task<IActionResult> SaveVendor([FromBody] Vendor request)
        {
            try
            {
                var result = await Task.Run(() => new VendorProcessor().SaveVendor(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UpdateVendor")]
        public async Task<IActionResult> UpdateVendor([FromBody] Vendor request)
        {
            try
            {
                var result = await Task.Run(() => new VendorProcessor().UpdateVendor(request));
                return new JsonResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("GetVendor")]
        public async Task<IActionResult> GetVendor(VendorListRequest request)
        {
            try
            {
                var result = await Task.Run(() => new VendorProcessor().GetVendor(request));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

       
        [HttpGet]
        [Route("DeleteVendor")]
        public async Task<IActionResult> DeleteVendor([FromQuery] int id)
        {
            try
            {
                var result = await Task.Run(() => new VendorProcessor().DeleteVendor(id));
                return new JsonResult(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
