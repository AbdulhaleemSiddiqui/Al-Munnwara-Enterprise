using Al_Munnwar_Enterprises.Data;
using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Module.Internal;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Al_Munnwar_Enterprises.Processor
{
    public class UserProcessor
    {
        public async Task<string> LoginUser(User request)
        {
          

            var result = await Task.Run(() => new UserData().LoginUser(request));
            return result;
        }

        public string SaveUser(User request)
        {
            UserData iD = new UserData();
            iD.SaveUser(request);
            return "ok";
        }
        public string UpdateUser(User request)
        {
            UserData iD = new UserData();
            iD.UpdateUser(request);
            return "ok";
        }
        public string DeleteUser(int id)
        {
            UserData iD = new UserData();
            iD.DeleteUser(id);
            return "ok";
        }
        public UserListResponse GetUser(UserListRequest request)
        {
            UserData iD = new UserData();
            UserListResponse response = new UserListResponse();
            response.user = new List<User>();

            #region request mapping 
            PageModel page = new PageModel();
            page.pageNumber = request.pageNumber;
            page.pageSize = request.pageSize;
            page.isPagining = request.isPagining;
            page.sortBy = request.sortBy;
            page.isSortAsc = request.isSortAsc;
            #endregion
            int rowCount = 0;
            var result = iD.GetUser(page, request.name, ref rowCount);
            response.totalRecords = rowCount;
            foreach (var user in result)
            {
                response.user.Add(new User
                {
                    name = user.name,
                    password=user.password,
                    phoneNbr=user.phoneNbr,
                    email=user.email,
                    id = user.id

                });
            }



            return response;
        }


    }
}
