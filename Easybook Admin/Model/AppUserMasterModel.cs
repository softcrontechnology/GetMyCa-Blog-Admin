using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class AppUserMasterModel : CommonPropertiesModel
    {
        public int app_user_master_id { get; set; }
        public System.Guid guid { get; set; }
        public int user_role_master_id { get; set; }
        public string user_first_name_elm { get; set; }
        public string user_last_name { get; set; }
        public string user_email_elm { get; set; }
        public string user_phone_elm { get; set; }
        public Nullable<System.DateTime> last_login_datetime { get; set; }
        public string user_login_elm { get; set; }
        public string user_pass_elm { get; set; }
        public string display_name { get; set; }
        public string profile_pic { get; set; }
       
    }
}