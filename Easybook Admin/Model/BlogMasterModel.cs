using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healing2Peace.Model
{
    public class BlogMasterModel : CommonPropertiesModel
    {
        public int blog_master_id { get; set; }
        public System.Guid guid { get; set; }
        public int blog_category_id { get; set; }
        public string category_name { get; set; }
        public string blog_title { get; set; }
        public string blog_friendly_url { get; set; }
        public string blog_description { get; set; }
        public string blog_outer_image { get; set; }
        public string blog_thumbnail { get; set; }
        public string blog_inner_banner_img { get; set; }
        public string author_name { get; set; }
        public int blog_view { get; set; }
        public bool is_published { get; set; }
        public Nullable<System.DateTime> published_on { get; set; }
        public bool is_featured { get; set; }
        public string blog_tag_id { get; set; }
        public string tag_name { get; set; }
        public string page_title { get; set; }
        public string meta_Key { get; set; }
        public string meta_description { get; set; }

    }
}