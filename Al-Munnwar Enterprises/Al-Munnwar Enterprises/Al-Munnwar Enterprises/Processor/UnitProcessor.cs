using Al_Munnwar_Enterprises.Data;
using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Module.Internal;

using System.Collections.Generic;


namespace Al_Munnwar_Enterprises.Processor
{
    public class UnitProcessor
    {
        public string SaveUnit(Unit request)
        {
            UnitData  iD = new UnitData();
            iD.SaveUnit(request);
            return "ok";
        }
        public string UpdateUnit(Unit request)
        {
            UnitData iD = new UnitData();
            iD.UpdateUnit(request);
            return "ok";
        }
        public string DeleteUnit(int id)
        {
            UnitData iD = new UnitData();
            iD.DeleteUnit(id);
            return "ok";
        }
        public UnitListResponse GetUnit(UnitListRequest request)
        {
            UnitData iD = new UnitData();
            UnitListResponse response = new UnitListResponse();
            response.unit = new List<Unit>();

            #region request mapping 
            PageModel page = new PageModel();
            page.pageNumber = request.pageNumber;
            page.pageSize = request.pageSize;
            page.isPagining = request.isPagining;
            page.sortBy = request.sortBy;
            page.isSortAsc = request.isSortAsc;
            #endregion
            int rowCount = 0;
            var result = iD.GetUnit(page, request.name, ref rowCount);
            response.totalRecords = rowCount;
            foreach (var Unit in result)
            {
                response.unit.Add(new Unit
                {
                    name = Unit.name,

                    id = Unit.id

                });
            }



            return response;
        }


    }
}
