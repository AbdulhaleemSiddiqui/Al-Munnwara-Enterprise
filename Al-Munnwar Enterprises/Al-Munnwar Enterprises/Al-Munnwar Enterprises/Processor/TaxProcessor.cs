using Al_Munnwar_Enterprises.Data;
using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Module.Internal;

using System.Collections.Generic;


namespace Al_Munnwar_Enterprises.Processor
{
    public class TaxProcessor
    {
        public string SaveTax(Tax request)
        {
            TaxData iD = new TaxData();
            iD.SaveTax(request);
            return "ok";
        }
        public string UpdateTax(Tax request)
        {
            TaxData iD = new TaxData();
            iD.UpdateTax(request);
            return "ok";
        }
        public string DeleteTax(int id)
        {
            TaxData iD = new TaxData();
            iD.DeleteTax(id);
            return "ok";
        }
        public TaxListResponse GetTax(TaxListRequest request)
        {
            TaxData iD = new TaxData();
            TaxListResponse response = new TaxListResponse();
            response.Tax = new List<Tax>();

            #region request mapping 
            PageModel page = new PageModel();
            page.pageNumber = request.pageNumber;
            page.pageSize = request.pageSize;
            page.isPagining = request.isPagining;
            page.sortBy = request.sortBy;
            page.isSortAsc = request.isSortAsc;
            #endregion
            int rowCount = 0;
            var result = iD.GetTax(page, request.type, ref rowCount);
            response.totalRecords = rowCount;
            foreach (var Tax in result)
            {
                response.Tax.Add(new Tax
                {
                    amount = Tax.amount, 
                    type = Tax.type,

                    id = Tax.id

                });
            }



            return response;
        }


    }
}
