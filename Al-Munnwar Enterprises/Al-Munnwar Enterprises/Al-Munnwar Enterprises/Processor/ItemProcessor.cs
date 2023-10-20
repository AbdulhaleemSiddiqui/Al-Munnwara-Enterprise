using Al_Munnwar_Enterprises.Data;
using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Module.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Munnwar_Enterprises.Processor
{
    public class ItemProcessor
    {
        public string SaveItem(ItemRequest request)
        {
            ItemData  iD = new ItemData();
            iD.SaveItem(request);
            return "ok";
        }
        public string UpdateItem(ItemRequest request)
        {
            ItemData iD = new ItemData();
            iD.UpdateItem(request);
            return "ok";
        }
        public string DeleteItem(int id)
        {
            ItemData iD = new ItemData();
            iD.DeleteItem(id);
            return "ok";
        }
        public ItemListResponse GetItem(ItemListRequest request)
        {
            ItemData iD = new ItemData();
            ItemListResponse response = new ItemListResponse();
            response.item = new List<ItemResponse>();

            #region request mapping 
            PageModel page = new PageModel();
            page.pageNumber = request.pageNumber;
             page.pageSize = request.pageSize;
            page.isPagining = request.isPagining;
            page.sortBy = request.sortBy;
            page.isSortAsc = request.isSortAsc;
            #endregion
            int rowCount = 0;
           var result =iD.GetItem(page,request.name,request.vendorName,ref rowCount);
            response.totalRecords = rowCount;
            foreach (var item in result)
            {
                response.item.Add(new ItemResponse
                {
                    date=item.date,
                    name=item.name,
                    unit=item.unit,
                    vendor=item.vendor,
                    price=item.price,
                    id=item.id

                });
            }
          


            return response;
        }
        public List<Vendor> GetVendor()
        {
            ItemData iD = new ItemData();
            var response=iD.GetVendor();
            return response;
        }
        public List<Unit> GetUnit()
        {
            ItemData iD = new ItemData();
            var response=iD.GetUnit();
            return response;
        }
   
    }
}
