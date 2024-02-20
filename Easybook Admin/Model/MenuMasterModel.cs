using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class MenuMasterModel : CommonPropertiesModel
    {
        public int menu_master_id { get; set; }
        public System.Guid guid { get; set; }
        public string display_name { get; set; }
        public string page_url { get; set; }
        public int parent_id { get; set; }
        public int parent_order { get; set; }
        public int child_order { get; set; }
        public string cssclass { get; set; }
       
    }
}