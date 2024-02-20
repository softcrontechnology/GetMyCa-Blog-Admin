using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class ActivityLogModel : CommonPropertiesModel
    {
        public long activity_log_id { get; set; }
        public System.Guid guid { get; set; }
        public int app_user_id { get; set; }
        public string user_name { get; set; }
        public string heading { get; set; }
        public string heading_class { get; set; }
        public string activity { get; set; }
        public string icon { get; set; }
        public string icon_class { get; set; }

    }

   
}