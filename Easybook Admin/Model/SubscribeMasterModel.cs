using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class SubscribeMasterModel : CommonPropertiesModel
    {
        public int subscribe_master_id { get; set; }
        public System.Guid guid { get; set; }
        public string subscribe_email { get; set; }
       

    }
}