
using System.Collections.Generic;


namespace Al_Munnwar_Enterprises.Module.External
{
    public class UserListResponse
    {
        public int totalRecords { get; set; }
        public List<User> user { get; set; }
    }
}
