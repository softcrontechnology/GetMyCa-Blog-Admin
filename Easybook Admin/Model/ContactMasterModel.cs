using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class ContactMasterModel : CommonPropertiesModel
    {
        public int contact_master_id { get; set; }
        public System.Guid guid { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
       

    }
}