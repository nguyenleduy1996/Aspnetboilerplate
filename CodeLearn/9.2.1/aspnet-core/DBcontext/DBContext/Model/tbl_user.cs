using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBcontext.DBContext.Model
{
    public class tbl_user
    {
        public string username { set; get; }

        public string name { set; get; }
        public string email { set; get; }
        public string phone { set; get; }
        public string password { set; get; }
        public bool isactive { set; get; }
        public string role { set; get; }
        public bool islocked { set; get; }
        public int failattempt { set; get; }
    }
}
