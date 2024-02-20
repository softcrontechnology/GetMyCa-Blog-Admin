using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class UserRoleModel : CommonPropertiesModel
    {
        public int user_role_master_id { get; set; }
        public System.Guid guid { get; set; }
        public string user_role_name { get; set; }
        public string assigned_menu_ids { get; set; }
        public string assigned_feature_ids { get; set; }
       
    }
}