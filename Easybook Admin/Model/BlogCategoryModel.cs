using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class BlogCategoryModel : CommonPropertiesModel
    {
        public int blog_category_id { get; set; }
        public System.Guid guid { get; set; }
        public string category_name { get; set; }

        public string category_type { get; set; }

    }
}