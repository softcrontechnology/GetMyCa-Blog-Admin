using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class CommonPropertiesModel
    {
        public System.DateTime created_on { get; set; }
        public int created_by { get; set; }
        public System.DateTime modified_on { get; set; }
        public int modified_by { get; set; }
        public string user_ip { get; set; }
        public bool is_active { get; set; }
    }
}