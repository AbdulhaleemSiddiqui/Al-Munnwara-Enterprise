using Al_Munnwar_Enterprises.Data;
using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Module.Internal;
using System.Collections.Generic;


namespace Al_Munnwar_Enterprises.Processor
{
    public class PurchaseProcessor
    {
        public string SavePurchase(PurchaseRequest request)
        {
            PurchaseData iD = new PurchaseData();
            iD.SavePurchase(request);
            return "ok";
        }

        public string UpdatePurchase(PurchaseRequest request)
        {
            PurchaseData iD = new PurchaseData();
            iD.Update(request);
            return "ok";
        }
        public string DeletePurchase(int id)
        {
            ItemData iD = new ItemData();
            iD.DeleteItem(id);
            return "ok";
        }

        public PurchaseListResponse GetPurchase(PurchaseListRequest request)
        {
            PurchaseListResponse response = new PurchaseListResponse();
            response.item = new List<PurchaseResponse>();
            PurchaseData iD = new PurchaseData();
            #region request mapping 
            PageModel page = new PageModel();
            page.pageNumber = request.pageNumber;
            page.pageSize = request.pageSize;
            page.isPagining = request.isPagining;
            page.sortBy = request.sortBy;
            page.isSortAsc = request.isSortAsc;
            #endregion
            int rowCount = 0;
            var result = iD.Get(page, request.name, ref rowCount);
            response.totalRecords = rowCount;
            foreach (var item in result)
            {
                response.item.Add(new PurchaseResponse
                {
                    date = item.date,
                    ExpanceAmount = item.ExpanceAmount,
                    Expancename = item.Expancename,
                    ItemName = item.ItemName,
                    ItemPrice = item.ItemPrice,
                    id = item.id,

                });
            }



            return response;
        }

        public ItemListResponse GetItem()
        {
            PurchaseData iD = new PurchaseData();
            ItemListResponse response = new ItemListResponse();
            response.item = new List<ItemResponse>();


            var result = iD.GetItem();
            foreach (var item in result)
            {
                response.item.Add(new ItemResponse
                {
                    date = item.date,
                    name = item.name,
                    unit = item.unit,
                    vendor = item.vendor,
                    price = item.price,
                    id = item.id

                });
            }

            return response;
        }
    }
}
