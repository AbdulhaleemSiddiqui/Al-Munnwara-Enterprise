using Al_Munnwar_Enterprises.Data;
using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Module.Internal;

using System.Collections.Generic;


namespace Al_Munnwar_Enterprises.Processor
{
    public class VendorProcessor
    {
        public string SaveVendor(Vendor request)
        {
            VendorData iD = new VendorData();
            iD.SaveVendor(request);
            return "ok";
        }
        public string UpdateVendor(Vendor request)
        {
            VendorData iD = new VendorData();
            iD.UpdateVendor(request);
            return "ok";
        }
        public string DeleteVendor(int id)
        {
            VendorData iD = new VendorData();
            iD.DeleteVendor(id);
            return "ok";
        }
        public VendorListResponse GetVendor(VendorListRequest request)
        {
            VendorData iD = new VendorData();
            VendorListResponse response = new VendorListResponse();
            response.vendor = new List<Vendor>();

            #region request mapping 
            PageModel page = new PageModel();
            page.pageNumber = request.pageNumber;
            page.pageSize = request.pageSize;
            page.isPagining = request.isPagining;
            page.sortBy = request.sortBy;
            page.isSortAsc = request.isSortAsc;
            #endregion
            int rowCount = 0;
            var result = iD.GetVendor(page, request.name, ref rowCount);
            response.totalRecords = rowCount;
            foreach (var Vendor in result)
            {
                response.vendor.Add(new Vendor
                {
                    name = Vendor.name,
    
                    id = Vendor.id

                });
            }



            return response;
        }
    

    }
}
