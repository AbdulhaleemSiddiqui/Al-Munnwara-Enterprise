using Al_Munnwar_Enterprises.Data;
using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Module.Internal;
using System;
using System.Collections.Generic;

namespace Al_Munnwar_Enterprises.Processor
{
    public class QuotationProcessor
    {
        public string SaveQuotation(QuotationRequest request)
        {
            try
            {
                QuotationData iD = new QuotationData();
                iD.SaveQuotation(request);
                return "ok";
            }
            catch (Exception)
            {

                throw;
            }
        
        }
        public string UpdateQuotation(QuotationRequest request)
        { 
           QuotationData iD = new QuotationData();
            iD.UpdateQuotation(request);
            return "ok";
        }
        public string UpdateStatus(QuotationRequest request)
        { 
           QuotationData iD = new QuotationData();
            iD.UpdateStatus(request);
            return "ok";
        }
        public string DeleteQuotation(int id)
        {
           QuotationData iD = new QuotationData();
            iD.DeleteQuotation(id);
            return "ok";
        }
        public QuotationListResponse GetQuotation(QuotationListRequest request)
        {
           QuotationData iD = new QuotationData();
           QuotationListResponse response = new QuotationListResponse();
            response.Quotation = new List<QuotationResponse>();

            #region request mapping 
            PageModel page = new PageModel();
            page.pageNumber = request.pageNumber;
            page.pageSize = request.pageSize;
            page.isPagining = request.isPagining;
            page.sortBy = request.sortBy;
            page.isSortAsc = request.isSortAsc;
            #endregion
            int rowCount = 0;
            var result = iD.GetQuotation(page, request.itemName, ref rowCount);
            response.totalRecords = rowCount;
            foreach (var Quotation in result)
            {
                response.Quotation.Add(new QuotationResponse
                {
                    id = Quotation.id,
                    qty = Quotation.qty,
                    status = Quotation.status,
                    rate = Quotation.rate,
                    iName = Quotation.iName,
                    uName = Quotation.uName,
                    type = Quotation.type,
                    date= Quotation.date
                });
            }



            return response;
        }
        public List<Tax> GetTax()
        {
           QuotationData iD = new QuotationData();
            var response = iD.GetTax("");
            return response;
        }

        public List<ItemResponse> GetItem()
        {
            QuotationData iD = new QuotationData();
            var response = iD.GetItem();
            return response;
        }

    }
}
