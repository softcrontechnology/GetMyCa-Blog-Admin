using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class ItemMasterModel : CommonPropertiesModel
    {
        public int item_master_id { get; set; }
        public System.Guid guid { get; set; }
        public int item_category_id { get; set; }
        public string category_name { get; set; }
        public string item_name { get; set; }
        public string item_title { get; set; }
        public string item_description { get; set; }
        public string item_image { get; set; }
        public Nullable<decimal> item_old_price { get; set; }
        public Nullable<decimal> item_new_price { get; set; }
        public Nullable<bool> is_discount { get; set; }
        public Nullable<decimal> item_discount_in_percentage { get; set; }
        public Nullable<decimal> item_discount_in_amount { get; set; }
        public int item_stock { get; set; }

    }
}